CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(50) NOT NULL,
	DailyRate INT,
	WeeklyRate INT,
	MonthlyRate INT,
	WeekendRate INT,
)

CREATE TABLE Cars(
	Id INT PRIMARY KEY IDENTITY,
	PlateNumber VARCHAR(7) NOT NULL,
	Manufacturer NVARCHAR(20) NOT NULL,
	Model VARCHAR(10) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Doors INT NOT NULL,
	CHECK(Doors < 6),
	Picture VARBINARY(MAX),
	CHECK(DATALENGTH(Picture) <= 10 * 1024),
	Condition VARCHAR(10) NOT NULL,
	Available BIT NOT NULL, 
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL, 
	LastName NVARCHAR(50) NOT NULL,
	Title VARCHAR(30) NOT NULL,
	Notes NVARCHAR(MAX),
)

CREATE TABLE Customers(
	Id INT PRIMARY KEY IDENTITY,
	DriverLicenceNumber VARCHAR(30) NOT NULL,
	FullName NVARCHAR(100) NOT NULL,
	[Address] NVARCHAR(100) NOT NULL,
	City NVARCHAR(30) NOT NULL,
	ZIPCode INT NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RentalOrders(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
	CarId INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL,
	TankLevel INT,
	KilometrageStart INT,
	KilometrageEnd INT,
	TotalKilometrage INT,
	StartDate DATETIME2,
	EndDate DATETIME2,
	TotalDays INT,
	RateApplied INT,
	TaxRate INT,
	OrderStatus VARCHAR(20),
	Notes VARCHAR(MAX)
)

INSERT INTO Categories(CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
	 VALUES 
			('Test', 1, 2, 3, 4),
			('Test2', 1, 2, 3, 4),
			('Test3', NULL, NULL, NULL, NULL)

INSERT INTO Cars(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
	 VALUES 
			('Test', 'Test', 'Test', 2002, 1, 3, NULL, 'Test', 0),
			('Test2', 'Test2', 'Test2', 1999, 2, 5, NULL, 'Test2',1),
			('Test3', 'Test3', 'Test3', 2019, 3, 5, NULL, 'Test3', 0)

INSERT INTO Employees(FirstName, LastName, Title, Notes)
	 VALUES 
			('Test', 'Test', 'Test', NULL),
			('Test2', 'Test2', 'Test2', NULL),
			('Test3', 'Test3', 'Test3', NULL)

INSERT INTO Customers(DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes)
	 VALUES 
			('Test', 'Test', 'Test', 'Test', 21, NULL),
			('Test2', 'Test2', 'Test2', 'Test2', 44, NULL),
			('Test3', 'Test3', 'Test3', 'Test3', 100, NULL)

INSERT INTO RentalOrders([EmployeeId], [CustomerId], [CarId], [KilometrageStart])
     VALUES (2, 1, 3, 1253620),
			(1, 2, 3, 1236322036),
			(3, 3, 1, 1523692);