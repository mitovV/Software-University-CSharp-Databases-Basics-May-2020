  SELECT c.CountryCode, COUNT(mc.MountainId) AS MountainRanges
    FROM Countries c
    JOIN MountainsCountries mc
      ON mc.CountryCode = c.CountryCode
   WHERE c.CountryCode IN ('BG', 'US', 'RU')
GROUP BY c.CountryCode