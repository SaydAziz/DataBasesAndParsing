USE [PROG260FA23]
GO
ALTER TABLE [dbo].[CharInfo] DROP CONSTRAINT [FK_CharInfo_Characters]
GO
ALTER TABLE [dbo].[Characters] DROP CONSTRAINT [FK_Characters_Locations]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 10/22/2023 12:03:01 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Locations]') AND type in (N'U'))
DROP TABLE [dbo].[Locations]
GO
/****** Object:  Table [dbo].[CharInfo]    Script Date: 10/22/2023 12:03:01 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CharInfo]') AND type in (N'U'))
DROP TABLE [dbo].[CharInfo]
GO
/****** Object:  Table [dbo].[Characters]    Script Date: 10/22/2023 12:03:01 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Characters]') AND type in (N'U'))
DROP TABLE [dbo].[Characters]
GO
/****** Object:  Table [dbo].[Characters]    Script Date: 10/22/2023 12:03:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Characters](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Character] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Map_Location] [int] NULL,
 CONSTRAINT [PK_Characters] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CharInfo]    Script Date: 10/22/2023 12:03:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CharInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CharID] [int] NOT NULL,
	[IsOC] [bit] NOT NULL,
	[IsSwordFighter] [bit] NOT NULL,
	[IsMagical] [bit] NOT NULL,
 CONSTRAINT [PK_CharInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 10/22/2023 12:03:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Locations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Characters]  WITH CHECK ADD  CONSTRAINT [FK_Characters_Locations] FOREIGN KEY([Map_Location])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Characters] CHECK CONSTRAINT [FK_Characters_Locations]
GO
ALTER TABLE [dbo].[CharInfo]  WITH CHECK ADD  CONSTRAINT [FK_CharInfo_Characters] FOREIGN KEY([CharID])
REFERENCES [dbo].[Characters] ([ID])
GO
ALTER TABLE [dbo].[CharInfo] CHECK CONSTRAINT [FK_CharInfo_Characters]
GO
