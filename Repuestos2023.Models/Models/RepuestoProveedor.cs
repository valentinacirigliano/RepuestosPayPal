using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Repuestos2023.Models.Models
{
    [Table("RepuestosProveedores")]
    public class RepuestoProveedor
    {
        [Key]
        public int RepuestoProveedorId { get; set; }
        
        [ForeignKey("Repuesto")]
        [Required]
        public int RepuestoId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Repuestos { get; set; }

        [ForeignKey("Proveedor")]
        [Required]
        public int ProveedorId { get; set; }


        [ValidateNever]
        public IEnumerable<SelectListItem> Proveedores { get; set; }
    }
}
