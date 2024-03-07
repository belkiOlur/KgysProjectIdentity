using AutoMapper;
using ClosedXML.Excel;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using KgysProjectIdentity.Web.Areas.Admin.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Diagnostics;
using System.Globalization;

namespace KgysProjectIdentity.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogService _logService;
        private readonly UserManager<AppUser> _userManager;//Kullanıcı ile ilgili işlemler için
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IAlertService _alert;
        private readonly IMapper _mapper;
        private string UserName => User.Identity!.Name!;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context, IMapper mapper, ILogService logService, IAlertService alert)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
            _logService = logService;
            _alert = alert;
        }

        public IActionResult Index()
        {
            //using (var workbook = new XLWorkbook(@"C:\Users\349103\Desktop\requestSon.xlsx"))
            //{
            //    var worksheet = workbook.Worksheet(1); // İlk çalışma sayfasını al
            //    var rows = worksheet.RangeUsed().RowsUsed(); // Başlık satırını atla

            //    foreach (var row in rows)
            //    {
            //        int productId;
            //        decimal projectCoordinateX = 0m;
            //        decimal projectCoordinateY = 0m;

            //        // ProductId için güvenli dönüşüm
            //        if (!int.TryParse(row.Cell(1).GetValue<string>(), out productId))
            //        {
            //            // productId dönüştürülemezse hata işleme veya atlama
            //            continue;
            //        }

            //        // ProjectCoordinateX için güvenli dönüşüm
            //        string projectCoordinateXStr = row.Cell(2).Value.ToString().Replace(",", ".").Trim();
            //        if (!string.IsNullOrEmpty(projectCoordinateXStr) &&
            //            !decimal.TryParse(projectCoordinateXStr, NumberStyles.Any, CultureInfo.InvariantCulture, out projectCoordinateX))
            //        {
            //            // projectCoordinateX dönüştürülemezse hata işleme veya atlama
            //            continue;
            //        }

            //        // ProjectCoordinateY için güvenli dönüşüm
            //        string projectCoordinateYStr = row.Cell(3).Value.ToString().Replace(",", ".").Trim();
            //        if (!string.IsNullOrEmpty(projectCoordinateYStr) &&
            //            !decimal.TryParse(projectCoordinateYStr, NumberStyles.Any, CultureInfo.InvariantCulture, out projectCoordinateY))
            //        {
            //            // projectCoordinateY dönüştürülemezse hata işleme veya atlama
            //            continue;
            //        }
            //        var product = _context.KgysRequest.Find(productId);
            //        if (product != null)
            //        {
            //            product.ProjectCoordinateX = projectCoordinateX;
            //            product.ProjectCoordinateY = projectCoordinateY;
            //            _context.KgysRequest.Update(product);
            //        }
            //    }

            //    _context.SaveChanges();

            //}

            ViewBag.Alert = _alert.AddGet();
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult AlertAdd()
        {
            ViewBag.Alert = _alert.AddGet();
            return View();
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult AlertAdd(AlertViewModel jobs)
        {
            ViewBag.Alert = _alert.AddGet();

            if (ModelState.IsValid)
            {
                try
                {
                    string log = _alert.AddPost(jobs, UserName);
                    _logService.LogForAdd(log);
                    TempData["status"] = "Tekrarlı Görev Başarıyla Eklendi";
                    return RedirectToAction("AlertAdd");
                }
                catch (Exception)
                {
                    string logError = jobs.Job + " isimli tekrarlı görevi " + UserName + " kullanıcısı " + DateTime.Now + " tarihinde eklemeye çalışırken hata oluştu.";
                    _logService.LogForAdd(logError);
                    //Sahipsiz Validation yollamak için kullanılır
                    ModelState.AddModelError(String.Empty, "Tekrarlı Görev Kaydedilirken Hata Oluştu");
                    return View();
                }

            }
            else
            {

                return View();
            }

        }
        [Authorize(Roles = "Admin,Silme")]
        [HttpPost]
        public IActionResult AlertRemove(AlertViewModel team)
        {
            string log = _alert.Remove(team, UserName);
            _logService.LogForAdd(log);
            return RedirectToAction("AlertAdd");
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            returnUrl ??= Url.Action("Index", "Home");

            if (model.Name == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Adı veya Şifre Hatalı.");
                return View();
            }
            //Birinci false bilgilerin cookie de saklanması için, ikinci false 3 kere arka arkaya yanlış girişde bir süre şifrenin kilitlenmesi için.
            var signInResult = await _signInManager.PasswordSignInAsync(model.Name, model.Password!, model.RememberMe, false);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl!);
            }


            ModelState.AddModelErrorList(new List<string>() { "Kullanıcı Adı veya Şifre Hatalı. " });///birden fazla gelmediği için new list yapıldı.

            return View();


        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}