using AutoMapper;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Core.ViewModels;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
namespace KgysProjectIdentity.Service.Services
{
    public class SpareMaterialsService : ISpareMaterialsService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private string log = "";
        private string donusVerisi = "";
        public SpareMaterialsService(AppDbContext context, IMapper mapper, ILogService logService, ILogDifferenceDetectService detect)
        {
            _context = context;
            _mapper = mapper;
            _logService = logService;
            _detect = detect;
        }
        public IEnumerable<SpareMaterialsModel> GetSpareMaterials()
        {
            try
            {
                var spareMaterials = _context.SpareMaterials;
                return spareMaterials;
            }
            catch
            {
                return Enumerable.Empty<SpareMaterialsModel>();
            }
        }
        public IEnumerable<SpareMaterialsModel> GetSpareMaterialsDetail(int id)
        {
            try
            {
                return _context.SpareMaterials.Where(x => x.SpareMaterialId == id)
                    .OrderByDescending(y=>y.UpdateDate)
                    .ThenBy(z => z.Properties)
                    .ThenBy(z=>z.MaterialDetails)
                    .ToList();
            }
            catch
            {
                return Enumerable.Empty<SpareMaterialsModel>();
            }
        }
        public string AddSpareMaterialDetail(SpareMaterialsViewModel spareMaterials, string UserName)
        {
            try
            {
                spareMaterials.Id=0;
                _context.SpareMaterials.Add(_mapper.Map<SpareMaterialsModel>(spareMaterials));
                _context.SaveChanges();
                donusVerisi = "Yedek Malzeme Başarıyla Eklendi.";
            }
            catch
            {
                donusVerisi = "Yedek Malzeme Eklenemedi.";
            }
            return donusVerisi;
        }
        public string RemoveSpareMaterialDetail(int id, string UserName)
        {
            try
            {
                var request = _context.SpareMaterials.Find(id);
                log = request!.MaterialDetails + " isimli yedek malzemeyi " + UserName + " kullanıcısı " + DateTime.Now + " tarihinde sildi.";
                _context.SpareMaterials.Remove(request!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                donusVerisi = "Yedek Malzeme Başarıyla Silindi.";
            }
            catch
            {
                donusVerisi = "Yedek Malzeme Silinemedi.";
            }
            return donusVerisi;
        }

        public List<SpareMaterialDefinationsModel> GetTopMaterials()
        {
            try
            {
                return _context.SpareMaterialDefinations.Where(x => x.Sorted == 1).OrderBy(y => y.SpareMaterialName).ToList();
            }
            catch
            {
                return new List<SpareMaterialDefinationsModel>();
            }
        }
        public string GetMiddleMaterialsName(int materialCode)
        {

            string name = "";
            try
            {
                var material = _context.SpareMaterialDefinations.Where(x => x.SpareMaterialCode == materialCode).OrderBy(y => y.SpareMaterialName);
                if (material.Count() != 0)
                {

                    foreach (var item in material)
                    {
                        name = name + item.SpareMaterialName + ", ";
                    }
                    if (name.Length > 100)
                    {
                        name = name.Substring(0, 97) + "...";
                    }
                    else
                    {
                        name = name.Substring(0, name.Length - 2);
                    }
                }
                else
                {
                    name = "İçerisine henüz ürün eklenmemiş.";
                }
                name = CapitalizeAfterComma(name.ToLower());
            }
            catch
            {
                name = "Ürün Alınamadı";
            }
            return name;
        }
        public string CapitalizeAfterComma(string input)
        {
            string donus = string.Join(", ", input
                .Split(',')
               .Select(word => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word.Trim())));
            return donus;
        }

        public string AddSpareMaterialDefinations(SpareMaterialDefinationsViewModel spareMaterialDefinations, string UserName)
        {
            try
            {
                if (spareMaterialDefinations.SpareMaterialCode == 0)
                {
                    spareMaterialDefinations.Sorted = 1;
                    spareMaterialDefinations.SpareMaterialCode = 0;
                }
                else
                {
                    spareMaterialDefinations.Sorted = 2;
                }
                _context.SpareMaterialDefinations.Add(_mapper.Map<SpareMaterialDefinationsModel>(spareMaterialDefinations));
                _context.SaveChanges();
                _logService.LogForAdd($"{UserName} tarafından {DateTime.Now} tarihinde {spareMaterialDefinations.SpareMaterialName} isimli Yedek Malzeme Başlığı Eklendi");
                donusVerisi = "Yedek Malzeme Başarıyla Eklendi.";
            }
            catch
            {
                donusVerisi = "Yedek Malzeme Eklenemedi.";

            }
            return donusVerisi;
        }

        public string RemoveSpareMaterialDefinations(int id, string UserName)
        {
            try
            {
                if (_context.SpareMaterialDefinations.Any(x => x.SpareMaterialCode == id) || _context.SpareMaterials.Any(x => x.SpareMaterialId == id))
                {
                    return "Bu Yedek Malzeme Başlığına Bağlı Yedek Malzemeler veya Talepler Bulunmaktadır. Önce Bu Yedek Malzemeleri veya Talepleri Siliniz.";
                }
                var request = _context.SpareMaterialDefinations.Find(id)!;
                log = $"{UserName} tarafından {DateTime.Now} tarihinde {request.SpareMaterialName} isimli Yedek Malzeme Başlığını Sildi";
                _context.SpareMaterialDefinations.Remove(request!);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                donusVerisi = "Yedek Malzeme Başarıyla Sildi.";
            }
            catch
            {
                donusVerisi = "Yedek Malzeme Silinemedi.";
            }
            return donusVerisi;
        }
        public string UpdateSpareMaterialDefinations(SpareMaterialDefinationsViewModel spareMaterials, string UserName)
        {
            try
            {
                var material = _context.SpareMaterialDefinations.Find(spareMaterials.Id)!;
                log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + material.SpareMaterialName + " isimli yedek malzeme ana başlığını " + spareMaterials.SpareMaterialName + " olarak güncelledi.";
                material.SpareMaterialName = spareMaterials.SpareMaterialName;
                _context.SpareMaterialDefinations.Update(material);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                donusVerisi = "Yedek Malzeme Başarıyla Güncellendi.";
            }
            catch
            {
                donusVerisi = "Yedek Malzeme Güncellenemedi.";
            }
            return donusVerisi;
        }

        public List<SpareMaterialDefinationsModel> GetMiddleMaterials(int id)
        {
            try
            {
                return _context.SpareMaterialDefinations.Where(x => x.SpareMaterialCode == id).OrderBy(y => y.SpareMaterialName).ToList();
            }
            catch
            {
                return new List<SpareMaterialDefinationsModel>();
            }
        }
        public int GetMaterialsProductCount(int Id)
        {
            var count = _context.SpareMaterials.Where(x => x.SpareMaterialId == Id).ToList();
            return (count.Count);
        }
        public List<SpareMaterialsModel> GettingMaterialsProduct(int Id)
        {
            List<SpareMaterialsModel> productGet = _context.SpareMaterials.Where(x => x.SpareMaterialId == Id)
                .OrderBy(x => x.SpareMaterialId)
                .ThenBy(x => x.Properties)
                .ThenBy(x => x.MaterialDetails)
                .ToList();
            return productGet;
        }
        public int GetMaterialsProductDetailCount(int properties, string materialDetails, int Id)
        {
            try
            {
                int pieces = 0;
                var material = _context.SpareMaterials.Where(x => x.SpareMaterialId == Id && x.Properties == properties && x.MaterialDetails == materialDetails).ToList();
                foreach (var item in material)
                {
                    if (item.RequestOrGet == 1)
                    {
                        pieces = pieces + item.Pieces;
                    }
                    else
                    {
                        pieces = pieces - item.Pieces;
                    }
                }
                if (pieces <= 0)
                {
                    pieces = 0;
                }
                return pieces;
            }
            catch
            {
                return 0;
            }
        }
        public int GetMaterialsCount(int Id)
        {
            try
            {
                int pieces = 0;
                var material = _context.SpareMaterials.Where(x => x.SpareMaterialId == Id).ToList();
                foreach (var item in material)
                {
                    if (item.RequestOrGet == 1)
                    {
                        pieces = pieces + item.Pieces;
                    }
                    else
                    {
                        pieces = pieces - item.Pieces;
                    }
                }
                if (pieces <= 0)
                {
                    pieces = 0;
                }
                return pieces;
            }
            catch
            {
                return 0;
            }
        }

        public string SpareMaterialDefinationsName(int id)
        {
            string name = "";
            try
            {
                name = _context.SpareMaterialDefinations.Find(id)!.SpareMaterialName!;
            }
            catch
            {
                name = "";
            }
            name = CapitalizeAfterComma(name.ToLower());
            return name;
        }

        public int GetSpareMatarialsCodeFromId(int id)
        {
            return _context.SpareMaterialDefinations.Find(id)!.SpareMaterialCode;
        }

        public SpareMaterialsModel GetSpareMaterialsById(int id)
        {
           return _context.SpareMaterials.Find(id)!;
        }

        public string UpdateSpareMaterialDetail(SpareMaterialsModel spareMaterials, string UserName)
        {
            try
            {
                log = UserName + " kullanıcısı " + DateTime.Now + " tarihinde " + _detect.SpareMaterialUpdate(spareMaterials);
                _context.SpareMaterials.Update(spareMaterials);
                _context.SaveChanges();
                _logService.LogForAdd(log);
                donusVerisi = "Yedek Malzeme Başarıyla Güncellendi.";
            }
            catch
            {
                donusVerisi = "Yedek Malzeme Güncellenemedi.";
            }
            return donusVerisi;
        }
    }
}
