using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri,Yönetici")]
    public class SpareMaterialsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISelectListItemsService _selectItems;
        private readonly ISpareMaterialsService _spareMaterialsService;
        private string UserName => User.Identity!.Name!;
        private string donusVerisi = "";   
        public SpareMaterialsController(IMapper mapper, ISelectListItemsService selectItems, ISpareMaterialsService spareMaterialsService, AppDbContext context)
        {
            _mapper = mapper;
            _selectItems = selectItems;
            _spareMaterialsService = spareMaterialsService;
            _context = context;
        }
        private SelectList MaterialDefinationsSelectList => new(_context.SpareMaterialDefinations, "Id", "SpareMaterialName");
        private SelectList MaterialDetailSelectList => new(_context.SpareMaterialDetails, "Detail", "Detail");
        private SelectList MaterialMeasurementSelectList => new(_context.SpareMaterialsMeasurement, "Measurement", "Measurement");
        private SelectList TenderSelectList => new(_context.TenderProjects.OrderByDescending(x=>x.UpdatedTime), "Id", "ProjectName");
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
        public IActionResult MaterialDetailAdd(int Id)
        {            
            ViewBag.MaterialDetailSelectList = MaterialDetailSelectList;
            ViewBag.MaterialMeasurementSelectList = MaterialMeasurementSelectList;
            ViewBag.Materials = new SelectList(_context.SpareMaterialDefinations.Where(x=>x.SpareMaterialCode==Id), "Id", "SpareMaterialName");
            ViewBag.TenderSelectList = TenderSelectList;
            return View();
        }
        [HttpPost]
        public IActionResult MaterialDetailAdd(SpareMaterialsViewModel model)
        {
            donusVerisi = _spareMaterialsService.AddSpareMaterialDetail(model, UserName);
            int newId = _spareMaterialsService.GetSpareMatarialsCodeFromId(model.SpareMaterialId);
            if (donusVerisi.Contains("Başarıyla"))
            {
                TempData["status"] = donusVerisi;
            }
            else
            {
                TempData["error"] = donusVerisi;
            }
            return RedirectToAction("MaterialDetailAdd", new { Id = newId });
        }
        public IActionResult MaterialDetailIndex(int Id)
        {
            ViewBag.PageName = _spareMaterialsService.SpareMaterialDefinationsName(Id);
            ViewBag.Materials = _spareMaterialsService.GetSpareMaterialsDetail(Id);
            ViewBag.SpareMaterialsCode = _spareMaterialsService.GetSpareMatarialsCodeFromId(Id);
            return View();
        }
        [HttpPost]
        public IActionResult RemoveSpareMaterialDetail(SpareMaterialsViewModel model)
        {
            int newId = model.SpareMaterialId;
            donusVerisi = _spareMaterialsService.RemoveSpareMaterialDetail(model.Id, UserName);          
            if (donusVerisi.Contains("Başarıyla"))
            {
                TempData["status"] = donusVerisi;
            }
            else
            {
                TempData["error"] = donusVerisi;
            }
            return RedirectToAction("MaterialDetailIndex", new { Id = newId });
        }
        public IActionResult MaterialDetailUpdate(int Id)
        {
            int spareCode = _spareMaterialsService.GetSpareMatarialsCodeFromId(Id);
            ViewBag.MaterialDetailSelectList = MaterialDetailSelectList;
            ViewBag.MaterialMeasurementSelectList = MaterialMeasurementSelectList;
            ViewBag.Materials = new SelectList(_context.SpareMaterialDefinations.Where(x => x.SpareMaterialCode == spareCode), "Id", "SpareMaterialName");
            ViewBag.TenderSelectList = TenderSelectList;
            var model = _spareMaterialsService.GetSpareMaterialsById(Id);
            return View(model);
        }
        [HttpPost]
        public IActionResult MaterialDetailUpdate(SpareMaterialsModel model)
        {
            donusVerisi = _spareMaterialsService.UpdateSpareMaterialDetail(model, UserName);
            int newId = model.SpareMaterialId;
            if (donusVerisi.Contains("Başarıyla"))
            {
                TempData["status"] = donusVerisi;
            }
            else
            {
                TempData["error"] = donusVerisi;
            }
            return RedirectToAction("MaterialDetailIndex", new { Id = newId });
        }
        [HttpGet]
        public IActionResult MaterialDetailsExcelCreate(int Id)
        {
            ViewBag.PageName = _spareMaterialsService.SpareMaterialDefinationsName(Id);
            ViewBag.Materials = _spareMaterialsService.GetMiddleMaterials(Id);
            return View();
        }       
    }
}
