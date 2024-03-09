using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;

namespace KgysProjectIdentity.Service.Services
{
    public class IpPhoneService:IIpPhoneService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _log;
        private readonly ILogDifferenceDetectService _detect;

        public IpPhoneService(AppDbContext context, IMapper mapper, ILogService log, ILogDifferenceDetectService detect)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _detect = detect;
        }

        public bool Add(IpPhoneViewModel phone, string UserName)
        {
            try
            {
                var log = UserName + " isimli kullanıcı " + _detect.IpPhoneAdd(phone);
                phone.UpdatedPersonel = UserName;
                phone.UpdatedTime=DateTime.Now;
                _context.IpPhoneProject.Add(_mapper.Map<IpPhoneModel>(phone));
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Complete(int id, string UserName)
        {
            try
            {
                var phone = _context.IpPhoneProject.Find(id)!;
                var log = UserName + " isimli kullanıcı " + DateTime.Now +" tarihinde "+ phone.Campus + " binasında "+ phone.Unit+" biriminindeki Ip Telefon Projesini tamamlandı konumuna çekti.";
                phone.Priority = "Mevcut";
                phone.UpdatedPersonel = UserName;
                phone.UpdatedTime = DateTime.Now;
                _context.IpPhoneProject.Update(phone);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(int id, string UserName)
        {
            try
            {
                var phone = _context.IpPhoneProject.Find(id)!;
                var log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + phone.Campus + " binasında bulunan " + phone.Unit + " birimine ait projeyi sildi.";
                _context.IpPhoneProject.Remove(phone);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(IpPhoneModel phone, string UserName)
        {
            try
            {
                var log = UserName + " isimli kullanıcısı " + DateTime.Now/*+ _detect.IpPhoneUpdate(phone)*/;
                phone.UpdatedPersonel = UserName;
                phone.UpdatedTime = DateTime.Now;
                _context.IpPhoneProject.Update(phone);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
