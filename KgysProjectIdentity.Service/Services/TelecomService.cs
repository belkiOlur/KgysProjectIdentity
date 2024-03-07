using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class TelecomService : ITelecomService
    {
        private readonly AppDbContext _context;

        public TelecomService(AppDbContext context)
        {
            _context = context;
        }

        public string GetKuppaPrice(TelecomFoViewModel request)
        {
            string kuppaPrice;
            if (request.KuppaDistance > 2499 && (request.KuppaDistance != null || request.KuppaDate != null))
            {
                try
                {
                    var kuppaMultiplier = _context.KuppaMultipliers.AsNoTracking().Where(x => x.Year == Convert.ToInt32(request.KuppaDate.Value.Year)).FirstOrDefault()!.Multiplier;
                    kuppaPrice = Convert.ToString(request.KuppaDistance * kuppaMultiplier) + " TL + Vergiler";
                }
                catch
                {
                    var kuppaMultiplier = _context.KuppaMultipliers.OrderByDescending(x => x.Year).FirstOrDefault()!.Multiplier;
                    kuppaPrice = Convert.ToString(request.KuppaDistance * kuppaMultiplier) + " TL + Vergiler";
                }
            }
            else
            {
                if(request.KuppaDistance < 2499)
                {
                    kuppaPrice = " 0 TL";
                }
                else if (request.KuppaDistance > 2499 && request.KuppaDistance != null && request.KuppaDate == null)
                {
                    var kuppaMultiplier = _context.KuppaMultipliers.OrderByDescending(x => x.Year).FirstOrDefault()!.Multiplier;
                    kuppaPrice = Convert.ToString(request.KuppaDistance * kuppaMultiplier) + " TL + Vergiler";
                }
                else
                {
                    kuppaPrice = " 0 TL";
                }
            }
            return kuppaPrice;
        }

        public int GetTelecomIdByName(string name)
        {
            var id = _context.Products.Where(x => x.Name == name).FirstOrDefault()!.Id;
            return id;
        }
    }
}
