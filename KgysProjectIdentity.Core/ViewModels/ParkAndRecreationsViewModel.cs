namespace KgysProjectIdentity.Core.ViewModels
{
    public class ParkAndRecreationsViewModel
    {
        public int Id { get; set; }
        public string? ParkDistrict { get; set; }
        public string? ParkDistrictType { get; set; }
        public string? ParkNeighboor { get; set; }
        public string? ParkName { get; set; }
        public string? ParkAdress { get; set; }
        public string? ParkCoordinate { get; set; }
        public string? ParkCamerasSetup { get; set; }
        public string? ParkCamerasIsActive { get; set; }
        public DateTime? ParkCamerasActiveDate { get; set; }
        public int? ParkPoleCount { get; set; }
        public int? ParkTotalCameraCount { get; set; }
        public int? ParkFixedCameraCount { get; set; }
        public int? ParkDomeCameraCount { get; set; }
        public string? ParkNvrAdress { get; set; }
        public string? ParkLiveMonitoring { get; set; }
        public string? ParkExplain { get; set; }
        public string? ParkSupervisor { get; set; }
        public string? ParkSupervisorTel { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
