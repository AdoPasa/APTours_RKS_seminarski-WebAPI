CREATE PROCEDURE sp_ToursDTOSelectUpcomingByParameters 
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
				T3.TourReservationID,
				T5.Date AS ReservedAt,
				T3.NumberOfPassengers,
				CAST(CASE WHEN T4.TourID IS NULL THEN 0 ELSE 1 END AS BIT) AS 'Favorite',				
				CAST(0 AS BIT) AS 'CanAddReview'
	FROM Tours AS T1
		 JOIN TourDates AS T2 ON T2.TourID = T1.TourID
		 JOIN TourReservations AS T3 ON T3.TourDateID = T2.TourDateID AND T3.UserID = @UserID
		 LEFT JOIN FavoriteTours AS T4 ON T4.TourID = T1.TourID AND T4.UserID = @UserID
		 JOIN TourDates AS T5 ON T5.TourDateID = T3.TourDateID
	WHERE T1.Active = 1 AND T2.Date > GETDATE()
	ORDER BY T5.Date ASC
	OFFSET (@Page*@NumberOfResults) ROWS
	FETCH NEXT @NumberOfResults ROWS ONLY
END