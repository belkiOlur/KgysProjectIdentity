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
    public class WaitingJobsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private readonly IWaitingJobService _waitingJobs;
        private SelectList ReportEgmSelectList => new(_selectItems.ReportToEgmSelect(), "Value", "Data");
        private SelectList StatusSelectList => new(_selectItems.StatusSelect(), "Value", "Data");
        private SelectList DistrictSelectList => new(_context.DistrictModels, "Id", "districtName");
        private SelectList UsersSelectList => new(_context.UserAndValue.ToList(), "Value", "Name");
        private SelectList ResponseOfficials => new(_selectItems.UsersReponse(), "UserName", "FullName");
        private string UserName => User.Identity!.Name!;
        public WaitingJobsController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, ILogService logService, IWaitingJobService waitingJobs)
        {

            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _logService = logService;
            _waitingJobs = waitingJobs;
            if (!_context.WaitingJobs.Any())
            {
                _context.WaitingJobs.Add(new WaitingJobsModel() { EbysNumber = "20230305", Subject = "KGYS", District = "Bayraklı", Summary = "Özet", TalkToManager = "Hayır", Status = "Beklemede", StatusMessage = "Amir Talimatı", FinishOrNot = "Bitmedi", Answer = "Hayır", EBYSanswer = "205231", UpdatedUser = "349103", ArrivalDate = DateTime.Now, FinishDate = DateTime.Now, UpdatedTime = DateTime.Now, Material = "Demir", Discription = "Yok" });
                _context.SaveChanges();

            }
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.Jobs = _context.WaitingJobs.Where(x => x.Status != "Tamamlandı").ToList();
            ViewBag.Officials = ResponseOfficials;

            return View();
        }


        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult FinishWaitingJobsIndex()
        {
            ViewBag.Jobs = _context.WaitingJobs.Where(x => x.Status == "Tamamlandı").ToList();

            return View("Index");
        }

        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult AssignedJobsIndex()
        {
            if (User.HasClaim(x => x.Value == "PlanlamaAmiri"))
            {
                List<AppUser>? userNamee = new();
                if (User.HasClaim(x => x.Value == "PlanlamaAmiri"))
                {
                    userNamee = _selectItems.UsersReponse();
                }
                else
                {
                    userNamee.Add(_context.Users.Where(x => x.UserName == UserName).FirstOrDefault()!);
                }
                var officialsAssingnedJobs = _context.OfficialsJobs.ToList();
                ViewBag.OfficialsJob = officialsAssingnedJobs;
                ViewBag.UserName = userNamee;
                ViewBag.Jobs = _waitingJobs.AssignJobNotFinished(officialsAssingnedJobs);
                ViewBag.TenderProject = _context.TenderProjects.Where(x => x.Status != "Tamamlandı");
                return View();
            }
            else
            {
                List<AppUser>? userNamee = new();
                if (User.HasClaim(x => x.Value == "PlanlamaAmiri"))
                {
                    userNamee = _selectItems.UsersReponse();
                }
                else
                {
                    userNamee.Add(_context.Users.Where(x => x.UserName == UserName).FirstOrDefault()!);
                }
                var assignedJobs = _context.OfficialsJobs.Where(x => x.Name == UserName).ToList();
                ViewBag.OfficialsJob = assignedJobs;
                ViewBag.UserName = userNamee;
                ViewBag.Jobs = _waitingJobs.AssignJobNotFinishedElse(assignedJobs);
                ViewBag.TenderProject = _context.TenderProjects.Where(x => x.Status != "Tamamlandı");
                return View();
            }
        }
        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult FinishAssignedJobsIndex()
        {
            if (User.HasClaim(x => x.Value == "PlanlamaAmiri"))
            {
                var officialsAssingnedJobs = _context.OfficialsJobs.ToList();
                ViewBag.OfficialsJob = officialsAssingnedJobs;
                ViewBag.UserName = _selectItems.UsersReponse();
                ViewBag.Jobs = _waitingJobs.AssignJobFinished(officialsAssingnedJobs);
                ViewBag.TenderProject = _context.TenderProjects.Where(x => x.Status == "Tamamlandı");
                return View("AssignedJobsIndex");
            }
            else
            {
                var assignedJobs = _context.OfficialsJobs.Where(x => x.Name == UserName).ToList();
                ViewBag.OfficialsJob = assignedJobs;
                ViewBag.UserName = _selectItems.UsersReponse();
                ViewBag.Jobs = _waitingJobs.AssignJobFinishedElse(assignedJobs);

                ViewBag.TenderProject = _context.TenderProjects.Where(x => x.Status == "Tamamlandı");
                return View("AssignedJobsIndex");
            }
        }

        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult JobsIndex(int id)
        {
            ViewBag.Jobs = _context.WaitingJobs.Where(x => x.Id == id).ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Silme")]
        public IActionResult Remove(WaitingJobsViewModel jobsViewModel)
        {
            int id = jobsViewModel.Id;
            try
            {
                var jobs = _context.WaitingJobs.Find(id);
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + jobs!.Summary + " işini sildi.";
                _context.WaitingJobs.Remove(jobs!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                return RedirectToAction("Index");
            }
            catch
            {
                var jobs = _context.WaitingJobs.Find(id);
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + jobs!.Summary + " işini silerken beklenmeyen hata oluştu.";
                _logService.LogForAdd(log);
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.Officials = UsersSelectList;
            return View();
        }

        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(WaitingJobsViewModel jobs)
        {

            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.Officials = UsersSelectList;
            jobs.UpdatedTime = DateTime.Now;
            jobs.UpdatedUser = UserName;
            try
            {
                string log = _detect.WaitingJobsAdd(jobs);
                _context.WaitingJobs.Add(_mapper.Map<WaitingJobsModel>(jobs));
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Görev Başarıyla Eklendi";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + jobs.Summary + " işini eklerken beklenmeyen hata oluştu.";
                _logService.LogForAdd(logError);
                //Sahipsiz Validation yollamak için kullanılır
                ModelState.AddModelError(String.Empty, "Görev Kaydedilirken Hata Oluştu");
                return View();
            }

        }

        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var jobs = _context.WaitingJobs.Find(id);

            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.Officials = UsersSelectList;
            return View(_mapper.Map<WaitingJobsViewModel>(jobs));

        }

        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Update(WaitingJobsViewModel updateJobs)
        {
            updateJobs.UpdatedTime = DateTime.Now;
            updateJobs.UpdatedUser = User.Identity!.Name;
            try
            {
                string log = _detect.WaitingJobsUpdate(updateJobs);
                _context.WaitingJobs.Update(_mapper.Map<WaitingJobsModel>(updateJobs));
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Görev Başarıyla Güncellendi";
                return RedirectToAction("AssignedJobsIndex");
            }
            catch
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + updateJobs.Summary + " işini Güncellerken eksiklik nedeniyle hata oluştu.";
                _logService.LogForAdd(logError);
                ViewBag.DistrictSelect = DistrictSelectList;
                ViewBag.StatusSelect = StatusSelectList;
                ViewBag.ReportSelect = ReportEgmSelectList;
                ViewBag.Officials = UsersSelectList;
                TempData["error"] = "Görev Güncellenirken Hata ile Karşılaşıldı";
                return View();
            }

        }

        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult EbysUpdate(int id)
        {
            var jobs = _context.WaitingJobs.Find(id);
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.Officials = UsersSelectList;
            return View(_mapper.Map<WaitingJobsViewModel>(jobs));

        }


        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult EbysUpdate(WaitingJobsViewModel updateJobs)
        {
            var job = _context.WaitingJobs.Find(updateJobs.Id);
            try
            {
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + job!.Summary + " işine ait Ebys Numarasını" + job.EbysNumber + " = " + updateJobs.EbysNumber + "olacak şekilde güncelledi.";
                job.EbysNumber = updateJobs.EbysNumber;
                job.UpdatedTime = DateTime.Now;
                job.UpdatedUser = UserName;
                _context.WaitingJobs.Update(_mapper.Map<WaitingJobsModel>(job));
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Görev Başarıyla Güncellendi";
                return RedirectToAction("AssignedJobsIndex");
            }
            catch
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + updateJobs.Summary + " işinin EBYS numarasını güncellerken eksiklik nedeniyle hata oluştu.";
                _logService.LogForAdd(logError);
                ViewBag.DistrictSelect = DistrictSelectList;
                ViewBag.StatusSelect = StatusSelectList;
                ViewBag.ReportSelect = ReportEgmSelectList;
                ViewBag.Officials = UsersSelectList;
                return View();
            }
        }

        [Authorize(Roles = "Admin,Planlama,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {
            var jobs = _context.WaitingJobs.ToList();

            return View(_mapper.Map<List<WaitingJobsViewModel>>(jobs));
        }
        [Authorize(Roles = "Yönetici,PlanlamaAmiri")]
        public IActionResult Instructions(WaitingJobsViewModel inst)
        {
            try
            {
                var jobs = _context.WaitingJobs.Find(inst.Id);
                if (inst.StatusMessage != null)
                {

                    jobs!.StatusMessage = inst.StatusMessage;
                    jobs.TalkToManager = "Evet";
                    _context.WaitingJobs.Update(_mapper.Map<WaitingJobsModel>(jobs));
                }
                foreach (var official in inst.Officials!)
                {
                    var mission = _context.OfficialsJobs.Any(x => x.JobId == inst.Id && x.Name == official);
                    if (!mission)
                    {
                        _context.OfficialsJobs.Add(new OfficialsJobsModel { JobId = inst.Id, Name = official });
                    }

                }
                _context.SaveChanges();
                TempData["status"] = "Görev Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "Güncelleme Tamamlanamadı";
                return RedirectToAction("Index");
            }
        }

        [AcceptVerbs("GET", "POST")] //Clientta uzaktaki veri tabını kontrol etmek için kullanılır
        public IActionResult EBYSNumberCheck(string ebysNumber)
        {
            var anyProduct = _context.WaitingJobs.Any(x => x.EbysNumber == ebysNumber);
            if (anyProduct)
            {
                return Json("Kaydetmeye Çalıştığınız Başvuru Mevcuttur.");
            }
            else
            {

                return Json(true);
            }

        }
    }
}
