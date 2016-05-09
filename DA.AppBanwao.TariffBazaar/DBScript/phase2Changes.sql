use TariffBazaar
ALTER TABLE item
ADD ForRent bit

ALTER TABLE item 
ADD ForSale bit

ALTER TABLE item 
ADD SaleAmount decimal(15, 2)

GO
INSERT INTO [dbo].[Roles]
           ([RoleName])
     VALUES
           ('Vendor')
GO

GO

/****** Object:  Table [dbo].[Logs]    Script Date: 10/06/2015 11:10:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[Logs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[MessageType] [varchar](50) NULL,
	[Message] [nvarchar](max) NULL,
	[MethodName] [varchar](70) NULL,
	[FileName] [varchar](50) NULL,
	[UserName] [varchar](255) NULL,
	[EventTimeStamp] [datetime] NULL,
	[IpAddress] [varchar](20) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


GO

/****** Object:  Table [dbo].[UserDetails]    Script Date: 10/06/2015 14:29:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[UserDetails](
	[UserDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Gender] [int] NULL,
	[DOB] [date] NULL,
	[Profession] [varchar](50) NULL,
	[CityId] [int] NULL,
	[CountryId] [int] NULL,
	[Pincode] [varchar](15) NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]





GO

/****** Object:  Table [dbo].[Countries]    Script Date: 10/06/2015 16:03:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Countries](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](10) NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



INSERT INTO [dbo].[Countries]
           ([Name])
     VALUES
           ('India')
GO

