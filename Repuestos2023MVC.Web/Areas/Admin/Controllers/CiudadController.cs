using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using Repuestos2023MVC.Web.ViewModels;

namespace Repuestos2023MVC.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CiudadController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CiudadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var ciudadesLista = _unitOfWork.Ciudades.GetAll();
            var ciudadesVm = new List<CiudadEditVm>();
            foreach (var item in ciudadesLista)
            {
                var nuevoVm = new CiudadEditVm
                {
                    CiudadId = item.CiudadId,
                    Provincia = _unitOfWork.Provincias.Get(c => c.ProvinciaId == item.ProvinciaId),
                    Nombre= item.Nombre,
                    Ciudad=item
                };
                ciudadesVm.Add(nuevoVm);
            }
            return View(ciudadesVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var ciudadEditVm = new CiudadEditVm();
            ciudadEditVm.Provincias = _unitOfWork.Provincias.GetAll().Select(p => new SelectListItem
            {
                Text = p.Nombre,
                Value = p.ProvinciaId.ToString()
            }).ToList();

            return View(ciudadEditVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ciudad city)
        {
            if (!ModelState.IsValid)
            {
                var ciudadEditVm = new CiudadEditVm();
                ciudadEditVm.Provincias = _unitOfWork.Provincias.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Nombre,
                    Value = p.ProvinciaId.ToString()
                }).ToList();
                return View(city);
            }
            if (_unitOfWork.Ciudades.Exists(city))
            {
                ModelState.AddModelError(string.Empty, "City already exists!!!");
                var ciudadEditVm = new CiudadEditVm();
                ciudadEditVm.Provincias = _unitOfWork.Provincias.GetAll().Select(p => new SelectListItem
                {
                    Text = p.Nombre,
                    Value = p.ProvinciaId.ToString()
                }).ToList();
                return View(city);
            }
            _unitOfWork.Ciudades.Add(city);
            _unitOfWork.Save();
            TempData["success"] = "Record added successfully!!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var city = _unitOfWork.Ciudades.Get(c => c.CiudadId == id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ciudad city)
        {

            if (!ModelState.IsValid)
            {
                return View(city);
            }
            if (_unitOfWork.Ciudades.Exists(city))
            {
                ModelState.AddModelError(string.Empty, "City already exists!!!");
                return View(city);
            }
            _unitOfWork.Ciudades.Update(city);
            _unitOfWork.Save();
            TempData["success"] = "Record updated successfully!!";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var city = _unitOfWork.Ciudades.Get(c => c.CiudadId == id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var city = _unitOfWork.Ciudades.Get(c => c.CiudadId == id);
            if (city == null)
            {
                ModelState.AddModelError(string.Empty, "City does not exist");
            }
            _unitOfWork.Ciudades.Delete(city);
            _unitOfWork.Save();
            TempData["success"] = "Record removed successfully!!";

            return RedirectToAction("Index");
        }

    }
}
