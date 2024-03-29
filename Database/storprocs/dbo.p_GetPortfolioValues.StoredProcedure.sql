USE [DGRPoC]
GO
/****** Object:  StoredProcedure [dbo].[p_GetPortfolioValues]    Script Date: 1/6/2024 7:01:14 PM ******/
DROP PROCEDURE [dbo].[p_GetPortfolioValues]
GO
/****** Object:  StoredProcedure [dbo].[p_GetPortfolioValues]    Script Date: 1/6/2024 7:01:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_GetPortfolioValues]
	@PortfolioID BIGINT,
	@BusinessDate DATETIME
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT
		ID,
		PortfolioID,
		Date,
		Value1,
		Value2,
		Value3,
		Value4,
		Value5
	FROM
		dbo.PortfolioValues
	WHERE 1=1
		AND PortfolioID = @PortfolioID
		AND Date = @BusinessDate
END
GO
