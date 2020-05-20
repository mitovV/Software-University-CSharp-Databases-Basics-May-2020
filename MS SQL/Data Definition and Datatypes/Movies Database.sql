CREATE TABLE Directors(
	Id INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes VARCHAR(MAX)
)

CREATE TABLE Genres(
	Id INT PRIMARY KEY IDENTITY,
	GenreName NVARCHAR(50) NOT NULL,
	Notes VARCHAR(MAX)
)

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes VARCHAR(MAX)
)

CREATE TABLE Movies(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(50) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
	CopyrightYear INT NOT NULL,
	[Length] TIME NOT NULL,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Rating INT,
	Notes VARCHAR(MAX)
) 

INSERT INTO Directors(DirectorName, Notes)
	 VALUES 
			('Test', 'test test'),			
			('Test2', 'test test'),
			('Test3', 'test test'),
			('Test4', NULL),
			('Test5', NULL)

INSERT INTO Genres(GenreName, Notes)
     VALUES 
		    ('Test', 'test test'),			
		    ('Test2', 'test test'),
			('Test3', 'test test'),
			('Test4', NULL),
			('Test5', NULL)

INSERT INTO Categories(CategoryName, Notes)
	 VALUES
		    ('Test', 'test test'),			
		    ('Test2', 'test test'),
			('Test3', 'test test'),
			('Test4', NULL),
			('Test5', NULL)

INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
	 VALUES
			('Test', 1, 2002, '1:22:43', 2, 1, 5, NULL),
			('Test2', 2, 1999, '1:54:11', 1,2, 12, NULL),
			('Test3', 3, 1992, '1:32:23', 4, 3, 10, NULL),
			('Test4', 4, 2019, '2:10:55', 3, 4, 5, NULL),
			('Test5', 5, 2020, '1:30:14', 5, 5, 6, NULL)