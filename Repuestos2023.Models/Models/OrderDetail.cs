using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.Models.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        public int RepuestoId { get; set; }
        [ForeignKey("RepuestoId")]
        [ValidateNever]
        public Repuesto Repuesto { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    }
}
