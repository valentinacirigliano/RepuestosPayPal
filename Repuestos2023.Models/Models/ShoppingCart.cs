using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.Models.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int RepuestoId { get; set; }
        [ForeignKey("RepuestoId")]
        [ValidateNever]
        public Repuesto Repuesto { get; set; }
        [Range(1, 1000, ErrorMessage = "{0} must be between {1} and {2}")]
        public int Cantidad { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public double Precio { get; set; }
    }
}
