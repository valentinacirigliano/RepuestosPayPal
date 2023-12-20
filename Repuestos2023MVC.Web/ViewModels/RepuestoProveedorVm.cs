using Repuestos2023.Models.Models;

namespace Repuestos2023MVC.Web.ViewModels
{
    public class RepuestoProveedorVm
    {
        public int id { get; set; }
        public Repuesto Repuesto { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
