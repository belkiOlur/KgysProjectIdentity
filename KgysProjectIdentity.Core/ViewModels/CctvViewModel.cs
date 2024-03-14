using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class CctvViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Boş Bıraklılamaz")]
        public string? ProjectName { get; set; }
        [Required(ErrorMessage = "Boş Bıraklılamaz")]
        public string? ProjectDistrict { get; set; }
        [Required(ErrorMessage = "Boş Bıraklılamaz")]
        public string? Unit { get; set; }
        public string? ExProjectName { get; set; }
        public string? ProjectReason { get; set; }
        public string? Status { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
