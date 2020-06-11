CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(7) AS
BEGIN
	DECLARE @level VARCHAR(7)

	IF(@salary < 30000)
	BEGIN
	SET	@level = 'Low'
	END
	ELSE IF (@salary < 50000)
	BEGIN
	SET	@level = 'Average'
	END
	ELSE IF (@salary > 50000)
	BEGIN
	SET	@level = 'High'
	END

	RETURN  @level
END