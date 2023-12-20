
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repuestos2023.Models.Models;

namespace Repuestos2023.DataLayer.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<SelectListGroup>();
            modelBuilder.Ignore<SelectListItem>();

            modelBuilder.Entity<Provincia>().HasData(
                    new Provincia { ProvinciaId = 1, Nombre = "Buenos Aires"}
               );
            modelBuilder.Entity<Ciudad>().HasData(
                    new Ciudad { CiudadId = 1, Nombre = "Lobos", ProvinciaId = 1 }
               );
            
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<RepuestoProveedor> RepuestosProveedores { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<PayPalTransaction> PayPalTransactions { get; set; }
	}
}
