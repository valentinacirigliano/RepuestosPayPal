using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repuestos2023.Models.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Repuestos2023MVC.Web.ViewModels
{
    public class CiudadEditVm
    {
        public Ciudad Ciudad { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Provincias { get; set; }
        public Provincia Provincia { get; set; }
        public int CiudadId { get; set; }
        public string Nombre { get; set; }
        public int ProvinciaId { get; set; }
    }
}
