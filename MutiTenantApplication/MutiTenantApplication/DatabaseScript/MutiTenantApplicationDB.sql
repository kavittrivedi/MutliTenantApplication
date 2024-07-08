USE [MutiTenantApplicationDB]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT IF EXISTS [FK__Users__TenantId__398D8EEE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Posts]') AND type in (N'U'))
ALTER TABLE [dbo].[Posts] DROP CONSTRAINT IF EXISTS [FK__Posts__TenantId__3C69FB99]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08-07-2024 19:36:50 ******/
DROP TABLE IF EXISTS [dbo].[Users]
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 08-07-2024 19:36:50 ******/
DROP TABLE IF EXISTS [dbo].[Tenants]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 08-07-2024 19:36:50 ******/
DROP TABLE IF EXISTS [dbo].[Posts]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 08-07-2024 19:36:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostId] [int] NOT NULL,
	[TenantId] [int] NULL,
	[Title] [nvarchar](200) NULL,
	[Content] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 08-07-2024 19:36:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants](
	[TenantId] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08-07-2024 19:36:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[TenantId] [int] NULL,
	[UserName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Posts] ([PostId], [TenantId], [Title], [Content]) VALUES (1, 1, N'Title 1', N'Title 1 Content')
GO
INSERT [dbo].[Posts] ([PostId], [TenantId], [Title], [Content]) VALUES (2, 1, N'Title 2', N'Title 2 content')
GO
INSERT [dbo].[Posts] ([PostId], [TenantId], [Title], [Content]) VALUES (3, 2, N'Title 3', N'Title 3 content')
GO
INSERT [dbo].[Posts] ([PostId], [TenantId], [Title], [Content]) VALUES (4, 2, N'Title 4', N'Title 4 content')
GO
INSERT [dbo].[Tenants] ([TenantId], [Name]) VALUES (1, N'Tenant 1')
GO
INSERT [dbo].[Tenants] ([TenantId], [Name]) VALUES (2, N'Tenant 2')
GO
INSERT [dbo].[Users] ([UserId], [TenantId], [UserName]) VALUES (1, 1, N'Tenant 1 user 1')
GO
INSERT [dbo].[Users] ([UserId], [TenantId], [UserName]) VALUES (2, 1, N'Tenant 1 user2')
GO
INSERT [dbo].[Users] ([UserId], [TenantId], [UserName]) VALUES (3, 2, N'Tenant 2 user 2')
GO
INSERT [dbo].[Users] ([UserId], [TenantId], [UserName]) VALUES (4, 2, N'Tenant 2 user 2')
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO
