using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;

namespace KgysProjectIdentity.Service.Services
{
    public class WareHouseService : IWareHouseService
    {
        private readonly AppDbContext _context;

        public WareHouseService(AppDbContext context)
        {
            _context = context;
        }

        public MaterialsDetailViewModel MaterialAdd(MaterialsDetailViewModel materialAdd, string UserName)
        {
            materialAdd.UpdateTime = DateTime.Now;
            materialAdd.UpdateUser = UserName;
            materialAdd.AddTime = DateTime.Now;
            materialAdd.Material = _context.Materials.Find(Convert.ToInt32(materialAdd.Material))!.Material;
            if (materialAdd.Product != null)
            {
                materialAdd.Product = _context.MaterialsProductsModels.Find(Convert.ToInt32(materialAdd.Product))!.ProductName;
            }
            return materialAdd;
        }

        public string MaterialDetailFirstLetterUpper(MaterialsDetailViewModel mAdd)
        {
            string firstLetter = mAdd.Product!.Length > 0 ? mAdd.Product[0].ToString().ToUpper() : "";
            string restOfTheText = mAdd.Product.Length > 1 ? mAdd.Product.Substring(1).ToLower() : "";
            string word = firstLetter + restOfTheText;
            return word;
        }

        public string MaterialFirstLetterUpper(MaterialsModel mAdd)
        {
            string firstLetter = mAdd.Material!.Length > 0 ? mAdd.Material[0].ToString().ToUpper() : "";
            string restOfTheText = mAdd.Material.Length > 1 ? mAdd.Material.Substring(1).ToLower() : "";
            string word = firstLetter + restOfTheText;
            return word;
        }

        public MaterialsDetailViewModel MaterialUpdate(MaterialsDetailViewModel materialUpdate, string UserName)
        {
            var productName = _context.MaterialsDetail.AsNoTracking().Where(x => x.Id == materialUpdate.Id).FirstOrDefault()?.Product;
            materialUpdate.UpdateTime = DateTime.Now;
            materialUpdate.UpdateUser = UserName;
            if (materialUpdate.Status == "Personel Kullanımında" || materialUpdate.Status == "Sahada Kullanımda" || materialUpdate.Status == "ETMYS Çıkış Yapıldı" || materialUpdate.Status == "ETMYS Çıkış Yapıldı - HEK")
            {
                materialUpdate.RemoveTime = DateTime.Now;
            }
            if (materialUpdate.Material != null)
            {
                materialUpdate.Material = _context.Materials.Find(Convert.ToInt32(materialUpdate.Material))!.Material;
            }
            if (materialUpdate.Product != null && productName != materialUpdate.Product)
            {
                materialUpdate.Product = _context.MaterialsProductsModels.Find(Convert.ToInt32(materialUpdate.Product))!.ProductName;
            }
            return materialUpdate;
        }
    }
}
