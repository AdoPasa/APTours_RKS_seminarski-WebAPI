CREATE PROCEDURE sp_TourReviewsInsert
	@UserID INT,
    @TourID INT,
    @Grade INT,
	@Review NVARCHAR(250)
AS 
SET NOCOUNT ON 
SET XACT_ABORT ON  

BEGIN TRAN	
	DECLARE @TourReservationID INT = (SELECT TOP 1 T1.TourReservationID
									  FROM TourReservations AS T1
									  	   LEFT JOIN TourReviews AS T2 ON T2.TourReservationID = T1.TourReservationID
										   JOIN TourDates AS T3 ON T3.TourDateID = T1.TourDateID
									  WHERE  T1.UserID = @UserID AND T1.TourID = @TourID AND T3.Date < GETDATE() AND T2.TourReviewID IS NULL)

	IF @TourReservationID IS NOT NULL BEGIN
		INSERT INTO TourReviews VALUES (@UserID, @TourID, @TourReservationID, @Grade, @Review, GETDATE())

		UPDATE Users SET NumberOfReviews += 1 WHERE UserID = @UserID
		UPDATE Tours SET NumberOfReviews += 1, GradeSum += @Grade, Grade = (GradeSum + @Grade)/CAST((NumberOfReviews + 1) AS decimal(18,2)) WHERE TourID = @TourID
	END
	ELSE 
		RAISERROR('Rezervacija bez recenzije nije pronadjena', 16, 1)
COMMIT