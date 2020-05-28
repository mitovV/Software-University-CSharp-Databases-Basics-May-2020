CREATE TABLE Manufacturers(
	ManufacturerID INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50),
	EstablishedOn SMALLDATETIME
)


CREATE TABLE Models(
	ModelID INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(50) NOT NULL,
	ManufacturerID INT FOREIGN KEY REFERENCES  Manufacturers(ManufacturerID)
)