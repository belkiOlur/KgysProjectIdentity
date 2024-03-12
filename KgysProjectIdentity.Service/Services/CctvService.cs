using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;
using System.Net;

namespace KgysProjectIdentity.Service.Services
{
    public class CctvService : ICctvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _log;
        private readonly ILogDifferenceDetectService _detect;
        public CctvService(AppDbContext context, IMapper mapper, ILogService log, ILogDifferenceDetectService detect)
        {
            _context = context;
            _mapper = mapper;
            _log = log;
            _detect = detect;
        }

        public bool ProjectAdd(CctvProjectViewModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.ProjectName + " fazını ekledi.";
                _context.CctvProjects.Add(_mapper.Map<CctvProjectModel>(project));
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ProjectRemove(int id, string UserName)
        {
            try
            {
                var project = _context.CctvProjects.Find(id)!;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.ProjectName + " fazını sildi.";
                _context.CctvProjects.Remove(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ProjectDetailAdd(CctvViewModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.ProjectName + " fazına " + project.ExProjectName + " eski projesi olan " + project.ProjectDistrict + project.Unit + " birimine " + project.ProjectReason + " nedeni ile proje ekledi.";
                _context.CctvProjectDetail.Add(_mapper.Map<CctvModel>(project));
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectDetailRemove(CctvModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.ProjectName + " fazına " + project.ExProjectName + " eski projesi olan " + project.ProjectDistrict + project.Unit + " biriminin " + project.ProjectReason + " nedenli projeyi sildi.";
                _context.CctvProjectDetail.Remove(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectDetailUpdate(CctvModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + _detect.CctvProjectDetailUpdate(project);
                _context.CctvProjectDetail.Update(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectProductAdd(CctvProductsViewModel project, string UserName)
        {
            try
            {
                var detail = _context.CctvProjectDetail.Find(project.DetailId)!;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.FloorOfSystem + " katındaki sistem odasına kayıtlı " + project.ProductName + " malzemesini " + project.ProductModel+" model olmak üzere" + project.FloorOfProduct + " katta " + project.PlannedPlace +" içerisine "+ project.ProductPieces+ " adet olarak " + detail.ProjectName+" projesinde"+detail.ProjectDistrict+" ilçesi "+detail.Unit+" projesine ekledi.";
                _context.CctvProducts.Add(_mapper.Map<CctvProductsModel>(project));
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectProductUpdate(CctvProductsModel project, string UserName)
        {
            try
            {
                string log = UserName + " isimli kullanıcı " + _detect.CctvProjectProductUpdate(project);
                _context.CctvProducts.Update(project);
                _context.SaveChanges();
                _log.LogForAdd(log);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ProjectProductRemove(CctvProductsModel project, string UserName)
        {
            try
            {
                var detail = _context.CctvProjectDetail.Find(project.DetailId)!;
                string log = UserName + " isimli kullanıcı " + DateTime.Now + " tarihinde " + project.FloorOfSystem + " katındaki sistem odasına kayıtlı " + project.ProductName + " malzemesini " + project.ProductModel + " model olmak üzere" + project.FloorOfProduct + " katta " + project.PlannedPlace + " içerisinde bulunan" + project.ProductPieces + " adet olarak " + detail.ProjectName + " projesinde" + detail.ProjectDistrict + " ilçesi " + detail.Unit + " projesinedeki ürünü sildi.";
                _context.CctvProducts.Remove(project);
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
