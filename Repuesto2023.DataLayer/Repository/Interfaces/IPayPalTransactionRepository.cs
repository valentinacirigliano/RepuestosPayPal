using Repuestos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.DataLayer.Repository.Interfaces
{
	public interface IPayPalTransactionRepository : IRepository<PayPalTransaction>
	{
		bool Exists(PayPalTransaction paypalTransaction);
		public PayPalTransaction? GetById(int id);
	}
}
