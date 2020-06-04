  SELECT e.EmployeeID, e.FirstName, emp.EmployeeID, emp.FirstName
    FROM Employees e 
	JOIN Employees emp
	  ON emp.EmployeeID = e.ManagerID
   WHERE emp.EmployeeID IN (3, 7)
ORDER BY e.EmployeeID 