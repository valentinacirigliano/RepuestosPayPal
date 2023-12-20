using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.Servicios
{
    public interface IPayPalService
    {
        Task<string> CreateOrder(double orderTotal);
    }
}
