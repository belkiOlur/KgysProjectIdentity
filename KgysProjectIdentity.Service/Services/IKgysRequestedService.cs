using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;

namespace KgysProjectIdentity.Service.Services
{
    public interface IKgysRequestedService
    {
        IEnumerable<KgysRequestModel> GetRequest();
        KgysRequestModel GetRequestFindById(int id);
        public IEnumerable<KgysRequestModel> GetRequestWhereId(int id);
        IEnumerable<KgysRequestedModel> GetBeforeRequestById();
        IEnumerable<KgysPlannedModel> GetPlanned();
        IEnumerable<KgysPlannedModel> GetPlannedFindById(int id);
        IEnumerable<KgysRequestModel> GetOrderRequest();
        IEnumerable<KgysRequestModel> GetDistrictRequestStatusRequest(string district);
        IEnumerable<KgysPlannedModel> GetDistrictPlannedStatusNotRequest(string district);
        IEnumerable<KgysRequestModel> GetDistrictRequestAll(string district);
        IEnumerable<KgysPlannedModel> GetDistrictPlannedAll(string district);
        IEnumerable<RepetitiveRequestModel> GetRepetitive(int id);
        public string Remove(int id, string UserName);
        public string Add(KgysRequestViewModel request, string UserName);
        Task Update(KgysRequestViewModel updateRequest,string UserName);

    }
}
