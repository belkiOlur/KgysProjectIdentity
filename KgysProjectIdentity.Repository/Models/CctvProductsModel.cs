using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Repository.Models
{
    public class CctvProductsModel
    {
        public int Id { get; set; }
        public int DetailId { get; set; }
        public string? FloorOfSystem { get; set; }
        public string? ProductName { get; set; }
        public string? ProductModel { get; set; }
        public string? FloorOfProduct { get; set; }
        public string? PlannedPlace { get; set; }
        public int ProductPieces { get; set; }
    }
}
