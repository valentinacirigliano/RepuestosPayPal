using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.Models.Models
{
	public class PayPalTransaction
	{
		public int Id { get; set; }
		public string PaymentId { get; set; }
		public string PayerId { get; set; }
		public string UserId { get; set; } // Puedes adaptar esto según tu modelo de usuario
		public DateTime TransactionTime { get; set; }
	}
}
