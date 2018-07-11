
CREATE PROCEDURE [dbo].[sp_TourReviewsDTOSelectByTourID]
	@TourID INT
AS BEGIN
	SELECT T1.TourReviewID, T2.FirstName + ' ' +  T2.LastName AS [User], T2.ProfilePhoto, T1.Grade, T1.Review, T1.CreatedAt AS [CreatedAtDate]
	FROM TourReviews AS T1
		 JOIN Users AS T2 ON T2.UserID = T1.UserID
	WHERE T1.TourID = @TourID
END