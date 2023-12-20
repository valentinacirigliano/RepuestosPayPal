using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using Repuestos2023MVC.Web.ViewModels;
using System.Security.Claims;
using PayPal.Api;
using Amount = PayPal.Api.Amount;
using Repuestos2023MVC.Web.Areas.PayPal;
using Microsoft.Extensions.Options;
using PayPalCheckoutSdk.Core;

namespace Repuestos2023MVC.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private IHttpContextAccessor httpContextAccessor;
		IConfiguration _configuration;
		private readonly PayPalSettings _payPalSettings;
		[BindProperty]
		public ShoppingCartVm shoppingCartVm { get; set; }
		public CartController(IUnitOfWork unitOfWork, IHttpContextAccessor context, 
			IConfiguration iconfiguration, IOptions<PayPalSettings> payPalSettings)
		{
			_unitOfWork = unitOfWork;
			httpContextAccessor = context;
			_configuration = iconfiguration;
			_payPalSettings = payPalSettings.Value;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(c => c.ApplicationUserId == userId.Value, propertiesNames: "Repuesto"),
				OrderHeader = new()

			};

			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Precio = GetPriceBasedOnQuantity(itemCart.Cantidad,
					itemCart.Repuesto.PrecioUnitario,
					itemCart.Repuesto.Precio50,
					itemCart.Repuesto.Precio100);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Cantidad * itemCart.Precio;
			}

			return View(shoppingCartVm);
		}
		
		
		private Payment payment;
		private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
		{
			var paymentExecution = new PaymentExecution()
			{
				payer_id = payerId
			};
			this.payment = new Payment()
			{
				id = paymentId
			};
			return this.payment.Execute(apiContext, paymentExecution);
		}
		private Payment CreatePayment(APIContext apiContext, string redirectUrl,
			string descripcion, double totalAmount)
		{
			//create itemlist and add item objects to it  

			var itemList = new ItemList()
			{
				items = new List<Item>()
			};
			//Adding Item Details like name, currency, price etc  
			var carrito = GetShoppingCartVm();
			foreach (var item in carrito.CartList)
			{
				itemList.items.Add(new Item()
				{
					name = item.Repuesto.Descripcion,
					currency = "USD",
					price = item.Precio.ToString(),
					quantity = item.Cantidad.ToString(),
					sku = "asd"
				});
			}

			var payer = new Payer()
			{
				payment_method = "paypal"
			};
			// Configure Redirect Urls here with RedirectUrls object  
			var redirUrls = new RedirectUrls()
			{
				cancel_url = redirectUrl + "&Cancel=true",
				return_url = redirectUrl
			};
			
			//Final amount with details  
			var amount = new Amount()
			{
				currency = "USD",
				total = totalAmount.ToString()  
												
			};
			var transactionList = new List<Transaction>
			{
				// Adding description about the transaction  
				new Transaction()
				{
					description = descripcion,
					invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No  
					amount = amount,
					item_list = itemList
				}
			};
			this.payment = new Payment()
			{
				intent = "sale",
				payer = payer,
				transactions = transactionList,
				redirect_urls = redirUrls
			};
			// Create a payment using a APIContext  
			return this.payment.Create(apiContext);
		}

		[HttpGet]
		public async Task<IActionResult> CheckoutGet()
		{
			try
			{
				var clientId = "AfC1YUOTZzy-umuWUMzfroDDGRWohqt_p2IUxnjcgkRUNymCxfprY3Xck4U0TUvgBBKVivYW4p1eHbsW";
				var clientSecret = "EKxsZB1feFXSONi9Xnoy4X0w4Hwxs_QCysrfx2HWYtw86YPz0Ce__nGadjr7bDTFukeNeqiXOfBUoLUR";
				var apiContext = PaypalConfiguration.GetAPIContext(clientId, clientSecret);
				var shoppingCartVm = GetShoppingCartVm();

				var redirectUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Customer/Cart/CheckoutSuccess";
				var description = GetTransactionDescription(shoppingCartVm); // Implementa este método según tu lógica
				var totalAmount = shoppingCartVm.OrderHeader.OrderTotal;

				var payment = CreatePayment(apiContext, redirectUrl, description, totalAmount);
				

				HttpContext.Session.SetString("payment", payment.id);
				var paymentId = HttpContext.Session.GetString("payment");

				var approvalUrl = payment.links.FirstOrDefault(link => link.rel.ToLower().Trim() == "approval_url")?.href;
				return Redirect(approvalUrl);
			}
			catch (Exception ex)
			{
				return View("PaymentFailed");
			}
		}

		private string GetTransactionDescription(ShoppingCartVm shoppingCartVm)
		{
			string descripcion = "";
			foreach (var item in shoppingCartVm.CartList)
			{
				descripcion += item.Cantidad + " " + item.Repuesto.Descripcion + " | ";
			}
			return descripcion;
		}


		
		[HttpGet]
		public IActionResult CheckoutSuccess(string paymentId, string token, string PayerID)
		{
			try
			{
				// Agrega logs para verificar los valores de los parámetros
				Console.WriteLine($"PaymentID: {paymentId}, Token: {token}, PayerID: {PayerID}");
				var clientId = "AfC1YUOTZzy-umuWUMzfroDDGRWohqt_p2IUxnjcgkRUNymCxfprY3Xck4U0TUvgBBKVivYW4p1eHbsW";
				var clientSecret = "EKxsZB1feFXSONi9Xnoy4X0w4Hwxs_QCysrfx2HWYtw86YPz0Ce__nGadjr7bDTFukeNeqiXOfBUoLUR";
				var apiContext = PaypalConfiguration.GetAPIContext(clientId, clientSecret);

				var paymentId2 = HttpContext.Session.GetString("payment");

				var executedPayment = ExecutePayment(apiContext, PayerID, paymentId);

				// Guarda la información de la transacción en tu base de datos
				SaveTransactionInfo(executedPayment, PayerID,paymentId);

				return View("PaymentSuccess");
			}
			catch (Exception ex)
			{
				return View("PaymentFailed");
			}
		}

		private ShoppingCartVm GetShoppingCartVm()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(c => c.ApplicationUserId == userId.Value, propertiesNames: "Repuesto"),
				OrderHeader = new()

			};

			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Precio = GetPriceBasedOnQuantity(itemCart.Cantidad,
					itemCart.Repuesto.PrecioUnitario,
					itemCart.Repuesto.Precio50,
					itemCart.Repuesto.Precio100);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Cantidad * itemCart.Precio;
			}
			return shoppingCartVm;
		}

		private void SaveTransactionInfo(Payment executedPayment, string payerId, string paymentId)
		{
			// Implementa la lógica para guardar la información de la transacción en tu base de datos
			var transaction = new PayPalTransaction
			{
				PaymentId = executedPayment.id,
				PayerId = executedPayment.payer.payer_info.payer_id,
				UserId = payerId,
				TransactionTime = DateTime.UtcNow
			};
			_unitOfWork.BeginTransaction();
			_unitOfWork.PayPalTransactions.Add(transaction);
			_unitOfWork.Save();
			var usuario = _unitOfWork.ApplicationUsers.Get(u => u.Id == (GetShoppingCartVm().Buyer.Id));
			var orderheader = new OrderHeader
			{
				ApplicationUserId = usuario.Id,
				OrderDate = DateTime.Now,
				ShippingDate = DateTime.Now,
				OrderTotal = GetShoppingCartVm().OrderHeader.OrderTotal,
				PaymentDate = DateTime.Now,
				PaymentDueDate = DateTime.Now,
				Name = usuario.Name,
				StreetAddress = usuario.Direccion,
				City = _unitOfWork.Ciudades.Get(c => c.CiudadId == usuario.CiudadId).Nombre,
				State = _unitOfWork.Provincias.Get(p => p.ProvinciaId == usuario.ProvinciaId).Nombre,
				ZipCode = usuario.CodPostal,
				PhoneNumber = usuario.PhoneNumber,
				PayPalPaymentId=paymentId,
				TransactionId = _unitOfWork.PayPalTransactions.GetAll(t=>t.PayerId==usuario.Id).Last().Id.ToString()
			};
			_unitOfWork.OrderHeaders.Add(orderheader);
			_unitOfWork.Save();
			foreach (var item in GetShoppingCartVm().CartList)
			{
				var orderdetail = new OrderDetail
				{
					OrderId = _unitOfWork.OrderHeaders.GetAll(t => t.ApplicationUserId == usuario.Id).Last().Id,
					RepuestoId = item.RepuestoId,
					Cantidad = item.Cantidad,
					Precio = item.Precio
				};
				_unitOfWork.OrderDetails.Add(orderdetail);
				_unitOfWork.Save();
			}

			foreach (var item in GetShoppingCartVm().CartList)
			{
				_unitOfWork.ShoppingCarts.Delete(item);
			}
			_unitOfWork.Save();
			_unitOfWork.CommitTransaction();

		}
		private double GetPriceBasedOnQuantity(int quantity, double price, double price50, double price100)
		{
			if (quantity <= 50)
			{
				return price;
			}
			else if (quantity <= 100)
			{
				return price50;
			}
			else
			{
				return price100;
			}
		}

		public IActionResult Plus(int cartId)
		{
			var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId);
			_unitOfWork.ShoppingCarts.IncrementQuantity(cartInDb, 1);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
		public IActionResult Minus(int cartId)
		{
			var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId);
			if (cartInDb.Cantidad == 1)
			{
				_unitOfWork.ShoppingCarts.Delete(cartInDb);
			}
			else
			{
				_unitOfWork.ShoppingCarts.DecrementQuantity(cartInDb, 1);

			}
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}

		public IActionResult RemoveFromCart(int cartId)
		{
			var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId);
			_unitOfWork.ShoppingCarts.Delete(cartInDb);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}



		#region API CALLS
		[HttpDelete]
		public IActionResult Delete(int id)
		{

			try
			{
				var cart = _unitOfWork.ShoppingCarts.Get(c => c.Id == id);
				_unitOfWork.ShoppingCarts.Delete(cart);
				_unitOfWork.Save();
				return Json(new { success = true, message = "Cart Removed Satisfactory" });

			}
			catch (Exception)
			{

				return Json(new { success = false, message = "Problems while trying to remove a cart" });

			}
		}

		[HttpPost]
		public IActionResult GetTotal()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			var shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(s => s.ApplicationUserId == claims.Value, propertiesNames: "Repuesto")
			};
			foreach (var item in shoppingCartVm.CartList)
			{
				item.Precio = GetPriceBasedOnQuantity(item.Cantidad,
					item.Repuesto.PrecioUnitario,
					item.Repuesto.Precio50,
					item.Repuesto.Precio100);
				shoppingCartVm.OrderHeader.OrderTotal += item.Precio * item.Cantidad;
			}
			return Json(new { total = shoppingCartVm.OrderHeader.OrderTotal });
		}
		#endregion


	}
}





