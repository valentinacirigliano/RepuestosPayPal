using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repuestos2023.DataLayer.Repository.Interfaces;
using Repuestos2023.Models.Models;
using Repuestos2023MVC.Web.ViewModels;

namespace Repuestos2023MVC.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RepuestoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RepuestoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            var repuestoVm = new RepuestoEditVm
            {
                Repuesto = new Repuesto(),
                CategoriesList = _unitOfWork.Categorias
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.NombreCategoria,
                        Value = c.CategoriaId.ToString()
                    })
            };

            if (id == null || id == 0)
            {
                return View(repuestoVm);

            }
            else
            {
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                repuestoVm.Repuesto = _unitOfWork.Repuestos.GetById(id.Value);
                if (repuestoVm.Repuesto.Imagen != null)
                {
                    var oldImage = Path.Combine(wwwRootPath, repuestoVm.Repuesto.Imagen.TrimStart('\\'));
                    if (!System.IO.File.Exists(oldImage))
                    {
                        //var noExiste = Path.Combine(wwwRootPath, @images\SinImagenDisponible.jpg");
                        var noExiste = @"\images\SinImagenDisponible.jpg";
                        repuestoVm.Repuesto.Imagen = noExiste;
                    }
                }

                return View(repuestoVm);

            }
        }
        [HttpPost]
        public IActionResult Upsert(RepuestoEditVm repuestoVm, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                repuestoVm.CategoriesList = _unitOfWork.Categorias
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.NombreCategoria,
                        Value = c.CategoriaId.ToString()
                    });

                return View(repuestoVm);
            }
            if (file != null)
            {
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);

                var repuestoFile = fileName + extension;

                if (repuestoVm.Repuesto.Imagen != null)
                {
                    var oldImage = Path.Combine(wwwRootPath, repuestoVm.Repuesto.Imagen.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }


                var uploadFolder = Path.Combine(wwwRootPath, @"images\repuestos\");
                using (var fileStream = new FileStream(Path.Combine(uploadFolder, repuestoFile), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                repuestoVm.Repuesto.Imagen = @"\images\repuestos\" + repuestoFile;
            }
            if (repuestoVm.Repuesto.RepuestoId == 0)
            {
                _unitOfWork.Repuestos.Add(repuestoVm.Repuesto);
            }
            else
            {
                _unitOfWork.Repuestos.Update(repuestoVm.Repuesto);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }


        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var repuestoList = _unitOfWork.Repuestos.GetAll();
            List<RepuestoListVm> repuestoListVm = new List<RepuestoListVm>();
            foreach (var repuesto in repuestoList)
            {
                var repuestoVm = new RepuestoListVm()
                {
                    RepuestoId = repuesto.RepuestoId,
                    Descripcion = repuesto.Descripcion,
                    Stock = repuesto.Stock.ToString(),
                    Categoria = (_unitOfWork.Categorias.Get(c=>c.CategoriaId==repuesto.CategoriaId)).NombreCategoria,
                    PrecioLista = repuesto.PrecioLista
                };
                repuestoListVm.Add(repuestoVm);

            }
            return Json(new { data = repuestoListVm });

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "Not Found" });

            }
            var repuestoDelete = _unitOfWork.Repuestos.GetById(id.Value);
            if (repuestoDelete == null)
            {
                return Json(new { success = false, message = "Repuesto not found" });
            }
            try
            {
                _unitOfWork.Repuestos.Delete(repuestoDelete);
                _unitOfWork.Save();
                var wwwRootPath = _webHostEnvironment.ContentRootPath;
                if (repuestoDelete.Imagen != null)
                {
                    var imageToDelete = Path
                        .Combine(wwwRootPath, repuestoDelete.Imagen.TrimStart('\\'));
                    if (System.IO.File.Exists(imageToDelete))
                    {
                        System.IO.File.Delete(imageToDelete);
                    }
                }
                return Json(new { success = true, message = "Repuesto Deleted succesfully" });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

    }
}
