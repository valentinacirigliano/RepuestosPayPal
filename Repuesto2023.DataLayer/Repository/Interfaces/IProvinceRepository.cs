using Repuestos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.DataLayer.Repository.Interfaces
{
    public interface IProvinceRepository : IRepository<Provincia>
    {
        void Update(Provincia province);
        bool Exists(Provincia province);
    }
}
