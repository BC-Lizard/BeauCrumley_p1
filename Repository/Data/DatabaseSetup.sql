--CREATE DATABASE Project1_DB;


CREATE TABLE States(
StateNo INT PRIMARY KEY IDENTITY(1,1),
StateName VARCHAR(50) NOT NULL,
StateCode VARCHAR(50) NOT NULL,
TaxRate DECIMAL(4,2) NOT NULL);

INSERT INTO States (StateName, StateCode, TaxRate)
VALUES
('Test State', 'TS', 0.77);

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
('Test Store Name', 'Test Store City', 1, 77777, 'Test Store Street Address');

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
('testpart', 'this describes the test part', 7.77, 0.0, 'Images/testpart.jpg');

--SELECT * FROM Items;


CREATE TABLE Inventory(
CONSTRAINT InventoryItemNo PRIMARY KEY(StoreNo, PartNo),
StoreNo INT FOREIGN KEY REFERENCES Stores(StoreNo) ON DELETE CASCADE,
PartNo INT FOREIGN KEY REFERENCES Items(PartNo) ON DELETE CASCADE,
Quantity INT NOT NULL);

INSERT INTO Inventory (StoreNo, PartNo, Quantity)
VALUES
(1, 1, 7);

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