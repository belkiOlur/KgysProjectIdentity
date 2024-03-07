using Microsoft.AspNetCore.Http;

namespace KgysProjectIdentity.Core.ViewModels
{
    public class ProImages
    {
        public List<IFormFile>? Images { get; set; }
        public int ProjectId { get; set; }
    }
}
