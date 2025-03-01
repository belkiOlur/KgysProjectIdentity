using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class SpareMaterialsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Malzeme Adı Boş Geçilemez.")]
        public int SpareMaterialId { get; set; }

        [Required(ErrorMessage = "Malzeme Detayı Boş Geçilemez.")]
        public int Properties { get; set; }
        [Required(ErrorMessage = "Malzeme Detay Türü Boş Geçilemez.")]
        public string? MaterialDetails { get; set; }
        [Required(ErrorMessage = "Malzeme Adeti Boş Geçilemez.")]
        public int Pieces { get; set; }
        [Required(ErrorMessage = "Malzeme Ölçütü Boş Geçilemez.")]
        public string? Measurement { get; set; }
        [Required(ErrorMessage = "Talep/Alım Boş Geçilemez.")]
        public int RequestOrGet { get; set; }
        [Required(ErrorMessage = "Talep Tarihi Boş Geçilemez.")]
        public DateTime? UpdateDate { get; set; }
        public string? EBYSNo { get; set; }
        public string? WhoWantIt { get; set; }
        public string? Descriptions { get; set; }
    }
}
