using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using Repuestos2023MVC.Web.ViewModels;

namespace Repuestos2023.Web.Areas.Admin.Controllers
{
    public class CiudadController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CiudadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var cityList = _unitOfWork.Ciudades.GetAll();
            return View(cityList);
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
                return View(city);
            }
            if (_unitOfWork.Ciudades.Exists(city))
            {
                ModelState.AddModelError(string.Empty, "City already exists!!!");
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
