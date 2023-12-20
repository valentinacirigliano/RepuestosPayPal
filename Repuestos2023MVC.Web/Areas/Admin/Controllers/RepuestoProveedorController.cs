using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using Repuestos2023MVC.Web.ViewModels;

namespace Repuestos2023MVC.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RepuestoProveedorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepuestoProveedorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var repProvLista = _unitOfWork.RepuestosProveedores.GetAll();
            var repuestoProveedorVmList = new List<RepuestoProveedorVm>();
            foreach (var item in repProvLista)
            {
                var nuevoVm = new RepuestoProveedorVm
                {
                    id=item.RepuestoProveedorId,
                    Repuesto = _unitOfWork.Repuestos.Get(r => r.RepuestoId == item.RepuestoId),
                    Proveedor= _unitOfWork.Proveedores.Get(r => r.ProveedorId == item.ProveedorId)
                };
                repuestoProveedorVmList.Add(nuevoVm);
            }
            return View(repuestoProveedorVmList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var RepProvEditVm = new RepuestoProveedorEditVm();
            RepProvEditVm.Proveedores= _unitOfWork.Proveedores.GetAll().Select(p => new SelectListItem
            {
                Text = p.NombreProveedor,
                Value = p.ProveedorId.ToString()
            }).ToList();
            RepProvEditVm.Repuestos = _unitOfWork.Repuestos.GetAll().Select(p => new SelectListItem
            {
                Text = p.Descripcion,
                Value = p.RepuestoId.ToString()
            }).ToList();

            return View(RepProvEditVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RepuestoProveedor repuestoProveedor)
        {
            if (!ModelState.IsValid)
            {
                var repprov = new RepuestoProveedor
                {
                    Repuestos = _unitOfWork.Repuestos.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Descripcion,
                        Value = c.RepuestoId.ToString()
                    }).ToList(),
                    Proveedores = _unitOfWork.Proveedores.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.NombreProveedor,
                        Value = c.ProveedorId.ToString()
                    }).ToList()
                };
                return View(repuestoProveedor);
            }
            if (_unitOfWork.RepuestosProveedores.Exists(repuestoProveedor))
            {
                ModelState.AddModelError(string.Empty, "Registro ya existente");
                var repprov = new RepuestoProveedor
                {
                    Repuestos = _unitOfWork.Repuestos.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.Descripcion,
                        Value = c.RepuestoId.ToString()
                    }).ToList(),
                    Proveedores = _unitOfWork.Proveedores.GetAll().Select(c => new SelectListItem
                    {
                        Text = c.NombreProveedor,
                        Value = c.ProveedorId.ToString()
                    }).ToList()
                };
                return View(repuestoProveedor);
            }
            _unitOfWork.RepuestosProveedores.Add(repuestoProveedor);
            _unitOfWork.Save();
            TempData["success"] = "Registro guardado correctamente";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var repuestoProveedor = _unitOfWork.RepuestosProveedores.Get(c => c.RepuestoProveedorId == id);
            if (repuestoProveedor == null)
            {
                return NotFound();
            }
            var viewModel = new RepuestoProveedorVm
            {
                id = repuestoProveedor.RepuestoProveedorId,
                Repuesto = _unitOfWork.Repuestos.Get(r => r.RepuestoId == repuestoProveedor.RepuestoId),
                Proveedor = _unitOfWork.Proveedores.Get(p => p.ProveedorId == repuestoProveedor.ProveedorId)
            };
            return View(viewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var repuestoProveedor = _unitOfWork.RepuestosProveedores.Get(c => c.RepuestoProveedorId == id);
            if (repuestoProveedor == null)
            {
                ModelState.AddModelError(string.Empty, "Este registro no existe");
            }
            _unitOfWork.RepuestosProveedores.Delete(repuestoProveedor);
            _unitOfWork.Save();
            TempData["success"] = "Registro borrado exitosamente";

            return RedirectToAction("Index");
        }
    }
}
