using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class KgysPlannedViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İlçe Boş Geçilemez.")]
        public string? District { get; set; }
        public string? Neighbourhood { get; set; }
        [Required(ErrorMessage = "Nokta Adı Boş Geçilemez.")]
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
        [Required(ErrorMessage = "Proje Boş Geçilemez.")]
        public string? ProjectName { get; set; }
        [Required(ErrorMessage = "Maximo Boş Geçilemez.")]
        public string? MaximoId { get; set; }
        public string? LastStatus { get; set; }
        public int? TelecomFoId { get; set; }
        public int? KgysRequestId { get; set; }
        public DateTime? DateOfPlanned { get; set; }
        public DateTime? DateOfActivated { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }
        [Range(37.870000, 39.270000, ErrorMessage = "Enlem 37.870000° N ile 39.270000° N arasında olmalıdır.")]
        [Remote(action: "CoordinateCheck", controller: "KgysRequested", AdditionalFields = "ProjectCoordinateY")]
        public decimal ProjectCoordinateX { get; set; }

        [Range(26.228000, 27.650000, ErrorMessage = "Boylam 26.228000,° E ile 27.650000° E arasında olmalıdır.")]
        [Remote(action: "CoordinateCheck", controller: "KgysRequested", AdditionalFields = "ProjectCoordinateX")]
        public decimal ProjectCoordinateY { get; set; }
    }
}

