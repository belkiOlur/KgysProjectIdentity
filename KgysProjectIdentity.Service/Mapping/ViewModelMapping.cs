using AutoMapper;
using KgysProjectIdentity.Core.ViewModels;
using KgysProjectIdentity.Repository.Models;

namespace KgysProjectIdentity.Service.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<TelecomFoModel, TelecomFoViewModel>().ReverseMap();
            CreateMap<WaitingJobsModel, WaitingJobsViewModel>().ReverseMap();
            CreateMap<KgysRequestedModel, KgysRequestedViewModel>().ReverseMap();
            CreateMap<SecondaryRequestModel, SecondaryRequestViewModel>().ReverseMap();
            CreateMap<TelecomTeamModel, TelecomTeamViewModel>().ReverseMap();
            CreateMap<ProjectsModel, ProjectsViewModel>().ReverseMap();
            CreateMap<AlertModel, AlertViewModel>().ReverseMap();
            CreateMap<ParkAndRecreationsModel, ParkAndRecreationsViewModel>().ReverseMap();
            CreateMap<KgysRequestModel, KgysRequestViewModel>().ReverseMap();
            CreateMap<KgysPlannedModel, KgysPlannedViewModel>().ReverseMap();
            CreateMap<TenderProjectsModel, TenderProjectsViewModel>().ReverseMap();
            CreateMap<AccidentKgysModel, AccidentKgysViewModel>().ReverseMap();
            CreateMap<PaymentModel, PaymentViewModel>().ReverseMap();
            CreateMap<RepetitiveRequestModel, RepetitiveRequestViewModel>().ReverseMap();
            CreateMap<MaterialsDetailModel, MaterialsDetailViewModel>().ReverseMap();
            CreateMap<SecurityCodeModel, SecurityCodeViewModel>().ReverseMap();
            CreateMap<CalenderModel, CalenderViewModel>().ReverseMap();
            CreateMap<IpPhoneModel, IpPhoneViewModel>().ReverseMap();
            CreateMap<CctvModel, CctvViewModel>().ReverseMap();
            CreateMap<CctvProjectModel, CctvProjectViewModel>().ReverseMap();
            CreateMap<CctvProductsModel, CctvProductsViewModel>().ReverseMap();

        }
    }
}