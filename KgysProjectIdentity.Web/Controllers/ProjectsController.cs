using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private readonly IDistrictService _district;
        private readonly ITelecomService _telecom;
        private readonly IProjectService _project;
        private SelectList KgysPtsSelectList => new(_selectItems.KgysPtsSelect(), "Value", "Data");
        private SelectList ReportEgmSelectList => new(_selectItems.ReportToEgmSelect(), "Value", "Data");
        private SelectList StatusSelectList => new(_selectItems.StatusSelect(), "Value", "Data");
        private SelectList RequestSelectList => new(_selectItems.RequestSelect(), "Value", "Data");
        private SelectList DistrictSelectList => new(_context.DistrictModels, "Id", "districtName");
        private SelectList ProjectSelectList => new(_context.ProjectsModels, "Id", "Project");

        private string UserName => User.Identity!.Name!;
        public ProjectsController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, ILogService logService, IDistrictService district, ITelecomService telecom, IProjectService project)
        {
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _logService = logService;
            _district = district;
            _telecom = telecom;
            _project = project;
        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult Index(int id)
        {

            ViewBag.ProjectName = _context.ProjectsModels.Find(id)!.Project;
            ViewBag.ProjectId = id;
            var project = _context.ProjectsModels.Find(id)!.Project; ;
            ViewBag.Project = _context.KgysPlanned.Where(x => x.ProjectName == project).OrderBy(x => x.District).ToList();
            var control = _context.KgysPlanned.Where(x => x.ProjectName == project && x.LastStatus == "Planlanan").ToList();
            if (control == null || control.Count == 0)
            {
                return RedirectToAction("FinishProjectIndex", "Projects", new { id });
            }
            return View();
        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult FinishProjectIndex(int id)
        {
            ViewBag.ProjectName = _context.ProjectsModels.Find(id)!.Project;
            ViewBag.ProjectId = id;
            var project = _context.ProjectsModels.Find(id)!.Project; ;
            var projects = _context.KgysPlanned.Where(x => x.ProjectName == project && x.LastStatus != "Planlanan" && x.LastStatus != "Talep").OrderBy(x => x.District).ToList();
            return View(_mapper.Map<List<KgysPlannedViewModel>>(projects));
        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult ProjectAdd()
        {
            ViewBag.Projects = _context.ProjectsModels.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult ProjectAdd(ProjectsViewModel team)
        {
            ViewBag.Projects = _context.ProjectsModels.ToList();

            if (ModelState.IsValid)
            {

                try
                {
                    string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + team.Project + " isimli Projeyi ekledi.";
                    _context.ProjectsModels.Add(_mapper.Map<ProjectsModel>(team));
                    _context.SaveChanges();
                    _logService.LogForAdd(log);
                    TempData["status"] = "Başvuru Başarıyla Eklendi";
                    return RedirectToAction("ProjectAdd");
                }
                catch (Exception)
                {
                    string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + team.Project + " isimli Projeyi eklemeye çalışırken hata oluştu.";
                    //Sahipsiz Validation yollamak için kullanılır
                    ModelState.AddModelError(String.Empty, "Başvuru Kaydedilirken Hata Oluştu");
                    _logService.LogForAdd(logError);
                    return View();
                }

            }
            else
            {

                return View();
            }

        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult ProjectRemove(ProjectsViewModel team)
        {
            var id = team.Id;
            var product = _context.ProjectsModels.Find(id);
            string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + product!.Project + " isimli projeyi sildi.";
            _context.ProjectsModels.Remove(product!);
            _context.SaveChanges();
            _logService.LogForAdd(log);
            return RedirectToAction("ProjectAdd");
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.ProjectSelect = ProjectSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            var project = _context.KgysPlanned.Find(id);
            if (project!.District != "" && project!.District != null)
            {
                project!.District = _district.GetDistrictId(project.District);
            }

            if (project!.ProjectName != "" && project!.ProjectName != null)
            {
                project.ProjectName = _context.ProjectsModels.Where(x => x.Project == project.ProjectName)!.FirstOrDefault()!.Id.ToString();
            }
            return View(project);
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Update(KgysPlannedModel updateProject)
        {
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.ProjectSelect = ProjectSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            try
            {
                updateProject.TelecomFoId = _telecom.GetTelecomIdByName(updateProject.KgysName!);
            }
            catch { }
            updateProject = _project.ModelUpdate(updateProject, UserName);
            if (updateProject.LastStatus == "Talep")
            {
                var ids = _context.ProjectsModels.Where(x => x.Project == updateProject.ProjectName)!.FirstOrDefault()!.Id;
                if (updateProject.KgysRequestId != null && updateProject.TelecomFoId != null)
                {
                    _project.UpdateForStatusRequest(updateProject, UserName);
                    TempData["status"] = "Nokta Talep Durumuna Alındı";
                    return RedirectToAction("Index", "Projects", new { id = ids });

                }
                else
                {
                    TempData["Error"] = "Bu Nokta Talepten Planlanmadığından Geri Talep Durumuna Alınamaz";
                    return RedirectToAction("Index", "Projects", new { id = ids });
                }

            }
            if (updateProject.ProjectStatus == "Tamamlandı" || updateProject.LastStatus == "Mevcut")
            {
                updateProject.ProjectStatus = "Tamamlandı";
                updateProject.LastStatus = "Mevcut";
                updateProject.DateOfActivated = DateTime.Now;
            }
            if (!ModelState.IsValid)
            {
                var project = _context.KgysPlanned.Find(updateProject.Id);
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + project!.KgysName + " isimli noktayı güncellerken hata oluştu.";
                _logService.LogForAdd(logError);
                return View(_mapper.Map<KgysPlannedViewModel>(project));
            }

            string log = _detect.ProjectUptade(updateProject);
            _context.KgysPlanned.Update(_mapper.Map<KgysPlannedModel>(updateProject));
            _context.SaveChanges();
            _logService.LogForAdd(log);
            var ProjectId = _context.ProjectsModels.Where(x => x.Project == updateProject.ProjectName)!.FirstOrDefault()!.Id;
            TempData["status"] = "Nokta Başarıyla Güncellendi";
            return RedirectToAction("Index", "Projects", new { id = ProjectId });
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult CreateExcel(int id)
        {
            ViewBag.ProjectName = _context.ProjectsModels.Find(id)!.Project;
            ViewBag.ProjectId = id;
            var project = _context.ProjectsModels.Find(id)!.Project; ;
            ViewBag.Project = _context.KgysPlanned.Where(x => x.ProjectName == project).ToList();
            return View();
        }
        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult FinishedCreateExcel(int id)
        {
            ViewBag.ProjectName = _context.ProjectsModels.Find(id)!.Project;
            ViewBag.ProjectId = id;
            var project = _context.ProjectsModels.Find(id)!.Project; ;
            ViewBag.Project = _context.KgysPlanned.Where(x => x.ProjectName == project).ToList();
            return View();
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        public IActionResult ViewPicture(int id)
        {
            ViewBag.Id = id;
            ViewBag.Name = _context.KgysPlanned.Find(id)!.KgysName;
            ViewBag.ProjectName = _context.KgysPlanned.Find(id)!.ProjectName;
            var project = _context.ProjectEightyImagedModel.ToList();
            return View(_mapper.Map<List<ProjectEightyImagedModel>>(project));
        }

        [Authorize(Roles = "Admin,Silme")]
        public IActionResult Remove(KgysPlannedViewModel projectID)
        {
            var id = projectID.Id;
            var project = _context.KgysPlanned.Find(id)!;
            var ids = _context.ProjectsModels.Where(x => x.Project == project.ProjectName).FirstOrDefault()!.Id;
            try
            {
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + project!.KgysName + " isimli Planlanan noktayı sildi.";
                _context.KgysPlanned.Remove(project!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Nokta Başarıyla Silindi";
                return RedirectToAction("Index", "Projects", new { id = ids });
            }
            catch
            {
                string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + project!.KgysName + " isimli Planlanan noktayı silmeye çalışırken hata oluştu.";
                TempData["status"] = "Nokta Silinirken Hata Oluştu";
                _logService.LogForAdd(logError);
                return RedirectToAction("Index", "Projects", new { id = ids });
            }
        }

        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult Add(int id)
        {
            ViewBag.projectName = _context.ProjectsModels.Find(id)!.Project;
            ViewBag.projectId = id;
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.ProjectSelect = ProjectSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            return View();
        }


        [Authorize(Roles = "Admin,KGYS,Planlama,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult Add(KgysPlannedViewModel project)
        {
            ViewBag.DistrictSelect = DistrictSelectList;
            ViewBag.ProjectSelect = ProjectSelectList;
            ViewBag.RequestSelect = RequestSelectList;
            ViewBag.StatusSelect = StatusSelectList;
            ViewBag.ReportSelect = ReportEgmSelectList;
            ViewBag.KgysPtsSelect = KgysPtsSelectList;
            try
            {
                project.TelecomFoId = _telecom.GetTelecomIdByName(project.KgysName!); ;
            }
            catch { }
            var ids = Convert.ToInt32(project.ProjectName);
            _project.ModelUpdateForAdd(project, UserName);
            if (ModelState.IsValid)
            {
                try
                {
                    string log = _detect.ProjectAdd(project);
                    _context.KgysPlanned.Add(_mapper.Map<KgysPlannedModel>(project));
                    _context.SaveChanges();
                    _logService.LogForAdd(log);
                    TempData["status"] = "Nokta Başarıyla Eklendi";
                    return RedirectToAction("Index", "Projects", new { id = ids });
                }
                catch (Exception)
                {

                    string logError = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + project!.KgysName + " isimli Planlanan noktayı eklemeye çalışırken hata oluştu.";
                    //Sahipsiz Validation yollamak için kullanılır
                    _logService.LogForAdd(logError);
                    ModelState.AddModelError(String.Empty, "Nokta Kaydedilirken Hata Oluştu");
                    return View();
                }

            }
            else
            {

                return View();
            }

        }

    }
}
