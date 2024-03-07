using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class TelecomFoViewModel
    {
        public int Id { get; set; }
        public string? EbysNumber { get; set; }
        [Required(ErrorMessage = "Başvuru Tipi Boş Geçilemez.")]
        public string? Aplication { get; set; }
        [Required(ErrorMessage = "İlçe Adı Boş Geçilemez.")]
        public string? District { get; set; }
        [Required(ErrorMessage = "Nokta Adı Boş Geçilemez.")]
        public string? Name { get; set; }
        [Remote(action: "TemosNumberCheck", controller: "TelecomFo")]
        public string? TemosNumber { get; set; }
        [Range(1, 4096, ErrorMessage = "Vlan Numarası 1 ila 4096 Arasında olabilir.")]
        public int? Vlan { get; set; }
        public string? IpAddress { get; set; }
        [Required(ErrorMessage = "Nokta Koordinatı Boş Geçilemez.")]
        public string? Coordinate { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? SlaStartTime { get; set; }
        [Required(ErrorMessage = "EPMYS Boş Geçilemez.")]
        public string? EpymStatus { get; set; }
        [Required(ErrorMessage = "EOM Boş Geçilemez.")]
        public string? EomStatus { get; set; }
        [Required(ErrorMessage = "MPLS Durumu Boş Geçilemez.")]
        public string? MplsOperationStatus { get; set; }
        [Required(ErrorMessage = "MPLS NMS Durumu Boş Geçilemez.")]
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
        [Range(37.870000, 39.270000, ErrorMessage = "Enlem 37.870000° N ile 39.270000° N arasında olmalıdır.")]
        [Remote(action: "CoordinateCheck", controller: "KgysRequested", AdditionalFields = "ProjectCoordinateY")]
        public decimal ProjectCoordinateX { get; set; }

        [Range(26.228000, 27.650000, ErrorMessage = "Boylam 26.228000,° E ile 27.650000° E arasında olmalıdır.")]
        [Remote(action: "CoordinateCheck", controller: "KgysRequested", AdditionalFields = "ProjectCoordinateX")]
        public decimal ProjectCoordinateY { get; set; }
    }
}
