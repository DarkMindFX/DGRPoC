USE [DGRPoC]
GO
/****** Object:  StoredProcedure [dbo].[p_GenerateRandomPortfolioValues]    Script Date: 1/5/2024 11:04:17 AM ******/
DROP PROCEDURE [dbo].[p_GenerateRandomPortfolioValues]
GO
/****** Object:  StoredProcedure [dbo].[p_GenerateRandomPortfolioValues]    Script Date: 1/5/2024 11:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Usage:
EXEC dbo.p_GenerateRandomPortfolioValues 
	@PortfolioID = 400, 
	@Date = '2023-01-01', 
	@Count = 10000
*/

CREATE PROCEDURE [dbo].[p_GenerateRandomPortfolioValues]
	@PortfolioID BIGINT,
	@Date DATE,
	@Count INT = 10000
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @i AS INT
	SET @i = @Count

	WHILE @i > 0 
	BEGIN
		INSERT INTO dbo.PortfolioValues (PortfolioID, Date, Value1, Value2, Value3, Value4, Value5)
		SELECT @PortfolioID, @Date, RAND() * 1000000, RAND() * 1000000, RAND() * 1000000, RAND() * 1000000, RAND() * 1000000

		SET @i = @i - 1
	END
END
GO
