IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FuelReports')
CREATE DATABASE FuelReports;
GO
USE FuelReports;

CREATE TABLE FuelTypes (
ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY default NEWID(),
FuelType varchar(50) NOT NULL
);

CREATE TABLE PetrolStations (
ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY default NEWID(),
Name varchar(100) NOT NULL,
Address varchar(100) NOT NULL,
City varchar(100) NOT NULL
);

CREATE TABLE FuelRecords (
ID UNIQUEIDENTIFIER NOT NULL PRIMARY KEY default NEWID(),
PetrolStationID  UNIQUEIDENTIFIER NOT NULL,
FuelTypeID  UNIQUEIDENTIFIER NOT NULL,
Price decimal(18,2) NOT NULL,
Date date NOT NULL
);

ALTER TABLE FuelRecords ADD CONSTRAINT FuelRecords_PetrolStations
FOREIGN KEY (PetrolStationID) 
REFERENCES PetrolStations(ID);

ALTER TABLE FuelRecords ADD CONSTRAINT FuelRecords_FuelTypes
FOREIGN KEY (FuelTypeID) 
REFERENCES FuelTypes(ID);