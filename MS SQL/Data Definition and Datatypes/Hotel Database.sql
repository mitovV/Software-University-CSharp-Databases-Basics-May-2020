CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL, 
	LastName NVARCHAR(50) NOT NULL, 
	Title VARCHAR(50) NOT NULL, 
	Notes VARCHAR(MAX)
)

CREATE TABLE Customers(
	AccountNumber INT PRIMARY KEY NOT NULL,
	FirstName NVARCHAR(50) NOT NULL, 
	LastName NVARCHAR(50) NOT NULL, 
	PhoneNumber VARCHAR(10),
	EmergencyName NVARCHAR(100) NOT NULL, 
	EmergencyNumber NVARCHAR(100) NOT NULL, 
	Notes VARCHAR(MAX)
)

CREATE TABLE RoomStatus(
	RoomStatus VARCHAR(10) PRIMARY KEY NOT NULL, 
	Notes VARCHAR(MAX)
)

CREATE TABLE RoomTypes(
	RoomType VARCHAR(20) PRIMARY KEY NOT NULL, 
	Notes VARCHAR(MAX)
)

CREATE TABLE BedTypes(
	BedType VARCHAR(10) PRIMARY KEY NOT NULL, 
	Notes VARCHAR(MAX)
)

CREATE TABLE Rooms(
	RoomNumber INT PRIMARY KEY NOT NULL, 
	[RoomType] VARCHAR(20) FOREIGN KEY REFERENCES RoomTypes(RoomType) NOT NULL,
	BedType VARCHAR(10) FOREIGN KEY REFERENCES BedTypes(BedType) NOT NULL, 
	Rate INT, 
	RoomStatus VARCHAR(10) FOREIGN KEY REFERENCES RoomStatus(RoomStatus) NOT NULL, 
	Notes VARCHAR(MAX)
)

CREATE TABLE Payments(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL, 
	PaymentDate DATETIME2, 
	AccountNumber INT, 
	FirstDateOccupied DATETIME2, 
	LastDateOccupied DATETIME2, 
	TotalDays INT, 
	AmountCharged DECIMAL(10,2), 
	TaxRate INT, 
	TaxAmount DECIMAL(10,2), 
	PaymentTotal DECIMAL(10,2), 
	Notes VARCHAR(MAX)
)

CREATE TABLE Occupancies(
	Id INT PRIMARY KEY IDENTITY, 
	EmployeeId INT REFERENCES Employees(Id) NOT NULL, 
	DateOccupied DATETIME2, 
	AccountNumber INT, 
	RoomNumber INT, 
	RateApplied INT, 
	PhoneCharge INT, 
	Notes VARCHAR(MAX)
)

INSERT INTO Employees(FirstName, LastName, Title)
	 VALUES 
			('Test', 'Test', 'Test'),
			('Test2', 'Test2', 'Test2'),
			('Test3', 'Test3', 'Test3')

INSERT INTO Customers(AccountNumber, FirstName, LastName, EmergencyName, EmergencyNumber)
	 VALUES 
			(1, 'Test', 'Test', 'Test', 'Test'),
			(2, 'Test2', 'Test2', 'Test2', 'Test2'),			
			(3, 'Test3', 'Test3', 'Test3', 'Test3')

INSERT INTO RoomStatus(RoomStatus)
	 VALUES 
			('Test'),
			('Test2'),
			('Test3')

INSERT INTO RoomTypes(RoomType)
	 VALUES	
			('Test'),
			('Test2'),
			('Test3')

INSERT INTO	BedTypes(BedType)
	 VALUES 
			('Test'),
			('Test2'),
			('Test3')

INSERT INTO Rooms(RoomNumber, RoomType, BedType, RoomStatus)
	 VALUES 
			(1, 'Test', 'Test', 'Test'),
			(2, 'Test2', 'Test2', 'Test2'),
			(3, 'Test3', 'Test3', 'Test3')

INSERT INTO Payments(EmployeeId)
	 VALUES 
			(1),
			(2),
			(3)

INSERT INTO Occupancies(EmployeeId)
	 VALUES 
			(1),
			(2),
			(3)
