namespace KgysProjectIdentity.Repository.Models
{
    public class IpPhoneModel
    {
        public int Id { get; set; }
        public string? Campus { get; set; }
        public string? Unit { get; set; }
        public bool Central { get; set; }
        public string? CentralBrand { get; set; }
        public bool PoeSwitch { get; set; }
        public bool CableIsTrue { get; set; }
        public int PhonePieces { get; set; }
        public string? Priority { get; set; }
        public string? UpdatedPersonel { get; set; }
        public DateTime? UpdatedTime { get; set; }
    }
}
