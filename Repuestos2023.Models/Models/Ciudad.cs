using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Repuestos2023.Models.Models
{
    public class Ciudad
    {
        [Key]
        public int CiudadId { get; set; }
        [Required]
        [DisplayName("Ciudad")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Provincia")]
        public int ProvinciaId { get; set; }

        [ValidateNever]

        public Provincia Provincia { get; set; }
        
    }
}
