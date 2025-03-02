using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Repository.Models
{
    public class SpareMaterialsModel
    {
        public int Id { get; set; }
        public int SpareMaterialId { get; set; }
        public int Properties { get; set; }
        public string? MaterialDetails { get; set; }
        public int Pieces { get; set; }
        public string? Measurement { get; set; }
        public string? Descriptions { get; set; }
        public int RequestOrGet { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? EBYSNo { get; set; }
        public string? WhoWantIt { get; set; }
        public int TenderId { get; set; }   
    }
}
