using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repuestos2023.Models.Models;

namespace Repuestos2023.Web.ViewModels
{
    public class RepuestoEditVm
    {
        public Repuesto Repuesto { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RepuestosList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoriesList { get; set; }
    }
}
