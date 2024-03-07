using AutoMapper;
using Azure.Core;
using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DistrictService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string GetDistrictType(string district)
        {
            string type;
            if (district == "Balçova" || district == "Bayraklı" || district == "Bornova" || district == "Buca" || district == "Çiğli" || district == "Gaziemir" || district == "Güzelbahçe" || district == "Karabağlar" || district == "Karşıyaka" || district == "Konak" || district == "Narlıdere")
            {
                type = "Merkez";
            }
            else
            {
                type = "Dış";
            }
            return type;
        }

        public IEnumerable<DistrictModel> GetDistrict()
        {
            var district = _context.DistrictModels.ToList();
            return district;
        }

        public string GetDistrictId(string district)
        {
            var ids =_context.DistrictModels.Where(x => x.districtName == district).FirstOrDefault()!.Id.ToString();
            return ids;
        }

        public string GetDistrictNameById(int id)
        {
            var districtName=_context.DistrictModels.Find(id)!.districtName;
            return districtName;
        }
    }
}
