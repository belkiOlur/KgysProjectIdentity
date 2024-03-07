using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Bırakılamaz.")]
        [Display(Name = "Eski Şifre :")]
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
