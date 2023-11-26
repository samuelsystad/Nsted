CREATE DATABASE IF NOT EXISTS NstedDb;
USE NstedDb;

CREATE TABLE IF NOT EXISTS Kunder (
    KundeId INT AUTO_INCREMENT PRIMARY KEY,
    Fornavn LONGTEXT NOT NULL,
    Etternavn LONGTEXT NOT NULL,
    Telefon INT NOT NULL,
    Email LONGTEXT NOT NULL,
    Adresse LONGTEXT NOT NULL,
    Registrert DATETIME(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS Registreringer (
    RegistreringId INT AUTO_INCREMENT PRIMARY KEY,
    KundeId INT NOT NULL,
    BookingTilUke INT NOT NULL,
    HenvendelseMottatt DATETIME(6) NOT NULL,
    CaseFerdig TINYINT(1) NOT NULL,
    ProduktType LONGTEXT NOT NULL,
    Feilbeskrivelse LONGTEXT NOT NULL,
    AvtaltLevering DATETIME(6) NOT NULL,
    ProduktMottatt DATETIME(6) NOT NULL,
    AvtaltFerdigstillelseInnen DATETIME(6) NOT NULL,
    ServiceFerdig DATETIME(6) NOT NULL,
    AntallTimerUtført INT NOT NULL,
    OrdreNr INT NOT NULL,
    ServiceSkjema TINYINT(1) NOT NULL,
    FOREIGN KEY (KundeId) REFERENCES Kunder(KundeId) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS ServiceSkjemas (
    ServiceSkjemaId INT AUTO_INCREMENT PRIMARY KEY,
    KundeId INT NOT NULL,
    MottattDato DATETIME(6) NOT NULL,
    OrdreNr INT NOT NULL,
    ProduktType LONGTEXT NOT NULL,
    ÅrsModell INT NOT NULL,
    Servicetype LONGTEXT NOT NULL,
    SerieNummer INT NOT NULL,
    AvtaltMedKunden LONGTEXT NOT NULL,
    Reparasjonsbeskrivelse LONGTEXT NOT NULL,
    BrukteDeler LONGTEXT NOT NULL,
    Arbeidstimer INT NOT NULL,
    FerdigstiltDato DATETIME(6) NOT NULL,
    ReturDeler LONGTEXT NOT NULL,
    ForsendelsesMåte LONGTEXT NOT NULL,
    KundeSignatur LONGTEXT NOT NULL,
    MekanikerSignatur LONGTEXT NOT NULL,
    FOREIGN KEY (KundeId) REFERENCES Kunder(KundeId) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Email LONGTEXT NOT NULL,
    PasswordHash LONGTEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS FullførteOrdrer (
    ServiceSkjemaId INT AUTO_INCREMENT PRIMARY KEY,
    KundeId INT NOT NULL,
    MottattDato DATETIME(6) NOT NULL,
    OrdreNr INT NOT NULL,
    ProduktType LONGTEXT NOT NULL,
    ÅrsModell INT NOT NULL,
    Servicetype LONGTEXT NOT NULL,
    SerieNummer INT NOT NULL,
    AvtaltMedKunden LONGTEXT NOT NULL,
    Reparasjonsbeskrivelse LONGTEXT NOT NULL,
    BrukteDeler LONGTEXT NOT NULL,
    Arbeidstimer INT NOT NULL,
    FerdigstiltDato DATETIME(6) NOT NULL,
    ReturDeler LONGTEXT NOT NULL,
    ForsendelsesMåte LONGTEXT NOT NULL,
    KundeSignatur LONGTEXT NOT NULL,
    MekanikerSignatur LONGTEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
