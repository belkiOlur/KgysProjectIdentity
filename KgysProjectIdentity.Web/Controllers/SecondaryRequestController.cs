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
    public class SecondaryRequestController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private readonly IDistrictService _district;
        private SelectList StatusSelectList => new(_selectItems.StatusSelect(), "Value", "Data");
        private SelectList DistrictSelectList => new(_context.DistrictModels, "Id", "districtName");
        private string UserName => User.Identity!.Name!;
        public SecondaryRequestController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, ILogService logService, IDistrictService district)
        {

            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _logService = logService;
            if (!_context.SecondaryRequestModels.Any())
            {
                _context.SecondaryRequestModels.Add(new SecondaryRequestModel()
                {
                    District = "Balçova",
                    Name = "Balçova Polis Merkezi Amirliği",
                    InstitutialFO = "Mevcut",
                    TtDistance = "-",
                    TtRequest = "-",
                    TtStatus = "-",
                    Switch = "Mevcut",
                    Configuration = "Başlanılmadı",
                    Cable = "Başlanılmadı",
                    PcImaje = "PC Gelmedi",
                    Status = "Devam Ediyor",
                    UpdatedPersonel = "347347",
                    UpdatedTime = DateTime.Now
                });
                _context.SaveChanges();

            }
            _district = district;
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.Products = _context.SecondaryRequestModels.ToList();
            return View();
        }
        public IActionResult FinishApplicationIndex()
        {
            ViewBag.Products = _context.SecondaryRequestModels.ToList();
            return View();
        }
        [Authorize(Roles = "Admin,Silme")]
        [HttpPost]
        public IActionResult Remove(SecondaryRequestViewModel products)
        {
            int id = products.Id;
            var product = _context.SecondaryRequestModels.Find(id);
            try
            {
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product!.Name + " isimli Tali İzleme Başvuruyu sildi.";
                _context.SecondaryRequestModels.Remove(product);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                return RedirectToAction("Index");
            }
            catch
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product!.Name + " isimli Tali İzleme Başvurusu bulunamadığından silinemedi.";
                _logService.LogForAdd(logError);
                TempData["status"] = "Başvuru bulunamadığından silinemedi.";
                return RedirectToAction("Index");
            }
        }


        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.DistrictSelect = DistrictSelectList; ;
            ViewBag.StatusSelect = StatusSelectList;
            return View();
        }
        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(SecondaryRequestViewModel product)
        {


            ViewBag.DistrictSelect = DistrictSelectList; ;
            ViewBag.StatusSelect = StatusSelectList;
            product.District = _district.GetDistrictNameById(Convert.ToInt32(product.District));
            product.UpdatedPersonel = UserName!;
            product.UpdatedTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    string log = _detect.SecondaryRequestAdd(product);
                    _context.SecondaryRequestModels.Add(_mapper.Map<SecondaryRequestModel>(product));
                    _context.SaveChanges();
                    _logService.LogForAdd(log);
                    TempData["status"] = "Başvuru Başarıyla Eklendi";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product.Name + " isimli Tali İzleme Başvuruyu eklerken hata oluştu.";
                    _logService.LogForAdd(logError);
                    //Sahipsiz Validation yollamak için kullanılır
                    ModelState.AddModelError(String.Empty, "Başvuru Kaydedilirken Hata Oluştu");
                    return View();
                }

            }
            else
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product.Name + " isimli Tali İzleme Başvuru bilgilerinin eksikliği nedeniyle eklenemedi.";
                _logService.LogForAdd(logError);
                return View();
            }

        }


        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.SecondaryRequestModels.Find(id);
            product!.District = _district.GetDistrictId(product.District!);
            ViewBag.DistrictSelect = DistrictSelectList; ;
            ViewBag.StatusSelect = StatusSelectList;
            return View(_mapper.Map<SecondaryRequestViewModel>(product));
        }


        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Update(SecondaryRequestViewModel updateProduct)
        {
            updateProduct.District = _context.DistrictModels.Find(Convert.ToInt32(updateProduct.District))!.districtName;
            if (!ModelState.IsValid)
            {
                string logError = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + updateProduct.Name + " isimli Tali İzleme Başvuru bilgilerinin eksikliği nedeniyle güncellerken hata oluştu.";
                _logService.LogForAdd(logError);
                ViewBag.DistrictSelect = DistrictSelectList; ;
                ViewBag.StatusSelect = StatusSelectList;
                return View();
            }
            string log = _detect.SecondaryRequestUpdate(updateProduct);
            _context.SecondaryRequestModels.Update(_mapper.Map<SecondaryRequestModel>(updateProduct));
            _context.SaveChanges();
            _logService.LogForAdd(log);
            TempData["status"] = "Başvuru Başarıyla Güncellendi";
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {
            var products = _context.SecondaryRequestModels.ToList();
            return View(_mapper.Map<List<SecondaryRequestViewModel>>(products));
        }

        [Authorize(Roles = "Admin,Hat,PlanlamaAmiri")]
        public IActionResult FineshedCreateExcel()
        {
            var products = _context.SecondaryRequestModels.ToList();
            return View(_mapper.Map<List<SecondaryRequestViewModel>>(products));
        }

        //[AllowAnonymous]
        //[AcceptVerbs("GET", "POST")] //Clientta uzaktaki veri tabını kontrol etmek için kullanılır
        //public IActionResult TemosNumberCheck(string temosNumber)
        //{
        //    var anyProduct = _context.Products.Any(x => x.TemosNumber == temosNumber);
        //    if (anyProduct)
        //    {
        //        return Json("Kaydetmeye Çalıştığınız Başvuru Mevcuttur.");
        //    }
        //    else
        //    {

        //        return Json(true);
        //    }


        //}
    }
}
