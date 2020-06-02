  SELECT [Name] AS Game,
    CASE 
	WHEN DATEPART(HOUR, [Start]) <= 11 THEN 'Morning'
	WHEN DATEPART(HOUR, [Start]) <= 17 THEN 'Afternoon'
	ELSE 'Evening'
	END AS [Part of the Day],
	CASE
    WHEN Duration <= 3 THEN 'Extra Short'
    WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
    WHEN Duration > 6 THEN 'Long'
    WHEN Duration IS NULL THEN 'Extra Long'
  END AS Duration
    FROM Games
ORDER BY Game, Duration, [Part of the Day]