using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using Repuestos2023MVC.Web.ViewModels;

namespace Repuestos2023MVC.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProveedorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProveedorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var listaProveedores = _unitOfWork.Proveedores.GetAll();
            var proveedoresVm = new List<ProveedorListVm>();
            foreach (var item in listaProveedores)
            {
                var nuevoVm = new ProveedorListVm
                {
                    id = item.ProveedorId,
                    Proveedor = item,
                    Ciudad = (_unitOfWork.Ciudades.Get(c => c.CiudadId == item.CiudadId)).Nombre
                };
                proveedoresVm.Add(nuevoVm);
            }
            return View(proveedoresVm);
        }



        [HttpGet]
        public IActionResult Create()
        {
            var proveedor = new Proveedor
            {
                ProvinciasLista = _unitOfWork.Provincias.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.ProvinciaId.ToString()
                }).ToList(),
                CiudadesLista = _unitOfWork.Ciudades.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.CiudadId.ToString()
                }).ToList()
            };

            return View(proveedor);
        }


        //TODO:Modificar create para que el combo de ciudades cambie de acuerdo a la provincia seleccionada
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Proveedor supplier)
        {
            if (!ModelState.IsValid)
            {
                var proveedor = new Proveedor
                {
                    ProvinciasLista = _unitOfWork.Provincias.GetAll()
                        .Select(c => new SelectListItem
                        {
                            Text = c.Nombre,
                            Value = c.ProvinciaId.ToString()
                        }).ToList(),
                    CiudadesLista = _unitOfWork.Ciudades.GetAll()
                        .Select(c => new SelectListItem
                        {
                            Text = c.Nombre,
                            Value = c.CiudadId.ToString()
                        }).ToList() 
                };
                return View(supplier);
            }
            if (_unitOfWork.Proveedores.Exists(supplier))
            {
                ModelState.AddModelError(string.Empty, "Supplier already exists!!!");
                var proveedor = new Proveedor
                {
                    ProvinciasLista = _unitOfWork.Provincias.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Nombre,
                        Value = c.ProvinciaId.ToString()
                    }).ToList(),
                    CiudadesLista = _unitOfWork.Ciudades.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Nombre,
                        Value = c.CiudadId.ToString()
                    }).ToList()
                };
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
