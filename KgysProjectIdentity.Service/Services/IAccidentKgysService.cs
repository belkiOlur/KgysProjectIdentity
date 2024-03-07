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
    public interface IAccidentKgysService
    {
        IEnumerable<AccidentKgysModel> GetAccident();
        IEnumerable<AccidentKgysModel> GetFinishAccident();

        IEnumerable<AccidentKgysModel> GetAccidentDetail(int id);
        AccidentKgysViewModel ModelComplement(AccidentKgysViewModel accident, string UserName);
        Task Add(AccidentKgysViewModel accident);
        public string Remove(int id, string UserName);
        AccidentKgysModel UpdateGET(int id);

        Task UpdatePOST(AccidentKgysViewModel accident);
        IEnumerable<AccidentKgysModel> Excel();

    }
}
