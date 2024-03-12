using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Services;
using KgysProjectIdentity.Web.CustomValidations;
using KgysProjectIdentity.Web.Localization;
using Microsoft.AspNetCore.Identity;

namespace KgysProjectIdentity.Web.Areas.Admin.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            //Dependency Injection olması için yazıldı
            services.AddScoped<GetProject>();
            services.AddScoped<Telecom>();
            services.AddScoped<ILogDifferenceDetectService, LogDifferenceDetectService>();
            services.AddScoped<IEmailService, EmailService>(); //Email için konuldu
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<ISelectListItemsService, SelectListItemsService>();
            services.AddScoped<IAccidentKgysService, AccidentKgysService>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<IKgysRequestedService, KgysRequestedService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<INeighbourhoodService, NeighbourhoodService>();
            services.AddScoped<ITelecomService, TelecomService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITenderService, TenderService>();
            services.AddScoped<IWaitingJobService, WaitingJobService>();
            services.AddScoped<IWareHouseService, WareHouseService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IIpPhoneService, IpPhoneService>();
            services.AddScoped<ICctvService, CctvService>();
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(30);
            });
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "1234567890";//User Name için İzin verilen karakterler
                //options.Password.RequiredLength = 6;//Password için izin verilen uzunluk
                options.Password.RequireNonAlphanumeric = false;//noktalama işaretleri için
                options.Password.RequireLowercase = false;//Küçük harf tanımı için
                options.Password.RequireUppercase = false;//Büyük harf tanımı için
                options.Password.RequireDigit = false;//Rakam kullanmak için

            }).AddPasswordValidator<PasswordValidator>()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
