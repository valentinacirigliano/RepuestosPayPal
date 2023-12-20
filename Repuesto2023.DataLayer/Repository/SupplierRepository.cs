using Repuestos2023.DataLayer.Data;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;

namespace Repuestos2023.DataLayer.Repository
{
    public class SupplierRepository: Repository<Proveedor>, ISupplierRepository
    {
        private readonly ApplicationDbContext _db;
        public SupplierRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Proveedor supplier)
        {
            if (supplier.ProveedorId == 0)
            {
                return _db.Proveedores.Any(c => c.NombreProveedor == supplier.NombreProveedor);
            }
            return _db.Proveedores.Any(c => c.NombreProveedor == supplier.NombreProveedor && c.ProveedorId != supplier.ProveedorId);
        }


        public void Update(Proveedor supplier)
        {
            _db.Proveedores.Update(supplier);
        }

    }
}
