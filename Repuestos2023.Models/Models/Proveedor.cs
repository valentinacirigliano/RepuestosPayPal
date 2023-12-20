using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repuestos2023.Models.Models
{
    public class Proveedor
    {
        [Key]
        public int ProveedorId { get; set; }
        [Required]
        [DisplayName("Proveedor")]

        public string NombreProveedor { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [DisplayName("Provincia")]
        [ForeignKey("Provincia")]
        public int ProvinciaId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ProvinciasLista { get; set; }
        [DisplayName("Ciudad")]
        [ForeignKey("Ciudad")]
        public int CiudadId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CiudadesLista { get; set; }


        
    }
}
