using Repuestos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.DataLayer.Repository.Interfaces
{
    public interface IRepuestoProveedorRepository:IRepository<RepuestoProveedor>
    {
        void Update(RepuestoProveedor repuestoProveedor);
        bool Exists(RepuestoProveedor repuestoProveedor);
    }
}
