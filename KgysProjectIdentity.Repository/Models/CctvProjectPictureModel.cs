using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Repository.Models
{
    public class CctvProjectPictureModel
    {
        public int Id { get; set; }
        public int CctvDetailId { get; set; }
        public string? PictureUrl { get; set; }
    }
}
