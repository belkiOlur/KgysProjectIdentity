using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface IProjectService
    {
        KgysPlannedModel ModelUpdate(KgysPlannedModel model,string UserName);
        Task UpdateForStatusRequest(KgysPlannedModel model, string UserName);
        KgysPlannedViewModel ModelUpdateForAdd(KgysPlannedViewModel model, string UserName);
    }
}
