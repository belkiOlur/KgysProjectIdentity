using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgysProjectIdentity.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccidentKgys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccidentKgysName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentKgysDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidenKgysType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccidentInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentEbysNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlcoholTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentReport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentInsuranceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentPolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentComplementInsuranceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentComplementPolicyNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccidentRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentRegistrationStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentDamagePrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentExpertiseCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentExpertiseTelephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentExpertiseEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentExpertiseEmailSend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountPaid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepairCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepairFinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentKgys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistrictModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    districtName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KgysPlanned",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Neighbourhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KgysName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CameraCount = table.Column<int>(type: "int", nullable: true),
                    DomeCameraCount = table.Column<int>(type: "int", nullable: true),
                    PtsCameraCount = table.Column<int>(type: "int", nullable: true),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectCoordinate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectYear = table.Column<int>(type: "int", nullable: true),
                    ProjectProtocol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectExcavation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectBasis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectPole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectCabinet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectAssembly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectEnergyCable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectEnergyConnect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectFiberOptic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectConnection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectRecording = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximoId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelecomFoId = table.Column<int>(type: "int", nullable: true),
                    KgysRequestId = table.Column<int>(type: "int", nullable: true),
                    DateOfPlanned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfActivated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectCoordinateX = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    ProjectCoordinateY = table.Column<decimal>(type: "decimal(8,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KgysPlanned", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KgysRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KgysName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Neighbourhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestMethodDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestArraviedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestedByWho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedByWhoDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestCoordinate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestEvaluation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestEvaluationDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestGoToDiscovery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestFirstDiscovery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestAskToDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestAskToDistrictEbysNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestAnswerFromDisctrictEbysNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestArrangementNumber = table.Column<int>(type: "int", nullable: true),
                    TelecomFoId = table.Column<int>(type: "int", nullable: true),
                    LastStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectCoordinateX = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    ProjectCoordinateY = table.Column<decimal>(type: "decimal(8,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KgysRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KgysRequested",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArraviedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedByWho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedByWhoDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coordinate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evaluation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoToDiscovery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstDiscovery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastDiscovery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AskToDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AskToDistrictEbysNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerFromDisctrictEbysNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedOrNot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KgysRequested", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KuppaMultipliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: true),
                    Multiplier = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KuppaMultipliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialsDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialBrandCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialBrand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicalSpecification1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EtmysNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhoTake = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shelf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialsProductsModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialsProductsModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nanet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nanet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NeighbourModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    neighbourName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    districtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeighbourModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfficialsJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialsJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkAndRecreations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkDistrictType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkNeighboor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkCoordinate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkCamerasSetup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkCamerasIsActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkCamerasActiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParkPoleCount = table.Column<int>(type: "int", nullable: true),
                    ParkTotalCameraCount = table.Column<int>(type: "int", nullable: true),
                    ParkFixedCameraCount = table.Column<int>(type: "int", nullable: true),
                    ParkDomeCameraCount = table.Column<int>(type: "int", nullable: true),
                    ParkNvrAdress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkLiveMonitoring = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkExplain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkSupervisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkSupervisorTel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkAndRecreations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentEbysNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentPrice = table.Column<int>(type: "int", nullable: false),
                    PaymentCodeId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionCalender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    end = table.Column<DateTime>(type: "datetime2", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    textColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionCalender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EbysNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aplication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemosNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vlan = table.Column<int>(type: "int", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coordinate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SlaStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EpymStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EomStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MplsOperationStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MplsNmsStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckEGM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportToEgm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KuppaDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KuppaID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KuppaPStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KuppaDistance = table.Column<int>(type: "int", nullable: true),
                    KuppaPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectCoordinateX = table.Column<decimal>(type: "decimal(8,6)", nullable: false),
                    ProjectCoordinateY = table.Column<decimal>(type: "decimal(8,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEightyImagedModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEightyImagedModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepetitiveRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    RequestArraviedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestedByWho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedByWhoDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallBack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerEbysNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepetitiveRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecondaryRequestModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutialFO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TtDistance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TtRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TtStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Switch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Configuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PcImaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedPersonel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondaryRequestModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Use = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelecomTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelecomTeam = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelecomTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderOfAdmissionCommissionOfficials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficialsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderOfAdmissionCommissionOfficials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderOfSpecificationOfficials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderOfSpecificationOfficials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestEbysNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhoRequested = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evaluation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerEbysNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommissionerConfirmation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificationEbys = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificationApproval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitOfTender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendOfTender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenderTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WhoResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeOfWork = table.Column<int>(type: "int", nullable: false),
                    WhoHasJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceEbysNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionCommissionEstablished = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionCommissionEbys = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAndValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAndValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaitingJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EbysNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TalkToManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishOrNot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EBYSanswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitingJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentKgys");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DistrictModels");

            migrationBuilder.DropTable(
                name: "KgysPlanned");

            migrationBuilder.DropTable(
                name: "KgysRequest");

            migrationBuilder.DropTable(
                name: "KgysRequested");

            migrationBuilder.DropTable(
                name: "KuppaMultipliers");

            migrationBuilder.DropTable(
                name: "LogModel");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "MaterialsDetail");

            migrationBuilder.DropTable(
                name: "MaterialsProductsModels");

            migrationBuilder.DropTable(
                name: "Nanet");

            migrationBuilder.DropTable(
                name: "NeighbourModels");

            migrationBuilder.DropTable(
                name: "OfficialsJobs");

            migrationBuilder.DropTable(
                name: "ParkAndRecreations");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PaymentCode");

            migrationBuilder.DropTable(
                name: "PermissionCalender");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProjectEightyImagedModel");

            migrationBuilder.DropTable(
                name: "ProjectsModels");

            migrationBuilder.DropTable(
                name: "RepetitiveRequest");

            migrationBuilder.DropTable(
                name: "SecondaryRequestModels");

            migrationBuilder.DropTable(
                name: "SecurityCode");

            migrationBuilder.DropTable(
                name: "TelecomTeams");

            migrationBuilder.DropTable(
                name: "TenderOfAdmissionCommissionOfficials");

            migrationBuilder.DropTable(
                name: "TenderOfSpecificationOfficials");

            migrationBuilder.DropTable(
                name: "TenderProjects");

            migrationBuilder.DropTable(
                name: "UserAndValue");

            migrationBuilder.DropTable(
                name: "WaitingJobs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
