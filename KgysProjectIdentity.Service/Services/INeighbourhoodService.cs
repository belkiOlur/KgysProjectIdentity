using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface INeighbourhoodService
    {
        string GetNeighbourNameById(int id);
        IEnumerable<NeighbourModel> GetNeighbourhootByDistrictId(int districtId);
    }
}
