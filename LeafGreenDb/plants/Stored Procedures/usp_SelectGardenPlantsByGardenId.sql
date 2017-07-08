-- =============================================
-- Author:		Nick Ganter
-- Create date: 7/8/2017
-- Description:	Selects all plants for a garden id
-- =============================================

CREATE PROC plants.usp_SelectGardenPlantsByGardenId
	@GardenId INT
AS
BEGIN 
    SET XACT_ABORT ON;
	SET NOCOUNT ON;

	BEGIN TRY 

	SELECT
		gp.GardenPlantId
		,gp.PlantId
		,p.CommonName
		,p.Family
		,p.ScientificName
		,p.Symbol
		,p.PlantHash
	FROM
		plants.GardenPlants gp
		INNER JOIN plants.Plants p ON p.PlantId = gp.PlantId
	WHERE
		gp.GardenId = @GardenId

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