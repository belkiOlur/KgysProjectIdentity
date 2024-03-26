using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using KgysProjectIdentity.Core.Areas.Admin.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace KgysProjectIdentity.Repository.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TelecomFoModel için ProjectCoordinateX ve ProjectCoordinateY yapılandırması
            modelBuilder.Entity<TelecomFoModel>()
                .Property(p => p.ProjectCoordinateX)
                .HasColumnType("decimal(8, 6)");

            modelBuilder.Entity<TelecomFoModel>()
                .Property(p => p.ProjectCoordinateY)
                .HasColumnType("decimal(8, 6)");

            // KgysPlannedModel için 
            modelBuilder.Entity<KgysPlannedModel>()
                .Property(p => p.ProjectCoordinateX)
                .HasColumnType("decimal(8, 6)");

            modelBuilder.Entity<KgysPlannedModel>()
                .Property(p => p.ProjectCoordinateY)
                .HasColumnType("decimal(8, 6)");

            // KgysRequestModel için 
            modelBuilder.Entity<KgysRequestModel>()
                .Property(p => p.ProjectCoordinateX)
                .HasColumnType("decimal(8, 6)");

            modelBuilder.Entity<KgysRequestModel>()
                .Property(p => p.ProjectCoordinateY)
                .HasColumnType("decimal(8, 6)");
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<TelecomFoModel> Products { get; set; }
        public DbSet<ProjectEightyImagedModel> ProjectEightyImagedModel { get; set; }
        public DbSet<WaitingJobsModel> WaitingJobs { get; set; }
        public DbSet<OfficialsJobsModel> OfficialsJobs { get; set; }
        public DbSet<NanetModel> Nanet { get; set; }
        public DbSet<UserAndValueListModel> UserAndValue { get; set; }
        public DbSet<KgysRequestedModel> KgysRequested { get; set; }
        public DbSet<SecondaryRequestModel> SecondaryRequestModels { get; set; }
        public DbSet<DistrictModel> DistrictModels { get; set; }
        public DbSet<NeighbourModel> NeighbourModels { get; set; }
        public DbSet<ProjectsModel> ProjectsModels { get; set; }
        public DbSet<TelecomTeamModel> TelecomTeams { get; set; }
        public DbSet<AlertModel> Alerts { get; set; }
        public DbSet<ParkAndRecreationsModel> ParkAndRecreations { get; set; }
        public DbSet<KgysRequestModel> KgysRequest { get; set; }
        public DbSet<KgysPlannedModel> KgysPlanned { get; set; }
        public DbSet<TenderProjectsModel> TenderProjects { get; set; }
        public DbSet<AccidentKgysModel> AccidentKgys { get; set; }
        public DbSet<LogModel> LogModel { get; set; }
        public DbSet<KuppaMultiplierModel>  KuppaMultipliers { get; set; }
        public DbSet<PaymentModel> Payment { get; set; }
        public DbSet<PaymentCodeModel> PaymentCode { get; set; }
        public DbSet<RepetitiveRequestModel> RepetitiveRequest { get; set; }
        public DbSet<MaterialsModel> Materials { get; set; }
        public DbSet<MaterialsDetailModel> MaterialsDetail { get; set; }
        public DbSet<SecurityCodeModel> SecurityCode { get; set; }
        public DbSet<MaterialsProductsModel> MaterialsProductsModels { get; set; }
        public DbSet<TenderOfSpecificationOfficialsModel> TenderOfSpecificationOfficials{ get; set; }
        public DbSet<TenderOfAdmissionCommissionOfficialsModel> TenderOfAdmissionCommissionOfficials { get; set; }
        public DbSet<CalenderModel> PermissionCalender { get; set; }
        public DbSet<IpPhoneModel> IpPhoneProject { get; set; }
        public DbSet<PriorityForIpPhoneModel> PriorityForPhone { get; set; }
        public DbSet<CctvProjectModel> CctvProjects { get; set; }
        public DbSet<CctvModel> CctvProjectDetail { get; set; }
        public DbSet<CctvProductsModel> CctvProducts { get; set; }
        public DbSet<ProductsOfCctvProjectModel> ProductsOfCctv { get; set; }
        public DbSet<ModelForCctvProjectModel> ModelForCctv { get; set; }
        public DbSet<CctvEk1Model> CctvEk1 { get; set; }
        public DbSet<CctvProjectPictureModel> CctvPictures { get; set; }
        public DbSet<CctvEbysNumbersModel> CctvEbysNumbers { get; set; }
    }

}
