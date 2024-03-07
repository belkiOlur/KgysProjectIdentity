using AutoMapper;
using KgysProjectIdentity.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class NeighbourhoodService : INeighbourhoodService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NeighbourhoodService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<NeighbourModel> GetNeighbourhootByDistrictId(int districtId)
        {
            var neighbourhood = _context.NeighbourModels.Where(x => x.districtId == districtId).ToList();
            return neighbourhood;
        }

        public string GetNeighbourNameById(int id)
        {
            var neighbourhood = _context.NeighbourModels.Find(id)!.neighbourName;
            return neighbourhood;
        }
    }
}
