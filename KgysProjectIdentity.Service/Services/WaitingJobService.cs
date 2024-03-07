using KgysProjectIdentity.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class WaitingJobService : IWaitingJobService
    {
        private readonly AppDbContext _context;

        public WaitingJobService(AppDbContext context)
        {
            _context = context;
        }

        public List<WaitingJobsModel> AssignJobNotFinishedElse(List<OfficialsJobsModel> assignedJobs)
        {
            List<WaitingJobsModel> assignJob = new();
            foreach (var job in assignedJobs)
            {
                var jobs = _context.WaitingJobs.FirstOrDefault(x => x.Id == job.JobId && x.Status != "Tamamlandı");
                if (jobs != null) { assignJob.Add(jobs); }
            }
            return assignJob;
        }

        public List<WaitingJobsModel> AssignJobNotFinished(List<OfficialsJobsModel> officialsAssingnedJobs)
        {
            List<WaitingJobsModel> assignJob = new();
            foreach (var job in officialsAssingnedJobs)
            {
                var jobs = _context.WaitingJobs.FirstOrDefault(x => x.Id == job.JobId && x.Status != "Tamamlandı");
                if (jobs != null && !assignJob.Contains(jobs)) { assignJob.Add(jobs); }
            }
            return assignJob;
        }

        public List<OfficialsJobsModel> UsersNotFinishedElse(List<OfficialsJobsModel> assignedJobs)
        {
            List<OfficialsJobsModel> users = new();
            foreach (var job in assignedJobs)
            {
                var user = _context.OfficialsJobs.FirstOrDefault(x => x.Name == job.Name);
                if (user != null && !users.Contains(user)) { users.Add(user); }
            }
            return users;
        }

        public List<OfficialsJobsModel> UsersNotFinished(List<OfficialsJobsModel> officialsAssingnedJobs)
        {
            List<OfficialsJobsModel> users = new();
            foreach (var job in officialsAssingnedJobs)
            {
                var user = _context.OfficialsJobs.FirstOrDefault(x => x.Name == job.Name);
                if (user != null && !users.Contains(user)) { users.Add(user); }
            }
            return users;
        }




        public List<WaitingJobsModel> AssignJobFinishedElse(List<OfficialsJobsModel> assignedJobs)
        {
            List<WaitingJobsModel> assignJob = new();
            foreach (var job in assignedJobs)
            {
                var jobs = _context.WaitingJobs.FirstOrDefault(x => x.Id == job.JobId && x.Status == "Tamamlandı");
                if (jobs != null) { assignJob.Add(jobs); }
            }
            return assignJob;
        }

        public List<WaitingJobsModel> AssignJobFinished(List<OfficialsJobsModel> officialsAssingnedJobs)
        {
            List<WaitingJobsModel> assignJob = new();
            foreach (var job in officialsAssingnedJobs)
            {
                var jobs = _context.WaitingJobs.FirstOrDefault(x => x.Id == job.JobId && x.Status == "Tamamlandı");
                if (jobs != null && !assignJob.Contains(jobs)) { assignJob.Add(jobs); }
            }
            return assignJob;
        }

        public List<OfficialsJobsModel> UsersFinishedElse(List<OfficialsJobsModel> assignedJobs)
        {
            List<OfficialsJobsModel> users = new();
            foreach (var job in assignedJobs)
            {
                var user = _context.OfficialsJobs.FirstOrDefault(x => x.Name == job.Name);
                if (user != null && !users.Contains(user)) { users.Add(user); }
            }
            return users;
        }

        public List<OfficialsJobsModel> UsersFinished(List<OfficialsJobsModel> officialsAssingnedJobs)
        {
            List<OfficialsJobsModel> users = new();
            foreach (var job in officialsAssingnedJobs)
            {
                var user = _context.OfficialsJobs.FirstOrDefault(x => x.Name == job.Name);
                if (user != null && !users.Contains(user)) { users.Add(user); }
            }
            return users;
        }
    }
}
