using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class SpareMaterialDefinationsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Malzeme Adı Boş Geçilemez.")]
        public string? SpareMaterialName { get; set; }
        [Required(ErrorMessage = "Malzeme Türü Boş Geçilemez.")]
        public int SpareMaterialCode { get; set; }
        public int Sorted { get; set; }
    }
}
