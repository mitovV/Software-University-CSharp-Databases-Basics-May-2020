SELECT MIN(Salary) AS MinAverageSalary
  FROM (  SELECT d.[Name], AVG(Salary) AS Salary
		    FROM Employees e 
		 	JOIN Departments d
		      ON d.DepartmentID = e.DepartmentID
		GROUP BY d.[Name]) AS tmp 