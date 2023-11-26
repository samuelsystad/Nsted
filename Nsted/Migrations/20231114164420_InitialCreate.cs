using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nsted.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    KundeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fornavn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Etternavn = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefon = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Adresse = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Registrert = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.KundeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Registreringer",
                columns: table => new
                {
                    RegistreringId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KundeId = table.Column<int>(type: "int", nullable: false),
                    BookingTilUke = table.Column<int>(type: "int", nullable: false),
                    HenvendelseMottatt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CaseFerdig = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ProduktType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Feilbeskrivelse = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AvtaltLevering = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProduktMottatt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AvtaltFerdigstillelseInnen = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ServiceFerdig = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AntallTimerUtført = table.Column<int>(type: "int", nullable: false),
                    OrdreNr = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ServiceSkjema = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registreringer", x => x.RegistreringId);
                    table.ForeignKey(
                        name: "FK_Registreringer_Kunder_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunder",
                        principalColumn: "KundeId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Registreringer_KundeId",
                table: "Registreringer",
                column: "KundeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registreringer");

            migrationBuilder.DropTable(
                name: "Kunder");
        }
    }
}
