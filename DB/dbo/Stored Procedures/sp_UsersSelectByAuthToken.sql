
CREATE PROCEDURE sp_UsersSelectByAuthToken
	@AuthToken NVARCHAR(100)
AS BEGIN
	SELECT T1.*
	FROM Users AS T1
		 JOIN AuthenticationTokens AS T2 ON T2.UserID = T1.UserID
	WHERE T2.AuthenticationToken = @AuthToken AND T2.IsDeleted = 0
END