CREATE PROC usp_EmployeesBySalaryLevel @level VARCHAR(7) AS
SELECT tmp.FirstName, tmp.LastName 
  FROM (SELECT FirstName, LastName,
			   dbo.ufn_GetSalaryLevel(Salary) AS [Salary Level] 
		  FROM Employees) AS tmp
 WHERE tmp.[Salary Level] = @level