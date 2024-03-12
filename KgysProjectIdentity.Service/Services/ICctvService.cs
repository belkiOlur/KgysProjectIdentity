using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface ICctvService
    {
        bool ProjectAdd(CctvProjectViewModel project, string UserName);
        bool ProjectRemove(int id, string UserName);
        bool ProjectDetailAdd(CctvViewModel project, string UserName);
        bool ProjectDetailUpdate(CctvModel project, string UserName);
        bool ProjectDetailRemove(CctvModel project, string UserName);
        bool ProjectProductAdd(CctvProductsViewModel project, string UserName);
        bool ProjectProductUpdate(CctvProductsModel project, string UserName);
        bool ProjectProductRemove(CctvProductsModel project, string UserName);
    }
}
