using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KgysProjectIdentity.Service.Services
{
    public class AlertService : IAlertService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public AlertService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<AlertModel> AddGet()
        {
            var alert = _context.Alerts.ToList();
            return alert;
        }

        public string AddPost(AlertViewModel jobs,string UserName)
        {
            string log = jobs.Job + " isimli tekrarlı görevi " + UserName + " kullanıcısı " + DateTime.Now + " tarihinde ekledi.";
            _context.Alerts.Add(_mapper.Map<AlertModel>(jobs));
            _context.SaveChanges();
            return log;
        }

        public string Remove(AlertViewModel team,string UserName)
        {
            var id = team.Id;
            var product = _context.Alerts.Find(id);
            string log = product!.Job + " isimli tekrarlı görevi " + UserName + " kullanıcısı " + DateTime.Now + " tarihinde sildi.";
            _context.Alerts.Remove(product!);
            _context.SaveChanges();
            return log; ;
        }
    }
}
