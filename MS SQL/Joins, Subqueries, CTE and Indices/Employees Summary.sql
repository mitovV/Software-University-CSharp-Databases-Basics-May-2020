  SELECT TOP(50) e.EmployeeID,
		 CONCAT(e.FirstName, ' ', e.LastName) AS EmployeeName,
		 CONCAT(mng.FirstName, ' ', mng.LastName) AS ManagerName,
		 d.[Name] AS DepartmentName
    FROM Employees e 
	JOIN Employees mng
	  ON mng.EmployeeID = e.ManagerID
	JOIN Departments d
	  ON d.DepartmentID = e.DepartmentID
ORDER BY e.EmployeeID 