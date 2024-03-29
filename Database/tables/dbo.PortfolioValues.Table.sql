USE [DGRPoC]
GO
ALTER TABLE [dbo].[PortfolioValues] DROP CONSTRAINT [FK_PortfolioValues_Portfolio]
GO
/****** Object:  Table [dbo].[PortfolioValues]    Script Date: 1/6/2024 7:00:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PortfolioValues]') AND type in (N'U'))
DROP TABLE [dbo].[PortfolioValues]
GO
/****** Object:  Table [dbo].[PortfolioValues]    Script Date: 1/6/2024 7:00:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortfolioValues](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PortfolioID] [bigint] NOT NULL,
	[Date] [date] NOT NULL,
	[Value1] [decimal](18, 4) NULL,
	[Value2] [decimal](18, 4) NULL,
	[Value3] [decimal](18, 4) NULL,
	[Value4] [decimal](18, 4) NULL,
	[Value5] [decimal](18, 4) NULL,
 CONSTRAINT [PK_Portfolio] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PortfolioValues]  WITH CHECK ADD  CONSTRAINT [FK_PortfolioValues_Portfolio] FOREIGN KEY([PortfolioID])
REFERENCES [dbo].[Portfolio] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PortfolioValues] CHECK CONSTRAINT [FK_PortfolioValues_Portfolio]
GO
