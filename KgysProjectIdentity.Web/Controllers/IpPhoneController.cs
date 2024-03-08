using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize]
    public class IpPhoneController:Controller
    {
        private readonly IIpPhoneService _ipphone;

        public IpPhoneController(IIpPhoneService ipphone)
        {
            _ipphone = ipphone;
        }

        private string UserName => User.Identity!.Name!;
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Add(IpPhoneViewModel phone)
        {
            try
            {               
                TempData["status"] = "Silme İşlemi Tamamlandı";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["status"] = "Silme İşlemi Başarısız Oldu";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(IpPhoneModel phone)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Remove(IpPhoneViewModel phone)
        {
            return View();
        }
    }
}
