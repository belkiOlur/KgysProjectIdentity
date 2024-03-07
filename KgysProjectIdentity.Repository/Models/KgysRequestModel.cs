namespace KgysProjectIdentity.Repository.Models
{
    public class KgysRequestModel
    {
        public int Id { get; set; }
        public string? KgysName { get; set; }
        public string? District { get; set; }
        public string? Neighbourhood { get; set; }
        public string? RequestMethod { get; set; }
        public string? RequestMethodDetail { get; set; }
        public DateTime? RequestArraviedDate { get; set; }
        public string? RequestedByWho { get; set; }
        public string? RequestedByWhoDetail { get; set; }
        public string? RequestedTelephoneNumber { get; set; }
        public string? RequestAddress { get; set; }
        public string? RequestCoordinate { get; set; }
        public string? RequestEvaluation { get; set; }
        public string? RequestEvaluationDetail { get; set; }
        public string? RequestGoToDiscovery { get; set; }
        public string? RequestFirstDiscovery { get; set; }
        public string? RequestType { get; set; }
        public string? RequestAskToDistrict { get; set; }
        public string? RequestAskToDistrictEbysNumber { get; set; }
        public string? RequestAnswerFromDisctrictEbysNumber { get; set; }
        public int? RequestArrangementNumber { get; set; }
        public int? TelecomFoId { get; set; }
        public string? LastStatus { get; set; }
        public string? ProjectName { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }
        public decimal ProjectCoordinateX { get; set; }
        public decimal ProjectCoordinateY { get; set; }
    }
}
