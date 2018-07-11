CREATE PROCEDURE sp_TourReservationsInsert
	@UserID INT,
    @TourID INT,
    @TourDateID INT,
	@NumberOfPassengers INT
AS 
SET NOCOUNT ON 
SET XACT_ABORT ON  

BEGIN TRAN	
	SET @TourDateID =  (SELECT T1.TourDateID
						FROM TourDates AS T1
							 JOIN Tours AS T2 ON T2.TourID = T1.TourID
							 OUTER APPLY (SELECT ISNULL(SUM(I1.NumberOfPassengers), 0) AS NumberOfReservedPlaces
												 FROM TourReservations AS I1 
												 WHERE I1.TourDateID = T1.TourDateID) AS ReservedPlaces
						WHERE T1.TourDateID = @TourDateID AND
							  (T2.NumberOfPlaces - NumberOfReservedPlaces) > 0)	

	IF @TourDateID IS NOT NULL BEGIN
		DECLARE @TourReservationID INT = (SELECT TourReservationID FROM TourReservations WHERE TourDateID = @TourDateID AND UserID = @UserID)

		IF @TourReservationID IS NULL BEGIN
			INSERT INTO TourReservations VALUES (@UserID, @TourID, @TourDateID, @NumberOfPassengers, GETDATE())
			UPDATE Users SET NumberOfTours += 1 WHERE UserID = @UserID
		END 
		ELSE BEGIN
			UPDATE TourReservations SET NumberOfPassengers += @NumberOfPassengers WHERE TourReservationID = @TourReservationID
		END
	END
	ELSE 
		RAISERROR('Nije moguce dodati rezervaciju (broj slobodnih mjesta nije validan)', 16, 1)
COMMIT