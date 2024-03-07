using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace KgysProjectIdentity.Web.Controllers
{
    [Authorize(Roles = "Admin,KGYS,Planlama")]
    public class ProjectEightyImagedModelsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProjectEightyImagedModelsController(AppDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: ProjectEightyImagedModels
        public async Task<IActionResult> Index()
        {
            return _context.ProjectEightyImagedModel != null ?
                        View(await _context.ProjectEightyImagedModel.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.projectEightyImagedModel'  is null.");
        }

        // GET: ProjectEightyImagedModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectEightyImagedModel == null)
            {
                return NotFound();
            }

            var projectEightyImagedModel = await _context.ProjectEightyImagedModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectEightyImagedModel == null)
            {
                return NotFound();
            }

            return View(projectEightyImagedModel);
        }
        //************************************************************************************************************************************************
        public IActionResult AddPicture(int id)
        {
            ProImages vm = new();
            ViewBag.images = id;

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddPicture(ProImages vm)
        {
            var projectName = _context.KgysPlanned.Find(vm.ProjectId)!.ProjectName;
            var kgysName = _context.KgysPlanned.Find(vm.ProjectId)!.KgysName;
            foreach (var item in vm.Images!)
            {
                string log = User.Identity!.Name + " kullanıcısı " + projectName + kgysName + " noktası içerisine resim ekledi.";
                string stringFileName = UploadFile(item);

                _context.ProjectEightyImagedModel.Add(new ProjectEightyImagedModel() { PictureUrl = stringFileName, ProjectId = vm.ProjectId });
                _context.LogModel.Add(new LogModel { Log = log });

            }
            _context.SaveChanges();
            var projectId = _context.ProjectsModels.Where(x => x.Project == projectName).FirstOrDefault()!.Id;
            return RedirectToAction("Index", "Projects", new { id = projectId });
        }
        //************************************************************************************************************************************************
        private string UploadFile(IFormFile file)
        {
            string? fileName = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);
            }
            return fileName!;
        }

        // GET: ProjectEightyImagedModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectEightyImagedModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PictureUrl")] ProjectEightyImagedModel projectEightyImagedModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectEightyImagedModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projectEightyImagedModel);
        }

        // GET: ProjectEightyImagedModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjectEightyImagedModel == null)
            {
                return NotFound();
            }

            var projectEightyImagedModel = await _context.ProjectEightyImagedModel.FindAsync(id);
            if (projectEightyImagedModel == null)
            {
                return NotFound();
            }
            return View(projectEightyImagedModel);
        }

        // POST: ProjectEightyImagedModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PictureUrl")] ProjectEightyImagedModel projectEightyImagedModel)
        {
            if (id != projectEightyImagedModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectEightyImagedModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectEightyImagedModelExists(projectEightyImagedModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectEightyImagedModel);
        }

        // GET: ProjectEightyImagedModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectEightyImagedModel == null)
            {
                return NotFound();
            }

            var projectEightyImagedModel = await _context.ProjectEightyImagedModel
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.path = _context.ProjectEightyImagedModel.Find(id)!.PictureUrl;
            ViewBag.Id = _context.ProjectEightyImagedModel.Find(id)!.ProjectId;
            if (projectEightyImagedModel == null)
            {
                return NotFound();
            }

            return View(projectEightyImagedModel);
        }

        // POST: ProjectEightyImagedModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectEightyImagedModel == null)
            {
                return Problem("Entity set 'AppDbContext.projectEightyImagedModel'  is null.");
            }
            var projectEightyImagedModel = await _context.ProjectEightyImagedModel.FindAsync(id);
            int ProjectIds = _context.ProjectEightyImagedModel.Find(id)!.ProjectId;
            string deleteItem = _context.KgysPlanned.Find(ProjectIds)!.KgysName!;
            if (projectEightyImagedModel != null)
            {
                string log = User.Identity!.Name + " kullanıcı " + deleteItem + " isimli noktadan 1 adet resim sildi.";
                _context.ProjectEightyImagedModel.Remove(projectEightyImagedModel);
                _context.LogModel.Add(new LogModel { Log = log });
                _context.SaveChanges();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ViewPicture", "Projects", new { id = ProjectIds });
        }

        private bool ProjectEightyImagedModelExists(int id)
        {
            return (_context.ProjectEightyImagedModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
