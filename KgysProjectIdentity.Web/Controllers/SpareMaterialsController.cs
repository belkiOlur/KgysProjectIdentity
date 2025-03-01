using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri,Yönetici")]
    public class SpareMaterialsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISelectListItemsService _selectItems;
        private readonly ISpareMaterialsService _spareMaterialsService;
        private string UserName => User.Identity!.Name!;
        private string donusVerisi = "";
        public SpareMaterialsController(IMapper mapper, ISelectListItemsService selectItems, ISpareMaterialsService spareMaterialsService)
        {
            _mapper = mapper;
            _selectItems = selectItems;
            _spareMaterialsService = spareMaterialsService;
        }
        public IActionResult Index()
        {
            ViewBag.TopMaterials = _spareMaterialsService.GetTopMaterials();
            return View();
        }
        [HttpPost]
        public IActionResult AddSpareMaterialDefinations(SpareMaterialDefinationsViewModel model)
        {
           donusVerisi = _spareMaterialsService.AddSpareMaterialDefinations(model, UserName);
            if (donusVerisi.Contains("Başarıyla"))
            {
                TempData["status"] = donusVerisi;
            }
            else
            {
                TempData["error"] = donusVerisi;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemoveSpareMaterialDefinations(SpareMaterialDefinationsViewModel model)
        {
            donusVerisi = _spareMaterialsService.RemoveSpareMaterialDefinations(model.Id, UserName);
            if (donusVerisi.Contains("Başarıyla"))
            {
                TempData["status"] = donusVerisi;
            }
            else
            {
                TempData["error"] = donusVerisi;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateSpareMaterialDefinations(SpareMaterialDefinationsViewModel model)
        {
            donusVerisi = _spareMaterialsService.UpdateSpareMaterialDefinations(model, UserName);
            if (donusVerisi.Contains("Başarıyla"))
            {
                TempData["status"] = donusVerisi;
            }
            else
            {
                TempData["error"] = donusVerisi;
            }
            return RedirectToAction("Index");
        }
        public IActionResult MaterialDetails(int Id)
        {
            ViewBag.PageName= _spareMaterialsService.SpareMaterialDefinationsName(Id);
            ViewBag.Materials = _spareMaterialsService.GetMiddleMaterials(Id);
            ViewBag.SpareMaterialsCode = Id;
            return View();
        }
      
    }
}
