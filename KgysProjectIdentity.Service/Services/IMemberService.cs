using KgysProjectIdentity.Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace KgysProjectIdentity.Service.Services
{
    public interface IMemberService
    {
        Task<UseViewModel> GetUseViewModelByUserNameAsync(string userName);
        Task Logout();
        Task<bool> CheckPasswordAsync(string userName, string oldPassword);
        Task<(bool, IEnumerable<IdentityError>?)> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
        Task<UserEditViewModel> GetUserEditViewModelAsync(string id);
        Task<string> SecurityCodeCreate();
        Task<bool> EmailCheck(string email);
        Task SecurityCodeUsageCheck(string email);
        Task<bool> SecuritCodeSendAndSave(string email, string newCode);
        Task<string> NewPasswordCreate();
        Task<bool> SecurityCodeCheck(string code);
        Task<string> FindUser(string code);
        Task SecurityCodeUsage(string code);
        Task<bool> SecurityCodeConfirm(string code);
        Task<(bool, IEnumerable<IdentityError>?,string)> EditUser(UserEditViewModel request,string user);
        Task UserEditLogSave(string log);
    }
}
