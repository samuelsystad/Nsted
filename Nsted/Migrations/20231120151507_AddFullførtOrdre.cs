using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nsted.Migrations
{
    public partial class AddFullførtOrdre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FullførteOrdrer",
                columns: table => new
                {
                    ServiceSkjemaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KundeId = table.Column<int>(type: "int", nullable: false),
                    MottattDato = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    OrdreNr = table.Column<int>(type: "int", nullable: false),
                    ProduktType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ÅrsModell = table.Column<int>(type: "int", nullable: false),
                    Servicetype = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerieNummer = table.Column<int>(type: "int", nullable: false),
                    AvtaltMedKunden = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reparasjonsbeskrivelse = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BrukteDeler = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Arbeidstimer = table.Column<int>(type: "int", nullable: false),
                    FerdigstiltDato = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReturDeler = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ForsendelsesMåte = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KundeSignatur = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MekanikerSignatur = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullførteOrdrer", x => x.ServiceSkjemaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullførteOrdrer");
        }
    }
}
