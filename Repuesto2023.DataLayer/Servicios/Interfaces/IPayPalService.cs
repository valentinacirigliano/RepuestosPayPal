using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.DataLayer.Servicios.Interfaces
{
    public interface IPayPalService
    {
        Task<string> CreateOrder(double orderTotal);
        Task<bool> CapturePayment(string orderId);
    }
}
