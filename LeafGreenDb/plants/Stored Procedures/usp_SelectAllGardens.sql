-- =============================================
-- Author:		Nick Ganter
-- Create date: 6/17/2017
-- Description:	Selects All Gardens
-- =============================================

CREATE PROC plants.usp_SelectAllGardens

AS
BEGIN 
    SET XACT_ABORT ON;
	SET NOCOUNT ON;

	BEGIN TRY 

	SELECT
		GardenName
		,IsArchived
		,DateAdded
		,[Location].Lat AS Latitude
		,[Location].Long AS Longitude
		,GardenId
	FROM
		plants.Gardens

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