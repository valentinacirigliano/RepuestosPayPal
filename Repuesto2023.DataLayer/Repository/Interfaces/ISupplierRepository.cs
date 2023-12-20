using Repuestos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.DataLayer.Repository.Interfaces
{
    public interface ISupplierRepository : IRepository<Proveedor>
    {
        void Update(Proveedor supplier);
        bool Exists(Proveedor supplier);
    }
}
