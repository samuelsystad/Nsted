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
