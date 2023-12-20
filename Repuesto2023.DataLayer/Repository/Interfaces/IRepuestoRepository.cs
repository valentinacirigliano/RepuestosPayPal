using Repuestos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.DataLayer.Repository.Interfaces
{
    public interface IRepuestoRepository : IRepository<Repuesto>
    {
        void Update(Repuesto repuesto);
        bool Exists(Repuesto repuesto);
        Repuesto? GetById(int id);
    }
}
