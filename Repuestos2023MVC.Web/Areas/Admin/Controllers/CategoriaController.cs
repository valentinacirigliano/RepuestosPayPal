using Microsoft.AspNetCore.Mvc;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repuestos2023MVC.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var categoryList = _unitOfWork.Categorias.GetAll();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            if (_unitOfWork.Categorias.Exists(category))
            {
                ModelState.AddModelError(string.Empty, "Category already exists!!!");
                return View(category);
            }
            _unitOfWork.Categorias.Add(category);
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
            var category = _unitOfWork.Categorias.Get(c => c.CategoriaId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            if (_unitOfWork.Categorias.Exists(category))
            {
                ModelState.AddModelError(string.Empty, "Category already exists!!!");
                return View(category);
            }
            _unitOfWork.Categorias.Update(category);
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
            var category = _unitOfWork.Categorias.Get(c => c.CategoriaId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var category = _unitOfWork.Categorias.Get(c => c.CategoriaId == id);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "Category does not exist");
            }
            _unitOfWork.Categorias.Delete(category);
            _unitOfWork.Save();
            TempData["success"] = "Record removed successfully!!";

            return RedirectToAction("Index");
        }

    }
}
