--CREATE DATABASE Project1_DB;


CREATE TABLE States(
StateNo INT PRIMARY KEY IDENTITY(1,1),
StateName VARCHAR(50) NOT NULL,
StateCode VARCHAR(50) NOT NULL,
TaxRate DECIMAL(4,2) NOT NULL);

INSERT INTO States (StateName, StateCode, TaxRate)
VALUES
--('Test State', 'TS', 0.77),
('Texas', 'TX', 0.07),
('Louisiana', 'LA', 0.05);

--SELECT * FROM States;


CREATE TABLE Stores(
StoreNo INT PRIMARY KEY IDENTITY(1,1),
StoreName VARCHAR(50) NOT NULL,
StoreCity VARCHAR(50) NOT NULL,
StoreState INT FOREIGN KEY REFERENCES States(StateNo) ON DELETE CASCADE,
StoreZipCode INT NOT NULL,
StoreStreetAddress VARCHAR(50) NOT NULL);

INSERT INTO Stores (StoreName, StoreCity, StoreState, StoreZipCode, StoreStreetAddress)
VALUES
--('Test Store Name', 'Test Store City', 1, 77777, 'Test Store Street Address'),
('BEE Downtown', 'Houston', 2, 77075, '123 EZ St.'),
('BEE Heights', 'The Heights', 2, 77175, '44599 Long Rd.'),
('BEE Shore', 'Bay Town', 2, 77055, '745 Crooked Ave.'),
('BEE Meadows', 'Webster', 2, 77598, '666 El Dorado Blvd.'),
('BEE Townly', 'Spring', 2, 77192, '4567 Yellow Ln.');

--SELECT * FROM Stores;


CREATE TABLE Users(
AccountNo INT PRIMARY KEY IDENTITY(1,1),
Firstname VARCHAR(50) NOT NULL,
Lastname VARCHAR(50) NOT NULL,
Username VARCHAR(50) NOT NULL,
PasswordSalt VARCHAR(64) NOT NULL,
PasswordHash CHAR(64) NOT NULL,
Phone char(10) NOT NULL,
Email VARCHAR(50) NOT NULL,
Permission INT NOT NULL,
DefaultStore INT FOREIGN KEY REFERENCES Stores(StoreNo) ON DELETE CASCADE);

INSERT INTO Users (Firstname, Lastname, Username, PasswordSalt, PasswordHash, Phone, Email, Permission, DefaultStore)
VALUES
('testfirst', 'testlast', 'TestUser', 'testsalt', '382d700319077f8a057f95a94d67be197842e3a7a1cd522488e1763cb7122051', '0123456789', 'test@email.com', 7, 1);
--password is 'testpassword'

--SELECT * FROM Users;


CREATE TABLE Items(
PartNo INT PRIMARY KEY IDENTITY(1,1),
PartName VARCHAR(50) NOT NULL,
PartDescription VARCHAR(1000),
PartPrice DECIMAL(8,2) NOT NULL,
PartSale DECIMAL(4,2) NOT NULL,
PartImage VARCHAR(100));

INSERT INTO Items(PartName, PartDescription, PartPrice, PartSale, PartImage)
VALUES
--('testpart', 'this describes the test part', 7.77, 0.0, 'Images/testpart.jpg'),
('Solder Flux', 'A must have for solder work! Beware the fumes.', 9.99, 0.0, 'Images/flux.jpg'),
('Fume Extractor', 'Reliable fan and filter keep the fumes out of your breathing space.', 39.99, 0.0, 'Images/fumevent.jpg'),
('Solder', 'Spool of solder for all of you soldering needs!', 7.99, 0.0, 'Images/solder.jpg'),
('Soldering Iron', 'Tried and true soldering iron for working on all sorts of projects. Hobby or pro!', 119.99, 0.0, 'Images/solderingiron.jpg'),
('SMD Rework Station', 'Kit yourself up for working on surface mounted componenets. Bring your skills to the next level.', 89.99, 0.0, 'Images/smd.jpg'),
('Stereo Microscope', 'Microscope to allow for working on projects from a more comfortable position with less eye strain.', 209.99, 0.0, 'Images/microscope.jpg'),
('Oscilloscope', 'Make sure your signals are sorted. Measures electrical frequencies in circuts.', 149.99, 0.0, 'Images/osci.jpg'),
('Solderable Breadboard', 'Fast and easy prototyping for your circuts. Pick yours up today!', 19.99, 0.0, 'Images/breadboard.jpg'),
('Variable DC Power Supply', 'Bring your projects to life. No matter your voltage needs, this power supply has you cevered.', 79.99, 0.0, 'Images/pwrsply.jpg');

--SELECT * FROM Items;


CREATE TABLE Inventory(
CONSTRAINT InventoryItemNo PRIMARY KEY(StoreNo, PartNo),
StoreNo INT FOREIGN KEY REFERENCES Stores(StoreNo) ON DELETE CASCADE,
PartNo INT FOREIGN KEY REFERENCES Items(PartNo) ON DELETE CASCADE,
Quantity INT NOT NULL);

INSERT INTO Inventory (StoreNo, PartNo, Quantity)
VALUES
--(1, 1, 7),
(3, 2, 100),
(3, 3, 100),
(3, 4, 100),
(3, 5, 100),
(3, 6, 100),
(3, 7, 100),
(3, 8, 100),
(3, 9, 100),
(3, 10, 100),
(4, 2, 100),
(4, 3, 100),
(4, 4, 100),
(4, 5, 100),
(4, 6, 100),
(4, 7, 100),
(4, 8, 100),
(4, 9, 100),
(4, 10, 100),
(5, 2, 100),
(5, 3, 100),
(5, 4, 100),
(5, 5, 100),
(5, 6, 100),
(5, 7, 100),
(5, 8, 100),
(5, 9, 100),
(5, 10, 100),
(6, 2, 100),
(6, 3, 100),
(6, 4, 100),
(6, 5, 100),
(6, 6, 100),
(6, 7, 100),
(6, 8, 100),
(6, 9, 100),
(6, 10, 100),
(7, 2, 100),
(7, 3, 100),
(7, 4, 100),
(7, 5, 100),
(7, 6, 100),
(7, 7, 100),
(7, 8, 100),
(7, 9, 100),
(7, 10, 100);

--SELECT * FROM Inventory;


CREATE TABLE Orders(
OrderNo INT PRIMARY KEY IDENTITY(1,1),
StoreNo INT FOREIGN KEY REFERENCES Stores(StoreNo) ON DELETE NO ACTION,
AccountNo INT FOREIGN KEY REFERENCES Users(AccountNo) ON DELETE NO ACTION,
OrderDate DATETIME,
Subtotal DECIMAL(12,2),
Tax DECIMAL(12,2),
Total DECIMAL(12,2));

INSERT INTO Orders (StoreNo, AccountNo, OrderDate, Subtotal, Tax, Total)
VALUES
(1, 1, '1900-01-01 00:00:00', 7.77, 5.98, 13.75);

--SELECT * FROM Orders;


CREATE TABLE OrderHistory(
CONSTRAINT OrderHistoryNo PRIMARY KEY(OrderNo, PartNo),
OrderNo INT FOREIGN KEY REFERENCES Orders(OrderNo) ON DELETE CASCADE,
PartNo INT FOREIGN KEY REFERENCES Items(PartNo) ON DELETE CASCADE,
UnitPrice DECIMAL(12,2) NOT NULL,
Quantity INT NOT NULL);

INSERT INTO OrderHistory (OrderNo, PartNo, UnitPrice, Quantity)
VALUES
(1, 1, 7.77, 1);

--SELECT * FROM OrderHistory;