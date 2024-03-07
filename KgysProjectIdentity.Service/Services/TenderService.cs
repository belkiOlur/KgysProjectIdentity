using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class TenderService : ITenderService
    {
        private readonly AppDbContext _context;

        public TenderService(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAdmissionCommissionOfficials(TenderProjectsViewModel tender, int id)
        {
            foreach (var official in tender.AdmissionCommission!)
            {
                var mission = _context.TenderOfAdmissionCommissionOfficials.Any(x => x.TenderId == id && x.OfficialsName == official);
                if (!mission)
                {
                    _context.TenderOfAdmissionCommissionOfficials.Add(new TenderOfAdmissionCommissionOfficialsModel { TenderId = id, OfficialsName = official });
                }
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AddSpecificationOfficials(TenderProjectsViewModel tender , int id)
        {
            foreach (var official in tender.Officials!)
            {
                var mission = _context.TenderOfSpecificationOfficials.Any(x => x.TenderId == id && x.OfficialName == official);
                if (!mission)
                {
                    _context.TenderOfSpecificationOfficials.Add(new TenderOfSpecificationOfficialsModel { TenderId = id, OfficialName = official });
                }
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task AdmissionCommissionOfficials(TenderProjectsViewModel updateTender)
        {
            foreach (var official in updateTender.AdmissionCommission!)
            {
                var specificationOfficial = _context.TenderOfAdmissionCommissionOfficials.Where(x => x.TenderId == updateTender.Id);
                foreach (var specifications in specificationOfficial)
                {
                    if (updateTender.AdmissionCommission.IndexOf(specifications.OfficialsName!) == -1)
                    {
                        _context.TenderOfAdmissionCommissionOfficials.Remove(specifications);
                    }
                }
                var mission = _context.TenderOfAdmissionCommissionOfficials.Any(x => x.TenderId == updateTender.Id && x.OfficialsName == official);
                if (!mission)
                {
                    _context.TenderOfAdmissionCommissionOfficials.Add(new TenderOfAdmissionCommissionOfficialsModel { TenderId = updateTender.Id, OfficialsName = official });
                }
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public int AdmissionCommissionOfficialsCount(string Name)
        {
            var officialCount = _context.TenderOfAdmissionCommissionOfficials.AsNoTracking().Where(x => x.OfficialsName == Name).ToList();
            return Convert.ToInt32(officialCount.Count);
        }

        public IQueryable<TenderOfAdmissionCommissionOfficialsModel> FindOfficialsAdmissionsCommissioner(string Name)
        {
            return (_context.TenderOfAdmissionCommissionOfficials.Where(x => x.OfficialsName == Name));
        }

        public string FindOfficialsFullNameById(string id)
        {
            var usersName = _context.Users.Find(id)!.FullName!;
            return (usersName);
        }

        public IQueryable<TenderOfSpecificationOfficialsModel> FindOfficialsSpecificationsCommissioner(string Name)
        {
            var spesifications = _context.TenderOfSpecificationOfficials.Where(x=>x.OfficialName==Name);
            return spesifications;
        }

        public string FindOfficialsUserNameById(string id)
        {

            var users = _context.Users.Find(id)!.UserName!;
            return (users);
            
        }

        public string FindTendersName(int TenderId)
        {
            try
            {
                var name = _context.TenderProjects.Find(TenderId)!.ProjectName!;
                return name;
            }
            catch 
            {
                return("Bu İhale Dosyası Silinmiş");
            }
        }

        public string FindUserFullNameByUserName(string Name)
        {
            return (_context.Users.Where(x => x.UserName == Name).FirstOrDefault()!.FullName!);
        }

        public Task SpecificationOfficials(TenderProjectsViewModel updateTender)
        {
            foreach (var official in updateTender.Officials!)
            {
                var specificationOfficial = _context.TenderOfSpecificationOfficials.Where(x => x.TenderId == updateTender.Id);
                foreach (var specification in specificationOfficial)
                {
                    if (updateTender.Officials.IndexOf(specification.OfficialName!) == -1)
                    {
                        _context.TenderOfSpecificationOfficials.Remove(specification);
                    }
                }
                var mission = _context.TenderOfSpecificationOfficials.Any(x => x.TenderId == updateTender.Id && x.OfficialName == official);
                if (!mission)
                {
                    _context.TenderOfSpecificationOfficials.Add(new TenderOfSpecificationOfficialsModel { TenderId = updateTender.Id, OfficialName = official });
                }
                
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public int SpecificationOfficialsCount(string Name)
        {
            var officialCount = _context.TenderOfSpecificationOfficials.AsNoTracking().Where(x => x.OfficialName == Name).ToList();
            return Convert.ToInt32(officialCount.Count);
        }

        public Task UpdateAdmissionCommissionOfficials(TenderProjectsViewModel updateTender)
        {
            foreach (var official in updateTender.AdmissionCommission!)
            {
                var specificationOfficial = _context.TenderOfAdmissionCommissionOfficials.Where(x => x.TenderId == updateTender.Id);
                foreach (var specifications in specificationOfficial)
                {
                    if (updateTender.AdmissionCommission.IndexOf(specifications.OfficialsName!) == -1)
                    {
                        _context.TenderOfAdmissionCommissionOfficials.Remove(specifications);
                    }
                }
                var mission = _context.TenderOfAdmissionCommissionOfficials.Any(x => x.TenderId == updateTender.Id && x.OfficialsName == official);
                if (!mission)
                {
                    _context.TenderOfAdmissionCommissionOfficials.Add(new TenderOfAdmissionCommissionOfficialsModel { TenderId = updateTender.Id, OfficialsName = official });
                }
                _context.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task UpdateSpecificationOfficials(TenderProjectsViewModel updateTender)
        {
            foreach (var official in updateTender.Officials!)
            {
                var specificationOfficial = _context.TenderOfSpecificationOfficials.Where(x => x.TenderId == updateTender.Id);
                foreach (var specification in specificationOfficial)
                {
                    if (updateTender.Officials.IndexOf(specification.OfficialName!) == -1)
                    {
                        _context.TenderOfSpecificationOfficials.Remove(specification);
                    }
                }
                var mission = _context.TenderOfSpecificationOfficials.Any(x => x.TenderId == updateTender.Id && x.OfficialName == official);
                if (!mission)
                {
                    _context.TenderOfSpecificationOfficials.Add(new TenderOfSpecificationOfficialsModel { TenderId = updateTender.Id, OfficialName = official });
                }
                
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
