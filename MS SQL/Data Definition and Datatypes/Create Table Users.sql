CREATE TABLE Users(
	Id BIGINT PRIMARY KEY IDENTITY,
	Username VARCHAR(30) UNIQUE NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX),
	CHECK(DATALENGTH(ProfilePicture) <= 921600),
	LastLoginTime DATETIME2,
	IsDeleted  BIT NOT NULL
)

INSERT INTO Users
	 VALUES
			('Pesho', '12345', NULL, NULL, 0),
			('Gosho', '123456', NULL, NULL,1),
			('Ivan', '1234567', NULL, NULL, 0),
			('Gergana', '123456', NULL, NULL, 1),
			('Kiril', '123456', NULL, NULL, 0)