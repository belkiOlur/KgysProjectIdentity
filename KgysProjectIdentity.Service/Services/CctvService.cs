using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace KgysProjectIdentity.Service.Services
{
    public class CctvService : ICctvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _log;
        private readonly ILogDifferenceDetectService _detect;
        private GetProject _enjecte;
        public CctvService(AppDbContext context, IMapper mapper, ILogService log, ILogDifferenceDetectService detect, GetProject enjecte)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _detect = detect;
            _enjecte = enjecte;
        }

        public bool ProjectAdd(CctvProjectViewModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.ProjectName + " fazını ekledi.";
                _context.CctvProjects.Add(_mapper.Map<CctvProjectModel>(project));
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ProjectRemove(int id, string UserName)
        {
            try
            {
                var project = _context.CctvProjects.Find(id)!;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.ProjectName + " fazını sildi.";
                _context.CctvProjects.Remove(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ProjectDetailAdd(CctvViewModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.ProjectName + " fazına " + project.ExProjectName + " eski projesi olan " + project.ProjectDistrict + project.Unit + " birimine " + project.ProjectReason + " nedeni ile proje ekledi.";
                _context.CctvProjectDetail.Add(_mapper.Map<CctvModel>(project));
                _context.SaveChanges();
                _log.LogForAdd(log);
                var detailId = _context.CctvProjectDetail.OrderByDescending(x => x.Id).FirstOrDefault()!.Id;
                Ek1Add(detailId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectDetailRemove(CctvModel project, string UserName)
        {
            try
            {
                var detailId = project.Id;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.ProjectName + " fazına " + project.ExProjectName + " eski projesi olan " + project.ProjectDistrict + project.Unit + " biriminin " + project.ProjectReason + " nedenli projeyi sildi.";
                _context.CctvProjectDetail.Remove(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                Ek1Remove(detailId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectDetailUpdate(CctvModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + _detect.CctvProjectDetailUpdate(project);
                _context.CctvProjectDetail.Update(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectProductAdd(CctvProductsViewModel project, string UserName)
        {
            try
            {
                var detail = _context.CctvProjectDetail.Find(project.DetailId)!;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.FloorOfSystem + " katındaki sistem odasına kayıtlı " + project.ProductName + " malzemesini " + project.ProductModel + " model olmak üzere" + project.FloorOfProduct + " katta " + project.PlannedPlace + " içerisine " + project.ProductPieces + " adet olarak " + detail.ProjectName + " projesinde" + detail.ProjectDistrict + " ilçesi " + detail.Unit + " projesine ekledi.";
                _context.CctvProducts.Add(_mapper.Map<CctvProductsModel>(project));
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectProductUpdate(CctvProductsModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + _detect.CctvProjectProductUpdate(project);
                _context.CctvProducts.Update(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectProductRemove(CctvProductsModel project, string UserName)
        {
            try
            {
                var detail = _context.CctvProjectDetail.Find(project.DetailId)!;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.FloorOfSystem + " katındaki sistem odasına kayıtlı " + project.ProductName + " malzemesini " + project.ProductModel + " model olmak üzere" + project.FloorOfProduct + " katta " + project.PlannedPlace + " içerisinde bulunan" + project.ProductPieces + " adet olarak " + detail.ProjectName + " projesinde" + detail.ProjectDistrict + " ilçesi " + detail.Unit + " projesinedeki ürünü sildi.";
                _context.CctvProducts.Remove(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProductOfCctvAdd(CctvProductsViewModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde  CCTV Projeleri İçin" + project.ProductName + " malzemesini ekledi.";
                _context.ProductsOfCctv.Add(new ProductsOfCctvProjectModel { ProductName = project.ProductName });
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProductOfCctvRemove(CctvProductsViewModel project, string UserName)
        {
            try
            {

                var product = _context.ProductsOfCctv.Find(project.Id)!;
                var modelOfProduct = _context.ModelForCctv.Where(x => x.ProductId == project.Id);
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde  CCTV Projelerinde kullanılan" + product.ProductName + " malzemesini ve ";  
                _context.ProductsOfCctv.Remove(product);
                if (modelOfProduct != null)
                {
                    foreach (var item in modelOfProduct)
                    {
                        log += item.ProductsModel + ", ";
                        _context.ModelForCctv.Remove(item);
                    }
                }
                log += " modellerini sildi.";
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ModelsForCctvAdd(CctvProductsViewModel project, string UserName)
        {
            try
            {
                var productId = _context.ProductsOfCctv.AsNoTracking().FirstOrDefault(x => x.ProductName == project.ProductName)!.Id;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde  CCTV Projeleri İçin" + project.ProductName + " malzemesine " + project.ProductModel + " modelini ekledi.";
                _context.ModelForCctv.Add(new ModelForCctvProjectModel { ProductId = productId, ProductsModel = project.ProductModel });
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ModelsForCctvRemove(CctvProductsViewModel project, string UserName)
        {
            try
            {
                var modelOfProduct = _context.ModelForCctv.Find(project.Id)!;
                var product = _context.ProductsOfCctv.Find(modelOfProduct.ProductId)!;                
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde  CCTV Projelerinde kullanılan" + product.ProductName + " malzemesine ait "+ modelOfProduct.ProductsModel+" modelini sildi.";
                _context.ModelForCctv.Remove(modelOfProduct);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Task Ek1Add(int id)
        {
            _context.CctvEk1.Add(new CctvEk1Model { DetailId = id });
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        public Task Ek1Remove(int id)
        {
            var remove = _context.CctvEk1.Where(x => x.DetailId == id).FirstOrDefault()!;
            _context.CctvEk1.Remove(remove);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public bool CctvEk1Update(CctvEk1Model project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + _detect.CctvEk1Update(project);
                _context.CctvEk1.Update(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public byte[] CctvEk1CreateExcel(CctvEk1Model ek1, string projectName)
        {
            using (var package = new ExcelPackage())
            {
                int i = 1;
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Column(1).Style.Font.Bold = true; // A sütunu
                worksheet.Column(2).Style.Font.Bold = true; // B sütunu
                worksheet.Column(6).Style.Font.Bold = true; // F sütunu
                worksheet.Cells["A1:F1"].Merge = true;
                worksheet.Cells["A1"].Value = "Ek-1";
                worksheet.Cells["A1:F1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells["A2:F2"].Merge = true;
                worksheet.Cells["A2"].Value = $"İZMİR {projectName} ÇEVRE GÜVENLİK KAMERA SİSTEMİ TEKNİK ŞARTNAMESİ";
                worksheet.Cells["A2:F2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A3"].Value = "Sıra No";
                worksheet.Cells["B3"].Value = "Şartname Madde No";
                worksheet.Cells["C3:D3"].Merge = true;
                worksheet.Cells["C3:D3"].Value = "Donanım/Cihaz";
                worksheet.Cells["E3"].Value = "Birimi";
                worksheet.Cells["F3"].Value = "Miktar";
                worksheet.Cells["C1:E1"].Style.Font.Bold = true;
                worksheet.Cells["C2:E2"].Style.Font.Bold = true;
                worksheet.Cells["C3:E3"].Style.Font.Bold = true;
                worksheet.Cells[3, 1, 3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                int row = 4;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.1";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "KAMERA KONTROL/GÖRÜNTÜLEME VE YÖNETİM SİSTEMİ YAZILIMI";
                worksheet.Cells[row, 5].Value = "SET";
                worksheet.Cells[row, 6].Value = ek1.KameraYazilim;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.2";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "AĞ GÖRÜNTÜ KAYIT CİHAZI (NVR) TİP-1";
                worksheet.Cells[row, 5].Value = "SET";
                worksheet.Cells[row, 6].Value = ek1.NvrTip1;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.3";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "AĞ GÖRÜNTÜ KAYIT CİHAZI (NVR) TİP-2";
                worksheet.Cells[row, 5].Value = "SET";
                worksheet.Cells[row, 6].Value = ek1.NvrTip2;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.4";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "İÇ ORTAM SABİT DOME KAMERA TİP-1";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.DomeTip1;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.5";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "İÇ ORTAM SABİT DOME KAMERA (İFADE ALMA ODASI) TİP-2";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.DomeTip2;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.5.28";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "İFADE ALMA ODASI MİKROFONU";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.Mic;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.6";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "DIŞ ORTAM SABİT BULLET TİPİ KAMERA";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.DisBullet;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.7";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "HAREKETLİ (PTZ) KAMERA";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.PtzKamera;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.2.8";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "KAMERA KONTROL ÜNİTESİ";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.KameraKontrol;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.3";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "SAHA AĞ ANAHTARI";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.SahaSwitch;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.4";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "İÇ ORTAM AĞ ANAHTARI";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.IcSwitch;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.5";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "OPERATÖR BİLGİSAYARI ( İZLEME VE YÖNETİM MERKEZİ İÇİN ) TİP-1";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.PcTip1;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.6";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "OPERATÖR BİLGİSAYARI ( İZLEME NOKTASI İÇİN ) TİP-2";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.PcTip2;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.7";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "OPERATÖR BİLGİSAYARI MONİTÖRÜ";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.Monitor;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.8";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "ÇERÇEVESİZ DUVAR EKRAN";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.LedTv;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.9";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "DUVAR TİPİ RACK KABİN";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.DuvarKabin;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.10";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "RACK KABİN";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.RackKabin;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.11";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "SAHA DOLABI";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.SahaKabin;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.12.1";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "1 kVA KESİNTİSİZ GÜÇ KAYNAĞI (KGK)";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.KGK;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;

                worksheet.Cells[$"A{row}:A{row + 1}"].Merge = true;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[$"B{row}:B{row + 1}"].Merge = true;
                worksheet.Cells[row, 2].Value = "3.13.2";
                worksheet.Cells[$"C{row}:C{row + 1}"].Merge = true;
                worksheet.Cells[row, 3].Value = "CAT 6 UTP KABLO";
                worksheet.Cells[row, 4].Value = "İÇ ORTAM";
                worksheet.Cells[row + 1, 4].Value = "DIŞ ORTAM";
                worksheet.Cells[$"E{row}:E{row + 1}"].Merge = true;
                worksheet.Cells[row, 5].Value = "METRE";
                worksheet.Cells[row, 6].Value = ek1.Cat6Ic;
                worksheet.Cells[row + 1, 6].Value = ek1.Caat6Dis;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[(row + 1), 5, (row + 1), 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[(row + 1), 1, (row + 1), 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                row++;
                row++;
                i++;

                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.3";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "CAT6 PATCH PANEL";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.Cat6Panel;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.5";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "SİNGLE MOD FİBER OPTİK (F/O) KABLO";
                worksheet.Cells[row, 5].Value = "METRE";
                worksheet.Cells[row, 6].Value = ek1.FoSingle;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.6";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "FİBER PATCH PANEL";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.FoPanel;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.11";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "ENERJİ BESLEME KABLOLARI";
                worksheet.Cells[row, 5].Value = "METRE";
                worksheet.Cells[row, 6].Value = ek1.EnerjiKablo;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.12";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "DATA PRİZİ VE BAĞLANTI MODÜLLERİ";
                worksheet.Cells[row, 5].Value = "SET";
                worksheet.Cells[row, 6].Value = ek1.DataPriz;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.13";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "ENERJİ PRİZİ VE BAĞLANTI MODÜLLERİ";
                worksheet.Cells[row, 5].Value = "SET";
                worksheet.Cells[row, 6].Value = ek1.EnerjiPriz;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.14";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "KABLO KANALETLERİ";
                worksheet.Cells[row, 5].Value = "METRE";
                worksheet.Cells[row, 6].Value = ek1.Kanalet;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.15";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "METAL KABLO KANALI";
                worksheet.Cells[row, 5].Value = "METRE";
                worksheet.Cells[row, 6].Value = ek1.MetakKanal;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.16";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "ELEKTRİK PANOSU";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.EnerjiPano;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.13.17";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "ANAHTARLI OTOMATİK SİGORTA";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.OtoSigorta;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[$"A{row}:A{row + 1}"].Merge = true;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[$"B{row}:B{row + 1}"].Merge = true;
                worksheet.Cells[row, 2].Value = "3.14.2";
                worksheet.Cells[$"C{row}:C{row + 1}"].Merge = true;
                worksheet.Cells[row, 3].Value = "KAZI İŞLEMLERİ";
                worksheet.Cells[row, 4].Value = "TOPRAK";
                worksheet.Cells[row + 1, 4].Value = "ASFALT";
                worksheet.Cells[$"E{row}:E{row + 1}"].Merge = true;
                worksheet.Cells[row, 5].Value = "METRE";
                worksheet.Cells[row, 6].Value = ek1.KaziToprak;
                worksheet.Cells[row + 1, 6].Value = ek1.KaziAsfalt;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[(row + 1), 5, (row + 1), 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[(row + 1), 1, (row + 1), 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                row++;
                row++;
                i++;
                worksheet.Cells[$"A{row}:A{row + 1}"].Merge = true;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[$"B{row}:B{row + 1}"].Merge = true;
                worksheet.Cells[row, 2].Value = "3.14.3.1";
                worksheet.Cells[$"C{row}:C{row + 1}"].Merge = true;
                worksheet.Cells[row, 3].Value = "BORU";
                worksheet.Cells[row, 4].Value = "ENERJİ";
                worksheet.Cells[row + 1, 4].Value = "DATA";
                worksheet.Cells[$"E{row}:E{row + 1}"].Merge = true;
                worksheet.Cells[row, 5].Value = "METRE";
                worksheet.Cells[row, 6].Value = ek1.BoruEnerji;
                worksheet.Cells[row + 1, 6].Value = ek1.BoruData;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                worksheet.Cells[(row + 1), 5, (row + 1), 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[(row + 1), 1, (row + 1), 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                row++;
                row++;
                i++;
                int son = _enjecte.CctvEk1KameraCount(ek1);
                if (son == -1)
                {
                    son = 0;
                    worksheet.Cells[row, 1].Value = i;
                    worksheet.Cells[row, 2].Value = "3.15";
                    worksheet.Cells[row, 3].Value = "KAMERA DİREĞİ";
                    worksheet.Cells[row, 4].Value = "... METRE";
                    worksheet.Cells[row, 5].Value = "ADET";
                    worksheet.Cells[row, 6].Value = ek1.KameraDirek3M;
                    worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    row++;
                }
                worksheet.Cells[$"A{row}:A{row + son}"].Merge = true;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[$"B{row}:B{row + son}"].Merge = true;
                worksheet.Cells[row, 2].Value = "3.15";
                worksheet.Cells[$"C{row}:C{row + son}"].Merge = true;
                worksheet.Cells[row, 3].Value = "KAMERA DİREĞİ";
                worksheet.Cells[$"E{row}:E{row + son}"].Merge = true;
                worksheet.Cells[row, 5].Value = "ADET";
                if (ek1.KameraDirek3M != 0)
                {
                    worksheet.Cells[row, 4].Value = "3 METRE"; worksheet.Cells[row, 6].Value = ek1.KameraDirek3M; worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; row++;
                }
                if (ek1.KameraDirek4M != 0)
                {
                    worksheet.Cells[row, 4].Value = "4 METRE"; worksheet.Cells[row, 6].Value = ek1.KameraDirek4M; worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; row++;
                }
                if (ek1.KameraDirek5M != 0)
                {
                    worksheet.Cells[row, 4].Value = "5 METRE"; worksheet.Cells[row, 6].Value = ek1.KameraDirek5M; worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; row++;
                }
                if (ek1.KameraDirek6M != 0)
                {
                    worksheet.Cells[row, 4].Value = "6 METRE"; worksheet.Cells[row, 6].Value = ek1.KameraDirek6M; worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; row++;
                }
                if (ek1.KameraDirek7M != 0)
                {
                    worksheet.Cells[row, 4].Value = "7 METRE"; worksheet.Cells[row, 6].Value = ek1.KameraDirek7M; worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; row++;
                }
                if (ek1.KameraDirek8M != 0)
                {
                    worksheet.Cells[row, 4].Value = "8 METRE"; worksheet.Cells[row, 6].Value = ek1.KameraDirek8M; worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; row++;
                }
                if (ek1.KameraDirek9M != 0)
                {
                    worksheet.Cells[row, 4].Value = "9 METRE"; worksheet.Cells[row, 6].Value = ek1.KameraDirek9M; worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; row++;
                }
                if (ek1.KameraDirek10M != 0)
                {
                    worksheet.Cells[row, 4].Value = "10 METRE"; worksheet.Cells[row, 6].Value = ek1.KameraDirek10M; worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; worksheet.Cells[row, 1, row, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center; row++;
                }
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.16";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "UYARICI LEVHA";
                worksheet.Cells[row, 5].Value = "ADET";
                worksheet.Cells[row, 6].Value = ek1.UyariLevha;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                row++;
                i++;
                worksheet.Cells[row, 1].Value = i;
                worksheet.Cells[row, 2].Value = "3.17";
                worksheet.Cells[$"C{row}:D{row}"].Merge = true;
                worksheet.Cells[row, 3].Value = "SİSTEM PERFORMANSI ARTTIRIMI KAPSAMINDA YAPILACAK İŞ VE İŞLEMLER";
                worksheet.Cells[row, 5].Value = "SET";
                worksheet.Cells[row, 6].Value = ek1.SistemPerformans;
                worksheet.Cells[row, 1, row, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, 5, row, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                using (var range = worksheet.Cells[1, 1, row, 6])
                {
                    //worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Top.Color.SetColor(Color.Black);
                    range.Style.Border.Bottom.Color.SetColor(Color.Black);
                    range.Style.Border.Left.Color.SetColor(Color.Black);
                    range.Style.Border.Right.Color.SetColor(Color.Black);
                }
                for (int col = 2; col <= worksheet.Dimension.End.Column; col++)
                {
                    double maxLength = 0;
                    for (row = 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var cellLength = worksheet.Cells[row, col].Text.Length * 1.2; // Küçük bir katsayı ekleyerek daha iyi bir görsellik sağlayabilirsiniz
                        if (cellLength > maxLength)
                        {
                            maxLength = cellLength;
                        }
                    }
                    worksheet.Column(col).Width = maxLength;
                }
                package.Save();
                return package.GetAsByteArray();
            }
        }

        public bool CctvPictureRemove(CctvProjectPictureModel project, string UserName)
        {
            try
            {
                var detail = _context.CctvProjectDetail.Find(project.CctvDetailId)!;
                var picture = _context.CctvPictures.Find(project.Id)!;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + detail.ProjectName + " projesinde" + detail.ProjectDistrict + " ilçesi " + detail.Unit + " projesinedeki "+ picture.PictureUrl+" isimli resmi sildi.";
                _context.CctvPictures.Remove(picture);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
