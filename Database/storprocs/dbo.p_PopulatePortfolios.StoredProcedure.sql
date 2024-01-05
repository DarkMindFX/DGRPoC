USE [DGRPoC]
GO
/****** Object:  StoredProcedure [dbo].[p_PopulatePortfolios]    Script Date: 1/5/2024 11:04:17 AM ******/
DROP PROCEDURE [dbo].[p_PopulatePortfolios]
GO
/****** Object:  StoredProcedure [dbo].[p_PopulatePortfolios]    Script Date: 1/5/2024 11:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Usage:
EXEC p_PopulatePortfolios @Date = '2023-01-10'
*/

CREATE PROCEDURE [dbo].[p_PopulatePortfolios]
	@Date DATE
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE Portfolios_Cur CURSOR FOR
	SELECT p.ID, p.Name
	FROM dbo.Portfolio p

	OPEN Portfolios_Cur

	DECLARE @PortfolioID AS BIGINT
	DECLARE @Name AS NVARCHAR(50)

	FETCH NEXT FROM Portfolios_Cur
	INTO @PortfolioID, @Name

	DECLARE @Count AS INT = 0

	WHILE @@FETCH_STATUS = 0
	BEGIN

		SET @Count = 10000 + FLOOR(RAND() * 10000)

		EXEC dbo.p_GenerateRandomPortfolioValues 
			@PortfolioID = @PortfolioID, 
			@Date = @Date, 
			@Count = @Count

		PRINT(CONCAT('Portfolio ', @Name, ' Done: ', @Date, ', Records - ', @Count))

		FETCH NEXT FROM Portfolios_Cur
		INTO @PortfolioID, @Name

	END

	CLOSE Portfolios_Cur;
	DEALLOCATE Portfolios_Cur;

    
END
GO
