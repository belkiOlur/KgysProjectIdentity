using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class AccidentKgysViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nokta Adı Boş Geçilemez.")]
        public string? AccidentKgysName { get; set; }
        [Required(ErrorMessage = "İlçe Adı Boş Geçilemez.")]
        public string? AccidentKgysDistrict { get; set; }
        public string? AccidenKgysType { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentInfo { get; set; }
        public string? AccidentEbysNo { get; set; }
        public string? AlcoholTest { get; set; }
        public string? AccidentReport { get; set; }
        public string? AccidentInsuranceName { get; set; }
        public string? AccidentPolicyNo { get; set; }
        public string? AccidentComplementInsuranceName { get; set; }
        public string? AccidentComplementPolicyNo { get; set; }
        public DateTime? AccidentRegistrationDate { get; set; }
        public string? AccidentRegistrationNo { get; set; }
        public string? AccidentRegistrationStatus { get; set; }
        public string? AccidentDamagePrice { get; set; }
        public string? AccidentExpertiseCompany { get; set; }
        public string? AccidentExpertiseTelephone { get; set; }
        public string? AccidentExpertiseEmail { get; set; }
        public string? AccidentExpertiseEmailSend { get; set; }
        public string? Explanation { get; set; }
        public string? PriceStatus { get; set; }
        public string? AmountPaid { get; set; }
        public string? RepairCompany { get; set; }
        public DateTime? RepairFinishDate { get; set; }
        public string? Status { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
