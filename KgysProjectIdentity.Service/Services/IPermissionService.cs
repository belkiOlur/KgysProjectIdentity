using KgysProjectIdentity.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface IPermissionService
    {
        string UsersColor();
        string TextColor();
        Task PermissionAdd(CalenderViewModel permission, string userName);
        Task PermissionUpdate(CalenderViewModel permission, string userName);
        List<int> Years();
    }
}
