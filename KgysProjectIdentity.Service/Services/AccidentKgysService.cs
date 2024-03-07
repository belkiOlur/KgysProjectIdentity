using AutoMapper;
using Azure.Core;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class AccidentKgysService : IAccidentKgysService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AccidentKgysService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<AccidentKgysModel> GetAccident()
        {
            var accident = _context.AccidentKgys.Where(x => x.Status != "Tamamlandı");
            return accident;
        }

        public IEnumerable<AccidentKgysModel> GetFinishAccident()
        {
            var finishAccident = _context.AccidentKgys.Where(x => x.Status == "Tamamlandı");
            return finishAccident;
        }

        public IEnumerable<AccidentKgysModel> GetAccidentDetail(int id)
        {
            return _context.AccidentKgys.Where(x => x.Id == id).ToList();
        }

        public AccidentKgysViewModel ModelComplement(AccidentKgysViewModel accident, string UserName)
        {
            accident.UpdateDate = DateTime.Now;
            accident.UpdateUser = UserName;
            accident.AccidentKgysDistrict = _context.DistrictModels.Find(Convert.ToInt32(accident.AccidentKgysDistrict))!.districtName!;
            return accident;
        }

        public Task Add(AccidentKgysViewModel accident)
        {
            _context.AccidentKgys.Add(_mapper.Map<AccidentKgysModel>(accident));
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public string Remove(int id, string UserName)
        {
            var request = _context.AccidentKgys.Find(id);
            string log = request!.AccidentKgysName + " isimli kazalı noktayı " + UserName + " kullanıcısı " + DateTime.Now + " tarihinde sildi.";
            _context.AccidentKgys.Remove(request!);
            _context.SaveChanges();
            return log;
        }

        public AccidentKgysModel UpdateGET(int id)
        {
            var request = _context.AccidentKgys.Find(id);
            if (request!.AccidentKgysDistrict != "" && request!.AccidentKgysDistrict != null)
            {
                var changeDistrict = _context.DistrictModels.Where(x => x.districtName == request!.AccidentKgysDistrict).FirstOrDefault()!.Id.ToString();

                request!.AccidentKgysDistrict = changeDistrict;
            }
            return request;
        }

        public Task UpdatePOST(AccidentKgysViewModel accident)
        {
            _context.AccidentKgys.Update(_mapper.Map<AccidentKgysModel>(accident));
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<AccidentKgysModel> Excel()
        {
            return _context.AccidentKgys.ToList();
        }
    }
}
