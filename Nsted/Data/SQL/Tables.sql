CREATE DATABASE IF NOT EXISTS NstedDb;
USE NstedDb;

CREATE TABLE IF NOT EXISTS Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Email VARCHAR(255) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL
);

CREATE TABLE IF NOT EXISTS Kunder (
    KundeId INT AUTO_INCREMENT PRIMARY KEY,
    Fornavn VARCHAR(255) NOT NULL,
    Etternavn VARCHAR(255) NOT NULL,
    Telefon INT NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Adresse VARCHAR(255) NOT NULL,
    Registrert DATETIME NOT NULL
);

CREATE TABLE IF NOT EXISTS Registreringer (
    RegistreringId INT AUTO_INCREMENT PRIMARY KEY,
    KundeId INT NOT NULL,
    BookingTilUke INT NOT NULL,
    HenvendelseMottatt DATETIME NOT NULL,
    CaseFerdig BOOLEAN NOT NULL,
    ProduktType VARCHAR(255) NOT NULL,
    Feilbeskrivelse TEXT NOT NULL,
    AvtaltLevering DATETIME NOT NULL,
    ProduktMottatt DATETIME NOT NULL,
    AvtaltFerdigstillelseInnen DATETIME NOT NULL,
    ServiceFerdig DATETIME NOT NULL,
    AntallTimerUtført INT NOT NULL,
    OrdreNr BOOLEAN NOT NULL,
    ServiceSkjema BOOLEAN NOT NULL,
    FOREIGN KEY (KundeId) REFERENCES Kunder(KundeId)
);

CREATE TABLE IF NOT EXISTS ServiceSkjemas (
    ServiceSkjemaId INT AUTO_INCREMENT PRIMARY KEY,
    KundeId INT NOT NULL,
    MottattDato DATETIME NOT NULL,
    OrdreNr INT NOT NULL,
    ProduktType VARCHAR(255) NOT NULL,
    ÅrsModell INT NOT NULL,
    Servicetype VARCHAR(255) NOT NULL,
    SerieNummer INT NOT NULL,
    AvtaltMedKunden TEXT NOT NULL,
    Reparasjonsbeskrivelse TEXT NOT NULL,
    BrukteDeler TEXT NOT NULL,
    Arbeidstimer INT NOT NULL,
    FerdigstiltDato DATETIME NOT NULL,
    ReturDeler TEXT NOT NULL,
    ForsendelsesMåte VARCHAR(255) NOT NULL,
    KundeSignatur VARCHAR(255) NOT NULL,
    MekanikerSignatur VARCHAR(255) NOT NULL,
    FOREIGN KEY (KundeId) REFERENCES Kunder(KundeId)
);
