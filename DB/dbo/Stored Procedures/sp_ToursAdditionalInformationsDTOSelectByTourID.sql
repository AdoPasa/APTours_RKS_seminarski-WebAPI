CREATE PROCEDURE sp_ToursAdditionalInformationsDTOSelectByTourID
	@TourID INT
AS BEGIN
	SELECT -1 AS TourAdditionalInformationID, 'Trajanje izleta (dan)' AS [AdditionalInformationType], CAST(DurationInDays AS NVARCHAR(30)) AS [Value]
	FROM Tours 
	WHERE TourID = @TourID
	UNION
	SELECT 0 AS TourAdditionalInformationID, 'Broj mjesta' AS [AdditionalInformationType], CAST(NumberOfPlaces AS NVARCHAR(30)) AS [Value]
	FROM Tours 
	WHERE TourID = @TourID
	UNION
	SELECT T1.TourAdditionalInformationID, T2.[Value] AS [AdditionalInformationType], T1.[Value]
	FROM ToursAdditionalInformations AS T1
		 JOIN AdditionalInformationTypes AS T2 ON T2.AdditionalInformationTypeID = T1.AdditionalInformationTypeID
	WHERE T1.TourID = @TourID
END