using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nsted.Data;

#nullable disable

namespace Nsted.Migrations
{
    [DbContext(typeof(NstedDbContext))]
    [Migration("20231114164420_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Nsted.Models.Kunde", b =>
                {
                    b.Property<int>("KundeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Etternavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Fornavn")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Registrert")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Telefon")
                        .HasColumnType("int");

                    b.HasKey("KundeId");

                    b.ToTable("Kunder");
                });

            modelBuilder.Entity("Nsted.Models.Registrering", b =>
                {
                    b.Property<int>("RegistreringId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AntallTimerUtført")
                        .HasColumnType("int");

                    b.Property<DateTime>("AvtaltFerdigstillelseInnen")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("AvtaltLevering")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("BookingTilUke")
                        .HasColumnType("int");

                    b.Property<bool>("CaseFerdig")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Feilbeskrivelse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("HenvendelseMottatt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("KundeId")
                        .HasColumnType("int");

                    b.Property<bool>("OrdreNr")
                        .HasColumnType("int");

                    b.Property<DateTime>("ProduktMottatt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProduktType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ServiceFerdig")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("ServiceSkjema")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("RegistreringId");

                    b.HasIndex("KundeId");

                    b.ToTable("Registreringer");
                });

            modelBuilder.Entity("Nsted.Models.Registrering", b =>
                {
                    b.HasOne("Nsted.Models.Kunde", "Kunde")
                        .WithMany()
                        .HasForeignKey("KundeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kunde");
                });
#pragma warning restore 612, 618
        }
    }
}
