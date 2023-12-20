using Repuestos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.DataLayer.Repository.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

        void UpdateStatus(int orderId, string orderStatus, string? paymentStatus = null);

        // Método para actualizar información específica de PayPal
        void UpdatePayPalInfo(int orderId, string payPalOrderId, string payPalPaymentId);

    }
}
