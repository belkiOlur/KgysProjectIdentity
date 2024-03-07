using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.Identity;

namespace KgysProjectIdentity.Web.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>(); //Hataları liste halinde tutuyoruz
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {

                errors.Add(new() { Code = "PasswordNoContainUserName", Description = "Şifre Alanı Kullanıcı Adı İçeremez" });

            }
            if (password.ToLower().StartsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordNoContain1234", Description = "Şifre Alanı 1234 ile başlayamaz" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);

        }
    }
}
