CREATE PROC usp_GetHoldersWithBalanceHigherThan(@balance DECIMAL(18,4))
AS
BEGIN
	  SELECT ah.FirstName, ah.LastName
	    FROM AccountHolders ah
	    JOIN Accounts a
		  ON ah.Id = a.AccountHolderId
	GROUP BY ah.FirstName, ah.LastName
	  HAVING SUM(a.Balance) > @balance
	ORDER BY ah.FirstName, ah.LastName	
END