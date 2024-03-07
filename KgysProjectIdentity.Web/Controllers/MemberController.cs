using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using KgysProjectIdentity.Web.Areas.Admin.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace KgysProjectIdentity.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogDifferenceDetectService _detect;
        private readonly IFileProvider _fileProvider;
        private readonly IEmailService? _emailService;
        private readonly IMemberService _memberService;
        private string UserName => User.Identity!.Name!;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider, AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, IMemberService memberService, IEmailService? emailService = null)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
            _context = context;
            _emailService = emailService;
            _detect = detect;
            _mapper = mapper;
            _memberService = memberService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _memberService.GetUseViewModelByUserNameAsync(UserName));

        }
        [Authorize]
        public IActionResult PasswordChange()
        {

            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!await _memberService.CheckPasswordAsync(UserName, request.PasswordOld))
            {
                ModelState.AddModelError(string.Empty, "Eski Şifreyi Yanlış Girdiniz");
                return View();
            }

            var (isSucceeded, errors) = await _memberService.ChangePasswordAsync(UserName, request.PasswordOld, request.PasswordNew);

            if (!isSucceeded)
            {
                ModelState.AddModelErrorList((IEnumerable<IdentityError>)errors!);
                return View();
            }


            TempData["SuccessMassage"] = "Şifreniz Başarıyla Değiştirilmiştir.";


            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(SignInViewModel request)
        {
            string newCode = await _memberService.SecurityCodeCreate();
            await _memberService.EmailCheck(request.Email!);
            if (!await _memberService.EmailCheck(request.Email!))
            {
                TempData["Error"] = "Girilen E-Mail Sistemde Kayıtlı Değil.";
                return RedirectToAction("SignIn", "Home");
            }
            await _memberService.SecurityCodeUsageCheck(request.Email!);
            if (await _memberService.SecuritCodeSendAndSave(request.Email!, newCode))
            {
                TempData["SuccessMassage"] = "Güvenlik Kodunuz E-mailinize Gönderilmiştir.";
                return RedirectToAction("ForgetPasswordChange", "Member");
            }
            TempData["Error"] = "Girilen E-Mail Kullanıcı ile Eşleşmedi.";
            return RedirectToAction("SignIn", "Home");

        }

        [HttpGet]
        public IActionResult ForgetPasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPasswordChange(PasswordChangeViewModel request)
        {
            string newPassword = await _memberService.NewPasswordCreate();

            if (await _memberService.SecurityCodeCheck(request.PasswordOld))
            {
                var userName = await _memberService.FindUser(request.PasswordOld);

                if (userName != null)
                {
                    var currentUser = await _userManager.FindByNameAsync(userName);
                    string token = await _userManager.GeneratePasswordResetTokenAsync(currentUser!);
                    var resetChangePassword = await _userManager.ResetPasswordAsync(currentUser!, token, newPassword!);
                    if (!resetChangePassword.Succeeded)
                    {
                        ModelState.AddModelErrorList(resetChangePassword.Errors.Select(x => x.Description).ToList());
                        return RedirectToAction("SignIn", "Home");
                    }
                    await _userManager.ChangePasswordAsync(currentUser!, newPassword, request.PasswordNew);
                    await _memberService.SecurityCodeUsage(request.PasswordOld);
                    TempData["SuccessMassage"] = "Şifreniz Başarıyla Değiştirilmiştir";
                    return RedirectToAction("SignIn", "Home");
                }
            }
            TempData["Error"] = "Şifre Değiştirilemedi Tekrar Deneyiniz.";
            return RedirectToAction("SignIn", "Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserEdit(string id)
        {

            return View(await _memberService.GetUserEditViewModelAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByIdAsync(request.Id!);
            string log = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + currentUser!.UserName;
            log += _detect.UserEdit(request);
            currentUser!.UserName = request.UserName;
            currentUser.FullName = request.FullName;
            currentUser.Email = request.Email;

            if (request.Picture != null && request.Picture.Length > 0)
            {
                var wwroot = _fileProvider.GetDirectoryContents("wwwroot");

                var randomFName = $"{Guid.NewGuid()/*.ToString()*/}{Path.GetExtension(request.Picture.FileName)}";

                var newpicturePath = Path.Combine(wwroot!.First(x => x.Name == "userpictures").PhysicalPath!, randomFName);
                using var stream = new FileStream(newpicturePath, FileMode.Create);

                await request.Picture.CopyToAsync(stream);

                currentUser.Picture = randomFName;

                log += " fotoğrafını " + randomFName + ", ";
            }
            var updatedUser = await _userManager.UpdateAsync(currentUser);
            log += "olacak şekilde güncelledi.";
            if (!updatedUser.Succeeded)
            {
                ModelState.AddModelErrorList(updatedUser.Errors);
                return View();
            }
            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();
            await _userManager.UpdateSecurityStampAsync(currentUser);
            //await _signInManager.SignOutAsync();
            //await _signInManager.SignInAsync(currentUser,true);

            TempData["SuccessMassage"] = "Kullanıcı Bilgileri Başarıyla Değiştirilmiştir.";


            var userEditViewModel = new UserEditViewModel() //!!!!!!!!!Uygun Değil Düzelt Bunu!!!!!!!!!! Update Alanının dolması için yazıldı.
            {
                UserName = currentUser.UserName!,
                FullName = currentUser.FullName!,
                Email = currentUser.Email!,
            };

            return RedirectToAction(nameof(HomeController.Index), "Admin");
            //return View(userEditViewModel);
        }
        //public async Task<IActionResult> Logout() //return komutu için

        public async Task Logout() // Bu Methot için Program.cs'e options eklenir. // Çıkış navbar üzerindeki asp-root-returnurl üzerinden yapılır.
        {
            await _memberService.Logout();
            //return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            ViewBag.message = "Bu Sayfayı Görmeye Yetkiniz Yoktur. Yetki Almak İçin Bürolar Amirimizle Görüşünüz.";
            return View();
        }

        [AcceptVerbs("GET", "POST")] //Clientta uzaktaki veri tabını kontrol etmek için kullanılır
        public async Task<IActionResult> UserCheck(SignInViewModel request)
        {
            if (!await _memberService.EmailCheck(request.Email!))
            {
                return Json("Kullanmak İstediğiniz E-mail Mevcut Değildir.");
            }
            else
            {
                return Json(true);
            }
        }

        [AcceptVerbs("GET", "POST")] //Clientta uzaktaki veri tabını kontrol etmek için kullanılır
        public async Task<IActionResult> CodeCheck(SecurityCodeViewModel code)
        {

            if (!await _memberService.SecurityCodeCheck(code.PasswordOld))
            {
                return Json("Kullanmak İstediğiniz Kod Bulunamamıştır.");
            }
            if (!await _memberService.SecurityCodeConfirm(code.PasswordOld))
            {
                return Json("Kullanmak İstediğiniz Kod Kullanılmıştır.");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
