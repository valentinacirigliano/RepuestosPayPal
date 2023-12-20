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
    public class ProvinceRepository: Repository<Provincia>,IProvinceRepository
    {
        private readonly ApplicationDbContext _db;
        public ProvinceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Provincia provincia)
        {
            if (provincia.ProvinciaId == 0)
            {
                return _db.Provincias.Any(c => c.Nombre == provincia.Nombre);
            }
            return _db.Provincias.Any(c => c.Nombre == provincia.Nombre && c.ProvinciaId != provincia.ProvinciaId);
        }


        public void Update(Provincia provincia)
        {
            _db.Provincias.Update(provincia);
        }
    }

}

