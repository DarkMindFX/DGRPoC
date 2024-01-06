USE [DGRPoC]
GO
/****** Object:  StoredProcedure [dbo].[p_GetPortfolios]    Script Date: 1/6/2024 7:01:14 PM ******/
DROP PROCEDURE [dbo].[p_GetPortfolios]
GO
/****** Object:  StoredProcedure [dbo].[p_GetPortfolios]    Script Date: 1/6/2024 7:01:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[p_GetPortfolios]
	
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT top 1
		ID,
		Name
	FROM
		dbo.Portfolio
END
GO
