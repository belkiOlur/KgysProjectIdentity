using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Repository.Models
{
    public class CctvModel
    {
        public int Id { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDistrict { get; set; }
        public string? Unit { get; set; }
        public string? ExProjectName { get; set; }
        public string? ProjectReason { get; set; }
        public string? Status { get; set; }
    }
}
