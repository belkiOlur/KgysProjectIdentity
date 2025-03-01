using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class SpareMaterialsViewModel
    {
        public int Id { get; set; }
        public int SpareMaterialId { get; set; }

        [Required(ErrorMessage = "Malzeme Detayı Boş Geçilemez.")]
        public int Properties { get; set; }
        [Required(ErrorMessage = "Malzeme Detay Türü Boş Geçilemez.")]
        public string? MaterialDetails { get; set; }
        [Required(ErrorMessage = "Malzeme Adeti Boş Geçilemez.")]
        public int Pieces { get; set; }
        [Required(ErrorMessage = "Malzeme Ölçütü Boş Geçilemez.")]
        public string? Measurement { get; set; }
        public int RequestOrGet { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateUser { get; set; }
    }
}
