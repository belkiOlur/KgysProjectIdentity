using AutoMapper;
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
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly ILogService _logService;
        private readonly ILogDifferenceDetectService _detect;
        private readonly IMapper _mapper;
        private readonly IDistrictService _district;
        public ProjectService(IDistrictService district, AppDbContext context, IMapper mapper, ILogDifferenceDetectService detect, ILogService logService)
        {
            _district = district;
            _context = context;
            _mapper = mapper;
            _detect = detect;
            _logService = logService;
        }

        public KgysPlannedModel ModelUpdate(KgysPlannedModel updateProject, string UserName)
        {
            updateProject!.District = _district.GetDistrictNameById(Convert.ToInt32(updateProject.District));
            updateProject.UpdatedTime = DateTime.Now;
            updateProject.UpdatedUser = UserName;
            updateProject!.ProjectYear = _context.ProjectsModels.Find(Convert.ToInt32(updateProject.ProjectName))!.Year;
            if (updateProject!.ProjectName != "" && updateProject!.ProjectName != null)
            {
                updateProject.ProjectName = _context.ProjectsModels.Find(Convert.ToInt32(updateProject.ProjectName))!.Project;
            }
            return updateProject;
        }

        public KgysPlannedViewModel ModelUpdateForAdd(KgysPlannedViewModel project, string UserName)
        {
            project.District = _district.GetDistrictNameById(Convert.ToInt32(project.District));
            project.UpdatedTime = DateTime.Now;
            project.UpdatedUser = UserName;
            project.ProjectYear = _context.ProjectsModels.Find(Convert.ToInt32(project.ProjectName))!.Year;
            project.ProjectName = _context.ProjectsModels.Find(Convert.ToInt32(project.ProjectName))!.Project;
            project.ProjectProtocol = "Başlanılmadı";
            project.Id = 0;
            return project;
        }

        public Task UpdateForStatusRequest(KgysPlannedModel updateProject, string UserName)
        {
            updateProject.ProjectStatus = "Başlanılmadı";
            var project = _context.KgysRequest.FirstOrDefault(x => x.Id == updateProject.KgysRequestId);
            var plannedProject = _context.KgysPlanned.Find(updateProject.Id);
            plannedProject!.LastStatus = "Talep";
            project!.LastStatus = "Talep";
            string logRequest = _detect.ProjectUptade(updateProject);
            _context.KgysRequest.Update(_mapper.Map<KgysRequestModel>(project));
            _context.SaveChanges();
            _context.KgysPlanned.Update(_mapper.Map<KgysPlannedModel>(plannedProject));
            _context.SaveChanges();
            _logService.LogForAdd(logRequest);
            return Task.CompletedTask;
        }
    }
}
