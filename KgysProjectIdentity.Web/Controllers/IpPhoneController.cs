using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize]
    public class IpPhoneController:Controller
    {
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
