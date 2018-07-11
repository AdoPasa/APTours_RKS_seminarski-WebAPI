CREATE PROCEDURE sp_ToursDTOSelectFinishedByParameters 
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
				T2.NumberOfPassengers,
				T3.Date AS ReservedAt,
				CAST(0 AS BIT) AS 'Favorite',				
				CASE WHEN [T4].[TourReviewID] IS NULL THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS 'CanAddReview'
	FROM Tours AS T1
		 JOIN TourReservations AS T2 ON T2.TourID = T1.TourID
		 JOIN TourDates AS T3 ON T3.TourDateID = T2.TourDateID
		 LEFT JOIN TourReviews AS T4 ON T4.TourReservationID = T2.TourReservationID
	WHERE T2.UserID = @UserID AND T3.Date < GETDATE()
	ORDER BY T3.Date DESC
	OFFSET (@Page*@NumberOfResults) ROWS
	FETCH NEXT @NumberOfResults ROWS ONLY
END