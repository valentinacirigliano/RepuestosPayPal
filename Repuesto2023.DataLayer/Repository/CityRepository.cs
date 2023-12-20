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
    internal class CityRepository:Repository<Ciudad>,ICityRepository
    {
        private readonly ApplicationDbContext _db;
        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Ciudad city)
        {
            if (city.CiudadId == 0)
            {
                return _db.Ciudades.Any(c => c.Nombre == city.Nombre);
            }
            return _db.Ciudades.Any(c => c.Nombre == city.Nombre && c.CiudadId != city.CiudadId);
        }


        public void Update(Ciudad city)
        {
            _db.Ciudades.Update(city);
        }

        public IEnumerable<Ciudad> GetByProvincia(int provinciaId)
        {
            return _dbSet.Where(c => c.ProvinciaId == provinciaId).ToList();
        }
    }

}

