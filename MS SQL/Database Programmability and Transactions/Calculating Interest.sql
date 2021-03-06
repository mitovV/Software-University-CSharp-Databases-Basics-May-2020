CREATE PROC usp_CalculateFutureValueForAccount(@AccountId INT, @InterestRate FLOAT)
AS
BEGIN
	SELECT TOP(1) ah.Id AS [Account Id], 
		   ah.FirstName AS [First Name],
		   ah.LastName AS [Last Name],
		   a.Balance AS [Current Balance],
		   dbo.ufn_CalculateFutureValue(a.Balance, @InterestRate, 5) AS [Balance in 5 years]
	  FROM AccountHolders ah
	  JOIN Accounts a
		ON ah.Id = a.AccountHolderId
	 WHERE ah.Id = @AccountId
END
