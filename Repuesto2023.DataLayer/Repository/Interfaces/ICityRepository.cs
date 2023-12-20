using Repuestos2023.Models.Models;

namespace Repuestos2023.DataLayer.Repository.Interfaces
{
    public interface ICityRepository : IRepository<Ciudad>
    {
        void Update(Ciudad city);
        bool Exists(Ciudad city);
        IEnumerable<Ciudad> GetByProvincia(int provinciaId);
    }
}
