using Microsoft.AspNetCore.Mvc;
using Repuestos2023.DataLayer.Repository.Interfaces;
using System.Text.Unicode;

namespace Repuestos2023MVC.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenericoController : Controller
    {
        // GET: Generico
        private readonly IUnitOfWork unitOfWork;
        public GenericoController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public JsonResult GetCities(int provinciaId)
        {
            var lista = unitOfWork.Ciudades.GetByProvincia(provinciaId);
            return Json(lista);
        }
    }
}
