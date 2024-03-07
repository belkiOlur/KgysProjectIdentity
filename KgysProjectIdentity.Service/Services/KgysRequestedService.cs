using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;

namespace KgysProjectIdentity.Service.Services
{
    public class KgysRequestedService : IKgysRequestedService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        public KgysRequestedService(AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _logService = logService;
        }

        public IEnumerable<KgysRequestModel> GetRequest()
        {
            var accident = _context.KgysRequest.ToList();
            return accident;
        }
        public KgysRequestModel GetRequestFindById(int id)
        {
            var requestId = _context.KgysRequest.Find(id);
            return requestId;
        }
        public IEnumerable<KgysRequestModel> GetRequestWhereId(int id)
        {
            var requestId = _context.KgysRequest.Where(x => x.Id == id);
            return requestId;
        }
        public IEnumerable<KgysPlannedModel> GetPlanned()
        {
            var accident = _context.KgysPlanned.ToList();
            return accident;
        }
        public IEnumerable<KgysPlannedModel> GetPlannedFindById(int id)
        {
            var planned = _context.KgysPlanned.Where(x => x.KgysRequestId == id).ToList();
            return planned;
        }
        public IEnumerable<KgysRequestModel> GetOrderRequest()
        {
            var ordered = _context.KgysRequest.OrderBy(x => x.District).OrderBy(x => x.RequestArrangementNumber).ToList();
            return ordered;
        }

        public IEnumerable<KgysRequestModel> GetDistrictRequestStatusRequest(string district)
        {
            var districtsRequest = _context.KgysRequest.Where(x => x.District == district && x.LastStatus == "Talep").ToList();
            return districtsRequest;
        }

        public IEnumerable<KgysPlannedModel> GetDistrictPlannedStatusNotRequest(string district)
        {
            var districtsPlanned = _context.KgysPlanned.Where(x => x.District == district && x.LastStatus != "Talep").ToList();
            return districtsPlanned;
        }

        public IEnumerable<KgysRequestModel> GetDistrictRequestAll(string district)
        {
            var districtsRequest = _context.KgysRequest.Where(x => x.District == district).ToList();
            return districtsRequest;
        }

        public IEnumerable<KgysPlannedModel> GetDistrictPlannedAll(string district)
        {
            var districtsPlanned = _context.KgysPlanned.Where(x => x.District == district).ToList();
            return districtsPlanned;
        }

        public IEnumerable<RepetitiveRequestModel> GetRepetitive(int id)
        {
            var repetitive = _context.RepetitiveRequest.Where(x => x.RequestId == id);
            return repetitive;
        }

        public IEnumerable<KgysRequestedModel> GetBeforeRequestById()
        {
            var request = _context.KgysRequested.OrderByDescending(x => x.Id).ToList();
            return request;
        }

        public string Remove(int id, string UserName)
        {
            var request = _context.KgysRequest.Find(id);
            string log = request!.KgysName + " isimli talebi " + UserName + " kullanıcısı " + DateTime.Now + " tarihinde sildi.";
            _context.KgysRequest.Remove(request!);
            _context.SaveChanges();
            return log;
        }

        public string Add(KgysRequestViewModel request, string UserName)
        {
            request.UpdatedTime = DateTime.Now;
            request.UpdatedUser = UserName;
            request.District = _context.DistrictModels.Find(Convert.ToInt32(request.District))!.districtName;
            request.LastStatus = "Talep";
            _context.Products.Add(new TelecomFoModel() { District = request.District, EbysNumber = request.RequestAnswerFromDisctrictEbysNumber, Name = request.KgysName, Coordinate = Convert.ToString(request.ProjectCoordinateY)+","+ Convert.ToString(request.ProjectCoordinateX), Description = request.ProjectName, ProjectCoordinateY=request.ProjectCoordinateY, ProjectCoordinateX=request.ProjectCoordinateX });
            _context.SaveChanges();
            request.TelecomFoId = _context.Products.FirstOrDefault(x => x.Name == request.KgysName)!.Id;
            string log = _detect.KgysRequestAdd(request);
            _context.KgysRequest.Add(_mapper.Map<KgysRequestModel>(request));
            _context.SaveChanges();
            return log;
        }

        public Task Update(KgysRequestViewModel updateRequest, string UserName)
        {
            updateRequest!.UpdatedTime = DateTime.Now;
            updateRequest!.UpdatedUser = UserName;
            updateRequest!.District = _context.DistrictModels.Find(Convert.ToInt32(updateRequest.District))!.districtName;
            if (updateRequest.ProjectName != null)
            {
                var projectName = _context.ProjectsModels.Find(Convert.ToInt32(updateRequest.ProjectName))!.Project;
            }
            var projectYear = _context.ProjectsModels.Find(Convert.ToInt32(updateRequest.ProjectName))?.Year;
            if (updateRequest!.LastStatus == "Planlanan" || updateRequest!.LastStatus == "Mevcut")
            {
                try
                {
                    string logPlannedUpdate = updateRequest.UpdatedUser + " kullanıcısının " + updateRequest.UpdatedTime + "'de " + updateRequest.KgysName + " isimli talebi " + updateRequest.RequestType + " durumuna aldı.";
                    var project = _context.KgysPlanned.FirstOrDefault(x => x.KgysRequestId == updateRequest.Id);
                    project!.LastStatus = "Planlanan";
                    _context.KgysPlanned.Update(_mapper.Map<KgysPlannedModel>(project));
                    _context.SaveChanges();
                    var telecom = _context.Products.Find(updateRequest.TelecomFoId);
                    if (!telecom!.Description!.Contains(project.ProjectName!))
                    {
                        telecom!.Description = project.ProjectName + " / " + telecom.Description;
                        _context.Products.Update(telecom);
                    }
                    _logService.LogForAdd(logPlannedUpdate);
                }
                catch
                {
                    string logPlannedAdd = UserName + " kullanıcısının " + updateRequest.UpdatedTime + "'de " + updateRequest.KgysName + " isimli talebi " + updateRequest.RequestType + " durumuna aldı.";
                    var projectName = _context.ProjectsModels.Find(Convert.ToInt32(updateRequest.ProjectName))!.Project;
                    _context.KgysPlanned.Add(new KgysPlannedModel() { KgysName = updateRequest.KgysName, District = updateRequest.District, Neighbourhood = updateRequest.Neighbourhood, RequestType = updateRequest.RequestType, ProjectName = projectName, LastStatus = updateRequest.LastStatus, ProjectYear = projectYear, TelecomFoId = updateRequest.TelecomFoId, ProjectCoordinateX = updateRequest.ProjectCoordinateX, ProjectCoordinateY = updateRequest.ProjectCoordinateY, KgysRequestId = updateRequest.Id, DateOfPlanned = DateTime.Now });
                    _context.SaveChanges();
                    var telecom = _context.Products.Find(updateRequest.TelecomFoId);
                    telecom!.Description = projectName + " / " + telecom.Description;
                    _context.Products.Update(telecom);
                    _logService.LogForAdd(logPlannedAdd);
                }
            }
            string log = _detect.KgysRequestUpdate(updateRequest);
            _context.KgysRequest.Update(_mapper.Map<KgysRequestModel>(updateRequest));
            _context.SaveChanges();
            _logService.LogForAdd(log);
            return Task.CompletedTask;
        }


    }
}
