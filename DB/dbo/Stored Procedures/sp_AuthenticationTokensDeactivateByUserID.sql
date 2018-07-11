
CREATE PROCEDURE sp_AuthenticationTokensDeactivateByUserID
	@UserID INT
AS BEGIN
	UPDATE AuthenticationTokens 
	SET IsDeleted = 1, DateTimeDeleted = GETDATE()
	WHERE IsDeleted = 0 AND UserID = @UserID
END