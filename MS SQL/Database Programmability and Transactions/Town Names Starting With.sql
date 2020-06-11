CREATE PROC usp_GetTownsStartingWith @string NVARCHAR(MAX) AS
SELECT [Name]
  FROM Towns
 WHERE [Name] LIKE  @string + '%'