using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Repository.Models
{
    public class SpareMaterialDefinationsModel
    {
        public int Id { get; set; }
        public string? SpareMaterialName { get; set; }
        public int SpareMaterialCode { get; set; }
        public int Sorted { get; set; }
    }
}
