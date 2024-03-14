using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;

namespace KgysProjectIdentity.Repository.Models
{
    public class GetProject
    {

        private readonly AppDbContext _context;


        public GetProject(AppDbContext context)
        {
            _context = context;

        }
        public List<ProjectsModel> GettingProjects()
        {
            List<ProjectsModel> projects = _context.ProjectsModels.ToList();
            return projects;
        }
        public List<CctvProjectModel> GettingCctvProjects()
        {
            List<CctvProjectModel> cctvProjects = _context.CctvProjects.ToList();
            return cctvProjects;
        }
        public string GetOfficials(int id)
        {
            string officials = "";
            var getOfficial = _context.OfficialsJobs.Where(x => x.JobId == id).ToList();
            foreach (var item in getOfficial)
            {
                string userName = _context.Users.Where(x => x.UserName == item.Name).FirstOrDefault()!.FullName!; ;
                officials += userName + ", ";
            }
            return officials;
        }
        public string GetPaymentCalculate(int id)
        {
            int payment = 0;
            var getPayment = _context.Payment.Where(x => x.PaymentCodeId == id).ToList();
            foreach (var item in getPayment)
            {
                if (item.PaymentType == "Gelen Ödenek")
                {
                    payment += item.PaymentPrice;
                }
                if (item.PaymentType == "Harcama")
                {
                    payment -= item.PaymentPrice;
                }

            }
            return Convert.ToString(payment) + " TL";
        }
        public int GetRepetitiveCount(int id)
        {
            int repetitiveCount = 0;
            var repetitive = _context.RepetitiveRequest.Where(x => x.RequestId == id).ToList();
            if (repetitive.Count != 0)
            {
                repetitiveCount = repetitive.Count;
            }

            return (repetitiveCount);
        }

        public string GetUserName(string value)
        {
            string name = _context.UserAndValue.Where(x => x.Value == value).FirstOrDefault()!.Name!; 
            System.Threading.Thread.Sleep(5);

            return (name);
        }
        public int GetMaterialsCount(string value)
        {
            var count = _context.MaterialsDetail.Where(x => x.Material == value && x.Status == "Depoda").ToList();
            return (count.Count);
        }
        public string GetMaterialsDetailCount(string value)
        {
            string explanation="";
            string change = "";
            string brand = "";
            int count = 0;
            var list = _context.MaterialsDetail.Where(x => x.MaterialBrand == value && x.Status == "Depoda").OrderBy(x => x.MaterialBrand).ToList();
            foreach (var item in list)
            {
                count++;
                if (change != item.Explanation)
                {
                    explanation += item.Explanation + ", ";
                }
                change = item.Explanation!;
                brand = item.MaterialBrand!;
            }
            string materialCount = count + " adet " +brand +" ("+ explanation+") ";
            return (materialCount);
        }
        public List<MaterialsDetailModel> GettingMaterialsDetail(string materials)
        {
            List<MaterialsDetailModel> materialGet = _context.MaterialsDetail.Where(x => x.Material == materials && x.Status == "Depoda").OrderBy(x => x.MaterialBrand).ToList();
            return materialGet;
        }
        public int GetMaterialsProductCount(string value)
        {
            var id = _context.Materials.Where(x => x.Material == value).FirstOrDefault()!.Id;

            var count = _context.MaterialsProductsModels.Where(x => x.ProductId == id).ToList();
            return (count.Count);
        }
        public List<MaterialsProductsModel> GettingMaterialsProduct(string materials)
        {
            var id = _context.Materials.Where(x => x.Material == materials).FirstOrDefault()!.Id;
            List<MaterialsProductsModel> productGet = _context.MaterialsProductsModels.Where(x => x.ProductId == id).OrderBy(x => x.ProductName).ToList();
            return productGet;
        }
        public int GetMaterialsProductDetailCount(string value,string material)
        {
            try
            {
                var count = _context.MaterialsDetail.Where(x => x.Product == value && x.Material==material &&x.Status == "Depoda").ToList();
                return (count.Count);
            }
            catch
            {
                return 1;
            }
        }
        public string ProductOfCctvName(int id)
        {
            string productName = _context.ProductsOfCctv.Find(id)!.ProductName!;
            return productName;
        }
        public int CctvEk1KameraCount(CctvEk1Model ek1)
        {
            int count = -1;
            if (ek1.KameraDirek3M != 0) { count++; }
            if (ek1.KameraDirek4M != 0) { count++; }
            if (ek1.KameraDirek5M != 0) { count++; }
            if (ek1.KameraDirek6M != 0) { count++; }
            if (ek1.KameraDirek7M != 0) { count++; }
            if (ek1.KameraDirek8M != 0) { count++; }
            if (ek1.KameraDirek9M != 0) { count++; }
            if (ek1.KameraDirek10M != 0) { count++; }          
            return count;
        }

    }

}
