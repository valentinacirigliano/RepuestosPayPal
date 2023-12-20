using Repuestos2023.DataLayer.Data;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;

namespace Repuestos2023.DataLayer.Repository
{
    public class CategoryRepository : Repository<Categoria>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Categoria category)
        {
            if (category.CategoriaId == 0)
            {
                return _db.Categorias.Any(c => c.NombreCategoria == category.NombreCategoria);
            }
            return _db.Categorias.Any(c => c.NombreCategoria == category.NombreCategoria && c.CategoriaId != category.CategoriaId);
        }


        public void Update(Categoria category)
        {
            _db.Categorias.Update(category);
        }
    }

}
