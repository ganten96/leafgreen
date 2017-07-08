-- =============================================
-- Author:		Nick Ganter
-- Create date: 6/17/2017
-- Description:	Inserts a garden
-- =============================================

CREATE PROC [plants].[usp_InsertGarden]
	@GardenName VARCHAR(256)
	,@IsArchived BIT
	,@DateAdded DATETIME2(7)
	,@Latitude FLOAT
	,@Longitude FLOAT
	,@DeviceId VARCHAR(36)
AS
BEGIN 
    SET XACT_ABORT ON;
	SET NOCOUNT ON;

	BEGIN TRY 
	
	
	INSERT INTO plants.Gardens
	(
		GardenName
		,IsArchived
		,DateAdded
		,[Location]
		,DeviceId
	)	
	VALUES
	(
		@GardenName
		,@IsArchived
		,@DateAdded
		,geography::Point(@Latitude, @Longitude, 4326)
		,@DeviceId
	)

	DECLARE @lastInsertedGarden INT = (SELECT CAST(SCOPE_IDENTITY() AS INT))
	SELECT
		g.GardenId
		,g.GardenName
		,g.DateAdded
		,g.IsArchived
		,g.Location.Lat AS Latitude
		,g.Location.Long AS Longitude
	FROM
		plants.Gardens g
	WHERE
		g.GardenId = @lastInsertedGarden

	IF XACT_STATE() = 1
		COMMIT TRANSACTION;
	END Try 
	BEGIN CATCH
		DECLARE	@ErrorNumber INT,
				@ErrorLine INT,
				@ErrorSeverity INT,
				@ErrorState INT,
				@ErrorProcedure NVARCHAR(128),
				@ErrorMessage NVARCHAR(4000);
		SELECT
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE(),
			@ErrorMessage = ERROR_MESSAGE(),
			@ErrorNumber = ERROR_NUMBER(),
			@ErrorLine = ERROR_LINE(),
			@ErrorProcedure = ERROR_PROCEDURE();
		PRINT 'Error Severity: ' + CAST(@ErrorSeverity AS VARCHAR(10));
		PRINT 'Error Satte: ' + CAST(@ErrorState AS VARCHAR(10));
		PRINT 'Error number: ' + CAST(@ErrorNumber AS VARCHAR(10));
		PRINT 'Error line number: ' + CAST(@ErrorLine AS VARCHAR(10));
		PRINT 'Error Procedure: ' + @ErrorProcedure;
		PRINT 'Error Message: ' + @ErrorMessage;
        IF XACT_STATE() = -1 ROLLBACK TRANSACTION;
		THROW;
     END CATCH
END