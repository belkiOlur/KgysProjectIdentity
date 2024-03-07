using System.ComponentModel.DataAnnotations;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class TenderProjectsViewModel
    {
        public int Id { get; set; }
        public string? RequestEbysNo { get; set; }
        public string? WhoRequested { get; set; }
        public string? Evaluation { get; set; }
        public string? AnswerEbysNo { get; set; }
        public string? CommissionerConfirmation { get; set; }
        [Required]
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public string? Specification { get; set; }
        public string? SpecificationEbys { get; set; }
        public string? SpecificationApproval { get; set; }
        public string? UnitOfTender { get; set; }
        public string? SendOfTender { get; set; }
        public DateTime? TenderTime { get; set; }
        public string? WhoResponse { get; set; }
        public DateTime? DateOfStart { get; set; }
        public int TimeOfWork { get; set; }
        public string? Status { get; set; }
        public DateTime? DateOfEnd { get; set; }
        public string? WhoHasJob { get; set; }
        public string? PriceOfWork { get; set; }
        public string? PriceStatus { get; set; }
        public string? PriceEbysNo { get; set; }
        public List<string>? Officials { get; set; }
        public string? AdmissionCommissionEstablished { get; set; }
        public string? AdmissionCommissionEbys { get; set; }
        public List<string>? AdmissionCommission { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
