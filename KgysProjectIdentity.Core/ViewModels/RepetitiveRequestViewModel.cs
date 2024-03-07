using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class RepetitiveRequestViewModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        [Required(ErrorMessage = "Talep Tarihi Boş Geçilemez.")]
        public DateTime? RequestArraviedDate { get; set; }
        [Required(ErrorMessage = "Talep Eden Boş Geçilemez.")]
        public string? RequestedByWho { get; set; }
        public string? RequestedByWhoDetail { get; set; }
        public string? RequestedTelephoneNumber { get; set; }
        public string? RequestType { get; set; }
        public string? Explanation { get; set; }
        public string? CallBack { get; set; }
        public string? AnswerEbysNo { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
