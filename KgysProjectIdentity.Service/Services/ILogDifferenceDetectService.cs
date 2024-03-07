using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface ILogDifferenceDetectService
    {
        string AccidentKgysAdd(AccidentKgysViewModel accidentKgysViewModel);
        string AccidentKgysDifference(AccidentKgysViewModel accidentKgysViewModel);
        string KgysRequestAdd(KgysRequestViewModel newRequest);
        string KgysRequestUpdate(KgysRequestViewModel newRequest);
        string ParksAndRecreationAdd(ParkAndRecreationsViewModel newRequest);
        string ParksAndRecreationUpdate(ParkAndRecreationsViewModel newRequest);
        string ProjectAdd(KgysPlannedViewModel newRequest);
        string ProjectUptade(KgysPlannedModel newRequest);
        string SecondaryRequestAdd(SecondaryRequestViewModel newRequest);
        string SecondaryRequestUpdate(SecondaryRequestViewModel newRequest);
        string TelecomFoAdd(TelecomFoViewModel newRequest);
        string TelecomFoUpdate(TelecomFoViewModel newRequest);
        string TenderAdd(TenderProjectsViewModel newRequest);
        string TenderUpdate(TenderProjectsViewModel newRequest);
        string WaitingJobsAdd(WaitingJobsViewModel newRequest);
        string WaitingJobsUpdate(WaitingJobsViewModel newRequest);
        string UserEdit(UserEditViewModel newRequest);
        string MaterialsDetailDifference(MaterialsDetailViewModel newRequest);
    }
}
