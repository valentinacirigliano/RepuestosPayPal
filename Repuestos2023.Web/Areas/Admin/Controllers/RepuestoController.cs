using Microsoft.AspNetCore.Mvc;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;

namespace Repuestos2023.Web.Areas.Admin.Controllers
{
    public class RepuestoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepuestoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var repuestoList = _unitOfWork.Repuestos.GetAll();
            return View(repuestoList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Repuesto repuesto)
        {
            if (!ModelState.IsValid)
            {
                return View(repuesto);
            }
            if (_unitOfWork.Repuestos.Exists(repuesto))
            {
                ModelState.AddModelError(string.Empty, "Repuesto already exists!!!");
                return View(repuesto);
            }
            _unitOfWork.Repuestos.Add(repuesto);
            _unitOfWork.Save();
            TempData["success"] = "Record added successfully!!";
            return RedirectToAction("Index");
        }


    }
}
