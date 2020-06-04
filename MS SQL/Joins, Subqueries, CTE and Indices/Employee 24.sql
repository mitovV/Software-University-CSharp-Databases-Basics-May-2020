  SELECT e.EmployeeID, e.FirstName,
         IIF(DATEPART(YEAR, p.StartDate) >= 2005, NULL, p.[Name]) AS ProjectName
    FROM Employees e 
	JOIN EmployeesProjects ep
	  ON ep.EmployeeID = e.EmployeeID
	JOIN Projects p
	  ON p.ProjectID = ep.ProjectID
   WHERE e.EmployeeID = 24
ORDER BY e.EmployeeID 