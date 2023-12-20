using Microsoft.AspNetCore.Mvc;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;

namespace Repuestos2023MVC.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProvinciaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProvinciaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var provinceList = _unitOfWork.Provincias.GetAll();
            return View(provinceList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Provincia province)
        {
            if (!ModelState.IsValid)
            {
                return View(province);
            }
            if (_unitOfWork.Provincias.Exists(province))
            {
                ModelState.AddModelError(string.Empty, "Province already exists!!!");
                return View(province);
            }
            _unitOfWork.Provincias.Add(province);
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
            var province = _unitOfWork.Provincias.Get(c => c.ProvinciaId == id);
            if (province == null)
            {
                return NotFound();
            }
            return View(province);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Provincia province)
        {
            if (!ModelState.IsValid)
            {
                return View(province);
            }
            if (_unitOfWork.Provincias.Exists(province))
            {
                ModelState.AddModelError(string.Empty, "Province already exists!!!");
                return View(province);
            }
            _unitOfWork.Provincias.Update(province);
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
            var province = _unitOfWork.Provincias.Get(c => c.ProvinciaId == id);
            if (province == null)
            {
                return NotFound();
            }
            return View(province);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var province = _unitOfWork.Provincias.Get(c => c.ProvinciaId == id);
            if (province == null)
            {
                ModelState.AddModelError(string.Empty, "Province does not exist");
            }
            _unitOfWork.Provincias.Delete(province);
            _unitOfWork.Save();
            TempData["success"] = "Record removed successfully!!";

            return RedirectToAction("Index");
        }

    }
}
