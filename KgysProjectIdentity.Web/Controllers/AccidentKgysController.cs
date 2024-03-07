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
    public class AccidentKgysController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private readonly IAccidentKgysService _accident;
        private SelectList KgysPtsSelectList => new(_selectItems.KgysPtsSelect(), "Value", "Data");
        private SelectList ReportEgmSelectList => new(_selectItems.ReportToEgmSelect(), "Value", "Data");
        private SelectList DistrictSelectList => new(_context.DistrictModels, "Id", "districtName");
        private SelectList ParkSelectList => new(_selectItems.ParkSelect(), "Value", "Data");
        private string UserName => User.Identity!.Name!;

        public AccidentKgysController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ILogService logService, ISelectListItemsService selectItems, IAccidentKgysService accident)
        {
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _logService = logService;
            _selectItems = selectItems;
            _accident = accident;
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.AccidentKgys = _accident.GetAccident();
            return View();

        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult FinishIndex()
        {
            ViewBag.AccidentKgys = _accident.GetFinishAccident();
            return View("Index");

        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult Accident(int id)
        {
            var accident = _accident.GetAccidentDetail(id);

            return View(_mapper.Map<List<AccidentKgysViewModel>>(accident));
        }


        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.ParksSelect = ParkSelectList;
            ViewBag.District = DistrictSelectList;
            return View();
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(AccidentKgysViewModel accident)
        {
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.ParksSelect = ParkSelectList;
            ViewBag.District = DistrictSelectList;

            accident = _accident.ModelComplement(accident, UserName);

            if (ModelState.IsValid)
            {
                try
                {
                    string log = _detect.AccidentKgysAdd(accident);
                    _accident.Add(accident);
                    _logService.LogForAdd(log);
                    TempData["status"] = "Kazalı Nokta Başarıyla Eklendi";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Kazalı Nokta Kaydedilirken Hata Oluştu");
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError(String.Empty, "Kazalı Nokta Kaydedilirken Tespit Edilemeyen Hata Oluştu");
                return View();
            }

        }

        [Authorize(Roles = "Admin,Silme")]
        [HttpPost]
        public IActionResult Remove(AccidentKgysViewModel requestId)
        {
            try
            {
                string log = _accident.Remove(requestId.Id, UserName);
                _logService.LogForAdd(log);
                TempData["status"] = "Silme İşlemi Tamamlandı";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["status"] = "Silme İşlemi Başarısız Oldu";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var request = _accident.UpdateGET(id);
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.ParksSelect = ParkSelectList;
            ViewBag.District = DistrictSelectList;
            return View(_mapper.Map<AccidentKgysModel>(request));
        }


        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Update(AccidentKgysViewModel updateRequest)
        {

            updateRequest = _accident.ModelComplement(updateRequest, UserName);

            if (!ModelState.IsValid)
            {

                ViewBag.KgysPtsSelect = KgysPtsSelectList;
                ViewBag.ReportSelect = ReportEgmSelectList;
                ViewBag.ParksSelect = ParkSelectList;
                ViewBag.District = DistrictSelectList;
                TempData["status"] = "Güncelleme İşlemi Sırasında Sorun Oluştu";
                return View();
            }
            string log = _detect.AccidentKgysDifference(updateRequest);
            _accident.UpdatePOST(updateRequest);
            _logService.LogForAdd(log);
            TempData["status"] = "Talep Başarıyla Güncellendi";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {
            ViewBag.AccidentKgys = _accident.Excel();
            return View();

        }

    }
}
