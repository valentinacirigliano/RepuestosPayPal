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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int orderId, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(o => o.Id == orderId);

            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;

                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        // Método para actualizar información específica de PayPal
        public void UpdatePayPalInfo(int orderId, string payPalOrderId, string payPalPaymentId)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(o => o.Id == orderId);

            if (orderFromDb != null)
            {
                orderFromDb.PayPalOrderId = payPalOrderId;
                orderFromDb.PayPalPaymentId = payPalPaymentId;
            }
        }
    }

}
