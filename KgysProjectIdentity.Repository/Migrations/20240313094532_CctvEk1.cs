using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KgysProjectIdentity.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CctvEk1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CctvEk1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetailId = table.Column<int>(type: "int", nullable: false),
                    KameraYazilim = table.Column<int>(type: "int", nullable: false),
                    NvrTip1 = table.Column<int>(type: "int", nullable: false),
                    NvrTip2 = table.Column<int>(type: "int", nullable: false),
                    DomeTip1 = table.Column<int>(type: "int", nullable: false),
                    DomeTip2 = table.Column<int>(type: "int", nullable: false),
                    Mic = table.Column<int>(type: "int", nullable: false),
                    DisBullet = table.Column<int>(type: "int", nullable: false),
                    PtzKamera = table.Column<int>(type: "int", nullable: false),
                    SahaSwitch = table.Column<int>(type: "int", nullable: false),
                    IcSwitch = table.Column<int>(type: "int", nullable: false),
                    PcTip1 = table.Column<int>(type: "int", nullable: false),
                    PcTip2 = table.Column<int>(type: "int", nullable: false),
                    Monitor = table.Column<int>(type: "int", nullable: false),
                    LedTv = table.Column<int>(type: "int", nullable: false),
                    DuvarKabin = table.Column<int>(type: "int", nullable: false),
                    RackKabin = table.Column<int>(type: "int", nullable: false),
                    SahaKabin = table.Column<int>(type: "int", nullable: false),
                    KGK = table.Column<int>(type: "int", nullable: false),
                    Cat6Ic = table.Column<int>(type: "int", nullable: false),
                    Caat6Dis = table.Column<int>(type: "int", nullable: false),
                    Cat6Panel = table.Column<int>(type: "int", nullable: false),
                    FoSingle = table.Column<int>(type: "int", nullable: false),
                    FoPanel = table.Column<int>(type: "int", nullable: false),
                    EnerjiKablo = table.Column<int>(type: "int", nullable: false),
                    DataPriz = table.Column<int>(type: "int", nullable: false),
                    EnerjiPriz = table.Column<int>(type: "int", nullable: false),
                    Kanalet = table.Column<int>(type: "int", nullable: false),
                    MetakKanal = table.Column<int>(type: "int", nullable: false),
                    EnerjiPano = table.Column<int>(type: "int", nullable: false),
                    OtoSigorta = table.Column<int>(type: "int", nullable: false),
                    KaziToprak = table.Column<int>(type: "int", nullable: false),
                    KaziAsfalt = table.Column<int>(type: "int", nullable: false),
                    BoruEnerji = table.Column<int>(type: "int", nullable: false),
                    BoruData = table.Column<int>(type: "int", nullable: false),
                    KameraDirek3M = table.Column<int>(type: "int", nullable: false),
                    KameraDirek4M = table.Column<int>(type: "int", nullable: false),
                    KameraDirek5M = table.Column<int>(type: "int", nullable: false),
                    KameraDirek6M = table.Column<int>(type: "int", nullable: false),
                    KameraDirek7M = table.Column<int>(type: "int", nullable: false),
                    KameraDirek8M = table.Column<int>(type: "int", nullable: false),
                    KameraDirek9M = table.Column<int>(type: "int", nullable: false),
                    KameraDirek10M = table.Column<int>(type: "int", nullable: false),
                    UyariLevha = table.Column<int>(type: "int", nullable: false),
                    SistemPerformans = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CctvEk1", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CctvEk1");
        }
    }
}
