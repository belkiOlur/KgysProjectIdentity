using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize]
    public class TenderProjectsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly IDistrictService _district;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private readonly ITenderService _tender;
        private SelectList ReportEgmSelectList => new(_selectItems.ReportToEgmSelect(), "Value", "Data");
        private SelectList ParkSelectList => new(_selectItems.TenderSelect(), "Value", "Data");
        private SelectList ResponseOfficials => new(_selectItems.UsersReponse(), "UserName", "FullName");
        private SelectList Officials => new(_selectItems.Users(), "UserName", "FullName");
        private SelectList Commission => new(_selectItems.CommissionUsers(), "UserName", "FullName");
        private string UserName => User.Identity!.Name!;
        public TenderProjectsController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, ILogService logService, IDistrictService district, ITenderService tender)
        {
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _logService = logService;
            _district = district;
            _tender = tender;
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.TenderProjects = _context.TenderProjects.Where(x => x.Status != "Tamamlandı");
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult ProjectIndex(int id)
        {

            ViewBag.TenderName = _context.TenderProjects.Find(id)!.ProjectName;
            ViewBag.TenderProjects = _context.TenderProjects.Where(x => x.Id == id).ToList();
            ViewBag.SpecificationOfficials = _context.TenderOfSpecificationOfficials.Where(x => x.TenderId == id).Select(x => x.OfficialName).ToList();
            ViewBag.CommisionOfficials = _context.TenderOfAdmissionCommissionOfficials.Where(x => x.TenderId == id).Select(x => x.OfficialsName).ToList();
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult FinishProjectIndex()
        {
            ViewBag.TenderProjects = _context.TenderProjects.Where(x => x.Status == "Tamamlandı");
            return View("Index");
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult OfficialTenders(string id)
        {
            var userName = _tender.FindOfficialsUserNameById(id);
            ViewBag.OfficialsSpecification = _tender.FindOfficialsSpecificationsCommissioner(userName);
            ViewBag.OfficialsAdmissions = _tender.FindOfficialsAdmissionsCommissioner(userName);
            ViewBag.UserName = _tender.FindUserFullNameByUserName(userName);
            return View();
        }
        public IActionResult CommissionOfficials()
        {
            ViewBag.CommissionsOfficials = _context.UserRoles.Where(x => x.RoleId == "5a08b3a7-0083-4122-be9b-5c4e8f13506b" || x.RoleId == "5b1d3d0d-a450-4749-9c4f-d1204cfca595");
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.ResponseOfficials = ResponseOfficials;
            ViewBag.Officials = Officials;
            ViewBag.Commission = Commission;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.RequestSelect = ParkSelectList;
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(TenderProjectsViewModel tender)
        {
            tender.UpdatedTime = DateTime.Now;
            tender.UpdatedUser = UserName;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.TenderProjects.Add(_mapper.Map<TenderProjectsModel>(tender));
                    _context.SaveChanges();
                    var tenderIds = _context.TenderProjects.Where(x => x.ProjectName == tender.ProjectName).FirstOrDefault()!.Id;
                    if (tender.Officials != null)
                    {
                        _tender.AddSpecificationOfficials(tender, tenderIds);
                    }
                    if (tender.AdmissionCommission != null)
                    {
                        _tender.AddAdmissionCommissionOfficials(tender, tenderIds);
                    }
                    string log = _detect.TenderAdd(tender);
                    _logService.LogForAdd(log);
                    TempData["status"] = "İhale Başarıyla Eklendi";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + tender.ProjectName + " işni eklerken beklenmeyen hata oluştu.";
                    _logService.LogForAdd(logError);
                    //Sahipsiz Validation yollamak için kullanılır
                    ModelState.AddModelError(String.Empty, "İhale Kaydedilirken Hata Oluştu");
                    return View();
                }

            }
            else
            {
                string logEror = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + tender.ProjectName + " işni eklerken eksik bilgi nedeniyle hata oluştu.";
                _logService.LogForAdd(logEror);
                ModelState.AddModelError(String.Empty, "Nedenini Bilmediğim Bir Hata Oluştu");
                return View();
            }

        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.ResponseOfficials = ResponseOfficials;
            ViewBag.Officials = Officials;
            ViewBag.Commission = Commission;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.RequestSelect = ParkSelectList;
            var tender = _context.TenderProjects.Find(id);
            return View(_mapper.Map<TenderProjectsViewModel>(tender));
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Update(TenderProjectsViewModel updateTender)
        {
            updateTender.UpdatedTime = DateTime.Now;
            updateTender.UpdatedUser = UserName;
            if (!ModelState.IsValid)
            {
                string logEror = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + updateTender.ProjectName + " işini güncellerlerken eksik bilgi nedeniyle hata oluştu.";
                _logService.LogForAdd(logEror);
                ViewBag.ResponseOfficials = ResponseOfficials;
                ViewBag.Officials = Officials;
                ViewBag.Commission = Commission;
                ViewBag.ReportSelect = ReportEgmSelectList;
                ViewBag.RequestSelect = ParkSelectList;
                TempData["Error"] = "Güncelleme Eksiklik Nedeniyle Başarısız Oldu";
                return View();
            }
            string log = _detect.TenderUpdate(updateTender);
            _context.TenderProjects.Update(_mapper.Map<TenderProjectsModel>(updateTender));
            if (updateTender.Officials != null)
            {
                _tender.UpdateSpecificationOfficials(updateTender);
            }
            if (updateTender.AdmissionCommission != null)
            {
                _tender.UpdateAdmissionCommissionOfficials(updateTender);
            }
            _context.SaveChanges();
            _logService.LogForAdd(log);
            TempData["status"] = "İhale Başarıyla Güncellendi";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,;Silme")]
        public IActionResult Remove(int id)
        {
            var tender = _context.TenderProjects.Find(id)!;
            string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + tender.ProjectName + " işini sildi.";
            _context.TenderProjects.Remove(tender);
            _context.SaveChanges();
            _logService.LogForAdd(log);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {

            var products = _context.TenderProjects.Where(x => x.Status != "Tamamlandı");

            return View(_mapper.Map<List<TenderProjectsViewModel>>(products));
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult FinishCreateExcel()
        {

            var products = _context.TenderProjects.Where(x => x.Status == "Tamamlandı");

            return View("CreateExcel", _mapper.Map<List<TenderProjectsViewModel>>(products));
        }

    }
}
