using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class ProjectsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Boş Bırakılamaz")]
        public string? Project { get; set; }
        public int Year { get; set; }
    }
}
