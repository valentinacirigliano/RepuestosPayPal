using Repuestos2023.Models.Models;

namespace Repuestos2023MVC.Web.ViewModels
{
    public class ShoppingCartVm
    {
        public IEnumerable<ShoppingCart> CartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
		public ApplicationUser Buyer { get; set; }
	}
}
