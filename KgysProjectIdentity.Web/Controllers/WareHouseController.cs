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
    public class WareHouseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly ISelectListItemsService _selectItems;
        private readonly IWareHouseService _wareHouseService;
        private SelectList EtmysSelectList => new(_selectItems.EtmysSelect(), "Value", "Data");
        private SelectList MaterialSelectList => new(_context.Materials.OrderBy(x => x.Material), "Id", "Material");
        private string UserName => User.Identity!.Name!;
        public WareHouseController(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ISelectListItemsService selectItems, ILogService logService, IWareHouseService wareHouseService)
        {
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _selectItems = selectItems;
            _logService = logService;
            _wareHouseService = wareHouseService;
        }

        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        public IActionResult Index()
        {
            ViewBag.Materials = _context.Materials.OrderBy(x => x.Material).ToList();
            return View();
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        public IActionResult MaterialsDetailIndex(int id)
        {
            var materials = _context.Materials.Find(id)!.Material;
            ViewBag.Materials = _context.MaterialsDetail.Where(x => x.Material == materials && x.Status == "Depoda").ToList();
            return View();
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        public IActionResult UsingMaterialsDetailIndex()
        {
            ViewBag.Materials = _context.MaterialsDetail.Where(x => x.Status == "Personel Kullanımında" || x.Status == "Sahada Kullanımda").OrderBy(x => x.Material).ToList();
            return View("MaterialsDetailIndex");
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        public IActionResult WaitingMaterialsDetailIndex()
        {
            ViewBag.Materials = _context.MaterialsDetail.Where(x => x.Status == "ETMYS Çıkış yap" || x.Status == "Hek Bekliyor" || x.Status == "Tamir Edilecek").OrderBy(x => x.Material).ToList();
            return View("MaterialsDetailIndex");
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        public IActionResult SendMaterialsDetailIndex()
        {
            ViewBag.Materials = _context.MaterialsDetail.Where(x => x.Status == "ETMYS Çıkış Yapıldı").OrderBy(x => x.Material).ToList();
            return View("MaterialsDetailIndex");
        }

        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        public IActionResult HekMaterialsDetailIndex()
        {
            ViewBag.Materials = _context.MaterialsDetail.Where(x => x.Status == "ETMYS Çıkış Yapıldı - HEK").OrderBy(x => x.Material).ToList();
            return View("MaterialsDetailIndex");
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult MaterialAdd(MaterialsModel mAdd)
        {
            try
            {
                string saveMaterial = _wareHouseService.MaterialFirstLetterUpper(mAdd);
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + saveMaterial + " ürününü ekledi";
                _context.Materials.Add(new MaterialsModel { Material = saveMaterial });
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Ürün Başarıyla Eklendi";
                return RedirectToAction("MaterialsDetailAdd");
            }
            catch
            {
                TempData["error"] = "Ürün Eklenirken Hata Oluştu.";
                return RedirectToAction("MaterialsDetailAdd");
            }
        }

        [HttpPost]
        public IActionResult MaterialProductAdd(MaterialsDetailViewModel mAdd)
        {
            try
            {
                string saveMaterial = _wareHouseService.MaterialDetailFirstLetterUpper(mAdd);
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + saveMaterial + "alt ürününü ekledi";
                _context.MaterialsProductsModels.Add(new MaterialsProductsModel { ProductName = saveMaterial, ProductId = Convert.ToInt32(mAdd.Material) });
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Alt Ürün Başarıyla Eklendi";
                return RedirectToAction("MaterialsDetailAdd");
            }
            catch
            {
                TempData["error"] = "Alt Ürün Eklenirken Hata Oluştu.";
                return RedirectToAction("MaterialsDetailAdd");
            }
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri,Silme")]
        [HttpPost]
        public IActionResult MaterialRemove(MaterialsModel mRemove)
        {
            try
            {

                int id = mRemove.Id;
                var materialRemove = _context.Materials.Find(id);
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + materialRemove!.Material + " ürününü sildi";
                _context.Materials.Remove(materialRemove!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Ürün Başarıyla Silindi";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "Ürün Silinirken Hata Oluştu.";
                return RedirectToAction("Index");
            }

        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult MaterialUpdate(MaterialsModel mUpdate)
        {
            var materialUpdate = _context.Materials.Find(mUpdate.Id);

            try
            {
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + materialUpdate!.Material + " ürününün ismini = " + mUpdate.Material + " olarak değiştirdi.";
                materialUpdate.Material = mUpdate.Material;
                _context.Materials.Update(materialUpdate);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "Ürün Güncellenirken Hata Oluştu.";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult MaterialsDetailAdd()
        {
            ViewBag.Material = MaterialSelectList;
            ViewBag.Status = EtmysSelectList;
            return View();
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult MaterialsDetailAdd(MaterialsDetailViewModel materialAdd)
        {
            try
            {

                materialAdd = _wareHouseService.MaterialAdd(materialAdd, UserName);
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + materialAdd.SerialNo + " seri numaralı " + materialAdd.MaterialBrand + " Marka / Modelli cihazı ekledi.";
                _context.MaterialsDetail.Add(_mapper.Map<MaterialsDetailModel>(materialAdd));
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Ürün Başarıyla Eklendi.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "Ürün Eklenirken Hata Oluştu.";
                ViewBag.Material = MaterialSelectList;
                ViewBag.Status = EtmysSelectList;
                return View();
            }
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri,Silme")]
        [HttpPost]
        public IActionResult MaterialsDetailRemove(MaterialsDetailViewModel materialDetail)
        {
            try
            {
                int id = materialDetail.Id;
                var materials = _context.MaterialsDetail.Find(id)!;
                string log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + materials.SerialNo + " seri numaralı " + materials.MaterialBrand + " Marka / Modelli cihazı sildi.";
                _context.MaterialsDetail.Remove(materials);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["status"] = "Ürün Başarıyla Silindi.";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "Silinecek Ürün Bulunamadı.";
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        [HttpGet]
        public IActionResult MaterialsDetailUpdate(int id)
        {
            ViewBag.Material = MaterialSelectList;
            ViewBag.Status = EtmysSelectList;
            var materials = _context.MaterialsDetail.Find(id);
            if (materials!.Material != null && materials!.Material != "")
            {
                materials!.Material = _context.Materials.Where(x => x.Material == materials!.Material).FirstOrDefault()!.Id.ToString();
            }
            return View(_mapper.Map<MaterialsDetailViewModel>(materials));
        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        [HttpPost]
        public IActionResult MaterialsDetailUpdate(MaterialsDetailViewModel materialUpdate)
        {
            int ids = Convert.ToInt32(materialUpdate.Material);
            try
            {
                materialUpdate = _wareHouseService.MaterialUpdate(materialUpdate, UserName);
                string log = _detect.MaterialsDetailDifference(materialUpdate) + " olacak şekilde güncelledi";
                _context.MaterialsDetail.Update(_mapper.Map<MaterialsDetailModel>(materialUpdate));
                _context.SaveChanges();
                _logService.LogForAdd(log);
                TempData["Status"] = "Ürün Başarıyla Güncellendi.";
                return RedirectToAction("MaterialsDetailIndex", "WareHouse", new { id = ids });
            }
            catch
            {
                TempData["error"] = "Ürün Güncellenirken Hata Oluştu.";
                return RedirectToAction("MaterialsDetailIndex", "WareHouse", new { id = ids });
            }

        }
        [Authorize(Roles = "Admin,Depo,PlanlamaAmiri")]
        public IActionResult CreateExcel()
        {
            ViewBag.Materials = _context.Materials.OrderBy(x => x.Material).ToList();
            return View();
        }

        public ActionResult GetProduct(int productId)
        {

            List<MaterialsProductsModel> getProduct = _context.MaterialsProductsModels.Where(k => k.ProductId == productId).OrderBy(z => z.ProductName).ToList();
            return Json(getProduct);
        }

    }
}
