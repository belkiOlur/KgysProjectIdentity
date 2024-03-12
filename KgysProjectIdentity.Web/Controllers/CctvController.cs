using AutoMapper;
using GridShared;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using NuGet.Versioning;
using System.Data.Entity;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
    public class CctvController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICctvService _cctv;
        private readonly IMapper _mapper;
        private ISelectListItemsService _selectItems;
        private string UserName => User.Identity!.Name!;

        public CctvController(ICctvService cctv, IMapper mapper, AppDbContext context, ISelectListItemsService selectItems)
        {
            _cctv = cctv;
            _mapper = mapper;
            _context = context;
            _selectItems = selectItems;
        }
        private SelectList DistrictSelectList => new(_context.DistrictModels, "districtName", "districtName");
        private SelectList CctvProjectSelectList => new(_context.CctvProjects, "ProjectName", "ProjectName");
        private SelectList CctvReasonSelectList => new(_selectItems.CctvReasonSelect(), "Value", "Data");
        private SelectList CctvStatusSelectList => new(_selectItems.ParkSelect(), "Value", "Data");

        public IActionResult Project()
        {
            ViewBag.CctvProjects = _context.CctvProjects;
            return View();
        }
        [HttpPost]
        public IActionResult ProjectAdd(CctvProjectViewModel project)
        {
            if (!_cctv.ProjectAdd(project, UserName))
            {
                TempData["Error"] = "CCTV Proje Fazı Ekleme İşlemi Tamamlanamadı.";
                return RedirectToAction("Project");
            }
            TempData["Status"] = "CCTV Proje Fazı Ekleme  İşlemi Tamamlandı.";
            return RedirectToAction("Project");
        }
        [HttpPost]
        public IActionResult ProjectRemove(CctvProjectViewModel project)
        {
            if (!_cctv.ProjectRemove(project.Id, UserName))
            {
                TempData["Error"] = "CCTV Proje Fazı Silme İşlemi Tamamlanamadı.";
                return RedirectToAction("Project");
            }
            TempData["Status"] = "CCTV Proje Fazı Silme İşlemi Tamamlandı.";
            return RedirectToAction("Project");
        }
        public IActionResult Index(int id)
        {
            var projectName = _context.CctvProjects.Find(id)!.ProjectName;
            var projectDetail = _context.CctvProjectDetail.Where(x=>x.ProjectName==projectName);
            if (projectDetail != null)
            {
                ViewBag.CctvProjects = CctvProjectSelectList;
                ViewBag.CctvReason = CctvReasonSelectList;
                ViewBag.District = DistrictSelectList;
                ViewBag.CctvStatus = CctvStatusSelectList;
                ViewBag.ProjectName = projectName;
                ViewBag.ProjectDetail= projectDetail;
                return View();
            }
            ViewBag.ProjectName = null;
            TempData["Error"] = "CCTV Proje Fazına Ait Detay Bulunamadı.";
            return View("Index","Home");
        }
       
        [HttpPost]
        public IActionResult ProjectDetailAdd(CctvViewModel project)
        {

            int projectId = _context.CctvProjects.AsNoTracking().FirstOrDefault(x => x.ProjectName == project.ProjectName)!.Id;
            if (!_cctv.ProjectDetailAdd(project, UserName))
            {
                TempData["Error"] = "CCTV Proje Fazı Detayı Ekleme İşlemi Tamamlanamadı.";
                return RedirectToAction("Index", new { id = projectId });
            }
            TempData["Status"] = "CCTV Proje Fazı Detayı Ekleme İşlemi Tamamlandı.";
            return RedirectToAction("Index", new { id = projectId });
        }
        [HttpPost]
        public IActionResult ProjectDetailRemove(CctvViewModel project)
        {
            var projectDetails = _context.CctvProjectDetail.Find(project.Id)!;
            int projectId = _context.CctvProjects.AsNoTracking().FirstOrDefault(x => x.ProjectName == projectDetails.ProjectName)!.Id;
            if (!_cctv.ProjectDetailRemove(projectDetails, UserName))
            {
                TempData["Error"] = "CCTV Proje Fazı Detayı Silme İşlemi Tamamlanamadı.";
                return RedirectToAction("Index", new { id = projectId });
            }
            TempData["Status"] = "CCTV Proje Fazı Detayı Silme İşlemi Tamamlandı.";
            return RedirectToAction("Index", new { id = projectId });
        }
        [HttpGet]
        public IActionResult ProjectDetailUpdate(int id)
        {
            var project = _context.CctvProjectDetail.Find(id)!;
            ViewBag.CctvProjects = CctvProjectSelectList;
            ViewBag.CctvReason = CctvReasonSelectList;
            ViewBag.District = DistrictSelectList;
            ViewBag.CctvStatus = CctvStatusSelectList;
            ViewBag.ProjectName = project.ProjectName + " " + project.ProjectDistrict + " " + project.Unit;
            return View(project);
        }
        [HttpPost]
        public IActionResult ProjectDetailUpdate(CctvModel project)
        {
            var projectName = _context.CctvProjects.AsNoTracking().Where(x => x.ProjectName == project.ProjectName).FirstOrDefault()!;            
            if (!_cctv.ProjectDetailUpdate(project, UserName))
            {
                TempData["Error"] = "CCTV Proje Fazı Detayı Güncelleme İşlemi Tamamlanamadı.";
                return RedirectToAction("Index", new { id = projectName.Id });
            }
            TempData["Status"] = "CCTV Proje Fazı Detayı Güncelleme İşlemi Tamamlandı.";
            return RedirectToAction("Index", new { id = projectName.Id });
        }
        [HttpGet]
        public IActionResult Products(int id)
        {
            var products = _context.CctvProducts.AsNoTracking().Where(x => x.DetailId == id);
            var detail = _context.CctvProjectDetail.Find(id)!;
            ViewBag.Products = products;
            ViewBag.ProjectName = detail.ProjectName + " " + detail.ProjectDistrict + " " + detail.Unit;
            ViewBag.DetailId = id;
            return View();
        }
       
        [HttpPost]
        public IActionResult ProductsAdd(CctvProductsViewModel project)
        {                       
            if (!_cctv.ProjectProductAdd(project, UserName))
            {
                TempData["Error"] = "CCTV Proje Fazı Malzeme Detayı Ekleme İşlemi Tamamlanamadı.";
                return RedirectToAction("Products", new { id = project.DetailId });
            }
            TempData["Status"] = "CCTV Proje Fazı Malzeme Detayı Ekleme İşlemi Tamamlandı.";
            return RedirectToAction("Products", new { id = project.DetailId });
        }
        [HttpPost]
        public IActionResult ProductRemove(CctvProductsViewModel project)
        {
            var projectDetails = _context.CctvProducts.Find(project.Id)!;
            var detailId = projectDetails.DetailId;
            if (!_cctv.ProjectProductRemove(projectDetails, UserName))
            {
                TempData["Error"] = "CCTV Proje Fazı Malzeme Detayı Silme İşlemi Tamamlanamadı.";
                return RedirectToAction("Products", new { id = detailId });
            }
            TempData["Status"] = "CCTV Proje Fazı Malzeme Detayı Silme İşlemi Tamamlandı.";
            return RedirectToAction("Products", new { id = detailId });
        }
        [HttpGet]
        public IActionResult ProductUpdate(int id)
        {
            var project = _context.CctvProducts.Find(id);
            return View(project);
        }
        [HttpPost]
        public IActionResult ProductUpdate(CctvProductsModel project)
        {

            if (!_cctv.ProjectProductUpdate(project, UserName))
            {
                TempData["Error"] = "CCTV Proje Fazı Malzeme Detayı Güncelleme İşlemi Tamamlanamadı.";
                return RedirectToAction("ProjectProductsUpdate", new { id = project.Id });
            }
            TempData["Status"] = "CCTV Proje Fazı Malzeme Detayı Güncelleme İşlemi Tamamlandı.";
            return RedirectToAction("Products", new { id = project.DetailId });
        }
        public IActionResult ProjectFullDetail(int id)
        {
            ViewBag.Detail = _context.CctvProjectDetail.Find(id);
            ViewBag.Products = _context.CctvProducts.AsNoTracking().Where(x => x.DetailId == id);
            return View();
        }
        public IActionResult CreateExcel(int id)
        {
            ViewBag.Detail = _context.CctvProjectDetail.Find(id);
            ViewBag.Products = _context.CctvProducts.AsNoTracking().Where(x => x.DetailId == id);
            return View();
        }
       
    }
}
