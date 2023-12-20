using Azure.Core;
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
    public class RepuestoRepository : Repository<Repuesto>, IRepuestoRepository
    {
        private readonly ApplicationDbContext _db;
        public RepuestoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Repuesto repuesto)
        {
            if (repuesto.RepuestoId == 0)
            {
                return _db.Repuestos.Any(c => c.Descripcion == repuesto.Descripcion);
            }
            return _db.Repuestos.Any(c => c.Descripcion == repuesto.Descripcion && c.RepuestoId != repuesto.RepuestoId);
        }

        public Repuesto? GetById(int id)
        {
            return _db.Repuestos.SingleOrDefault(c => c.RepuestoId == id);
        }

        public void Update(Repuesto repuesto)
        {
            _db.Repuestos.Update(repuesto);
        }
    }
}
