CREATE PROC usp_GetEmployeesFromTown  @town NVARCHAR(MAX) AS
SELECT FirstName,LastName
  FROM Employees e
  JOIN Addresses a
    ON a.AddressID = e.AddressID
  JOIN Towns t
    ON t.TownID = a.TownID
 WHERE t.[Name] =  @town