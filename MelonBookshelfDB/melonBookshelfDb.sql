USE [master]
GO
/****** Object:  Database [MelonBookshelf]    Script Date: 25.7.2023 17:38:22 ******/
CREATE DATABASE [MelonBookshelf]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MelonBookshelf', FILENAME = N'C:\Sql Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MelonBookshelf.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MelonBookshelf_log', FILENAME = N'C:\Sql Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MelonBookshelf_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MelonBookshelf] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MelonBookshelf].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MelonBookshelf] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MelonBookshelf] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MelonBookshelf] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MelonBookshelf] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MelonBookshelf] SET ARITHABORT OFF 
GO
ALTER DATABASE [MelonBookshelf] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MelonBookshelf] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MelonBookshelf] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MelonBookshelf] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MelonBookshelf] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MelonBookshelf] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MelonBookshelf] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MelonBookshelf] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MelonBookshelf] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MelonBookshelf] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MelonBookshelf] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MelonBookshelf] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MelonBookshelf] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MelonBookshelf] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MelonBookshelf] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MelonBookshelf] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MelonBookshelf] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MelonBookshelf] SET RECOVERY FULL 
GO
ALTER DATABASE [MelonBookshelf] SET  MULTI_USER 
GO
ALTER DATABASE [MelonBookshelf] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MelonBookshelf] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MelonBookshelf] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MelonBookshelf] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MelonBookshelf] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MelonBookshelf] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MelonBookshelf', N'ON'
GO
ALTER DATABASE [MelonBookshelf] SET QUERY_STORE = OFF
GO
USE [MelonBookshelf]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Followers]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Followers](
	[Id1] [int] IDENTITY(1,1) NOT NULL,
	[Id] [nvarchar](450) NOT NULL,
	[RequestId] [int] NOT NULL,
 CONSTRAINT [PK_Followers] PRIMARY KEY CLUSTERED 
(
	[Id1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NULL,
	[Type] [int] NULL,
	[Author] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Priority] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[Motive] [nvarchar](max) NULL,
	[DateAdded] [datetime2](7) NULL,
	[CategoryId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resources]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[ResourceId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Author] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Location] [nvarchar](max) NULL,
	[Price] [float] NULL,
	[Invoice] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[DateAdded] [datetime2](7) NULL,
	[DateTaken] [datetime2](7) NULL,
	[DateReturn] [datetime2](7) NULL,
	[CategoryId] [int] NULL,
	[UserId] [nvarchar](450) NULL,
	[FileName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Upvotes]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Upvotes](
	[UpvoteId] [int] IDENTITY(1,1) NOT NULL,
	[Id] [nvarchar](450) NOT NULL,
	[RequestId] [int] NOT NULL,
 CONSTRAINT [PK_Upvotes] PRIMARY KEY CLUSTERED 
(
	[UpvoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WantedResources]    Script Date: 25.7.2023 17:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WantedResources](
	[Id1] [int] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[Id] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_WantedResources] PRIMARY KEY CLUSTERED 
(
	[Id1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230705092016_Initial', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230705094328_MgInitial', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230705100315_margoInitial', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230706070609_resourceNullOptions', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230707073312_try', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230708200814_tryout', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230710071631_descriptionRequest', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230710082421_upvotesDto', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230711132113_rel', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230713074157_categoryidNull', N'6.0.18')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230719133043_ResourceFileName', N'6.0.18')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'User', N'user', N'BaseUser')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2', N'Admin', N'admin', N'Admin')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'68fa5512-95c7-4abb-836f-90076ff5fc97', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd12e7e9e-cc46-4f87-bfc7-50f8a8c832f6', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'eb6134ee-a958-4e60-937d-d99414d81b37', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ccc8ebdf-c254-4735-b9a7-8b41417b993c', N'2')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'68fa5512-95c7-4abb-836f-90076ff5fc97', N'Ivo', N'Topolov', N'ivo', N'IVO', N'ddrachevpro@abv.bg', N'DDRACHEVPRO@ABV.BG', 0, N'AQAAAAEAACcQAAAAEJi56GLJmnP2pGA+pckIWQixpw7PZBgiDQtBaiKt4cuTOe8TxKZabAneiLRf+6JZYQ==', N'EZFNF3XFLNB3F2FTORH2V6IAK7BQOCBO', N'bc7d66b0-b2ea-4a56-9fd8-d99429d27910', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ccc8ebdf-c254-4735-b9a7-8b41417b993c', N'Dimitar', N'Rachev', N'bloodykey', N'BLOODYKEY', N'ddrachev123@gmail.com', N'DDRACHEV@ABV.BG', 0, N'AQAAAAEAACcQAAAAEIIGou6I7CZiejv9EuduFqzcBqdLNyJihwufT4XXx+CwoVy/DoJ+6fnAKgM4746Hiw==', N'UFEZFPGEAKYQ4O5NTNZWV43PEOO35BYT', N'a9c4df7c-d5f4-4453-823b-91becba376ae', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd12e7e9e-cc46-4f87-bfc7-50f8a8c832f6', N'Dimitar', N'Rachev', N'dimitar', N'DIMITAR', N'ddrachev123@gmail.com', N'DDRACHEV123@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEMAoyKY+gjX0Y2X3ykr/GwcNZDbYIGdOLmgiBvjeI4qlp1EodpQSfQ4QIWlzuh+Blw==', N'JETRKOAY62EVVBUHNNDCUFKQB24XDZXT', N'ed8bd0b1-d6b6-4fc8-8183-29b485f49588', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'eb6134ee-a958-4e60-937d-d99414d81b37', N'Stefan', N'Popov', N'stefan', N'STEFAN', N'ddrachev@abv.bg', N'DDRACHEV@ABV.BG', 0, N'AQAAAAEAACcQAAAAEOxuBKFq88nyh/ZoSlyh2i5La29tqbwYgSSa9ZZuZ6V6u92I/XrFzqD8jkUnifsJZQ==', N'HSJ7BL5QATLD6WURB5NLP3IS4P2UUZ66', N'2bc87ac7-5f18-4d9e-bdef-cb7e0b4388d1', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (9, N'.NET')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (10, N'PHP')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (11, N'C#')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (19, N'JavaScript')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (59, N'C++')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (60, N'Python')
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (61, N'Oracle')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Followers] ON 

INSERT [dbo].[Followers] ([Id1], [Id], [RequestId]) VALUES (97, N'68fa5512-95c7-4abb-836f-90076ff5fc97', 82)
INSERT [dbo].[Followers] ([Id1], [Id], [RequestId]) VALUES (98, N'68fa5512-95c7-4abb-836f-90076ff5fc97', 88)
INSERT [dbo].[Followers] ([Id1], [Id], [RequestId]) VALUES (99, N'68fa5512-95c7-4abb-836f-90076ff5fc97', 91)
INSERT [dbo].[Followers] ([Id1], [Id], [RequestId]) VALUES (100, N'68fa5512-95c7-4abb-836f-90076ff5fc97', 90)
SET IDENTITY_INSERT [dbo].[Followers] OFF
GO
SET IDENTITY_INSERT [dbo].[Requests] ON 

INSERT [dbo].[Requests] ([RequestId], [Status], [Type], [Author], [Title], [Priority], [Link], [Motive], [DateAdded], [CategoryId], [Description], [UserId]) VALUES (82, 1, 2, N'Adam Freeman', N'Pro ASP.NET Core 3', N'High', NULL, N'learning', CAST(N'2023-07-24T00:00:00.0000000' AS DateTime2), 9, N'ASP.NET Core 3 ', N'68fa5512-95c7-4abb-836f-90076ff5fc97')
INSERT [dbo].[Requests] ([RequestId], [Status], [Type], [Author], [Title], [Priority], [Link], [Motive], [DateAdded], [CategoryId], [Description], [UserId]) VALUES (88, 1, 2, N'Luke Welling and Laura Thomson', N'PHP and MySQL Web Development', N'High', N'internet', N'learn', CAST(N'2023-07-25T00:00:00.0000000' AS DateTime2), 10, N'PHP and MySQL Web Development', N'eb6134ee-a958-4e60-937d-d99414d81b37')
INSERT [dbo].[Requests] ([RequestId], [Status], [Type], [Author], [Title], [Priority], [Link], [Motive], [DateAdded], [CategoryId], [Description], [UserId]) VALUES (89, 1, 2, N'Steven Feuerstein', N'Oracle PL/SQL Programming', N'Low', N'internet', N'interest', CAST(N'2023-07-20T00:00:00.0000000' AS DateTime2), 61, N'Oracle PL/SQL Programming', N'eb6134ee-a958-4e60-937d-d99414d81b37')
INSERT [dbo].[Requests] ([RequestId], [Status], [Type], [Author], [Title], [Priority], [Link], [Motive], [DateAdded], [CategoryId], [Description], [UserId]) VALUES (90, 1, 2, N'David C. Kreines ', N'Oracle SQL: The Essential Reference', N'Medium', N'internet', N'for project', CAST(N'2023-07-18T00:00:00.0000000' AS DateTime2), 61, N'The Essential Reference of Oracle SQL', N'd12e7e9e-cc46-4f87-bfc7-50f8a8c832f6')
INSERT [dbo].[Requests] ([RequestId], [Status], [Type], [Author], [Title], [Priority], [Link], [Motive], [DateAdded], [CategoryId], [Description], [UserId]) VALUES (91, 1, 2, N'Al Sweigart', N'Automate the Boring Stuff with Python: Practical Programming for Total Beginners', N'Low', N'internet', N'learn', CAST(N'2023-07-25T00:00:00.0000000' AS DateTime2), 60, N'Beginner book for python', N'd12e7e9e-cc46-4f87-bfc7-50f8a8c832f6')
INSERT [dbo].[Requests] ([RequestId], [Status], [Type], [Author], [Title], [Priority], [Link], [Motive], [DateAdded], [CategoryId], [Description], [UserId]) VALUES (92, 1, 2, N'Scott Meyers', N'Effective Modern C++: 42 Specific Ways to Improve Your Use of C++11 and C++14', N'High', N'internet', N'improving my C++', CAST(N'2023-07-23T00:00:00.0000000' AS DateTime2), 59, N'42 Specific Ways to Improve Your Use of C++11 and C++14', N'd12e7e9e-cc46-4f87-bfc7-50f8a8c832f6')
SET IDENTITY_INSERT [dbo].[Requests] OFF
GO
SET IDENTITY_INSERT [dbo].[Resources] ON 

INSERT [dbo].[Resources] ([ResourceId], [Type], [Author], [Title], [Description], [Location], [Price], [Invoice], [Status], [DateAdded], [DateTaken], [DateReturn], [CategoryId], [UserId], [FileName]) VALUES (27, 3, N'Joseph Albahari', N'C# 10 in a Nutshell The Definitive Reference', N'C# 10 in a Nutshell', N'FileStorage\609c6bf6-5c7a-4329-a4f7-af4f0d14a82a.pdf', NULL, NULL, NULL, CAST(N'2023-07-24T00:00:00.0000000' AS DateTime2), NULL, NULL, 11, NULL, NULL)
INSERT [dbo].[Resources] ([ResourceId], [Type], [Author], [Title], [Description], [Location], [Price], [Invoice], [Status], [DateAdded], [DateTaken], [DateReturn], [CategoryId], [UserId], [FileName]) VALUES (28, 3, N'Jean Paul', N'Design Patterns in C#', N'23 design pattern for C#', N'FileStorage\5ecd786c-706f-41e2-92b4-4db298d15aff.pdf', NULL, NULL, NULL, CAST(N'2023-07-20T00:00:00.0000000' AS DateTime2), NULL, NULL, 11, NULL, NULL)
INSERT [dbo].[Resources] ([ResourceId], [Type], [Author], [Title], [Description], [Location], [Price], [Invoice], [Status], [DateAdded], [DateTaken], [DateReturn], [CategoryId], [UserId], [FileName]) VALUES (29, 2, N' Mark Myers', N'A Smarter Way to Learn JavaScript', N'Learning JavaScript is hell because of two problems. I remove the problems, and you start having fun.', N'', NULL, NULL, NULL, CAST(N'2023-07-25T00:00:00.0000000' AS DateTime2), NULL, NULL, 19, NULL, NULL)
INSERT [dbo].[Resources] ([ResourceId], [Type], [Author], [Title], [Description], [Location], [Price], [Invoice], [Status], [DateAdded], [DateTaken], [DateReturn], [CategoryId], [UserId], [FileName]) VALUES (30, 1, N'Marijn Haverbeke', N'Eloquent JavaScript: A Modern Introduction to Programming', N'JavaScript: A Modern Introduction to Programming', N'', NULL, NULL, NULL, CAST(N'2023-07-26T00:00:00.0000000' AS DateTime2), NULL, NULL, 19, NULL, NULL)
INSERT [dbo].[Resources] ([ResourceId], [Type], [Author], [Title], [Description], [Location], [Price], [Invoice], [Status], [DateAdded], [DateTaken], [DateReturn], [CategoryId], [UserId], [FileName]) VALUES (31, 2, N'Douglas Crockford', N'JavaScript: The Good Parts', N'Good parts', N'', 20, N'22', 1, CAST(N'2023-07-25T00:00:00.0000000' AS DateTime2), NULL, NULL, 19, NULL, NULL)
INSERT [dbo].[Resources] ([ResourceId], [Type], [Author], [Title], [Description], [Location], [Price], [Invoice], [Status], [DateAdded], [DateTaken], [DateReturn], [CategoryId], [UserId], [FileName]) VALUES (32, 1, N'Stanley B. Lippman', N'C++ Primer', N'c++', N'', NULL, NULL, NULL, CAST(N'2023-07-16T00:00:00.0000000' AS DateTime2), NULL, NULL, 59, NULL, NULL)
INSERT [dbo].[Resources] ([ResourceId], [Type], [Author], [Title], [Description], [Location], [Price], [Invoice], [Status], [DateAdded], [DateTaken], [DateReturn], [CategoryId], [UserId], [FileName]) VALUES (33, 2, N'Eric Matthes', N'Python Crash Course: A Hands-On, Project-Based Introduction to Programming', N'Python Crash Course', N'', 30, N'35', NULL, CAST(N'2023-07-23T00:00:00.0000000' AS DateTime2), NULL, NULL, 60, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Resources] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 25.7.2023 17:38:23 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 25.7.2023 17:38:23 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Followers_Id]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_Followers_Id] ON [dbo].[Followers]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Followers_RequestId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_Followers_RequestId] ON [dbo].[Followers]
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Requests_CategoryId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_Requests_CategoryId] ON [dbo].[Requests]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Requests_UserId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_Requests_UserId] ON [dbo].[Requests]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Resources_CategoryId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_Resources_CategoryId] ON [dbo].[Resources]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Resources_UserId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_Resources_UserId] ON [dbo].[Resources]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Upvotes_Id]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_Upvotes_Id] ON [dbo].[Upvotes]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Upvotes_RequestId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_Upvotes_RequestId] ON [dbo].[Upvotes]
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_WantedResources_Id]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_WantedResources_Id] ON [dbo].[WantedResources]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WantedResources_ResourceId]    Script Date: 25.7.2023 17:38:23 ******/
CREATE NONCLUSTERED INDEX [IX_WantedResources_ResourceId] ON [dbo].[WantedResources]
(
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Followers]  WITH CHECK ADD  CONSTRAINT [FK_Followers_AspNetUsers_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Followers] CHECK CONSTRAINT [FK_Followers_AspNetUsers_Id]
GO
ALTER TABLE [dbo].[Followers]  WITH CHECK ADD  CONSTRAINT [FK_Followers_Requests_RequestId] FOREIGN KEY([RequestId])
REFERENCES [dbo].[Requests] ([RequestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Followers] CHECK CONSTRAINT [FK_Followers_Requests_RequestId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Upvotes]  WITH CHECK ADD  CONSTRAINT [FK_Upvotes_AspNetUsers_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Upvotes] CHECK CONSTRAINT [FK_Upvotes_AspNetUsers_Id]
GO
ALTER TABLE [dbo].[Upvotes]  WITH CHECK ADD  CONSTRAINT [FK_Upvotes_Requests_RequestId] FOREIGN KEY([RequestId])
REFERENCES [dbo].[Requests] ([RequestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Upvotes] CHECK CONSTRAINT [FK_Upvotes_Requests_RequestId]
GO
ALTER TABLE [dbo].[WantedResources]  WITH CHECK ADD  CONSTRAINT [FK_WantedResources_AspNetUsers_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WantedResources] CHECK CONSTRAINT [FK_WantedResources_AspNetUsers_Id]
GO
ALTER TABLE [dbo].[WantedResources]  WITH CHECK ADD  CONSTRAINT [FK_WantedResources_Resources_ResourceId] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([ResourceId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WantedResources] CHECK CONSTRAINT [FK_WantedResources_Resources_ResourceId]
GO
USE [master]
GO
ALTER DATABASE [MelonBookshelf] SET  READ_WRITE 
GO
