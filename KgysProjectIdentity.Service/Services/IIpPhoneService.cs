using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface IIpPhoneService
    {
        bool Add(IpPhoneViewModel phone, string UserName);
        bool Update(IpPhoneModel phone, string UserName);
        bool Remove(int id, string UserName);
        bool Complete(int id, string UserName);
        bool ProjectAdd(PriorityForIpPhoneModel phone, string UserName);
        bool ProjectRemove(int id, string UserName);

    }
}
