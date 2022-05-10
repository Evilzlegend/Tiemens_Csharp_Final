/**** CREATE AND USE NEW DATABASE *****/
CREATE DATABASE [DMVLaw]
GO
USE [DMVLaw]
GO
SET QUOTED_IDENTIFIER ON
GO

/*****  TABLE AND VALUE INSERTIONS *****/

-- Drivers Table and Values --
CREATE TABLE Drivers (
  DriverID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  FirstName varchar(MAX) NOT NULL,
  LastName varchar(MAX) NOT NULL,
  SSN varchar(MAX) NOT NULL,
  Address varchar(MAX) NOT NULL,
  Phone varchar(MAX) NOT NULL
)

INSERT INTO Drivers (FirstName, LastName, SSN, Address, Phone) VALUES
('Talias', 'Goju', '674-29-4212', '124 Not a Street, GG 52291', '423-423-4529'),
('Poumn', 'Brinlu', '433-32-5523', '13 Another fake st, GG 52291', '532-523-5526'),
('Ifrit', 'Buckla', '523-97-3432', '74 County Rd 44, NW 55839', '852-422-7633'),
('Eck', 'Penati', '452-45-9482', '522 Store Front Ave, GG 52291', '923-523-6311'),
('Keylara', 'Grunji', '882-32-4280', '78 College Ave, GG 52291', '522-412-5222'),
('Kerpan', 'Slavari', '274-42-3389', '79 College Ave, GG 52291', '528-763-2442'),
('Lordly', 'Monusur', '422-19-2891', '8221 Glendo Road, GG 52291', '324-523-6231'),
('Yoshizawa', 'Monosuke', '482-07-5977', '1244 Pinfall Street, GG 52291', '232-522-5523'),
('Andro', 'Erickson', '285-24-4145', '88 South Bird Court, GG 52291', '242-522-5251'),
('Hizaw', 'Katzuku', '528-22-0154', '244 Flash Forward Ave, GG 52291', '922-422-4224');


-- Infractions Table and Values --
CREATE TABLE Infractions (
  InfractionID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  InfractionType varchar(MAX) NOT NULL
)

INSERT INTO Infractions (InfractionType) VALUES
('Speeding'),
('Distracted'),
('Impaired Under'),
('Impaired While'),
('Fault Accident'),
('No Proof Insurance'),
('Seat belt violation'),
('Expired License'),
('Revoked License'),
('Reckless driving');


-- DriverInfractions Table and Values --
CREATE TABLE DriverInfractions (
  DriverInfractionID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  DriverID int NOT NULL,
  InfractionID int NOT NULL,
  FOREIGN KEY (DriverID) REFERENCES Drivers(DriverID),
  FOREIGN KEY (InfractionID) REFERENCES Infractions(InfractionID)
)

INSERT INTO DriverInfractions (DriverID, InfractionID) VALUES
(9, 5),
(4, 1),
(1, 3),
(10, 2),
(8, 7),
(3, 9),
(5, 10),
(6, 4),
(2, 9),
(7, 5);


-- Users Table and Values --
CREATE TABLE Users (
  UserID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  FirstName varchar(MAX) NOT NULL,
  LastName varchar(MAX) NOT NULL,
  Username varchar(MAX) NOT NULL,
  Password varchar(MAX) NOT NULL,
  Role varchar(MAX) NOT NULL
)


INSERT INTO Users (FirstName, LastName, Username, Password, Role) VALUES
('Sarah', 'King', 'SKing', 'Password11', 'DMV Personnel'),
('Alan', 'Eli', 'AEli', 'Password22', 'Law Enforcement'),
('Mike', 'Balfanz', 'MBalfanz', 'Password33', 'Law Enforcement'),
('Kari', 'Patterson', 'KPatterson', 'Password44', 'DMV Personnel'),
('Jennie', 'Fries', 'JFries', 'Password55', 'Law Enforcement'),
('Skylar', 'Ryman', 'SRyman', 'Password66', 'DMV Personnel'),
('Hubert', 'Farnsworth', 'HFarnsworth', 'Password77', 'DMV Personnel'),
('Tony', 'Harks', 'THarks', 'Password88', 'Law Enforcement'),
('Bridget', 'Zeilman', 'BZeilman', 'Password99', 'Law Enforcement'),
('Jason', 'Scott', 'JScott', 'Password00', 'DMV Personnel');


-- Vehicles Table and Values --
CREATE TABLE Vehicles (
  VehicleID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
  DriverID int NOT NULL,
  VehicleMake varchar(MAX) NOT NULL,
  VehicleModel varchar(MAX) NOT NULL,
  VehicleColor varchar(MAX) NOT NULL,
  VehiclePlate varchar(MAX) NOT NULL,
  FOREIGN KEY (DriverID) REFERENCES Drivers(DriverID)
) 


INSERT INTO Vehicles (DriverID, VehicleMake, VehicleModel, VehicleColor, VehiclePlate) VALUES
(1, 'Ford', 'Ranger', 'Black', 'EF7-R4E'),
(2, 'Mitsubishi', 'Lancer', 'Grey', 'GJT-424'),
(3, 'Toyota', 'Corolla', 'Black', 'G4F-4F5'),
(4, 'Suburu', 'Outback', 'Orange', 'EW3-FT6'),
(5, 'Dodge', 'Ram', 'Red', 'NMY-433'),
(6, 'Honda', 'Pilot', 'White', 'UI7-GJ4'),
(7, 'Ford', 'Mustang', 'Red', '2-FAST'),
(8, 'Suburu', 'Forester', 'Green', 'TWQ-231'),
(9, 'Mitsubishi', 'Outlander', 'White', 'MIF-874'),
(10, 'Cheverolet', 'Silverado', 'Grey', 'PL3-33D');