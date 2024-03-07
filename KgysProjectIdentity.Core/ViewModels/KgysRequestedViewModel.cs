using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class KgysRequestedViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Yöntem Tipi Boş Geçilemez.")]
        public string? Method { get; set; }
        public string? MethodDetail { get; set; }
        public string? ArraviedDate { get; set; }
        public string? RequestedByWho { get; set; }
        public string? RequestedByWhoDetail { get; set; }
        public string? TelephoneNumber { get; set; }
        [Required(ErrorMessage = "İlçe Boş Geçilemez.")]
        public string? District { get; set; }
        [Required(ErrorMessage = "Mahalle / Semt Boş Geçilemez.")]
        public string? DistrictArea { get; set; }
        public string? Address { get; set; }
        public string? Coordinate { get; set; }
        public string? GoToDiscovery { get; set; }
        public string? FirstDiscovery { get; set; }
        public string? Evaluation { get; set; }
        public string? EvaluationDetail { get; set; }
        public string? Status { get; set; }
        public string? LastDiscovery { get; set; }
        public string? AskToDistrict { get; set; }
        public string? AskToDistrictEbysNumber { get; set; }
        public string? AnswerFromDisctrictEbysNumber { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? FinishedOrNot { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }


    }
}
