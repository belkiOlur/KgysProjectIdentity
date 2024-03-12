using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class CctvProductsViewModel
    {
        public int Id { get; set; }
        public int DetailId { get; set; }
        [Required(ErrorMessage = "Boş Bıraklılamaz")]
        public string? FloorOfSystem { get; set; }
        [Required(ErrorMessage = "Boş Bıraklılamaz")]
        public string? ProductName { get; set; }
        public string? ProductModel { get; set; }
        [Required(ErrorMessage = "Boş Bıraklılamaz")]
        public string? FloorOfProduct { get; set; }
        public string? PlannedPlace { get; set; }
        [Required(ErrorMessage = "Boş Bıraklılamaz")]
        public int ProductPieces { get; set; }
    }
}
