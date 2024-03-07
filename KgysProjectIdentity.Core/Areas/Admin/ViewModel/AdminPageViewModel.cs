using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.Areas.Admin.ViewModel
{
    public class AdminPageViewModel
    {

        [Remote(action: "UserCheck", controller: "Home")]
        [StringLength(6, MinimumLength = 1, ErrorMessage = "En Fazla 6 Karakter Girebilirsiniz.")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı:")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Ad - Soyad Boş Bırakılamaz.")]
        [Display(Name = "Ad - Soyad :")]
        public string FullName { get; set; } = null!;

        [Remote(action: "UserCheck", controller: "Home")]
        [EmailAddress(ErrorMessage = "Geçerli Bir Email Adresi Giriniz ")]
        [Required(ErrorMessage = "Email Boş Bırakılamaz.")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password), ErrorMessage = "Şifreler Eşleşmiyor.")]
        [Required(ErrorMessage = "Şifre Onayı Boş Bırakılamaz.")]
        [Display(Name = "Şifre Tekrar :")]
        public string PasswordConfirm { get; set; } = null!;

    }
}
