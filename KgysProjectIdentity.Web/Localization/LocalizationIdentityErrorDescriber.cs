using Microsoft.AspNetCore.Identity;

namespace KgysProjectIdentity.Web.Localization
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {//hataların Türkçeleştirilmesi için kullanılır
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DuplicateUserName", Description = $"Bu {userName} daha önce başkası tarafından kullanılmıştır." };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description = $"Bu {email} daha önce başkası tarafından kullanılmıştır." };
        }
    }

}
