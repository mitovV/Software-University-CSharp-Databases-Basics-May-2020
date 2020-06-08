 SELECT tmp.DepartmentID, tmp.Salary
   FROM (  SELECT DepartmentID, Salary,
		          DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS RankSalary
             FROM Employees
		 GROUP BY DepartmentID, Salary) AS tmp
  WHERE tmp.RankSalary = 3
