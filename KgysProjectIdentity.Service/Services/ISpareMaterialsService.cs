using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Core.ViewModels;

namespace KgysProjectIdentity.Service.Services
{
    public interface ISpareMaterialsService
    {
        IEnumerable<SpareMaterialsModel> GetSpareMaterials();
        IEnumerable<SpareMaterialsModel> GetSpareMaterialsDetail(int id);
        string AddSpareMaterialDefinations(SpareMaterialDefinationsViewModel spareMaterialDefinations, string UserName);
        string UpdateSpareMaterialDefinations(SpareMaterialDefinationsViewModel spareMaterials, string UserName);
        string RemoveSpareMaterialDefinations(int id, string UserName);

        string AddSpareMaterialDetail(SpareMaterialsViewModel spareMaterials, string UserName);
        string RemoveSpareMaterialDetail(int id, string UserName);
        List<SpareMaterialDefinationsModel> GetTopMaterials();
        string GetMiddleMaterialsName(int materialCode);
        List<SpareMaterialDefinationsModel> GetMiddleMaterials(int id);

    }
}
