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
    public class KgysRequestedController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private readonly IKgysRequestedService _kgysRequest;
        private readonly IDistrictService _district;
        private readonly INeighbourhoodService _neighbourhood;
        private SelectList KgysPtsSelectList => new(_selectItems.KgysPtsSelect(), "Value", "Data");
        private SelectList ReportEgmSelectList => new(_selectItems.ReportToEgmSelect(), "Value", "Data");
        private SelectList StatusSelectList => new(_selectItems.StatusSelect(), "Value", "Data");
        private SelectList StatusSelectEpymsList => new(_selectItems.StatusEpmysSelect(), "Value", "Data");
        private SelectList RequestSelectList => new(_selectItems.RequestSelect(), "Value", "Data");
        private SelectList DistrictSelectList => new(_context.DistrictModels, "Id", "districtName");
        private SelectList ProjectSelectList => new(_context.ProjectsModels, "Id", "Project");
        private string UserName => User.Identity!.Name!;
        public KgysRequestedController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, IKgysRequestedService kgysRequest, IDistrictService district, INeighbourhoodService neighbourhood, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _kgysRequest = kgysRequest;
            _district = district;
            _neighbourhood = neighbourhood;
            if (!_context.KgysRequest.Any())
            {
                _context.KgysRequest.Add(new KgysRequestModel() { District = "Balçova", Neighbourhood = "Bahçelerarası Mahallesi", KgysName = "BLC001", RequestType = "KGYS", LastStatus = "Talep" });
                _context.SaveChanges();

            }
            if (!_context.KgysPlanned.Any())
            {
                _context.KgysPlanned.Add(new KgysPlannedModel() { District = "Balçova", KgysName = "BLC001", RequestType = "PTS", LastStatus = "Planlanan" });
                _context.KgysPlanned.Add(new KgysPlannedModel() { District = "Balçova", KgysName = "BLC001", RequestType = "KGYS", LastStatus = "Mevcut" });
                _context.SaveChanges();

            }
            _logService = logService;
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.District = _district.GetDistrict();
            ViewBag.KgysPlanned = _kgysRequest.GetPlanned();
            var request = _kgysRequest.GetRequest();
            return View(_mapper.Map<List<KgysRequestViewModel>>(request));

        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]

        public IActionResult KgysRequestIndex()
        {

            ViewBag.District = _district.GetDistrict();
            ViewBag.Request = _kgysRequest.GetOrderRequest();
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult DistrictRequestIndex(int id)
        {
            var district = _district.GetDistrictNameById(id);
            ViewBag.District = district;
            ViewBag.Id = id;
            ViewBag.Request = _kgysRequest.GetDistrictRequestStatusRequest(district);
            ViewBag.KgysPlanned = _kgysRequest.GetDistrictPlannedStatusNotRequest(district);
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult Neighbourhood(int id)
        {
            var district = _district.GetDistrictNameById(id);
            ViewBag.District = district;
            ViewBag.DistrictId = id;
            ViewBag.Neighbourhood = _neighbourhood.GetNeighbourhootByDistrictId(id);
            ViewBag.KgysPlanned = _kgysRequest.GetDistrictPlannedAll(district);
            var request = _kgysRequest.GetDistrictRequestAll(district);
            return View(_mapper.Map<List<KgysRequestViewModel>>(request));

        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult KGYS(int id)
        {
            var districtId = _context.NeighbourModels.Find(id)!.districtId;
            var district = _district.GetDistrictNameById(districtId);
            ViewBag.Id = id;
            ViewBag.District = district;
            ViewBag.Neighbourhood = _neighbourhood.GetNeighbourNameById(id);
            ViewBag.KgysPlanned = _kgysRequest.GetDistrictPlannedStatusNotRequest(district);
            ViewBag.Request = _kgysRequest.GetDistrictRequestStatusRequest(district);
            return View();

        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult KgysRequestDetails(int id)
        {
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            try
            {
                var requestId = _kgysRequest.GetRequestFindById(id);
                var planned = _kgysRequest.GetPlannedFindById(requestId!.Id);
                var repetitive = _kgysRequest.GetRepetitive(id).ToList();
                ViewBag.Request = _kgysRequest.GetRequestWhereId(id).ToList();
                ViewBag.Id = id;

                if (planned != null)
                {
                    ViewBag.Planned = planned;

                }
                else { ViewBag.Planned = null; }
                if (repetitive.Count != 0)
                {
                    ViewBag.Repetitive = repetitive;

                }
                else { ViewBag.Repetitive = null; }
                return View();
            }
            catch
            {

                TempData["status"] = "İstenilen Veri Bulunamadı";
                return View("Index");
            }

        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult KgysPlannedDetails(int id)
        {

            try
            {
                var requestId = _context.KgysPlanned.Find(id)!.KgysRequestId;
                if (requestId != null)
                {
                    ViewBag.Request = _kgysRequest.GetRequestWhereId((int)requestId).ToList();
                }
                else
                {
                    ViewBag.Request = null;
                }
                ViewBag.Planned = _kgysRequest.GetPlannedFindById(id);
                return View();
            }
            catch
            {
                ViewBag.Request = null;
                ViewBag.Planned = _kgysRequest.GetPlannedFindById(id);
                return View();
            }
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult BeforeKgysRequestIndex()
        {
            var request = _kgysRequest.GetBeforeRequestById();
            return View(_mapper.Map<List<KgysRequestedViewModel>>(request));
        }

        [Authorize(Roles = "Admin,Silme")]
        [HttpPost]
        public IActionResult Remove(KgysRequestViewModel requestId)
        {
            try
            {
                string log = _kgysRequest.Remove(requestId.Id, UserName);
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
        public IActionResult Add()
        {
            ViewBag.EpmysSelect = StatusSelectEpymsList;
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.District = DistrictSelectList;
            ViewBag.Project = ProjectSelectList;
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(KgysRequestViewModel request)
        {
            ViewBag.EpmysSelect = StatusSelectEpymsList;
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.District = DistrictSelectList;
            ViewBag.Project = ProjectSelectList;

            if (ModelState.IsValid)
            {
                try
                {
                    string log = _kgysRequest.Add(request, UserName);
                    _logService.LogForAdd(log);
                    TempData["status"] = "Talep Başarıyla Eklendi";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    string logError = UserName + " kullanıcısının " + request.UpdatedTime + "'de " + request.KgysName + " isimli talebi ekleme denemesi başarısız oldu.";
                    _logService.LogForAdd(logError);
                    //Sahipsiz Validation yollamak için kullanılır
                    ModelState.AddModelError(String.Empty, "Talep Kaydedilirken Hata Oluştu");
                    return View();
                }
            }
            else
            {

                return View();
            }
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var request = _kgysRequest.GetRequestFindById(id);
            if (request!.District != "" && request!.District != null)
            {
                var changeDistrict = _district.GetDistrictId(request.District);
                request!.District = changeDistrict;
            }
            ViewBag.EpmysSelect = StatusSelectEpymsList;
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.District = DistrictSelectList;
            ViewBag.Project = ProjectSelectList;
            return View(_mapper.Map<KgysRequestModel>(request));
        }


        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Update(KgysRequestViewModel updateRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EpmysSelect = StatusSelectEpymsList;
                ViewBag.KgysPtsSelect = KgysPtsSelectList;
                ViewBag.StatusSelect = StatusSelectList;
                ViewBag.RequestSelect = RequestSelectList;
                ViewBag.ReportSelect = ReportEgmSelectList;
                ViewBag.District = DistrictSelectList;
                ViewBag.Project = ProjectSelectList;
                string logError = UserName + " kullanıcısının " + DateTime.Now + "'de " + updateRequest.KgysName + " isimli talebi güncelleme denemesi başarısız oldu.";
                _logService.LogForAdd(logError);
                TempData["Error"] = "Güncelleme İşlemi Sırasında Sorun Oluştu";
                return RedirectToAction("Update", "KgysRequest", new { id = updateRequest.Id });
            }
            _kgysRequest.Update(updateRequest, UserName);
            TempData["status"] = "Talep Başarıyla Güncellendi";
            return RedirectToAction("KgysRequestIndex");
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult RepetitiveAdd(RepetitiveRequestViewModel repetitive)
        {

            repetitive.UpdatedTime = DateTime.Now;
            repetitive.UpdatedUser = UserName;
            int id = repetitive.RequestId;
            if (!ModelState.IsValid)
            {
                return RedirectToAction("KgysRequestDetails", "KgysRequested", new { id });
            }
            string log = UserName + " kullanıcı " + DateTime.Now + " tarihinde " + repetitive.RequestedByWho + " tarafından " + _context.KgysRequest.Find(repetitive.RequestId)!.KgysName + " talebine istenilen ek talebi ekledi.";
            _context.RepetitiveRequest.Add(_mapper.Map<RepetitiveRequestModel>(repetitive));
            _context.SaveChanges();
            _logService.LogForAdd(log);
            TempData["status"] = "Talep Başarıyla Güncellendi";
            return RedirectToAction("KgysRequestDetails", "KgysRequested", new { id });
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult RepetitiveUpdate(int id)
        {
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            var repetitive = _context.RepetitiveRequest.Find(id);
            return View(_mapper.Map<RepetitiveRequestModel>(repetitive));
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult RepetitiveUpdate(RepetitiveRequestModel repetitive)
        {

            repetitive.UpdatedTime = DateTime.Now;
            repetitive.UpdatedUser = UserName;
            int id = repetitive.RequestId;
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Talep Güncellenirken Sorun Oluştu.";
                return RedirectToAction("KgysRequestDetails", "KgysRequested", new { id });
            }
            string log = UserName + " kullanıcı " + DateTime.Now + " tarihinde " + repetitive.RequestedByWho + " tarafından " + _context.KgysRequest.Find(repetitive.RequestId)!.KgysName + " talebine istenilen ek talep güncellendi.";
            _context.RepetitiveRequest.Update(repetitive);
            _context.SaveChanges();
            _logService.LogForAdd(log);
            TempData["status"] = "Talep Başarıyla Güncellendi";
            return RedirectToAction("KgysRequestDetails", "KgysRequested", new { id });
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult RepetitiveRemove(RepetitiveRequestViewModel repetitive)
        {
            var repetitiveDelete = _context.RepetitiveRequest.Find(repetitive.Id)!;
            int id = repetitiveDelete.RequestId;
            try
            {

                string log = User.Identity!.Name + " kullanıcı " + DateTime.Now + " tarihinde " + repetitiveDelete.RequestedByWho + " tarafından " + _context.KgysRequest.Find(id)!.KgysName + " talebine eklenen ek talebi sildi.";
                _context.RepetitiveRequest.Remove(repetitiveDelete);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Talep Başarıyla Silindi";
                return RedirectToAction("KgysRequestDetails", "KgysRequested", new { id });
            }
            catch
            {
                TempData["error"] = "Talep Silinirken Hata Oluştu;";
                return RedirectToAction("KgysRequestDetails", "KgysRequested", new { id });
            }
        }

        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {
            ViewBag.District = _context.DistrictModels.ToList();
            ViewBag.KgysPlanned = _context.KgysPlanned.ToList();
            var request = _context.KgysRequest.ToList();
            return View(_mapper.Map<List<KgysRequestViewModel>>(request));
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult DistrictCreateExcel(int id)
        {
            var district = _district.GetDistrictNameById(id);
            ViewBag.DistrictName = district + " İlçesinin ";
            ViewBag.District = district;
            ViewBag.KgysPlanned = _kgysRequest.GetDistrictPlannedStatusNotRequest(district);
            ViewBag.Request = _kgysRequest.GetDistrictRequestStatusRequest(district);
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult NeighbourhoodCreateExcel(int id)
        {
            var district = _district.GetDistrictNameById(id);
            ViewBag.District = district;
            ViewBag.DistrictId = id;
            ViewBag.Neighbourhood = _neighbourhood.GetNeighbourhootByDistrictId(id);
            ViewBag.KgysPlanned = _kgysRequest.GetDistrictPlannedAll(district);
            var request = _kgysRequest.GetDistrictRequestAll(district);
            return View(_mapper.Map<List<KgysRequestViewModel>>(request));

        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult KgysCreateExcel(int id)
        {
            var districtId = _context.NeighbourModels.Find(id);
            var district = _context.DistrictModels.Find(districtId!.districtId)!.districtName;
            ViewBag.DistrictName = district + " İlçesi " + districtId.neighbourName + " mahallesi";
            ViewBag.District = district;
            ViewBag.KgysPlanned = _context.KgysPlanned.Where(x => x.LastStatus == "Mevcut" || x.LastStatus == "Planlanan" && x.District == district && x.Neighbourhood == districtId.neighbourName).ToList();
            ViewBag.Request = _context.KgysRequest.Where(x => x.LastStatus == "Talep" && x.District == district && x.Neighbourhood == districtId.neighbourName).ToList();
            return View();
        }
        [Authorize(Roles = "Admin,Planlama,KGYS,PlanlamaAmiri")]
        public IActionResult BeforeCreateExcel()
        {
            var request = _context.KgysRequested.ToList();

            return View(_mapper.Map<List<KgysRequestedViewModel>>(request));
        }


        public ActionResult GetNeighbour(int districtID)
        {

            List<NeighbourModel> getNeighbour = _context.NeighbourModels.Where(k => k.districtId == districtID).OrderBy(z => z.neighbourName).ToList();
            return Json(getNeighbour);
        }
        [AcceptVerbs ("GET", "POST")] //Clientta uzaktaki veri tabını kontrol etmek için kullanılır
        public IActionResult CoordinateCheck(decimal ProjectCoordinateY, decimal ProjectCoordinateX)
        {
            decimal fark = 0.000500m;
            var coordinatePlanned = _context.KgysPlanned;
            var coordinateRequest = _context.KgysRequest;
            foreach (var coordinateY in coordinatePlanned)
            {
                if (((ProjectCoordinateY - coordinateY.ProjectCoordinateY) <= fark) && ((ProjectCoordinateY-coordinateY.ProjectCoordinateY) >= -fark))
                {
                    if (((ProjectCoordinateX - coordinateY.ProjectCoordinateX) <= fark) && ((ProjectCoordinateX - coordinateY.ProjectCoordinateX) >= -fark))
                    {
                        return Json("Kaydetmeye Çalıştığınız Başvuru "+coordinateY.ProjectName+" İçerisinde "+coordinateY.KgysName+" İsimli Noktaya Çok Yakın.");
                    }
                }

            }
            foreach (var coordinateY in coordinateRequest)
            {
                if (((ProjectCoordinateY - coordinateY.ProjectCoordinateY) <= fark) && ((ProjectCoordinateY - coordinateY.ProjectCoordinateY) >= -fark))
                {
                    if (((ProjectCoordinateX - coordinateY.ProjectCoordinateX) <= fark) && ((ProjectCoordinateX - coordinateY.ProjectCoordinateX) >= -fark))
                    {
                        return Json("Kaydetmeye Çalıştığınız Başvuru "+coordinateY.District+" İlçesi Talepleri İçerisinde "+coordinateY.KgysName+" İsimli Talebe Çok Yakın.");
                    }
                }

            }
            return Json(true);
        }
    }
}