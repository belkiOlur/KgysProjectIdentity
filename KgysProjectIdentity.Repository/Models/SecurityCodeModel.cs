namespace KgysProjectIdentity.Repository.Models
{
    public class SecurityCodeModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }

        public string? SecurityCode { get; set; }
        public string? Use { get; set; }
    }
}
