using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class MaterialsDetailViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Boş Geçilemez.")]
        public string? Material { get; set; }
        public string? Product { get; set; }
        public string? MaterialBrandCode { get; set; }
        [Required(ErrorMessage = " Boş Geçilemez.")]
        public string? MaterialBrand { get; set; }
        public string? TechnicalSpecification1 { get; set; }
        public string? SerialNo { get; set; }
        public string? Description { get; set; }
        public string? Explanation { get; set; }
        public string? Year { get; set; }
        public string? EtmysNumber { get; set; }
        public string? WhoTake { get; set; }
        public string? Status { get; set; }
        public string? Shelf { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? RemoveTime { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
