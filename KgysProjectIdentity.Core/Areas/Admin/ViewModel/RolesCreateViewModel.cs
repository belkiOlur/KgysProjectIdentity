using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.Areas.Admin.ViewModel
{
    public class RolesCreateViewModel
    {
        [StringLength(13, MinimumLength = 1, ErrorMessage = "En Fazla 13 Karakter Girebilirsiniz.")]
        [Required(ErrorMessage = "Rol Adı Boş Bırakılamaz.")]
        [Display(Name = "Rol Adı :")]
        public string? Name { get; set; }
    }
}
