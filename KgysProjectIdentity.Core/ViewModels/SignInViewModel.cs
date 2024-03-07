using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace KgysProjectIdentity.Core.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {

        }
        public SignInViewModel(string? name, string? password)
        {
            Name = name!;
            Password = password!;
        }
        [StringLength(6, MinimumLength = 1, ErrorMessage = "En Fazla 6 Karakter Girebilirsiniz.")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı :")]
        public string? Name { get; set; }

        [Remote(action: "UserCheck", controller: "Member")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
        [Display(Name = "Şifre :")]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
