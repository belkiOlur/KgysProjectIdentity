using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class IpPhoneService:IIpPhoneService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _log;
        private readonly ILogDifferenceDetectService _detect;

        public IpPhoneService(AppDbContext context, IMapper mapper, ILogService log, ILogDifferenceDetectService detect)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _detect = detect;
        }

        public bool Add(IpPhoneViewModel phone, string UserName)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(int id, string UserName)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(IpPhoneModel phone, string UserName)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
