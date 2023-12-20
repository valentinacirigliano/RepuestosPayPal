using Microsoft.AspNetCore.Mvc;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;

namespace Repuestos2023.Web.Areas.Admin.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProveedorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var suppliersList = _unitOfWork.Proveedores.GetAll();
            return View(suppliersList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Proveedor supplier)
        {
            if (!ModelState.IsValid)
            {
                return View(supplier);
            }
            if (_unitOfWork.Proveedores.Exists(supplier))
            {
                ModelState.AddModelError(string.Empty, "Supplier already exists!!!");
                return View(supplier);
            }
            _unitOfWork.Proveedores.Add(supplier);
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
            var supplier = _unitOfWork.Proveedores.Get(c => c.ProveedorId == id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Proveedor supplier)
        {
            if (!ModelState.IsValid)
            {
                return View(supplier);
            }
            if (_unitOfWork.Proveedores.Exists(supplier))
            {
                ModelState.AddModelError(string.Empty, "Supplier already exists!!!");
                return View(supplier);
            }
            _unitOfWork.Proveedores.Update(supplier);
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
            var supplier = _unitOfWork.Proveedores.Get(c => c.ProveedorId == id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var supplier = _unitOfWork.Proveedores.Get(c => c.ProveedorId == id);
            if (supplier == null)
            {
                ModelState.AddModelError(string.Empty, "Supplier does not exist");
            }
            _unitOfWork.Proveedores.Delete(supplier);
            _unitOfWork.Save();
            TempData["success"] = "Record removed successfully!!";

            return RedirectToAction("Index");
        }
    }
}
