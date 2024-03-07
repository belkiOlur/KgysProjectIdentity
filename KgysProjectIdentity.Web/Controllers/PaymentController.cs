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
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private SelectList PaymentSelectList => new(_selectItems.PaymentSelect(), "Value", "Data");

        private string UserName => User.Identity!.Name!;
        public PaymentController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _logService = logService;
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.Payment = _context.PaymentCode.ToList();
            return View();

        }
        public IActionResult Detail(int id)
        {
            ViewBag.Id = id;
            ViewBag.Name = _context.PaymentCode.Find(id)!.PaymentName;
            ViewBag.Payment = _context.Payment.Where(x => x.PaymentCodeId == id).ToList();
            ViewBag.PaymentTypeSelect = PaymentSelectList;
            ViewBag.PaymentSelect = new SelectList(_context.PaymentCode.Where(x => x.Id == id), "Id", "PaymentName");
            return View();

        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult PaymentCodeAdd(PaymentCodeModel code)
        {
            _context.PaymentCode.Add(code);
            _context.SaveChanges();
            string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + code.PaymentCode + " koda sahip " + code.PaymentName + " ana ödenek kalemini ekledi.";
            _logService.LogForAdd(log);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(PaymentViewModel paymentView)
        {
            paymentView.UpdateDate = DateTime.Now;
            paymentView.UpdateUser = UserName;
            ViewBag.PaymentTypeSelect = PaymentSelectList;
            ViewBag.PaymentSelect = new SelectList(_context.PaymentCode, "Id", "PaymentName");
            try
            {
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + paymentView.PaymentCodeId + " kodlu kaleme " + paymentView.PaymentPrice + " miktarında " + paymentView.PaymentType + " ekledi.";
                _context.Payment.Add(_mapper.Map<PaymentModel>(paymentView));
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Ödenek Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["errror"] = "Ödenek Ekleme Sırasında Hata Oluştu.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Remove(PaymentViewModel paymentView)
        {
            try
            {
                var payment = _context.Payment.Find(paymentView.Id);
                string log = User.Identity!.Name + " isimli kullanıcı " + DateTime.Now + " tarihinde " + paymentView.PaymentCodeId + " kodlu kalemden " + paymentView.PaymentPrice + " miktarında " + paymentView.PaymentType + " sildi.";
                _context.Payment.Remove(payment!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Ödenek Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["errror"] = "Ödenek Silme Sırasında Hata Oluştu.";
                return RedirectToAction("Index");
            }

        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult PaymentCodeRemove(PaymentCodeModel paymentView)
        {
            try
            {
                var payment = _context.PaymentCode.Find(paymentView.Id);
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + paymentView.PaymentCode + " kodlu kalemden " + paymentView.PaymentName + " isimli ödenek kalemini sildi.";
                _context.PaymentCode.Remove(payment!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Ödenek Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["errror"] = "Ödenek Silme Sırasında Hata Oluştu.";
                return RedirectToAction("Index");
            }

        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {
            ViewBag.Payment = _context.PaymentCode.ToList();
            return View();
        }



    }
}