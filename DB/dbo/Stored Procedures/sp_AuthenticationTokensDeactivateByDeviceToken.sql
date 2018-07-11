
CREATE PROCEDURE sp_AuthenticationTokensDeactivateByDeviceToken
	@DeviceToken NVARCHAR(100)
AS BEGIN
	UPDATE AuthenticationTokens 
	SET IsDeleted = 1, DateTimeDeleted = GETDATE()
	WHERE IsDeleted = 0 AND DeviceToken = @DeviceToken
END