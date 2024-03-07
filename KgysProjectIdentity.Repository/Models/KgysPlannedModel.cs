namespace KgysProjectIdentity.Repository.Models
{
    public class KgysPlannedModel
    {
        public int Id { get; set; }
        public string? District { get; set; }
        public string? Neighbourhood { get; set; }
        public string? KgysName { get; set; }
        public int? CameraCount { get; set; }
        public int? DomeCameraCount { get; set; }
        public int? PtsCameraCount { get; set; }
        public string? RequestType { get; set; }
        public string? ProjectCoordinate { get; set; }
        public int? ProjectYear { get; set; }
        public string? ProjectProtocol { get; set; }
        public string? ProjectExcavation { get; set; }
        public string? ProjectBasis { get; set; }
        public string? ProjectPole { get; set; }
        public string? ProjectCabinet { get; set; }
        public string? ProjectAssembly { get; set; }
        public string? ProjectEnergyCable { get; set; }
        public string? ProjectEnergyConnect { get; set; }
        public string? ProjectFiberOptic { get; set; }
        public string? ProjectConnection { get; set; }
        public string? ProjectRecording { get; set; }
        public string? ProjectDescription { get; set; }
        public string? ProjectTeam { get; set; }
        public string? ProjectStatus { get; set; }
        public string? ProjectName { get; set; }
        public string? MaximoId { get; set; }
        public string? LastStatus { get; set; }
        public int? TelecomFoId { get; set; }
        public int? KgysRequestId { get; set; }
        public DateTime? DateOfPlanned { get; set; }
        public DateTime? DateOfActivated { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }
        public decimal ProjectCoordinateX { get; set; }
        public decimal ProjectCoordinateY { get; set; }
    }
}
