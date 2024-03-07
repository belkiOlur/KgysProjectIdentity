namespace KgysProjectIdentity.Repository.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public string? Log { get; set; }
        public DateTime DateTime { get; set; }= DateTime.Now;
    }
}
