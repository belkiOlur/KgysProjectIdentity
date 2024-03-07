using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;

namespace KgysProjectIdentity.Service.Services
{
    public interface IWareHouseService
    {
        MaterialsDetailViewModel MaterialUpdate(MaterialsDetailViewModel materialUpdate, string UserName);
        MaterialsDetailViewModel MaterialAdd(MaterialsDetailViewModel materialAdd, string UserName);
        string MaterialDetailFirstLetterUpper(MaterialsDetailViewModel mAdd);
        string MaterialFirstLetterUpper(MaterialsModel mAdd);

    }
}
