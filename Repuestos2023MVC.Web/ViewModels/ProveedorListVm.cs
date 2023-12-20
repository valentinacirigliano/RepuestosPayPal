using Repuestos2023.Models.Models;

namespace Repuestos2023MVC.Web.ViewModels
{
    public class ProveedorListVm
    {
        public int id { get; set; }
        public Proveedor Proveedor { get; set; }
        public string Ciudad { get; set; }
    }
}
