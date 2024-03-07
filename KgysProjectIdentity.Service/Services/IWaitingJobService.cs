using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface IWaitingJobService
    {
        List<WaitingJobsModel> AssignJobNotFinished(List<OfficialsJobsModel> officialsAssingnedJobs);
        List<OfficialsJobsModel> UsersNotFinished(List<OfficialsJobsModel> officialsAssingnedJobs);
        List<WaitingJobsModel> AssignJobNotFinishedElse(List<OfficialsJobsModel> assignedJobs);
        List<OfficialsJobsModel> UsersNotFinishedElse(List<OfficialsJobsModel> assignedJobs);
        List<WaitingJobsModel> AssignJobFinished(List<OfficialsJobsModel> officialsAssingnedJobs);
        List<OfficialsJobsModel> UsersFinished(List<OfficialsJobsModel> officialsAssingnedJobs);
        List<WaitingJobsModel> AssignJobFinishedElse(List<OfficialsJobsModel> assignedJobs);
        List<OfficialsJobsModel> UsersFinishedElse(List<OfficialsJobsModel> assignedJobs);
    }
}
