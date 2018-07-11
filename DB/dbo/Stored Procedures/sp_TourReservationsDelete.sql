CREATE PROCEDURE sp_TourReservationsDelete
	@UserID INT,
	@TourID INT,
    @TourReservationID INT
AS 
SET NOCOUNT ON 
SET XACT_ABORT ON  

BEGIN TRAN	
	DECLARE @NumberOfDeletedRows INT = 0

	DELETE FROM TourReservations
		   FROM TourReservations AS T1
				JOIN TourDates AS T2 ON T2.TourDateID = T1.TourDateID
	WHERE T1.UserID = @UserID AND
		  T1.TourID = @TourID AND
		  T1.TourReservationID = @TourReservationID AND 
		  T2.Date >= DATEADD(DAY, 7, GETDATE())


	SELECT @NumberOfDeletedRows = @@ROWCOUNT

	IF @NumberOfDeletedRows != 1
		RAISERROR('Rezervacija nije izbrisana', 16, 1)
	ELSE 
		UPDATE Users SET NumberOfTours -= 1 WHERE UserID = @UserID
COMMIT