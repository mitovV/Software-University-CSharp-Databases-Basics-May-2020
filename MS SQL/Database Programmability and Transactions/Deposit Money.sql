CREATE PROC usp_DepositMoney (@AccountId INT, @MoneyAmount DECIMAL(18,4))
AS 
BEGIN

	IF(@MoneyAmount <= 0)
	RETURN

	IF((SELECT COUNT(*) FROM Accounts WHERE Id = @AccountId) > 0)
	BEGIN
		UPDATE Accounts 
		SET Balance += @MoneyAmount
		WHERE Id = @AccountId
	END
END