  SELECT PeakName, RiverName,
		 LOWER(PeakName + SUBSTRING(RiverName, 2, LEN(RiverName))) AS Mix 
    FROM Peaks,Rivers
   WHERE LOWER(RIGHT(PeakName,1)) = LOWER(LEFT(RiverName,1))
ORDER BY Mix
