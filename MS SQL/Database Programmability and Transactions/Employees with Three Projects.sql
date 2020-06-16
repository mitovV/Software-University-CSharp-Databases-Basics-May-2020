CREATE PROC usp_AssignProject(@emloyeeId INT, @projectID INT)
AS
BEGIN

	IF((SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @emloyeeId) >= 3)
	BEGIN
		RAISERROR('The employee has too many projects!', 16, 1);
	END

	INSERT INTO EmployeesProjects
	SELECT @emloyeeId, @projectID
END