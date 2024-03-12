using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize]
    public class IpPhoneController : Controller
    {
        private readonly IIpPhoneService _ipphone;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        private SelectList PrioritySelectList => new(_context.PriorityForPhone, "PriorityName", "PriorityName");
        public IpPhoneController(IIpPhoneService ipphone, AppDbContext context, IMapper mapper)
        {
            _ipphone = ipphone;
            _context = context;
            _mapper = mapper;
        }
        private string UserName => User.Identity!.Name!;
        public IActionResult Index()
        {
            ViewBag.Priority = _context.PriorityForPhone;
            ViewBag.IpPhone = _context.IpPhoneProject.AsNoTracking().Where(x => x.Priority == "Mevcut").ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Priority(string priority)
        {
            ViewBag.Priority = _context.PriorityForPhone;
            ViewBag.IpPhone = _context.IpPhoneProject.AsNoTracking().Where(x => x.Priority == priority);
            return View("Index");
        }
        [HttpGet]
        public IActionResult Complete(int id)
        {
            if (!_ipphone.Complete(id, UserName))
            {
                TempData["Error"] = "Ip Telefon Kurulumu;Güncelleme İşlemi Tamamlanamadı";
                return RedirectToAction("Index");
            }

            TempData["Status"] = "Ip Telefon Kurulumu Güncelleme İşlemi Başarıyla Tamamlandı";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.PriorityName = PrioritySelectList;
            return View();
        }
        [HttpPost]
        public IActionResult Add(IpPhoneViewModel phone)
        {
            if (!_ipphone.Add(phone, UserName))
            {
                TempData["Error"] = "Ip Telefon Kurulumu Ekleme İşlemi Tamamlanamadı";
                return RedirectToAction("Index");
            }
            TempData["Status"] = "Ip Telefon Kurulumu Ekleme İşlemi Başarıyla Tamamlandı";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Update(int id)
        {

            ViewBag.PriorityName = PrioritySelectList;
            var IpPhone = _context.IpPhoneProject.Find(id);
            return View(IpPhone);
        }
        [HttpPost]
        public IActionResult Update(IpPhoneModel phone)
        {

            if (!_ipphone.Update(phone, UserName))
            {
                TempData["Error"] = "Ip Telefon Kurulumu Güncelleme İşlemi Tamamlanamadı";
                return RedirectToAction("Index");
            }
            TempData["Status"] = "Ip Telefon Kurulumu Güncelleme İşlemi Başarıyla Tamamlandı";
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult Remove(IpPhoneViewModel phone)
        {
            if (!_ipphone.Remove(phone.Id, UserName))
            {
                TempData["Error"] = "Ip Telefon Kurulumu Silme İşlemi Tamamlanamadı";
                return RedirectToAction("Index");
            }

            TempData["Status"] = "Ip Telefon Kurulumu Silme İşlemi Başarıyla Tamamlandı";
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Project()
        {
            ViewBag.IpPhone = _context.PriorityForPhone;
            return View();
        }
        [HttpPost]
        public IActionResult ProjectAdd(PriorityForIpPhoneModel phone)
        {

            if (!_ipphone.ProjectAdd(phone, UserName))
            {
                TempData["Error"] = "Ip Telefon Kurulum Etabı Ekleme İşlemi Tamamlanamadı";
                return RedirectToAction("Project");
            }

            TempData["Status"] = "Ip Telefon Kurulum Etabı Ekleme İşlemi Başarıyla Tamamlandı";
            return RedirectToAction("Project");

        }
        [HttpPost]
        public IActionResult ProjectRemove(PriorityForIpPhoneModel phone)
        {

            if (!_ipphone.ProjectRemove(phone.Id, UserName))
            {
                TempData["Error"] = "Ip Telefon Kurulum Etabı Silme İşlemi Tamamlanamadı";
                return RedirectToAction("Project");

            }
            TempData["Status"] = "Ip Telefon Kurulum Etabı Silme İşlemi Başarıyla Tamamlandı";
            return RedirectToAction("Project");

        }
        [HttpGet]
        public IActionResult CreateExcel()
        {
            ViewBag.IpPhone = null;
            return View();
        }
        [HttpPost]
        public IActionResult CreateExcel(string ipPhone)
        {
            List<IpPhoneViewModel> ipPhoneList = JsonConvert.DeserializeObject<List<IpPhoneViewModel>>(ipPhone)!;
            ViewBag.IpPhone = ipPhoneList;
            return View();
        }
    }
}
