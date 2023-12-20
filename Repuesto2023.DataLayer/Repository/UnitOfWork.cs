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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categorias = new CategoryRepository(_db);
            Proveedores = new SupplierRepository(_db);
            Repuestos = new RepuestoRepository(_db);
            Ciudades = new CityRepository(_db);
            Provincias = new ProvinceRepository(_db);
            RepuestosProveedores = new RepuestoProveedorRepository(_db);
            ShoppingCarts = new ShoppingCartRepository(_db);
            OrderHeaders = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailRepository(_db);
			PayPalTransactions = new PayPalTransactionRepository(_db);
		}

        public ICategoryRepository Categorias { get; private set; }
        public ISupplierRepository Proveedores { get; private set; }
        public IRepuestoRepository Repuestos { get; private set; }
        public ICityRepository Ciudades { get; private set; }
        public IProvinceRepository Provincias { get; private set; }
        public IRepuestoProveedorRepository RepuestosProveedores { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }
        public IOrderHeaderRepository OrderHeaders { get; private set; }

        public IOrderDetailRepository OrderDetails { get; private set; }

        public IShoppingCartRepository ShoppingCarts { get; private set; }
		public IPayPalTransactionRepository PayPalTransactions { get; private set; }
		public void BeginTransaction()
        {
            _db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _db.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _db.Database.RollbackTransaction();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }

}
