using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Repuestos2023MVC.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var listaRepuestos = _unitOfWork.Repuestos.GetAll();
            foreach (var item in listaRepuestos)
            {
                item.Categoria = _unitOfWork.Categorias.Get(c => c.CategoriaId == item.CategoriaId);
            }
            return View(listaRepuestos);
        }
        public IActionResult Details(int? repuestoId)
        {
            if (repuestoId == null || repuestoId == 0)
            {
                return NotFound();
            }
            var repuesto = _unitOfWork.Repuestos.Get(p => p.RepuestoId == repuestoId, "Categoria");
            ShoppingCart cart = new ShoppingCart
            {
                RepuestoId = repuesto.RepuestoId,
                Repuesto = repuesto,
                Cantidad = 1
            };
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = userId.Value;
            var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.ApplicationUserId == userId.Value
                        && c.RepuestoId == cart.RepuestoId);
            if (cartInDb == null)
            {
                _unitOfWork.ShoppingCarts.Add(cart);

            }
            else
            {
                _unitOfWork.ShoppingCarts.IncrementQuantity(cartInDb, cart.Cantidad);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}