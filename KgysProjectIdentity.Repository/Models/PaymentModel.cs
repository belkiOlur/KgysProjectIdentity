namespace KgysProjectIdentity.Repository.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentEbysNumber { get; set; }
        public string? PaymentType { get; set; }
        public int PaymentPrice { get; set; }
        public int PaymentCodeId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateUser { get; set; }

    }
}
