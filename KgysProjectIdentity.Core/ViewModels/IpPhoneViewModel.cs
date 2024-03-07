using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class IpPhoneViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu Alanın Girilmesi Zorunludur.")]
        public string? Campus { get; set; }
        [Required(ErrorMessage = "Bu Alanın Girilmesi Zorunludur.")]
        public string? Unit { get; set; }
        public bool Central { get; set; }
        public string? CentralBrand { get; set; }
        public bool PoeSwitch { get; set; }
        public bool CableIsTrue { get; set; }
        [Required(ErrorMessage = "Bu Alanın Girilmesi Zorunludur.")]
        public int PhonePieces { get; set; }
        [Required(ErrorMessage = "Bu Alanın Girilmesi Zorunludur.")]
        public string? Priority { get; set; }
        public string? UpdatedPersonel { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
