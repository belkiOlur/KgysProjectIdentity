using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize]
    public class CalenderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogService _logger;
        private readonly IMapper _mapper;
        private readonly ITenderService _tender;
        private readonly IPermissionService _permission;
        private readonly ISelectListItemsService _selectItems;
        private string UserName => User.Identity!.Name!;
        public CalenderController(AppDbContext context, ITenderService tender, IPermissionService permission, IMapper mapper, ILogService logger, ISelectListItemsService selectItems)
        {
            _context = context;
            _tender = tender;
            _permission = permission;
            _mapper = mapper;
            _logger = logger;
            _selectItems = selectItems;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllPermissionsToCalender()
        {
            var permissions = _context.PermissionCalender.ToList();
            return new JsonResult(permissions);
        }
        [HttpPost]
        public IActionResult AddPermissionsToCalender(CalenderViewModel permission)
        {
            try
            {
                if (permission.end > permission.start)
                {
                    _permission.PermissionAdd(permission, UserName);

                    TempData["status"] = "İzin Başarıyla Eklendi";
                    return RedirectToAction("Index");
                }
                TempData["Error"] = "Tarih Uyumsuzluğundan İzin Eklenemedi";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "İzin Eklenemedi";
                return RedirectToAction("Index");

            }
        }
        public IActionResult UserPermission(int year)
        {
            ViewBag.UserName = _context.Users.Where(x => x.UserName == UserName);
            var permissions = _context.PermissionCalender.Where(x => x.userName == UserName && x.start!.Value.Year == year).OrderByDescending(x => x.start).ToList();
            if (User.HasClaim(x => x.Value == "PlanlamaAmiri"))
            {
                permissions = _context.PermissionCalender.Where(x => x.start!.Value.Year == year).OrderByDescending(x => x.start).ToList();
                ViewBag.UserName = _selectItems.UsersReponse();
            }
            ViewBag.UserPermission = permissions;
            ViewBag.Years = _permission.Years();
            return View();
        }
        [HttpPost]
        public IActionResult PermissionRemove(int id)
        {
            var permission = _context.PermissionCalender.Find(id);
            _context.PermissionCalender.Remove(permission!);
            _context.SaveChanges();
            string log = UserName + " kullanıcısı" + permission!.userName + " e ait " + permission.start + " başlangıçlı izni sildi.";
            _logger.LogForAdd(log);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PermissionUpdate(CalenderViewModel permission)
        {
            _permission.PermissionUpdate(permission, UserName);
            return RedirectToAction("Index");
        }

    }
}
