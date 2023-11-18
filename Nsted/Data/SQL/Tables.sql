CREATE DATABASE IF NOT EXISTS NstedDb;
use NstedDb;

{
CREATE TABLE IF NOT EXISTS Kunder
(
    KundeId INT PRIMARY KEY,
    Fornavn VARCHAR(50),
    Etternavn VARCHAR(50),
    Telefon INT,
    Email VARCHAR(100),
    Adresse VARCHAR(100),
    Registrert DATETIME
);

CREATE TABLE IF NOT EXISTS Registreringer
(
    RegistreringId INT PRIMARY KEY,
    KundeId INT,
    BookingTilUke INT NOT NULL,
    HenvendelseMottatt DATETIME NOT NULL,
    CaseFerdig BOOLEAN NOT NULL,
    ProduktType VARCHAR(255) NOT NULL,
    Feilbeskrivelse VARCHAR(500) NOT NULL,
    AvtaltLevering DATETIME NOT NULL,
    ProduktMottatt DATETIME NOT NULL,
    AvtaltFerdigstillelseInnen DATETIME NOT NULL,
    ServiceFerdig DATETIME NOT NULL,
    AntallTimerUtført INT NOT NULL,
    OrdreNr BOOLEAN NOT NULL,
    ServiceSkjema BOOLEAN NOT NULL,
    FOREIGN KEY (KundeId) REFERENCES Kunder(KundeId) -- Assumes Kunder table exists with KundeId as primary key
);

        }
