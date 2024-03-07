using KgysProjectIdentity.Core.Areas.Admin.ViewModel;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Web.Areas.Admin.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KgysProjectIdentity.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppDbContext _context;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, AppDbContext context, SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new RolesViewModel()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();


            return View(roles);
        }
        public IActionResult RolesCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RolesCreate(RolesCreateViewModel request)
        {
            var result = await _roleManager.CreateAsync(new AppRole() { Name = request.Name });
            string log = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + request.Name + " rolünü ekledi.";
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View();
            }
            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();
            TempData["SuccessMassage"] = "Rol Başarıyla Eklendi.";
            return RedirectToAction(nameof(HomeController.Index));
        }
        public async Task<IActionResult> RoleUpdate(string id)
        {
            var roleUpdate = await _roleManager.FindByIdAsync(id) ?? throw new Exception("Güncellenecek Rol Bulunamamıştır.");
            return View(new RolesUpdateViewModel() { Id = roleUpdate.Id, Name = roleUpdate.Name });
        }
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RolesUpdateViewModel request)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id!) ?? throw new Exception("Güncellenecek Rol Bulunamamıştır.");
            string log = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + roleToUpdate.Name + " = " + request.Name + " olacak şekilde güncelledi.";
            roleToUpdate.Name = request.Name;
            await _roleManager.UpdateAsync(roleToUpdate);
            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();
            TempData["SuccessMassage"] = "Rol Başarıyla Güncellendi";
            return RedirectToAction(nameof(RolesController.Index));
        }

        public async Task<IActionResult> RoleDelete(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id) ?? throw new Exception("Silinecek Rol Bulunamamıştır.");
            string log = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + roleToDelete.Name + " rolünü sildi.";
            var result = await _roleManager.DeleteAsync(roleToDelete);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());

            }
            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();
            TempData["SuccessMassage"] = "Rol Başarıyla Silindi";
            return RedirectToAction(nameof(RolesController.Index));
        }

        public async Task<IActionResult> AssignRoleToUser(string id)
        {
            var currentUser = (await _userManager.FindByIdAsync(id))!; //Güncell kullanıcıyı alır
            ViewBag.userId = id; //HttpPosta viewden göndermek için
            var roles = await _roleManager.Roles.ToListAsync(); //Rol bilgilerini alır
            var userRoles = await _userManager.GetRolesAsync(currentUser); //Kullanıcının Rol Bilgisini alır.
            var roleViewModelList = new List<AssingRoleToUserViewModel>();//Rolleri Listeler           


            foreach (var role in roles)
            {
                var assingRoleToUserViewModel = new AssingRoleToUserViewModel() { Id = role.Id, Name = role.Name };


                if (userRoles.Contains(role.Name!))
                {
                    assingRoleToUserViewModel.Exist = true;
                }
                roleViewModelList.Add(assingRoleToUserViewModel);
            }


            return View(roleViewModelList);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, List<AssingRoleToUserViewModel> requestList)
        {
            var userToAssignRoles = await _userManager.FindByIdAsync(userId);
            string log = User.Identity!.Name + " kullanıcısı " + DateTime.Now + " tarihinde " + userToAssignRoles!.UserName + " isimli kullanıcının rollerini = ";
            foreach (var role in requestList)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(userToAssignRoles, role.Name!);
                    log += role.Name + ", ";
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(userToAssignRoles, role.Name!);
                }


            }
            log += "olacak şekilde güncelledi.";
            _context.LogModel.Add(new LogModel { Log = log });
            _context.SaveChanges();
            // Kullanıcıyı oturum açma sayfasına yönlendir
            return RedirectToAction("Index", "Home");
        }
    }
}
