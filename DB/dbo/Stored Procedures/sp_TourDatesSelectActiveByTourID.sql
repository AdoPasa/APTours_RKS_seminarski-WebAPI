CREATE PROCEDURE sp_TourDatesSelectActiveByTourID
	@TourID INT
AS
SET NOCOUNT ON 
SET XACT_ABORT ON  

BEGIN TRAN
	DECLARE @NumberOfPlaces INT = (SELECT NumberOfPlaces FROM Tours WHERE TourID = @TourID)

	SELECT T1.*, (@NumberOfPlaces - NumberOfReservedPlaces) AS NumberOfFreePlaces
	FROM TourDates AS T1
		 OUTER APPLY (SELECT ISNULL(SUM(I1.NumberOfPassengers), 0) AS NumberOfReservedPlaces
							 FROM TourReservations AS I1 
							 WHERE I1.TourDateID = T1.TourDateID) AS ReservedPlaces
	WHERE T1.TourID = @TourID AND
		  T1.Date > GETDATE() AND
		  (@NumberOfPlaces - NumberOfReservedPlaces) > 0		  
COMMIT