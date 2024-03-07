using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize]
    public class ParkAndRecreationsController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly IDistrictService _district;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private SelectList StatusSelectList => new(_selectItems.StatusSelect(), "Value", "Data");
        private SelectList StatusSelectEpymsList => new(_selectItems.StatusEpmysSelect(), "Value", "Data");
        private SelectList RequestSelectList => new(_selectItems.RequestSelect(), "Value", "Data");
        private SelectList ParkSelectList => new(_selectItems.ParkSelect(), "Value", "Data");
        private SelectList DistrictSelectList => new(_context.DistrictModels, "Id", "districtName");
        private SelectList ReportEgmSelectList => new(_selectItems.ReportToEgmSelect(), "Value", "Data");
        private string UserName => User.Identity!.Name!;
        public ParkAndRecreationsController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, ILogService logService, IDistrictService district)
        {
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _logService = logService;
            _district = district;
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.ParkAndRecreation = _context.ParkAndRecreations.Where(x => x.ParkDistrictType == "Merkez").OrderBy(x => x.ParkDistrict).ToList();
            return View();
        }
        public IActionResult OuterIndex()
        {
            ViewBag.ParkAndRecreation = _context.ParkAndRecreations.Where(x => x.ParkDistrictType == "Dış").OrderBy(x => x.ParkDistrict).ToList();
            return View("Index");
        }


        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.ParksSelect = ParkSelectList;
            return View();
        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(ParkAndRecreationsViewModel parks)
        {
            parks.ParkDistrict = _district.GetDistrictNameById(Convert.ToInt32(parks.ParkDistrict));
            parks.ParkDistrictType = _district.GetDistrictType(parks.ParkDistrict);
            parks.UpdatedTime = DateTime.Now;
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.ParksSelect = ParkSelectList;
            if (ModelState.IsValid)
            {

                try
                {
                    string log = _detect.ParksAndRecreationAdd(parks);
                    _context.ParkAndRecreations.Add(_mapper.Map<ParkAndRecreationsModel>(parks));
                    _context.SaveChanges();
                    _logService.LogForAdd(log);
                    TempData["status"] = "Park ve Rekreasyon Alanı Başarıyla Eklendi";
                    return RedirectToAction("Index");
                }
                catch
                {
                    string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + parks.ParkName + " isimli alanı eklemek isterken hata oluştu.";
                    _logService.LogForAdd(logError);
                    ModelState.AddModelError(String.Empty, "Park ve Rekreasyon Alanı Kaydedilirken Hata Oluştu");
                    return View();
                }
            }
            else
            {
                string logErrorr = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + parks.ParkName + " isimli alanı eklemek isterken hata oluştu.";
                _context.LogModel.Add(new LogModel { Log = logErrorr });
                TempData["status"] = "İşlem Gerçekleşirken Hata ile Karşılaşıldı";
                return View();
            }
        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var parks = _context.ParkAndRecreations.Find(id);
            if (parks!.ParkDistrict != "" && parks!.ParkDistrict != null)
            {
                var changeDistrict = _context.DistrictModels.Where(x => x.districtName == parks!.ParkDistrict).FirstOrDefault()!.Id.ToString();

                parks!.ParkDistrict = changeDistrict;
            }
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.ParksSelect = ParkSelectList;
            return View(_mapper.Map<ParkAndRecreationsModel>(parks));
        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Update(ParkAndRecreationsViewModel parks)
        {
            parks.ParkDistrict = _district.GetDistrictNameById(Convert.ToInt32(parks.ParkDistrict));
            parks.ParkDistrictType = _district.GetDistrictType(parks.ParkDistrict);
            parks!.UpdatedTime = DateTime.Now;
            parks!.UpdatedUser = UserName;
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.ParksSelect = ParkSelectList;
            if (ModelState.IsValid)
            {
                try
                {
                    string log = _detect.ParksAndRecreationUpdate(parks);
                    _context.ParkAndRecreations.Update(_mapper.Map<ParkAndRecreationsModel>(parks));
                    _context.SaveChanges();
                    _logService.LogForAdd(log);
                    TempData["status"] = "Park ve Rekreasyon Alanı Başarıyla Güncellendi";
                    return RedirectToAction("Index");
                }
                catch
                {
                    string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + parks.ParkName + " isimli alanı güncellemek isterken hata oluştu.";
                    _logService.LogForAdd(logError);
                    ModelState.AddModelError(String.Empty, "Alan Düzenlenirken Hata Oluştu");
                    return View();
                }
            }
            else
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + parks.ParkName + " isimli alanı güncellemek isterken hata oluştu.";
                _logService.LogForAdd(logError);
                TempData["status"] = "Park ve Rekreasyon Alanı Güncellenerken Beklenmeyen Bir Hata Oluştu";
                return View();

            }

        }
        [Authorize(Roles = "Admin,Silme")]
        public IActionResult Remove(ParkAndRecreationsViewModel parks)
        {
            int id = parks.Id;
            var park = _context.ParkAndRecreations.Find(id)!;
            try
            {
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + parks.ParkName + " isimli alanı sildi.";
                _context.ParkAndRecreations.Remove(park);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Park ve Rekreasyon Alanı Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            catch
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + parks.ParkName + " isimli alanı silmek isterken hata oluştu.";
                _logService.LogForAdd(logError);
                TempData["status"] = "Nokta Silinirken Hata Oluştu";
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {
            var parkAndReaRecreation = _context.ParkAndRecreations.OrderBy(x => x.ParkDistrict);
            return View(_mapper.Map<List<ParkAndRecreationsViewModel>>(parkAndReaRecreation));

        }
    }
}
