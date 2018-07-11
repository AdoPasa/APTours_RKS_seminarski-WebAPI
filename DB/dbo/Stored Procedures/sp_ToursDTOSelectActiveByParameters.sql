CREATE PROCEDURE sp_ToursDTOSelectActiveByParameters 
	@UserID INT,
	@Page INT,
	@NumberOfResults INT
AS BEGIN
	SELECT DISTINCT	T1.TourID,
				T1.Title,
				T1.Description,
				T1.Image,
				T1.Price,
				T1.NumberOfReviews,
				T1.Grade,
				UpcomingDate.Date AS UpcomingDate,
				CAST(CASE WHEN T3.TourID IS NULL THEN 0 ELSE 1 END AS BIT) AS 'Favorite',				
				CAST(0 AS BIT) AS 'CanAddReview'
	FROM Tours AS T1
		 LEFT JOIN FavoriteTours AS T3 ON T3.TourID = T1.TourID AND T3.UserID = @UserID
		 OUTER APPLY (SELECT TOP 1 I1.Date FROM TourDates AS I1 
					  WHERE TourID = T1.TourID AND 
							I1.Date > GETDATE() AND
							T1.NumberOfPlaces > (SELECT ISNULL(SUM(NumberOfPassengers), 0) FROM TourReservations AS II1 WHERE II1.TourDateID = I1.TourDateID)
					  ORDER BY Date ASC
					 ) AS UpcomingDate
	WHERE T1.Active = 1 AND
		  UpcomingDate.Date IS NOT NULL
	ORDER BY UpcomingDate.Date ASC, T1.TourID DESC
	OFFSET (@Page*@NumberOfResults) ROWS
	FETCH NEXT @NumberOfResults ROWS ONLY
END