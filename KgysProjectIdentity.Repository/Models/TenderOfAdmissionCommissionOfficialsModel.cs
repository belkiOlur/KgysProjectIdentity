using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Repository.Models
{
    public class TenderOfAdmissionCommissionOfficialsModel
    {
        public int Id { get; set; }
        public string? OfficialsName { get; set; }
        public int TenderId { get; set; }
    }
}
