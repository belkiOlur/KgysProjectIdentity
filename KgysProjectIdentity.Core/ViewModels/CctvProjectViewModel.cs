using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class CctvProjectViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş Bırakılamaz")]
        public string? ProjectName { get; set; }
        public int ProjectYear { get; set; }
    }
}
