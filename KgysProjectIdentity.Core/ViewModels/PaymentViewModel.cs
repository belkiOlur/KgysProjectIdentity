using System.ComponentModel.DataAnnotations;


namespace KgysProjectIdentity.Core.ViewModels
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ödenek Geliş - Harcama Tarihi Boş Geçilemez.")]
        public DateTime? PaymentDate { get; set; }
        [Required(ErrorMessage = "Ödenek Geliş - Harcama EBYS Numarası Boş Geçilemez.")]
        public string? PaymentEbysNumber { get; set; }
       
        [Required(ErrorMessage = "Ödenek Tipi Boş Geçilemez.")]
        public string? PaymentType { get; set; }
        [Required(ErrorMessage = "Gelen/Harcanan Ödenek Miktarı Boş Geçilemez.")]
        public int? PaymentPrice { get; set; }
        public int? PaymentCodeId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateUser { get; set; }

    }
}
