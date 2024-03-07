using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class UserEditViewModel
    {
        public string? Id { get; set; }
        [StringLength(6, MinimumLength = 1, ErrorMessage = "En Fazla 6 Karakter Girebilirsiniz.")]
        [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı :")]
        public string? UserName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Geçerli Bir Email Adresi Giriniz ")]
        [Required(ErrorMessage = "Email Boş Bırakılamaz.")]
        [Display(Name = "Email :")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Ad - Soyad Boş Bırakılamaz.")]
        [Display(Name = "Ad - Soyad :")]
        public string? FullName { get; set; } = null!;
        public IFormFile? Picture { get; set; } = null;

    }
}
