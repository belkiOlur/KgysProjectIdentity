using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface IAlertService
    {
        IEnumerable<AlertModel> AddGet();
        string AddPost(AlertViewModel jobs, string UserName);
        string Remove(AlertViewModel team,string UserName);
    }
}
