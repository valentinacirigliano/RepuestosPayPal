using Repuestos2023.Models.Models;

namespace Repuestos2023.DataLayer.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<Categoria>
    {
        void Update(Categoria category);
        bool Exists(Categoria category);
    }
}
