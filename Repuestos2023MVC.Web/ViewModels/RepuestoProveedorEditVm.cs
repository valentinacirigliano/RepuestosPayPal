using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repuestos2023.Models.Models;

namespace Repuestos2023MVC.Web.ViewModels
{
    public class RepuestoProveedorEditVm
    {
        public RepuestoProveedor RepuestoProveedor { get; set; }


        [ValidateNever]
        public IEnumerable<SelectListItem> Repuestos { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Proveedores { get; set; }


        public Repuesto Repuesto { get; set; }
        public int RepuestoId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int ProveedorId { get; set; }
    }
}
