using KgysProjectIdentity.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public interface ITelecomService
    {
        int GetTelecomIdByName(string name);
        string GetKuppaPrice(TelecomFoViewModel request);
    }
}
