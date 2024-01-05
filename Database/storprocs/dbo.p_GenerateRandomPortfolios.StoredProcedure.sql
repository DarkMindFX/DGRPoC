USE [DGRPoC]
GO
/****** Object:  StoredProcedure [dbo].[p_GenerateRandomPortfolios]    Script Date: 1/5/2024 11:04:17 AM ******/
DROP PROCEDURE [dbo].[p_GenerateRandomPortfolios]
GO
/****** Object:  StoredProcedure [dbo].[p_GenerateRandomPortfolios]    Script Date: 1/5/2024 11:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Usage:
EXEC p_GenerateRandomPortfolios @Count = 100
*/

CREATE PROCEDURE [dbo].[p_GenerateRandomPortfolios]
	@Count INT = 100
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @i AS INT
	SET @i = @Count

	WHILE @i > 0 
	BEGIN
		INSERT INTO dbo.Portfolio ([Name])
		SELECT CONCAT( 'Portfolio-', FLOOR(RAND() * 100000))

		SET @i = @i - 1
	END	
    
END
GO
