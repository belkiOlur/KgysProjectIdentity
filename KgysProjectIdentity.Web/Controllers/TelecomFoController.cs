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
    public class TelecomFoController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper; //Product nesnelerini ProductViewModel'den almak için
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private readonly IDistrictService _district;
        private readonly ITelecomService _telecom;
        private SelectList AplicationSelectList => new(_selectItems.AplicationSelect(), "Value", "Data");
        private SelectList StatusSelectList => new(_selectItems.StatusSelect(), "Value", "Data");
        private SelectList StatusSelectEpymsList => new(_selectItems.StatusEpmysSelect(), "Value", "Data");
        private SelectList DistrictSelectList => new(_context.DistrictModels, "Id", "districtName");
        private SelectList TelecomSelectList => new(_context.TelecomTeams, "Id", "TelecomTeam");
        private string UserName => User.Identity!.Name!;


        public TelecomFoController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, ILogService logService, IDistrictService district, ITelecomService telecom)
        {

            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _logService = logService;
            _district = district;
            _telecom = telecom;
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.Products = _context.Products.Where(x => x.Aplication != null && x.Aplication != "Planlanan").ToList();
            return View();
        }
        //public IActionResult Indexx()
        //{
        //    var request = _context.Products.Where(x => x.KuppaID != null && (x.Aplication == null || x.Aplication == "" || x.Aplication == "Planlanan")).ToList();
        //    foreach(var item in request)
        //    {

        //        item.KuppaPrice = _telecom.GetKuppaPrice(_mapper.Map<TelecomFoViewModel>(item));
        //        _context.Products.Update(_mapper.Map<TelecomFoModel>(item));
        //        _context.SaveChanges();
        //    }
        //    TempData["status"] = "Tamamlandı";
        //    return View("Index");
        //}
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult FinishTtApplicationIndex()
        {
            ViewBag.Products = _context.Products.Where(x => x.CheckEGM == "Tamamlandı").ToList();
            return View();
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult RequestIndex()
        {
            ViewBag.Products = _context.Products.Where(x => x.KuppaID == null && (x.Aplication == null || x.Aplication == "" || x.Aplication == "Planlanan")).ToList();
            return View("Index");
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult KuppaIndex()
        {
            ViewBag.Products = _context.Products.Where(x => x.KuppaID != null && (x.Aplication == null || x.Aplication == "" || x.Aplication == "Planlanan")).ToList();
            //ViewBag.currenUser = _userManager.FindByNameAsync(User.Identity!.Name!);

            return View("Index");
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult TeamAdd()
        {
            ViewBag.Teams = _context.TelecomTeams.ToList();
            return View();
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult TeamAdd(TelecomTeamViewModel team)
        {
            ViewBag.Teams = _context.TelecomTeams.ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + team.TelecomTeam + " Telekom Takımını ekledi.";
                    _context.TelecomTeams.Add(_mapper.Map<TelecomTeamModel>(team));
                    _context.SaveChanges();
                    _logService.LogForAdd(log);
                    TempData["status"] = "Başvuru Başarıyla Eklendi";
                    return RedirectToAction("TeamAdd");
                }
                catch (Exception)
                {
                    string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + team.TelecomTeam + " Telekom Takımını eklerken beklenmeyen hata oluştu.";
                    _logService.LogForAdd(logError);
                    //Sahipsiz Validation yollamak için kullanılır
                    ModelState.AddModelError(String.Empty, "Başvuru Kaydedilirken Hata Oluştu");
                    return View();
                }

            }
            else
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + team.TelecomTeam + " Telekom Takımını eklerken eksik bilgi nedeniyle hata oluştu.";
                _logService.LogForAdd(logError);
                return View();
            }

        }
        [Authorize(Roles = "Admin,Hat,Silme")]
        [HttpPost]
        public IActionResult TeamRemove(TelecomTeamViewModel team)
        {
            try
            {
                var id = team.Id;
                var product = _context.TelecomTeams.Find(id);
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product!.TelecomTeam + " Telekom Takımını sildi.";
                _context.TelecomTeams.Remove(product!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                return RedirectToAction("TeamAdd");
            }
            catch
            {
                string logError = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + team.TelecomTeam + " Telekom Takımını silerken hata oluştu.";
                _logService.LogForAdd(logError);
                return RedirectToAction("TeamAdd");
            }
        }

        [Authorize(Roles = "Admin,Hat,Silme")]
        public IActionResult Remove(TelecomFoViewModel telecom)
        {
            var id = telecom.Id;
            var product = _context.Products.Find(id);
            try
            {
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product!.Name + " Telekom Başvurusunu sildi.";
                _context.Products.Remove(product!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                return RedirectToAction("Index");
            }
            catch
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product!.Name + " Telekom Başvurusunu silerken hata oluştu.";
                _logService.LogForAdd(logError);
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.AplicationSelect = AplicationSelectList;
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.StatusEpmysSelect = StatusSelectEpymsList;
            ViewBag.TtTeamSelect = TelecomSelectList;
            return View();
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(TelecomFoViewModel product)
        {
            ViewBag.AplicationSelect = AplicationSelectList;
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.StatusEpmysSelect = StatusSelectEpymsList;
            ViewBag.TtTeamSelect = TelecomSelectList;
            product!.District = _district.GetDistrictNameById(Convert.ToInt32(product.District));
            if (product.Team != null)
            {
                product!.Team = _context.TelecomTeams.Find(Convert.ToInt32(product.Team))!.TelecomTeam;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    string log = _detect.TelecomFoAdd(product);
                    _context.Products.Add(_mapper.Map<TelecomFoModel>(product));
                    _context.SaveChanges();
                    _logService.LogForAdd(log);
                    TempData["status"] = "Başvuru Başarıyla Eklendi";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product.Name + " Telekom Başvurusunu Eklerken Beklenmeyen Hata Oluştu.";
                    _logService.LogForAdd(logError);
                    //Sahipsiz Validation yollamak için kullanılır
                    ModelState.AddModelError(String.Empty, "Başvuru Kaydedilirken Hata Oluştu");
                    return View();
                }

            }
            else
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product.Name + " Telekom Başvurusunu Eklerken Eksiklik Nedeniyle Hata Oluştu.";
                _logService.LogForAdd(logError);
                TempData["status"] = "Başvuru Eksiklik Nedeniyle Eklenemedi.";
                return View();
            }
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.AplicationSelect = AplicationSelectList;
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.StatusEpmysSelect = StatusSelectEpymsList;
            ViewBag.TtTeamSelect = TelecomSelectList;
            var product = _context.Products.Find(id);
            if (product!.Team != "" && product!.Team != null)
            {
                product.Team = _context.TelecomTeams.Where(x => x.TelecomTeam == product!.Team).FirstOrDefault()!.Id.ToString();
            }
            if (product!.District != "" && product!.District != null)
            {
                product.District = _district.GetDistrictId(product!.District);
            }
            return View(_mapper.Map<TelecomFoViewModel>(product));
        }

        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Update(TelecomFoViewModel request)
        {
            request.KuppaPrice = _telecom.GetKuppaPrice(request);
            request.UpdatedTime = DateTime.Now;
            request.UpdatedUser = UserName;
            if (request!.Team != "" && request!.Team != null)
            {
                request!.Team = _context.TelecomTeams.Find(Convert.ToInt32(request.Team))!.TelecomTeam;
            }
            if (request!.District != "" && request!.District != null)
            {
                request!.District = _district.GetDistrictNameById(Convert.ToInt32(request.District));
            }
            if (!ModelState.IsValid)
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + request.Name + " Telekom Başvurusunu Güncellerken Eksiklik Nedeniyle Hata Oluştu.";
                _logService.LogForAdd(logError);
                TempData["status"] = "Başvuru Eksiklik Nedeniyle Güncellenemedi";
                RedirectToAction("Update", "TelecomFo", new { id = request.Id });
            }
            string log = _detect.TelecomFoUpdate(request);
            _context.Products.Update(_mapper.Map<TelecomFoModel>(request));
            _context.SaveChanges();
            _logService.LogForAdd(log);
            TempData["status"] = "Başvuru Başarıyla Güncellendi";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult TnUpdate(int id)
        {
            var product = _context.Products.Find(id);
            return View(_mapper.Map<TelecomFoViewModel>(product));
        }


        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult TnUpdate(TelecomFoViewModel request)
        {
            var updateRequest = _context.Products.Find(request.Id);
            updateRequest!.TemosNumber = request.TemosNumber;
            updateRequest!.UpdatedTime = DateTime.Now;
            updateRequest!.UpdatedUser = UserName;
            try
            {
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + updateRequest.Name + " İsimli Telekom Başvurusunun Temos Numarasını = " + request.TemosNumber + " olacak şekilde güncelledi.";
                _context.Products.Update(_mapper.Map<TelecomFoModel>(updateRequest));
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Başvuru Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            catch
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + updateRequest.Name + " İsimli Telekom Başvurusunun Temos Numarasını = " + request.TemosNumber + " olacak şekilde güncellerken beklenmeyen hata oluştu.";
                _logService.LogForAdd(logError);
                TempData["status"] = "Başvuru Güncellenemedi";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult KuppaMultiplier()
        {
            ViewBag.KuppaMultiplier = _context.KuppaMultipliers.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult KuppaMultiplierAdd(KuppaMultiplierModel kuppaMultiplier)
        {
            try
            {
                if (kuppaMultiplier.Multiplier != null)
                {
                    string log = UserName + " kuppa yılını = " + Convert.ToString(kuppaMultiplier.Year) + " kuppa çarpanını = " + Convert.ToString(kuppaMultiplier.Multiplier) + " olacak şekilde ekledi. ";
                    _context.KuppaMultipliers.Add(kuppaMultiplier);
                    _context.SaveChanges();
                    _logService.LogForAdd(log);
                }
                return RedirectToAction("KuppaMultiplier");
            }
            catch
            {
                return RedirectToAction("KuppaMultiplier");
            }
        }
        [Authorize(Roles = "Admin,Hat")]
        [HttpPost]
        public IActionResult KuppaMultiplierRemove(KuppaMultiplierModel kuppaMultiplier)
        {
            try
            {
                var KuppaMultiplier = _context.KuppaMultipliers.Find(kuppaMultiplier.Id);
                string log = UserName + " kuppa yılını = " + Convert.ToString(KuppaMultiplier!.Year) + " kuppa çarpanını = " + Convert.ToString(KuppaMultiplier!.Multiplier) + " olan veriyi sildi. ";
                _context.KuppaMultipliers.Remove(KuppaMultiplier);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                return RedirectToAction("KuppaMultiplier");
            }
            catch
            {
                return RedirectToAction("KuppaMultiplier");
            }
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {
            var products = _context.Products.ToList();
            return View(_mapper.Map<List<TelecomFoViewModel>>(products));
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult FinishedCreateExcel()
        {
            var products = _context.Products.ToList();
            return View(_mapper.Map<List<TelecomFoViewModel>>(products));
        }

        [AcceptVerbs("GET", "POST")] //Clientta uzaktaki veri tabını kontrol etmek için kullanılır
        public IActionResult TemosNumberCheck(string temosNumber)
        {
            var anyProduct = _context.Products.Any(x => x.TemosNumber == temosNumber);
            if (anyProduct)
            {
                return Json("Kaydetmeye Çalıştığınız Başvuru Mevcuttur.");
            }
            else
            {

                return Json(true);
            }

        }

    }
}
