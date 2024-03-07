using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;

namespace KgysProjectIdentity.Service.Services
{
    public interface ITenderService
    {
        Task AddSpecificationOfficials(TenderProjectsViewModel tender, int id);
        Task AddAdmissionCommissionOfficials(TenderProjectsViewModel tender, int id);
        Task UpdateSpecificationOfficials(TenderProjectsViewModel updateTender);
        Task UpdateAdmissionCommissionOfficials(TenderProjectsViewModel updateTender);
        int SpecificationOfficialsCount(string Name);
        int AdmissionCommissionOfficialsCount(string Name);
        string FindOfficialsFullNameById(string id);
        string FindOfficialsUserNameById(string id);
        IQueryable<TenderOfSpecificationOfficialsModel> FindOfficialsSpecificationsCommissioner(string Name);
        IQueryable<TenderOfAdmissionCommissionOfficialsModel> FindOfficialsAdmissionsCommissioner(string Name);
        string FindTendersName(int TenderId);
        string FindUserFullNameByUserName(string Name);
    }
}
