CREATE PROCEDURE sp_ToursDTOSelectSavedByParameters 
	@UserID INT,
	@Page INT,
	@NumberOfResults INT
AS BEGIN
	SELECT 	T1.TourID,
				T1.Title,
				T1.Description,
				T1.Image,
				T1.Price,
				T1.NumberOfReviews,
				T1.Grade,
				UpcomingDate.Date AS UpcomingDate,
				CAST(1 AS BIT) AS 'Favorite',				
				CAST(0 AS BIT) AS 'CanAddReview'
	FROM Tours AS T1
		 JOIN FavoriteTours AS T2 ON T2.TourID = T1.TourID AND T2.UserID = @UserID
		 OUTER APPLY (SELECT TOP 1 I1.Date FROM TourDates AS I1 
					  WHERE TourID = T1.TourID AND 
							I1.Date > GETDATE() AND
							T1.NumberOfPlaces > (SELECT ISNULL(SUM(NumberOfPassengers), 0) FROM TourReservations AS II1 WHERE II1.TourDateID = I1.TourDateID)
					  ORDER BY Date ASC
					 ) AS UpcomingDate
	WHERE T1.Active = 1
	ORDER BY IIF(UpcomingDate.Date IS NULL, 1, 0), UpcomingDate.Date ASC, T1.TourID DESC
	OFFSET (@Page*@NumberOfResults) ROWS
	FETCH NEXT @NumberOfResults ROWS ONLY
END