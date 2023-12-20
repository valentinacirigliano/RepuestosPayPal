using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.Models.Models
{
    public class Provincia
    {
        public int ProvinciaId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} y {1} characters", MinimumLength = 5)]

        public string Nombre { get; set; }
    }
}
