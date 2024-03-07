namespace KgysProjectIdentity.Repository.Models
{
    public class RepetitiveRequestModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public DateTime? RequestArraviedDate { get; set; }
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
