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
    public class RepuestoProveedorRepository:Repository<RepuestoProveedor>,IRepuestoProveedorRepository
    {
        private readonly ApplicationDbContext _db;
        public RepuestoProveedorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(RepuestoProveedor repuestoProveedor)
        {
            if (repuestoProveedor.RepuestoProveedorId == 0)
            {
                return _db.RepuestosProveedores.Any(rp => rp.RepuestoId == repuestoProveedor.RepuestoId &&
                                                     rp.ProveedorId == repuestoProveedor.ProveedorId);
            }
            return _db.RepuestosProveedores.Any(rp => rp.RepuestoId == repuestoProveedor.RepuestoId &&
                                                     rp.ProveedorId == repuestoProveedor.ProveedorId
                                            && rp.RepuestoProveedorId != repuestoProveedor.RepuestoProveedorId);
        }


        public void Update(RepuestoProveedor rp)
        {
            _db.RepuestosProveedores.Update(rp);
        }
    }
}
