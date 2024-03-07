using KgysProjectIdentity.Core.Areas.Admin.ViewModel;
using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KgysProjectIdentity.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;

        public HomeController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userList = await _userManager.Users.ToListAsync();
            ViewBag.userViewModelList = userList.Select(x => new UserViewModel()
            {
                Id = x.Id,
                Name = x.UserName,
                FullName = x.FullName!,
                Email = x.Email
            }).ToList();
            ViewBag.rolesList = await _roleManager.Roles.ToListAsync();


            return View();

        }

        [HttpPost] //Login İşlemi yapılacak
        public async Task<IActionResult> SignUp(AdminPageViewModel request)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.rolesList = await _roleManager.Roles.ToListAsync();
                return View("Index");

            }

            var identityResult = await _userManager.CreateAsync(new() { UserName = request.Name, Email = request.Email, FullName = request.FullName }, request.PasswordConfirm);
            var UserResult = _context.UserAndValue.Add(new UserAndValueListModel() { Name = request.FullName, Value = request.Name });


            if (!identityResult.Succeeded)
            {
                TempData["SuccessMassage"] = "Şifrenin 'Kullanıcı Adı' İçermediğinden veya '1234' ile Başlamadığından Emin Olun.";
                ViewBag.rolesList = await _roleManager.Roles.ToListAsync();
                return RedirectToAction(nameof(HomeController.Index));
            }
            string log = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + request.FullName + " isimli ve " + request.Name + " kullanıcı isimli kişisini ekledi.";
            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();

            ViewBag.rolesList = await _roleManager.Roles.ToListAsync();

            return RedirectToAction(nameof(HomeController.Index));

        }

        public async Task<IActionResult> UserDelete(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id) ?? throw new Exception("Silinecek Üye Bulunamamıştır.");
            string log = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + userToDelete!.FullName + " isimli ve " + userToDelete!.UserName + " kullanıcı isimli kişisini sildi.";
            var result = await _userManager.DeleteAsync(userToDelete);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());

            }

            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();
            TempData["SuccessMassage"] = "Üye Başarıyla Silindi";
            return RedirectToAction(nameof(HomeController.Index));
        }
        [Authorize(Roles = "Log")]
        public IActionResult Log()
        {
            ViewBag.Log = _context.LogModel.ToList();
            return View();
        }
        [Authorize(Roles = "Log")]
        public IActionResult DeleteLog()
        {
            try
            {
                var log = _context.LogModel.Where(x => x.DateTime.Year <= (DateTime.Now.Year - 2));
                foreach (var item in log)
                {
                    var deleteLog = _context.LogModel.Find(item.Id);
                    _context.LogModel.Remove(deleteLog!);
                }
                string logDeleter = User.Identity!.Name + " isimli kullanıcı" + DateTime.Now + " tarihinde 2 yıldan eski logları sildi.";
                _context.SaveChanges();
                _context.LogModel.Add(new LogModel { Log = logDeleter });
                _context.SaveChanges();
                TempData["SuccessMassage"] = "2 Yıldan Eski Loglar Başarıyla Silindi";
                return RedirectToAction("Log");
            }
            catch
            {
                TempData["Error"] = "2 Yıldan Eski Loglar Silinirken Bilinmeyen Bir Hata Alındı";
                return RedirectToAction("Log");
            }
        }

        [AcceptVerbs("GET", "POST")] //Clientta uzaktaki veri tabını kontrol etmek için kullanılır
        public IActionResult UserCheck(AdminPageViewModel request)
        {

            var userList = _context.UserAndValue.Any(x => x.Value == request.Name);
            var email = _context.Users.Any(x => x.Email == request.Email);
            if (userList)
            {
                return Json("Kaydetmeye Çalıştığınız Üye Mevcuttur.");
            }

            if (email)
            {
                return Json("Kullanmak İstediğiniz E-mail Mevcuttur.");
            }
            else
            {
                return Json(true);
            }
        }



    }
}
