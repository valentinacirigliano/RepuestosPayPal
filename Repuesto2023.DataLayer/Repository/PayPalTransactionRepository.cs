using Repuestos2023.DataLayer.Data;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.DataLayer.Repository
{
	public class PayPalTransactionRepository:Repository<PayPalTransaction>, IPayPalTransactionRepository
	{
		private readonly ApplicationDbContext _db;
		public PayPalTransactionRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public bool Exists(PayPalTransaction paypalTransaction)
		{
			if (paypalTransaction.Id == 0)
			{
				return _db.PayPalTransactions.Any(c => c.TransactionTime == paypalTransaction.TransactionTime);
			}
			return _db.PayPalTransactions.Any(c => c.PayerId == paypalTransaction.PayerId && c.PaymentId != paypalTransaction.PaymentId);
		}

		public PayPalTransaction? GetById(int id)
		{
			return _db.PayPalTransactions.SingleOrDefault(c => c.Id == id);
		}
	}
}
