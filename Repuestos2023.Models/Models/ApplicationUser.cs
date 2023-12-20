using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repuestos2023.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? Direccion { get; set; }
        public int ProvinciaId { get; set; }
        [ForeignKey("ProvinciaId")]
        [ValidateNever]
        public Provincia Provincia { get; set; }
        public int CiudadId { get; set; }
        [ForeignKey("CiudadId")]
        [ValidateNever]
        public Ciudad Ciudad { get; set; }
        public string? CodPostal { get; set; }


    }
}
