  SELECT tmp.ContinentCode, tmp.CurrencyCode, tmp.CurrencyUsage 
    FROM (  SELECT ContinentCode, CurrencyCode,
			 	   COUNT(CurrencyCode) AS CurrencyUsage,
				   DENSE_RANK() OVER(PARTITION BY ContinentCode ORDER BY COUNT(CurrencyCode) DESC) AS RankCurrency
			  FROM Countries
		  GROUP BY ContinentCode, CurrencyCode
		    HAVING COUNT(CurrencyCode) > 1) AS tmp
   WHERE tmp.RankCurrency = 1
ORDER BY tmp.ContinentCode