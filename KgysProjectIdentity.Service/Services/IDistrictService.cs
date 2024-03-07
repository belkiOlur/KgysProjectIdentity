using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface IDistrictService
    {
        IEnumerable<DistrictModel> GetDistrict();
        string GetDistrictNameById(int id);
        string GetDistrictId(string district);
        string GetDistrictType(string district);

    }
}
