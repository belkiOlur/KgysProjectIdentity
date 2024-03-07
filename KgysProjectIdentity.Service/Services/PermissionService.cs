using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITenderService _tender;
        private readonly ILogService _logger;
        public PermissionService(ITenderService tender, AppDbContext context, IMapper mapper, ILogService logger)
        {
            _tender = tender;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public Task PermissionAdd(CalenderViewModel permission, string userName)
        {
            permission.title = _tender.FindUserFullNameByUserName(userName)+" İzin Planlaması";
            permission.color = UsersColor();
            permission.textColor = TextColor();
            permission.userName = userName;
            _context.Add(_mapper.Map<CalenderModel>(permission));
            _context.SaveChanges();
            string log = userName + " kullanıcısı " + permission.start!.Value.ToString("dd/MM/yyyy") + " tarihinden " + permission.end!.Value.ToString("dd/MM/yyyy") + " tarihine kadar izin planladı.";
            _logger.LogForAdd(log);
            return Task.CompletedTask;
            
        }

        public Task PermissionUpdate(CalenderViewModel permission, string userName)
        {
            var permissionOld = _context.PermissionCalender.AsNoTracking().Where(x => x.Id == permission.Id).FirstOrDefault();
            permissionOld!.start = permission.start;
            permissionOld!.end = permission.end;
            _context.PermissionCalender.Update(_mapper.Map<CalenderModel>(permissionOld));
            _context.SaveChanges();
            string log = userName + " kullanıcı" + permissionOld + " ait olan izni güncelledi.";
            _logger.LogForAdd(log);
            return Task.CompletedTask;
        }

        public string TextColor()
        {
            string textColor = "#000";
            return textColor;
        }

        public string UsersColor()
        {
            Random random = new();
            List<string> colors = new();
            colors.Add("#FFCDD2");
            colors.Add("#EF9A9A");
            colors.Add("#F44336");
            colors.Add("#F48FB1");
            colors.Add("#E91E63");
            colors.Add("#CE93D8");
            colors.Add("#E1BEE7");
            colors.Add("#E040FB");
            colors.Add("#9575CD");
            colors.Add("#9FA8DA");
            colors.Add("#C5CAE9");
            colors.Add("#8C9EFF");
            colors.Add("#BBDEFB");
            colors.Add("#2196F3");
            colors.Add("#18FFFF");
            colors.Add("#00E5FF");
            colors.Add("#A5D6A7");
            colors.Add("#69F0AE");
            colors.Add("#76FF03");
            colors.Add("#D4E157");
            colors.Add("#EEFF41");
            colors.Add("#F9A825");
            colors.Add("#FFFF00");
            colors.Add("#FFCC80");
            colors.Add("#FFE0B2");
            colors.Add("#FFAB91");
            colors.Add("#FF8A65");
            colors.Add("#BCAAA4");
            colors.Add("#D7CCC8");
            colors.Add("#BDBDBD");
            colors.Add("#F5F5F5");
            colors.Add("#BC8F8F");
            colors.Add("#FFDEAD");
            colors.Add("#BC8F8F");
            colors.Add("#D2691E");
            colors.Add("#F0F8FF");
            colors.Add("#FFE4E1");
            colors.Add("#778899");
            string color = colors[random.Next(0, colors.Count)];
            return color;
        }

        public List<int> Years()
        {
            var permissionYears = _context.PermissionCalender.OrderBy(x=>x.start );
            List<int> years = new();
            foreach(var item in permissionYears)
            {
                if (!years.Any(y=>y==item.start!.Value.Year))
                {
                    years.Add(item.start!.Value.Year);
                }
            }
            return years;
        }
    }
}
