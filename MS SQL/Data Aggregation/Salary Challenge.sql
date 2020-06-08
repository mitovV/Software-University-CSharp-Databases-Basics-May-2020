SELECT TOP(10) FirstName, LastName, DepartmentID
  FROM Employees e1
 WHERE Salary > (SELECT AVG(Salary)
				   FROM Employees e2
				  WHERE e1.DepartmentID = e2.DepartmentID)
