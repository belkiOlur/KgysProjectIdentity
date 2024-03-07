using Microsoft.AspNetCore.Mvc;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class CalenderViewModel
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public string? color { get; set; }
        public string? textColor { get; set; }
        public string? userName { get; set;}
    }
}
