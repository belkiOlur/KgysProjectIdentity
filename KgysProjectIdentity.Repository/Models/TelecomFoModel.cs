namespace KgysProjectIdentity.Repository.Models
{
    public class TelecomFoModel
    {
        public int Id { get; set; }
        public string? EbysNumber { get; set; }
        public string? Aplication { get; set; }
        public string? District { get; set; }
        public string? Name { get; set; }
        public string? TemosNumber { get; set; }
        public int? Vlan { get; set; }
        public string? IpAddress { get; set; }
        public string? Coordinate { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? SlaStartTime { get; set; }
        public string? EpymStatus { get; set; }
        public string? EomStatus { get; set; }
        public string? MplsOperationStatus { get; set; }
        public string? MplsNmsStatus { get; set; }
        public string? CheckEGM { get; set; }
        public string? ReportToEgm { get; set; }
        public string? Description { get; set; }
        public DateTime? KuppaDate { get; set; }
        public string? KuppaID { get; set; }
        public string? KuppaPStatus { get; set; }
        public int? KuppaDistance { get; set; }
        public string? KuppaPrice { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }
        public string? Team { get; set; }
        public decimal ProjectCoordinateX { get; set; }
        public decimal ProjectCoordinateY { get; set; }
    }
}
