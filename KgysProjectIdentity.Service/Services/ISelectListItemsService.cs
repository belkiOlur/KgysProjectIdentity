using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Mvc;

namespace KgysProjectIdentity.Service.Services
{
    public interface ISelectListItemsService
    {
        List<StatusSelectList> AplicationSelect();
        List<StatusSelectList> StatusSelect();
        List<StatusSelectList> StatusEpmysSelect();
        List<StatusSelectList> ReportToEgmSelect();
        List<StatusSelectList> RequestSelect();
        List<StatusSelectList> RequestAddSelect();
        List<StatusSelectList> ParkSelect();
        List<StatusSelectList> KgysPtsSelect();
        List<StatusSelectList> EtmysSelect();
        List<StatusSelectList> PaymentSelect();
        List<StatusSelectList> TenderSelect();
        List<StatusSelectList> CctvReasonSelect();
        List<AppUser> Users();
        List<AppUser> CommissionUsers();
        List<AppUser> UsersReponse();
    }
}
