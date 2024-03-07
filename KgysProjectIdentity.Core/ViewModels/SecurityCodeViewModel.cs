using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class SecurityCodeViewModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        
        public string? SecurityCode { get; set; }
        public string? Use { get; set; }
        [Remote(action: "CodeCheck", controller: "Member")]
        [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
        [Display(Name = "Güvenlik Kodu:")]
        public string PasswordOld { get; set; } = null!;


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
        [Display(Name = "Yeni Şifre :")]
        public string PasswordNew { get; set; } = null!;


        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew), ErrorMessage = "Şifreler Eşleşmiyor.")]
        [Required(ErrorMessage = "Şifre Onayı Boş Bırakılamaz.")]
        [Display(Name = "Yeni Şifre Tekrar :")]
        public string PasswordNewConfirm { get; set; } = null!;
    }
}
