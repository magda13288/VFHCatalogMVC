USE [master]
GO
/****** Object:  Database [VFHCatalog]    Script Date: 24.07.2025 11:42:48 ******/
CREATE DATABASE [VFHCatalog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VFHCatalog', FILENAME = N'/var/opt/mssql/data/VFHCatalog.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VFHCatalog_log', FILENAME = N'/var/opt/mssql/data/VFHCatalog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VFHCatalog] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VFHCatalog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VFHCatalog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VFHCatalog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VFHCatalog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VFHCatalog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VFHCatalog] SET ARITHABORT OFF 
GO
ALTER DATABASE [VFHCatalog] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [VFHCatalog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VFHCatalog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VFHCatalog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VFHCatalog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VFHCatalog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VFHCatalog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VFHCatalog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VFHCatalog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VFHCatalog] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VFHCatalog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VFHCatalog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VFHCatalog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VFHCatalog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VFHCatalog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VFHCatalog] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [VFHCatalog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VFHCatalog] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VFHCatalog] SET  MULTI_USER 
GO
ALTER DATABASE [VFHCatalog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VFHCatalog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VFHCatalog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VFHCatalog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VFHCatalog] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VFHCatalog] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VFHCatalog] SET QUERY_STORE = OFF
GO
USE [VFHCatalog]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24.07.2025 11:42:48 ******/
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
/****** Object:  Table [dbo].[Addresses]    Script Date: 24.07.2025 11:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Street] [nvarchar](max) NULL,
	[BuildingNumber] [nvarchar](max) NULL,
	[FlatNumber] [nvarchar](max) NULL,
	[ZipCode] [nvarchar](max) NULL,
	[CityId] [int] NOT NULL,
	[RegionId] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 24.07.2025 11:42:48 ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 24.07.2025 11:42:48 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 24.07.2025 11:42:48 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 24.07.2025 11:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 24.07.2025 11:42:48 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 24.07.2025 11:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
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
	[AccountName] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[CompanyName] [nvarchar](max) NULL,
	[NIP] [nvarchar](max) NULL,
	[REGON] [nvarchar](max) NULL,
	[CEOName] [nvarchar](max) NULL,
	[CEOLastName] [nvarchar](max) NULL,
	[LogoPic] [varbinary](max) NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 24.07.2025 11:42:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditTrials]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditTrials](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[TrailType] [nvarchar](max) NOT NULL,
	[DateUtc] [datetime2](7) NOT NULL,
	[EntityName] [nvarchar](100) NOT NULL,
	[PrimaryKey] [nvarchar](100) NULL,
	[OldValues] [nvarchar](max) NULL,
	[NewValues] [nvarchar](max) NULL,
	[ChangedColumns] [nvarchar](max) NULL,
 CONSTRAINT [PK_AuditTrials] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[RegionId] [int] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colors]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Colors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyContactInformations]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyContactInformations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Possition] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_CompanyContactInformations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactDetailForSeedlings]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactDetailForSeedlings](
	[PlantSeedlingId] [int] NOT NULL,
	[ContactDetailId] [int] NOT NULL,
 CONSTRAINT [PK_ContactDetailForSeedlings] PRIMARY KEY CLUSTERED 
(
	[PlantSeedlingId] ASC,
	[ContactDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactDetailForSeeds]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactDetailForSeeds](
	[PlantSeedId] [int] NOT NULL,
	[ContactDetailId] [int] NOT NULL,
 CONSTRAINT [PK_ContactDetailForSeeds] PRIMARY KEY CLUSTERED 
(
	[PlantSeedId] ASC,
	[ContactDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactDetails]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactDetailInformation] [nvarchar](max) NULL,
	[ContactDetailTypeID] [int] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_ContactDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactDetailTypes]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactDetailTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_ContactDetailTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Destinations]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Destinations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Destinations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FruitSizeForListFilters]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FruitSizeForListFilters](
	[FruitSizeId] [int] NOT NULL,
	[PlantTypeId] [int] NOT NULL,
	[PlantGroupId] [int] NULL,
	[PlantSectionId] [int] NULL,
 CONSTRAINT [PK_FruitSizeForListFilters] PRIMARY KEY CLUSTERED 
(
	[FruitSizeId] ASC,
	[PlantTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FruitSizes]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FruitSizes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_FruitSizes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FruitTypeForListFilters]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FruitTypeForListFilters](
	[FruitTypeId] [int] NOT NULL,
	[PlantTypeId] [int] NOT NULL,
	[PlantGroupId] [int] NULL,
	[PlantSectionId] [int] NULL,
 CONSTRAINT [PK_FruitTypeForListFilters] PRIMARY KEY CLUSTERED 
(
	[FruitTypeId] ASC,
	[PlantTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FruitTypes]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FruitTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_FruitTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowingSeazons]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrowingSeazons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_GrowingSeazons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowthTypes]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrowthTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_GrowthTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowthTypesForListFilters]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrowthTypesForListFilters](
	[GrowthTypesId] [int] NOT NULL,
	[PlantTypeId] [int] NOT NULL,
	[PlantGroupId] [int] NULL,
	[PlantSectionId] [int] NULL,
 CONSTRAINT [PK_GrowthTypesForListFilters] PRIMARY KEY CLUSTERED 
(
	[GrowthTypesId] ASC,
	[PlantTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageAnswers]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageAnswers](
	[MessageId] [int] NOT NULL,
	[MessageAnswerId] [int] NOT NULL,
 CONSTRAINT [PK_MessageAnswers] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC,
	[MessageAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageReceivers]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageReceivers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[MessageId] [int] NOT NULL,
 CONSTRAINT [PK_MessageReceivers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[MessageContent] [nvarchar](max) NULL,
	[AddedDate] [datetime2](7) NOT NULL,
	[isAnswer] [bit] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewUserPlants]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewUserPlants](
	[UserId] [nvarchar](450) NOT NULL,
	[PlantId] [int] NOT NULL,
 CONSTRAINT [PK_NewUserPlants] PRIMARY KEY CLUSTERED 
(
	[PlantId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantDestinations]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantDestinations](
	[PlantDetailId] [int] NOT NULL,
	[DestinationId] [int] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantDestinations] PRIMARY KEY CLUSTERED 
(
	[PlantDetailId] ASC,
	[DestinationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantDetails]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PlantPassportNumber] [nvarchar](max) NULL,
	[ColorId] [int] NOT NULL,
	[FruitSizeId] [int] NOT NULL,
	[FruitTypeId] [int] NOT NULL,
	[PlantRef] [int] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantDetailsImages]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantDetailsImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageURL] [nvarchar](max) NULL,
	[PlantDetailId] [int] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantDetailsImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantGroups]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[PlantTypeId] [int] NOT NULL,
 CONSTRAINT [PK_PlantGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantGrowingSeazons]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantGrowingSeazons](
	[PlantDetailId] [int] NOT NULL,
	[GrowingSeazonId] [int] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantGrowingSeazons] PRIMARY KEY CLUSTERED 
(
	[PlantDetailId] ASC,
	[GrowingSeazonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantGrowthTypes]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantGrowthTypes](
	[PlantDetailId] [int] NOT NULL,
	[GrowthTypeId] [int] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantGrowthTypes] PRIMARY KEY CLUSTERED 
(
	[PlantDetailId] ASC,
	[GrowthTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantMessages]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantMessages](
	[PlantId] [int] NOT NULL,
	[MessageId] [int] NOT NULL,
	[isSeed] [bit] NOT NULL,
	[isSeedling] [bit] NOT NULL,
	[isNewPlant] [bit] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantMessages] PRIMARY KEY CLUSTERED 
(
	[PlantId] ASC,
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantOpinions]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantOpinions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[Opinion] [nvarchar](max) NULL,
	[PlantDetailId] [int] NOT NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantOpinions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plants]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlantTypeId] [int] NOT NULL,
	[PlantGroupId] [int] NOT NULL,
	[PlantSectionId] [int] NULL,
	[FullName] [nvarchar](max) NULL,
	[Photo] [nvarchar](max) NULL,
	[isActive] [bit] NOT NULL,
	[isNew] [bit] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Plants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantSections]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantSections](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[PlantGroupId] [int] NOT NULL,
 CONSTRAINT [PK_PlantSections] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantSeedlings]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantSeedlings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[PlantId] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantSeedlings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantSeeds]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantSeeds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[PlantId] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
	[UpdatedAtUtc] [datetime2](7) NULL,
	[InactivatedAtUtc] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[InactivatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantSeeds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantTags]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantTags](
	[PlantId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[Created] [datetime2](7) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[Modified] [datetime2](7) NULL,
	[StatusId] [int] NOT NULL,
	[InactivatedBy] [nvarchar](max) NULL,
	[Inactivated] [datetime2](7) NULL,
 CONSTRAINT [PK_PlantTags] PRIMARY KEY CLUSTERED 
(
	[PlantId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlantTypes]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlantTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_PlantTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Regions]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfAvailabilities]    Script Date: 24.07.2025 11:42:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfAvailabilities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToReplace] [bit] NOT NULL,
	[ForFree] [bit] NOT NULL,
	[Seed] [bit] NOT NULL,
	[Seedling] [bit] NOT NULL,
	[None] [bit] NOT NULL,
	[PlantRef] [int] NOT NULL,
 CONSTRAINT [PK_TypeOfAvailabilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241229224522_Initial', N'3.1.32')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250101185722_DataSeedForUserRegistrationForm', N'3.1.32')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250101193232_DataSeedIdentityRoles', N'3.1.32')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250101193823_AddingIdPropertyToApplicationUser', N'3.1.32')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250101202346_DataSeedAddedDefaultUsers', N'3.1.32')
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([Id], [Street], [BuildingNumber], [FlatNumber], [ZipCode], [CityId], [RegionId], [CountryId], [UserId]) VALUES (1, NULL, NULL, NULL, NULL, 1, 12, 1, N'0a249d73-5e9a-4c07-9832-27645a2c2fe8')
INSERT [dbo].[Addresses] ([Id], [Street], [BuildingNumber], [FlatNumber], [ZipCode], [CityId], [RegionId], [CountryId], [UserId]) VALUES (2, NULL, NULL, NULL, NULL, 1, 12, 1, N'2ef2b510-aa25-42ca-b68a-ee2fa0635924')
INSERT [dbo].[Addresses] ([Id], [Street], [BuildingNumber], [FlatNumber], [ZipCode], [CityId], [RegionId], [CountryId], [UserId]) VALUES (3, NULL, NULL, NULL, NULL, 1, 12, 1, N'b9c413fb-7822-4bf2-8028-30597aab757b')
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Admin', N'Admin', N'ADMIN', N'57a2bced-4c68-431a-93ac-958d6936a7c6')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'Company', N'Company', N'COMPANY', N'50e234a4-4ae7-4e25-964d-a8ac5d51ceaa')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'PrivateUser', N'PrivateUser', N'PRIVATE_USER', N'027ca4a0-3682-471e-aba3-a4c1d52ab2af')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0a249d73-5e9a-4c07-9832-27645a2c2fe8', N'Admin')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2ef2b510-aa25-42ca-b68a-ee2fa0635924', N'PrivateUser')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b9c413fb-7822-4bf2-8028-30597aab757b', N'PrivateUser')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AccountName], [FirstName], [LastName], [CompanyName], [NIP], [REGON], [CEOName], [CEOLastName], [LogoPic], [isActive]) VALUES (N'0a249d73-5e9a-4c07-9832-27645a2c2fe8', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEDeZRsN0w0Gs6YSisBi9jbmg7ihLkvOxZgsuCjScMg2GD1JtcbU2tSzMjclvwSrSxA==', N'Z4NQVBZ2LMDZAJM675CY3465JPFGY2PS', N'88df6574-21db-41c6-b833-ce439584e236', NULL, 0, 0, NULL, 1, 0, N'Admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AccountName], [FirstName], [LastName], [CompanyName], [NIP], [REGON], [CEOName], [CEOLastName], [LogoPic], [isActive]) VALUES (N'2ef2b510-aa25-42ca-b68a-ee2fa0635924', N'kinga123@gmail.com', N'KINGA123@GMAIL.COM', N'kinga123@gmail.com', N'KINGA123@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAECycEAX8sBrbNrgbYD2NdV0xoMgU2pJjfmsSi3J+ZczthajMzjaIuU5VMuKVyLGV/w==', N'3QZ3HMO2U2QS27FOTYYOKNS2AYMMYTZM', N'cce6f078-25f9-4bb8-9af5-73cd0c91d645', NULL, 0, 0, NULL, 1, 0, N'Kinga', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [AccountName], [FirstName], [LastName], [CompanyName], [NIP], [REGON], [CEOName], [CEOLastName], [LogoPic], [isActive]) VALUES (N'b9c413fb-7822-4bf2-8028-30597aab757b', N'sara2013@gmail.com', N'SARA2013@GMAIL.COM', N'sara2013@gmail.com', N'SARA2013@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAENew82n5Ros4D7PYuUpk0hyOVL6qkqteLtF1Wrz0uBd0BhoHnHd9VKfzUvIn/ySdRQ==', N'QHE5B4ZDTU3QRAMHMMLIMHOOWXLK73S7', N'b5d8a96f-6606-472d-9646-61ff81fa5a1d', NULL, 0, 0, NULL, 0, 0, N'Sara', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'af45057c-f236-4a34-80b5-0ddde474c35b', NULL, N'Create', CAST(N'2025-01-07T20:26:18.6372731' AS DateTime2), N'PlantDestination', N'1', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'64ca1896-a1d8-40d7-a0b0-11b65aa0e1a6', NULL, N'Delete', CAST(N'2025-01-07T20:32:55.5983562' AS DateTime2), N'PlantGrowthType', N'2', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'501b47c2-360c-46ce-93b9-12e7bc9e9395', NULL, N'Create', CAST(N'2025-01-07T20:32:56.2756691' AS DateTime2), N'PlantGrowthType', N'2', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'e5e541d7-ed08-4e6d-ac19-14651d02580d', NULL, N'Update', CAST(N'2025-01-07T20:32:36.0041813' AS DateTime2), N'Plant', N'1', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'6f02055c-d2f5-490e-9730-2bb941250e6d', NULL, N'Delete', CAST(N'2025-01-07T20:32:42.0085325' AS DateTime2), N'PlantDestination', N'1', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'ef97fe78-110e-41bb-b664-356c9ab298d6', NULL, N'Create', CAST(N'2025-01-02T19:07:08.9649994' AS DateTime2), N'PlantDetail', NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'b003bdb8-8709-4526-aae7-3a6307983267', NULL, N'Create', CAST(N'2025-01-07T20:26:18.6373996' AS DateTime2), N'PlantDestination', N'2', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'e1c64db3-7af3-4d2b-a507-47db51a36cd1', NULL, N'Create', CAST(N'2025-01-07T20:26:18.6731308' AS DateTime2), N'PlantDetailsImages', NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'e179fad4-3211-4f93-b9c0-47e12615b471', NULL, N'Create', CAST(N'2025-01-02T19:07:09.0096467' AS DateTime2), N'PlantDestination', N'3', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'72be03ac-cf14-4f8b-b570-4cfbf8d61aab', NULL, N'Create', CAST(N'2025-01-02T19:07:09.0452228' AS DateTime2), N'PlantDetailsImages', NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'971a836c-f218-43d3-ae30-6fcd37e7bfa9', NULL, N'Create', CAST(N'2025-01-02T19:07:08.9868485' AS DateTime2), N'PlantGrowthType', N'2', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'3b3ae62b-485f-4bd0-8956-75792152161e', NULL, N'Create', CAST(N'2025-01-07T20:26:18.5528850' AS DateTime2), N'PlantDetail', NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'208c0350-5ccc-425f-984c-8cb4b6d6bdbc', NULL, N'Delete', CAST(N'2025-01-07T20:32:42.0086862' AS DateTime2), N'PlantDestination', N'2', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'598a2bf8-846f-460e-8e2d-98d43f092d64', NULL, N'Create', CAST(N'2025-01-07T20:26:18.6373637' AS DateTime2), N'PlantDestination', N'3', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'1a1fb7cf-1a53-44e4-b9cf-9f91cb27c5e2', NULL, N'Create', CAST(N'2025-01-07T20:26:18.6220368' AS DateTime2), N'PlantGrowingSeazon', N'1', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'aa1aa3f6-87a8-4286-8ee9-a48b63f3e50f', NULL, N'Create', CAST(N'2025-01-07T20:26:18.6731813' AS DateTime2), N'PlantDetailsImages', NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'76cd4503-2449-43ce-a8fa-b39b70eb9889', NULL, N'Create', CAST(N'2025-01-07T20:32:43.1871700' AS DateTime2), N'PlantDestination', N'1', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'386d85e0-c10b-4bb8-ab4d-bdad216addda', NULL, N'Create', CAST(N'2025-01-07T20:26:18.6045113' AS DateTime2), N'PlantGrowthType', N'2', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'4e92f80d-1aec-4ec5-ada8-be29ab768fea', NULL, N'Create', CAST(N'2025-01-02T19:07:09.0096059' AS DateTime2), N'PlantDestination', N'2', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'76ff6559-5162-4742-a175-d59e6212cb82', NULL, N'Create', CAST(N'2025-01-07T20:32:43.1870575' AS DateTime2), N'PlantDestination', N'3', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'50cd38db-7075-43c5-aa54-d93a95d80604', NULL, N'Create', CAST(N'2025-01-02T19:07:09.0096334' AS DateTime2), N'PlantDestination', N'1', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'96e8d2be-5b3d-470d-9ccb-dc4ab485f041', NULL, N'Delete', CAST(N'2025-01-07T20:32:42.0087073' AS DateTime2), N'PlantDestination', N'3', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'34e9d881-9ab0-485b-8e22-e0b72c92f1e3', NULL, N'Create', CAST(N'2025-01-07T20:26:18.2299661' AS DateTime2), N'Plant', NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'140f0fb7-ebc3-4848-951e-ebba6acc4113', NULL, N'Update', CAST(N'2025-01-07T20:32:36.2471587' AS DateTime2), N'PlantDetail', N'1', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'426a98f4-0548-4cb0-89cb-ecde73ed13d9', NULL, N'Create', CAST(N'2025-01-02T19:07:09.0351455' AS DateTime2), N'PlantDetailsImages', NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'ace0bfc3-cf2a-410a-b4de-f7e85afb38b0', NULL, N'Create', CAST(N'2025-01-02T19:07:08.9979270' AS DateTime2), N'PlantGrowingSeazon', N'1', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'9f797374-9423-4743-ba5b-fd428d413acf', NULL, N'Create', CAST(N'2025-01-07T20:32:43.1871436' AS DateTime2), N'PlantDestination', N'2', NULL, NULL, NULL)
INSERT [dbo].[AuditTrials] ([Id], [UserId], [TrailType], [DateUtc], [EntityName], [PrimaryKey], [OldValues], [NewValues], [ChangedColumns]) VALUES (N'07569fa5-096b-454d-919e-ff20dc8e8916', NULL, N'Create', CAST(N'2025-01-02T19:07:08.8275154' AS DateTime2), N'Plant', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Cities] ON 

INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (1, N'Katowice', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (2, N'Gliwice', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (3, N'Zabrze', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (4, N'Sosnowiec', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (5, N'Bytom', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (6, N'Rybnik', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (7, N'Chorzów', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (8, N'Tychy', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (9, N'Dąbrowa Górnicza', 12)
INSERT [dbo].[Cities] ([Id], [Name], [RegionId]) VALUES (10, N'Jaworzno', 12)
SET IDENTITY_INSERT [dbo].[Cities] OFF
GO
SET IDENTITY_INSERT [dbo].[Colors] ON 

INSERT [dbo].[Colors] ([Id], [Name]) VALUES (1, N'White')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (2, N'Black')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (3, N'Red')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (4, N'Indigo')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (5, N'Orange')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (6, N'Pink')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (7, N'Multicolor')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (8, N'Green')
INSERT [dbo].[Colors] ([Id], [Name]) VALUES (9, N'Yellow')
SET IDENTITY_INSERT [dbo].[Colors] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [Name]) VALUES (1, N'Poland')
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Destinations] ON 

INSERT [dbo].[Destinations] ([Id], [Name]) VALUES (1, N'Ground')
INSERT [dbo].[Destinations] ([Id], [Name]) VALUES (2, N'Under covers')
INSERT [dbo].[Destinations] ([Id], [Name]) VALUES (3, N'Pot')
SET IDENTITY_INSERT [dbo].[Destinations] OFF
GO
INSERT [dbo].[FruitSizeForListFilters] ([FruitSizeId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (2, 1, 1, 1)
INSERT [dbo].[FruitSizeForListFilters] ([FruitSizeId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (3, 1, 1, 1)
INSERT [dbo].[FruitSizeForListFilters] ([FruitSizeId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (4, 1, 1, 1)
INSERT [dbo].[FruitSizeForListFilters] ([FruitSizeId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (5, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[FruitSizes] ON 

INSERT [dbo].[FruitSizes] ([Id], [Name]) VALUES (1, N'Not specified')
INSERT [dbo].[FruitSizes] ([Id], [Name]) VALUES (2, N'Small')
INSERT [dbo].[FruitSizes] ([Id], [Name]) VALUES (3, N'Cherry type')
INSERT [dbo].[FruitSizes] ([Id], [Name]) VALUES (4, N'Large-fruited')
INSERT [dbo].[FruitSizes] ([Id], [Name]) VALUES (5, N'Medium-fruited')
SET IDENTITY_INSERT [dbo].[FruitSizes] OFF
GO
INSERT [dbo].[FruitTypeForListFilters] ([FruitTypeId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (2, 1, 1, 1)
INSERT [dbo].[FruitTypeForListFilters] ([FruitTypeId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (3, 1, 1, 1)
INSERT [dbo].[FruitTypeForListFilters] ([FruitTypeId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (4, 1, 1, 2)
INSERT [dbo].[FruitTypeForListFilters] ([FruitTypeId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (5, 1, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[FruitTypes] ON 

INSERT [dbo].[FruitTypes] ([Id], [Name]) VALUES (1, N'Not specified')
INSERT [dbo].[FruitTypes] ([Id], [Name]) VALUES (2, N'Fleshy')
INSERT [dbo].[FruitTypes] ([Id], [Name]) VALUES (3, N'Multichambered')
INSERT [dbo].[FruitTypes] ([Id], [Name]) VALUES (4, N'Spicy')
INSERT [dbo].[FruitTypes] ([Id], [Name]) VALUES (5, N'Sweet')
SET IDENTITY_INSERT [dbo].[FruitTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[GrowingSeazons] ON 

INSERT [dbo].[GrowingSeazons] ([Id], [Name]) VALUES (1, N'Late')
INSERT [dbo].[GrowingSeazons] ([Id], [Name]) VALUES (2, N'Early')
INSERT [dbo].[GrowingSeazons] ([Id], [Name]) VALUES (3, N'Mid-late')
INSERT [dbo].[GrowingSeazons] ([Id], [Name]) VALUES (4, N'Mid-early')
INSERT [dbo].[GrowingSeazons] ([Id], [Name]) VALUES (5, N'Annual')
INSERT [dbo].[GrowingSeazons] ([Id], [Name]) VALUES (6, N'Perennial')
SET IDENTITY_INSERT [dbo].[GrowingSeazons] OFF
GO
SET IDENTITY_INSERT [dbo].[GrowthTypes] ON 

INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (1, N'Not specified')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (2, N'Tall growing')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (3, N'Dwarf')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (4, N'Potted')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (5, N'Determinate')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (6, N'Shrub')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (7, N'Sweet')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (8, N'Bush')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (9, N'Tree')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (10, N'Climbing plant')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (11, N'Hanging plant')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (12, N'Vine')
INSERT [dbo].[GrowthTypes] ([Id], [Name]) VALUES (13, N'Root')
SET IDENTITY_INSERT [dbo].[GrowthTypes] OFF
GO
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (6, 2, NULL, NULL)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (8, 2, NULL, NULL)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (9, 2, NULL, NULL)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (10, 2, NULL, NULL)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (11, 3, NULL, NULL)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (12, 3, NULL, NULL)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (13, 3, NULL, NULL)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (2, 1, 1, 1)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (3, 1, 1, 1)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (4, 1, 1, 1)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (5, 1, 1, 1)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (6, 1, 1, 2)
INSERT [dbo].[GrowthTypesForListFilters] ([GrowthTypesId], [PlantTypeId], [PlantGroupId], [PlantSectionId]) VALUES (7, 1, 1, 2)
GO
INSERT [dbo].[MessageAnswers] ([MessageId], [MessageAnswerId]) VALUES (1, 2)
GO
SET IDENTITY_INSERT [dbo].[MessageReceivers] ON 

INSERT [dbo].[MessageReceivers] ([Id], [UserId], [MessageId]) VALUES (1, N'2ef2b510-aa25-42ca-b68a-ee2fa0635924', 1)
INSERT [dbo].[MessageReceivers] ([Id], [UserId], [MessageId]) VALUES (2, N'b9c413fb-7822-4bf2-8028-30597aab757b', 2)
SET IDENTITY_INSERT [dbo].[MessageReceivers] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([Id], [UserId], [MessageContent], [AddedDate], [isAnswer]) VALUES (1, N'b9c413fb-7822-4bf2-8028-30597aab757b', N'Hello. I''m interested in your seeds. I live in the next city. Maybe we can meet for coffee? Check out my seeds, maybe something will interest you:)', CAST(N'2025-01-13T13:38:56.0000000' AS DateTime2), 1)
INSERT [dbo].[Messages] ([Id], [UserId], [MessageContent], [AddedDate], [isAnswer]) VALUES (2, N'2ef2b510-aa25-42ca-b68a-ee2fa0635924', N'Hi. Sure, I''d love to meet up. When are you free?', CAST(N'2025-01-13T14:10:11.0000000' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
INSERT [dbo].[NewUserPlants] ([UserId], [PlantId]) VALUES (N'2ef2b510-aa25-42ca-b68a-ee2fa0635924', 5)
GO
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, 1, CAST(N'2025-01-07T20:32:43.1846083' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, 2, CAST(N'2025-01-07T20:32:43.1845893' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, 3, CAST(N'2025-01-07T20:32:43.1843358' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, 1, CAST(N'2025-01-07T20:26:18.6363797' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, 2, CAST(N'2025-01-07T20:26:18.6366160' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, 3, CAST(N'2025-01-07T20:26:18.6365950' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (3, 1, CAST(N'2025-01-09T16:08:04.2854978' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, 1, CAST(N'2025-01-10T17:25:42.0963589' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, 2, CAST(N'2025-01-10T17:25:42.0962066' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, 3, CAST(N'2025-01-10T17:25:42.0963772' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, 1, CAST(N'2025-01-17T15:10:36.9498638' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, 2, CAST(N'2025-01-17T15:10:36.9497374' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDestinations] ([PlantDetailId], [DestinationId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, 3, CAST(N'2025-01-17T15:10:36.9498958' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PlantDetails] ON 

INSERT [dbo].[PlantDetails] ([Id], [Description], [PlantPassportNumber], [ColorId], [FruitSizeId], [FruitTypeId], [PlantRef], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, N'Black Cherry tomatoes are a popular heirloom cherry tomato variety known for their deep, rich coloration—ranging from dark red to mahogany—and their sweet, complex flavor. They grow in clusters on tall, vigorous plants, producing round fruits that typically measure about one inch in diameter. The taste is often described as sweet yet balanced by a mild acidity, making them a favorite for fresh eating, salads, or colorful garnishes. Their striking color and robust flavor profile set them apart from more common cherry tomatoes, and they thrive best with full sun, consistent watering, and support such as stakes or cages to handle their abundant growth.', N'PN-123456', 2, 3, 2, 1, CAST(N'2025-01-02T19:07:08.9646107' AS DateTime2), CAST(N'2025-01-07T20:32:36.2464778' AS DateTime2), NULL, N'system', N'system', NULL)
INSERT [dbo].[PlantDetails] ([Id], [Description], [PlantPassportNumber], [ColorId], [FruitSizeId], [FruitTypeId], [PlantRef], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, N'The Kiwi tomato is a unique and visually striking heirloom variety known for its greenish-yellow skin and vibrant green flesh. This medium-sized, indeterminate tomato has a sweet, fruity flavor with subtle citrus notes, making it a favorite for fresh salads, sandwiches, and garnishes.

The fruit typically weighs around 6-8 ounces (170-225 grams) and has a slightly firm texture, with a smooth, juicy interior. Its unusual color and rich flavor make it a standout choice for gardeners and chefs looking to add diversity to their tomato repertoire.

Kiwi tomato plants are vigorous and productive, with a good resistance to common tomato diseases. They thrive in warm, sunny conditions and require staking or caging to support their indeterminate growth habit. This variety is an excellent addition to any garden, offering both aesthetic appeal and a delightful taste experience.', NULL, 8, 4, 2, 2, CAST(N'2025-01-07T20:26:18.5519601' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDetails] ([Id], [Description], [PlantPassportNumber], [ColorId], [FruitSizeId], [FruitTypeId], [PlantRef], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (3, N'An excellent determinate tomato variety that is a family heirloom from one of the Amish communities. According to available information, seeds of this variety were purchased by Douglas Wallace from Minnesota in the late 1990s. The plant is tall with regular leaves and requires staking. The fruits are very large, weighing between half a kilogram and one kilogram. They are pink, slightly flattened, and very meaty, of the beefsteak type. The skin is delicate, often with noticeable cracking near the stem. It has an excellent taste and an intense aroma. Perfect for salads and sandwiches. The fruits reach maturity about 80 days after being planted in the ground.', NULL, 6, 4, 2, 3, CAST(N'2025-01-09T16:08:04.1978948' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetails] ([Id], [Description], [PlantPassportNumber], [ColorId], [FruitSizeId], [FruitTypeId], [PlantRef], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, N'The "Mama Irenas" tomato is a traditional Polish tomato variety, known for its large size, robust flavor, and high-quality flesh. This heirloom variety produces juicy, meaty tomatoes with a balanced sweet and tangy taste, making them perfect for fresh consumption, sauces, or preserves. The fruits are typically round to slightly flattened, with a vibrant red color when fully ripe.

This variety is well-regarded for its vigorous growth and resistance to common tomato diseases, thriving in a range of climates. It’s particularly cherished by gardeners and growers for its high yield and reliability. Ideal for those seeking a flavorful and versatile tomato that celebrates traditional gardening practices.', NULL, 3, 4, 2, 4, CAST(N'2025-01-10T17:25:37.2510270' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetails] ([Id], [Description], [PlantPassportNumber], [ColorId], [FruitSizeId], [FruitTypeId], [PlantRef], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, N'The Gold Medal tomato is a striking heirloom variety known for its large, bi-colored fruits and exceptional flavor. This indeterminate tomato produces beefsteak-style fruits that can weigh up to 1-2 pounds (450-900 grams) each. The tomatoes have a beautiful, golden-yellow base with red marbling, creating a sunset-like appearance when sliced.

The flesh of the Gold Medal tomato is juicy, sweet, and low in acidity, making it ideal for fresh eating, slicing, or adding to salads. The plants are vigorous and productive, thriving in warm climates and requiring sturdy support due to the size of the fruits.

This variety is prized for its rich, complex taste and visual appeal, making it a favorite among gardeners and tomato enthusiasts. Gold Medal tomatoes typically take about 75-85 days from transplanting to reach maturity.', NULL, 9, 4, 2, 5, CAST(N'2025-01-17T15:10:36.8893861' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
SET IDENTITY_INSERT [dbo].[PlantDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[PlantDetailsImages] ON 

INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, N'd33a2cca-fb2c-4910-b4a4-ea203897fa1d-Black Cherry.png', 1, CAST(N'2025-01-02T19:07:09.0344867' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, N'53aa688c-6c60-4ffd-b028-12b2b0d9da3e-Black Cherry.jfif', 1, CAST(N'2025-01-02T19:07:09.0448861' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (3, N'd2e5c35f-2c5e-46f5-856e-aa4bded6f221-Kiwi.jpg', 2, CAST(N'2025-01-07T20:26:18.6718488' AS DateTime2), CAST(N'2025-01-10T17:30:18.8146014' AS DateTime2), CAST(N'2025-01-10T17:30:18.8146389' AS DateTime2), N'system', N'System', N'System')
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, N'7cacb1e6-f828-43b0-80eb-690a9b886364-Kiwi.webp', 2, CAST(N'2025-01-07T20:26:18.6721011' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, N'17c90930-efa3-4672-b156-a813b8c2d3a4-Todd County Amish.jpg', 3, CAST(N'2025-01-09T16:08:04.3124211' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (6, N'43265594-1129-4abe-9e4b-d54d6b7fb086-Todd County Amish.jpg', 3, CAST(N'2025-01-09T16:08:04.3267468' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (7, N'd73261ac-4876-4a9a-b096-4debe2b4610d-Todd County Amish.jpg', 3, CAST(N'2025-01-09T16:08:04.3366396' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (8, N'71092401-a792-41ec-b0da-fa9ef9330fec-Mama Irena''s.jpg', 4, CAST(N'2025-01-10T17:25:42.7143136' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (9, N'ebc564e8-da1a-417a-9ce5-28460461fe5b-Mama Irena''s.jpg', 4, CAST(N'2025-01-10T17:25:43.1668222' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (10, N'7ec4df3e-3aef-44f6-9ebc-465e123e87ca-Kiwi.jpg', 2, CAST(N'2025-01-10T17:29:54.1245578' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (11, N'ba17b52a-a2e6-464b-9cc9-1f6278e64bce-Kiwi.jpg', 2, CAST(N'2025-01-10T17:30:14.1635375' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (12, N'd47a0b35-0776-4888-97fe-f598d91311c9-Gold medal.jpg', 5, CAST(N'2025-01-17T15:10:36.9707101' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (13, N'4e94c138-cba5-4068-996d-df861f8726a7-Gold medal.jpg', 5, CAST(N'2025-01-17T15:10:36.9807018' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantDetailsImages] ([Id], [ImageURL], [PlantDetailId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (14, N'82889386-bc3f-45e3-ac00-6fef804f2bb2-Gold medal.jpg', 5, CAST(N'2025-01-17T15:10:36.9880395' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
SET IDENTITY_INSERT [dbo].[PlantDetailsImages] OFF
GO
SET IDENTITY_INSERT [dbo].[PlantGroups] ON 

INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (1, N'Nightshade', 1)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (2, N'Cucurbits', 1)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (3, N'Legumes', 1)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (4, N'Cruciferous', 1)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (5, N'Leafy', 1)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (6, N'Onion', 1)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (7, N'Root', 1)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (8, N'Turnip greens', 1)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (9, N'Pitted', 2)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (10, N'Berry', 2)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (11, N'Pome', 2)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (12, N'Citrus', 2)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (13, N'Exotic', 2)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (14, N'Healing', 3)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (15, N'Spicy', 3)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (16, N'Essential oil', 3)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (17, N'Outdoor', 4)
INSERT [dbo].[PlantGroups] ([Id], [Name], [PlantTypeId]) VALUES (18, N'Indoor', 4)
SET IDENTITY_INSERT [dbo].[PlantGroups] OFF
GO
INSERT [dbo].[PlantGrowingSeazons] ([PlantDetailId], [GrowingSeazonId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, 1, CAST(N'2025-01-02T19:07:08.9974936' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantGrowingSeazons] ([PlantDetailId], [GrowingSeazonId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, 1, CAST(N'2025-01-07T20:26:18.6213142' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantGrowingSeazons] ([PlantDetailId], [GrowingSeazonId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (3, 1, CAST(N'2025-01-09T16:08:04.2628222' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantGrowingSeazons] ([PlantDetailId], [GrowingSeazonId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (3, 5, CAST(N'2025-01-09T16:08:04.2629399' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantGrowingSeazons] ([PlantDetailId], [GrowingSeazonId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, 1, CAST(N'2025-01-10T17:25:41.0286946' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantGrowingSeazons] ([PlantDetailId], [GrowingSeazonId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, 5, CAST(N'2025-01-10T17:25:41.0288499' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantGrowingSeazons] ([PlantDetailId], [GrowingSeazonId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, 1, CAST(N'2025-01-17T15:10:36.9301517' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantGrowingSeazons] ([PlantDetailId], [GrowingSeazonId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, 5, CAST(N'2025-01-17T15:10:36.9302355' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
GO
INSERT [dbo].[PlantGrowthTypes] ([PlantDetailId], [GrowthTypeId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, 2, CAST(N'2025-01-07T20:32:56.2745806' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantGrowthTypes] ([PlantDetailId], [GrowthTypeId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, 2, CAST(N'2025-01-07T20:26:18.6037007' AS DateTime2), NULL, NULL, N'system', NULL, NULL)
INSERT [dbo].[PlantGrowthTypes] ([PlantDetailId], [GrowthTypeId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (3, 2, CAST(N'2025-01-09T16:08:04.2455242' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantGrowthTypes] ([PlantDetailId], [GrowthTypeId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, 2, CAST(N'2025-01-10T17:25:39.6459853' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantGrowthTypes] ([PlantDetailId], [GrowthTypeId], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, 2, CAST(N'2025-01-17T15:10:36.9192514' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
GO
INSERT [dbo].[PlantMessages] ([PlantId], [MessageId], [isSeed], [isSeedling], [isNewPlant], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, 1, 1, 0, 0, CAST(N'2025-01-13T13:43:43.5173863' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantMessages] ([PlantId], [MessageId], [isSeed], [isSeedling], [isNewPlant], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, 2, 0, 0, 0, CAST(N'2025-01-13T14:11:06.1266874' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[PlantOpinions] ON 

INSERT [dbo].[PlantOpinions] ([Id], [UserId], [Opinion], [PlantDetailId], [DateAdded], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, N'b9c413fb-7822-4bf2-8028-30597aab757b', N'Very tasty. Sweet, meaty. Ideal as a snack. Fairly resistant to disease.', 1, CAST(N'2025-01-13T13:33:48.6750612' AS DateTime2), CAST(N'2025-01-13T13:33:48.6952210' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantOpinions] ([Id], [UserId], [Opinion], [PlantDetailId], [DateAdded], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, N'2ef2b510-aa25-42ca-b68a-ee2fa0635924', N'Very tasty. I recomend.', 1, CAST(N'2025-01-16T17:45:38.4856431' AS DateTime2), CAST(N'2025-01-16T17:45:38.5970023' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
SET IDENTITY_INSERT [dbo].[PlantOpinions] OFF
GO
SET IDENTITY_INSERT [dbo].[Plants] ON 

INSERT [dbo].[Plants] ([Id], [PlantTypeId], [PlantGroupId], [PlantSectionId], [FullName], [Photo], [isActive], [isNew], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, 1, 1, 1, N'Black Cherry', N'1846f9c3-b21b-4c1f-b321-c365807b0f45-Black Cherry.png', 1, 0, CAST(N'2025-01-02T19:07:08.8242431' AS DateTime2), CAST(N'2025-01-07T20:32:36.0006601' AS DateTime2), NULL, N'system', N'system', NULL)
INSERT [dbo].[Plants] ([Id], [PlantTypeId], [PlantGroupId], [PlantSectionId], [FullName], [Photo], [isActive], [isNew], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, 1, 1, 1, N'Kiwi', N'ddd8d532-cdea-4b3f-b5e4-983b53e883c5-Kiwi.jpg', 1, 0, CAST(N'2025-01-07T20:26:18.2237652' AS DateTime2), CAST(N'2025-01-10T17:30:19.5849730' AS DateTime2), NULL, N'system', N'System', NULL)
INSERT [dbo].[Plants] ([Id], [PlantTypeId], [PlantGroupId], [PlantSectionId], [FullName], [Photo], [isActive], [isNew], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (3, 1, 1, 1, N'Todd County Amish', N'3c173cad-2347-4f0a-a32c-406f13f0e60b-Todd County Amish.jpg', 1, 0, CAST(N'2025-01-09T16:08:03.9919979' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[Plants] ([Id], [PlantTypeId], [PlantGroupId], [PlantSectionId], [FullName], [Photo], [isActive], [isNew], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, 1, 1, 1, N'Mama Irena''s', N'5e835029-223c-4fba-ab00-d2bc17271492-Mama Irena''s.jpg', 1, 0, CAST(N'2025-01-10T17:25:33.1028228' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[Plants] ([Id], [PlantTypeId], [PlantGroupId], [PlantSectionId], [FullName], [Photo], [isActive], [isNew], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (5, 1, 1, 1, N'Gold medal', N'9507735d-f896-4403-aafd-62db4fa6b178-Gold medal.jpg', 1, 0, CAST(N'2025-01-17T15:10:36.7549818' AS DateTime2), CAST(N'2025-01-17T15:12:08.7142288' AS DateTime2), NULL, N'System', N'System', NULL)
SET IDENTITY_INSERT [dbo].[Plants] OFF
GO
SET IDENTITY_INSERT [dbo].[PlantSections] ON 

INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (1, N'Tomato', 1)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (2, N'Pepper', 1)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (3, N'Potato', 1)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (4, N'Eggplant', 1)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (5, N'Other', 1)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (6, N'Cucumber', 2)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (7, N'Zucchini', 2)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (8, N'Pumpkin', 2)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (9, N'Patison', 2)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (10, N'Other', 2)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (11, N'Beans', 3)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (12, N'Pea', 3)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (13, N'Lentils', 3)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (14, N'Broad bean', 3)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (15, N'Other', 3)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (16, N'Cabbage', 4)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (17, N'Brussels sprouts', 4)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (18, N'Broccoli', 4)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (19, N'Cauliflower', 4)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (20, N'Kohlrabi', 4)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (21, N'Other', 4)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (22, N'Lettuce', 5)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (23, N'Spinach', 5)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (24, N'Leaf parsley', 5)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (25, N'Other', 5)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (26, N'Onion', 6)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (27, N'Garlic', 6)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (28, N'Leek', 6)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (29, N'Other', 6)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (30, N'Carrot', 7)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (31, N'Root parsley', 7)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (32, N'Beetroot', 7)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (33, N'Root celery', 7)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (34, N'Other', 7)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (35, N'Radish', 8)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (36, N'Rutabaga', 8)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (37, N'Turnip', 8)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (38, N'Other', 8)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (39, N'Cherries', 9)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (40, N'Peach', 9)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (41, N'Plum', 9)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (42, N'Apricot', 9)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (43, N'Other', 9)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (44, N'Strawberry', 10)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (45, N'Blackberries', 10)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (46, N'Blueberries', 10)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (47, N'Raspberries', 10)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (48, N'Currants', 10)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (49, N'Berries', 10)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (50, N'Other', 10)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (51, N'Apple', 11)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (52, N'Pear', 11)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (53, N'Quince', 11)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (54, N'Pomegranate', 11)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (55, N'Other', 11)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (56, N'Lemon', 12)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (57, N'Tangerine', 12)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (58, N'Orange', 12)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (59, N'Grapefruit', 12)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (60, N'Other', 12)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (61, N'Banana', 13)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (62, N'Pineapple', 13)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (63, N'Lychee', 13)
INSERT [dbo].[PlantSections] ([Id], [Name], [PlantGroupId]) VALUES (64, N'Other', 13)
SET IDENTITY_INSERT [dbo].[PlantSections] OFF
GO
SET IDENTITY_INSERT [dbo].[PlantSeeds] ON 

INSERT [dbo].[PlantSeeds] ([Id], [UserId], [PlantId], [Count], [Description], [DateAdded], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (1, N'2ef2b510-aa25-42ca-b68a-ee2fa0635924', 1, 40, N'Eco cultivation. Disease-free seeds.', CAST(N'2025-01-13T13:22:32.1627266' AS DateTime2), CAST(N'2025-01-13T13:22:32.2159259' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantSeeds] ([Id], [UserId], [PlantId], [Count], [Description], [DateAdded], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (2, N'2ef2b510-aa25-42ca-b68a-ee2fa0635924', 2, 20, N'Eco cultivation. Disease-free seeds', CAST(N'2025-01-13T13:24:25.7575921' AS DateTime2), CAST(N'2025-01-13T13:24:25.7590480' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantSeeds] ([Id], [UserId], [PlantId], [Count], [Description], [DateAdded], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (3, N'b9c413fb-7822-4bf2-8028-30597aab757b', 3, 50, N'Eco cultivation. Disease-free seeds', CAST(N'2025-01-13T13:30:49.9765419' AS DateTime2), CAST(N'2025-01-13T13:30:50.0377236' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
INSERT [dbo].[PlantSeeds] ([Id], [UserId], [PlantId], [Count], [Description], [DateAdded], [CreatedAtUtc], [UpdatedAtUtc], [InactivatedAtUtc], [CreatedBy], [UpdatedBy], [InactivatedBy]) VALUES (4, N'b9c413fb-7822-4bf2-8028-30597aab757b', 4, 10, N'Eco cultivation. Disease-free seeds', CAST(N'2025-01-13T13:31:26.2874960' AS DateTime2), CAST(N'2025-01-13T13:31:26.2893994' AS DateTime2), NULL, NULL, N'System', NULL, NULL)
SET IDENTITY_INSERT [dbo].[PlantSeeds] OFF
GO
SET IDENTITY_INSERT [dbo].[PlantTypes] ON 

INSERT [dbo].[PlantTypes] ([Id], [Name]) VALUES (1, N'Vegetable')
INSERT [dbo].[PlantTypes] ([Id], [Name]) VALUES (2, N'Fruit')
INSERT [dbo].[PlantTypes] ([Id], [Name]) VALUES (3, N'Herb')
INSERT [dbo].[PlantTypes] ([Id], [Name]) VALUES (4, N'Flower')
INSERT [dbo].[PlantTypes] ([Id], [Name]) VALUES (5, N'Home plant')
INSERT [dbo].[PlantTypes] ([Id], [Name]) VALUES (6, N'Grass')
SET IDENTITY_INSERT [dbo].[PlantTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Regions] ON 

INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (1, N'Dolnośląskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (2, N'Kujawsko-Pomorskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (3, N'Lubelskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (4, N'Lubuskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (5, N'Łódzkie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (6, N'Małopolskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (7, N'Mazowieckie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (8, N'Opolskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (9, N'Podkarpackie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (10, N'Podlaskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (11, N'Pomorskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (12, N'Śląskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (13, N'Świętokrzyskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (14, N'Warmińsko-Mazurskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (15, N'Wielkopolskie', 1)
INSERT [dbo].[Regions] ([Id], [Name], [CountryId]) VALUES (16, N'Zachodniopomorskie', 1)
SET IDENTITY_INSERT [dbo].[Regions] OFF
GO
/****** Object:  Index [IX_Addresses_CityId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Addresses_CityId] ON [dbo].[Addresses]
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Addresses_CountryId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Addresses_CountryId] ON [dbo].[Addresses]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Addresses_RegionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Addresses_RegionId] ON [dbo].[Addresses]
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Addresses_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Addresses_UserId] ON [dbo].[Addresses]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 24.07.2025 11:42:49 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 24.07.2025 11:42:49 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AuditTrials_EntityName]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_AuditTrials_EntityName] ON [dbo].[AuditTrials]
(
	[EntityName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AuditTrials_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_AuditTrials_UserId] ON [dbo].[AuditTrials]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cities_RegionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Cities_RegionId] ON [dbo].[Cities]
(
	[RegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CompanyContactInformations_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_CompanyContactInformations_UserId] ON [dbo].[CompanyContactInformations]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ContactDetailForSeedlings_ContactDetailId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_ContactDetailForSeedlings_ContactDetailId] ON [dbo].[ContactDetailForSeedlings]
(
	[ContactDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ContactDetailForSeeds_ContactDetailId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_ContactDetailForSeeds_ContactDetailId] ON [dbo].[ContactDetailForSeeds]
(
	[ContactDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ContactDetails_ContactDetailTypeID]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_ContactDetails_ContactDetailTypeID] ON [dbo].[ContactDetails]
(
	[ContactDetailTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ContactDetails_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_ContactDetails_UserId] ON [dbo].[ContactDetails]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FruitSizeForListFilters_PlantGroupId_PlantSectionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_FruitSizeForListFilters_PlantGroupId_PlantSectionId] ON [dbo].[FruitSizeForListFilters]
(
	[PlantGroupId] ASC,
	[PlantSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FruitSizeForListFilters_PlantSectionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_FruitSizeForListFilters_PlantSectionId] ON [dbo].[FruitSizeForListFilters]
(
	[PlantSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FruitSizeForListFilters_PlantTypeId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_FruitSizeForListFilters_PlantTypeId] ON [dbo].[FruitSizeForListFilters]
(
	[PlantTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FruitTypeForListFilters_PlantGroupId_PlantSectionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_FruitTypeForListFilters_PlantGroupId_PlantSectionId] ON [dbo].[FruitTypeForListFilters]
(
	[PlantGroupId] ASC,
	[PlantSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FruitTypeForListFilters_PlantSectionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_FruitTypeForListFilters_PlantSectionId] ON [dbo].[FruitTypeForListFilters]
(
	[PlantSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FruitTypeForListFilters_PlantTypeId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_FruitTypeForListFilters_PlantTypeId] ON [dbo].[FruitTypeForListFilters]
(
	[PlantTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GrowthTypesForListFilters_PlantGroupId_PlantSectionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_GrowthTypesForListFilters_PlantGroupId_PlantSectionId] ON [dbo].[GrowthTypesForListFilters]
(
	[PlantGroupId] ASC,
	[PlantSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GrowthTypesForListFilters_PlantSectionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_GrowthTypesForListFilters_PlantSectionId] ON [dbo].[GrowthTypesForListFilters]
(
	[PlantSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GrowthTypesForListFilters_PlantTypeId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_GrowthTypesForListFilters_PlantTypeId] ON [dbo].[GrowthTypesForListFilters]
(
	[PlantTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageAnswers_MessageAnswerId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_MessageAnswers_MessageAnswerId] ON [dbo].[MessageAnswers]
(
	[MessageAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_MessageReceivers_MessageId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_MessageReceivers_MessageId] ON [dbo].[MessageReceivers]
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_MessageReceivers_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_MessageReceivers_UserId] ON [dbo].[MessageReceivers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Messages_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Messages_UserId] ON [dbo].[Messages]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_NewUserPlants_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_NewUserPlants_UserId] ON [dbo].[NewUserPlants]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantDestinations_DestinationId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantDestinations_DestinationId] ON [dbo].[PlantDestinations]
(
	[DestinationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantDetails_ColorId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantDetails_ColorId] ON [dbo].[PlantDetails]
(
	[ColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantDetails_FruitSizeId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantDetails_FruitSizeId] ON [dbo].[PlantDetails]
(
	[FruitSizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantDetails_FruitTypeId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantDetails_FruitTypeId] ON [dbo].[PlantDetails]
(
	[FruitTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantDetails_PlantRef]    Script Date: 24.07.2025 11:42:49 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_PlantDetails_PlantRef] ON [dbo].[PlantDetails]
(
	[PlantRef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantDetailsImages_PlantDetailId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantDetailsImages_PlantDetailId] ON [dbo].[PlantDetailsImages]
(
	[PlantDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantGroups_PlantTypeId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantGroups_PlantTypeId] ON [dbo].[PlantGroups]
(
	[PlantTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantGrowingSeazons_GrowingSeazonId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantGrowingSeazons_GrowingSeazonId] ON [dbo].[PlantGrowingSeazons]
(
	[GrowingSeazonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantGrowthTypes_GrowthTypeId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantGrowthTypes_GrowthTypeId] ON [dbo].[PlantGrowthTypes]
(
	[GrowthTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantMessages_MessageId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantMessages_MessageId] ON [dbo].[PlantMessages]
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantOpinions_PlantDetailId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantOpinions_PlantDetailId] ON [dbo].[PlantOpinions]
(
	[PlantDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PlantOpinions_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantOpinions_UserId] ON [dbo].[PlantOpinions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Plants_PlantGroupId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Plants_PlantGroupId] ON [dbo].[Plants]
(
	[PlantGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Plants_PlantSectionId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Plants_PlantSectionId] ON [dbo].[Plants]
(
	[PlantSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Plants_PlantTypeId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Plants_PlantTypeId] ON [dbo].[Plants]
(
	[PlantTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantSections_PlantGroupId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantSections_PlantGroupId] ON [dbo].[PlantSections]
(
	[PlantGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantSeedlings_PlantId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantSeedlings_PlantId] ON [dbo].[PlantSeedlings]
(
	[PlantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PlantSeedlings_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantSeedlings_UserId] ON [dbo].[PlantSeedlings]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantSeeds_PlantId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantSeeds_PlantId] ON [dbo].[PlantSeeds]
(
	[PlantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PlantSeeds_UserId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantSeeds_UserId] ON [dbo].[PlantSeeds]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlantTags_TagId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_PlantTags_TagId] ON [dbo].[PlantTags]
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Regions_CountryId]    Script Date: 24.07.2025 11:42:49 ******/
CREATE NONCLUSTERED INDEX [IX_Regions_CountryId] ON [dbo].[Regions]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TypeOfAvailabilities_PlantRef]    Script Date: 24.07.2025 11:42:49 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TypeOfAvailabilities_PlantRef] ON [dbo].[TypeOfAvailabilities]
(
	[PlantRef] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Cities_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Cities_CityId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Countries_CountryId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Regions_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Regions_RegionId]
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
ALTER TABLE [dbo].[AuditTrials]  WITH CHECK ADD  CONSTRAINT [FK_AuditTrials_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[AuditTrials] CHECK CONSTRAINT [FK_AuditTrials_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Regions_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Regions_RegionId]
GO
ALTER TABLE [dbo].[CompanyContactInformations]  WITH CHECK ADD  CONSTRAINT [FK_CompanyContactInformations_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[CompanyContactInformations] CHECK CONSTRAINT [FK_CompanyContactInformations_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ContactDetailForSeedlings]  WITH CHECK ADD  CONSTRAINT [FK_ContactDetailForSeedlings_ContactDetails_ContactDetailId] FOREIGN KEY([ContactDetailId])
REFERENCES [dbo].[ContactDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactDetailForSeedlings] CHECK CONSTRAINT [FK_ContactDetailForSeedlings_ContactDetails_ContactDetailId]
GO
ALTER TABLE [dbo].[ContactDetailForSeedlings]  WITH CHECK ADD  CONSTRAINT [FK_ContactDetailForSeedlings_PlantSeedlings_PlantSeedlingId] FOREIGN KEY([PlantSeedlingId])
REFERENCES [dbo].[PlantSeedlings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactDetailForSeedlings] CHECK CONSTRAINT [FK_ContactDetailForSeedlings_PlantSeedlings_PlantSeedlingId]
GO
ALTER TABLE [dbo].[ContactDetailForSeeds]  WITH CHECK ADD  CONSTRAINT [FK_ContactDetailForSeeds_ContactDetails_ContactDetailId] FOREIGN KEY([ContactDetailId])
REFERENCES [dbo].[ContactDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactDetailForSeeds] CHECK CONSTRAINT [FK_ContactDetailForSeeds_ContactDetails_ContactDetailId]
GO
ALTER TABLE [dbo].[ContactDetailForSeeds]  WITH CHECK ADD  CONSTRAINT [FK_ContactDetailForSeeds_PlantSeeds_PlantSeedId] FOREIGN KEY([PlantSeedId])
REFERENCES [dbo].[PlantSeeds] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactDetailForSeeds] CHECK CONSTRAINT [FK_ContactDetailForSeeds_PlantSeeds_PlantSeedId]
GO
ALTER TABLE [dbo].[ContactDetails]  WITH CHECK ADD  CONSTRAINT [FK_ContactDetails_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ContactDetails] CHECK CONSTRAINT [FK_ContactDetails_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ContactDetails]  WITH CHECK ADD  CONSTRAINT [FK_ContactDetails_ContactDetailTypes_ContactDetailTypeID] FOREIGN KEY([ContactDetailTypeID])
REFERENCES [dbo].[ContactDetailTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactDetails] CHECK CONSTRAINT [FK_ContactDetails_ContactDetailTypes_ContactDetailTypeID]
GO
ALTER TABLE [dbo].[FruitSizeForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_FruitSizeForListFilters_FruitSizes_FruitSizeId] FOREIGN KEY([FruitSizeId])
REFERENCES [dbo].[FruitSizes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FruitSizeForListFilters] CHECK CONSTRAINT [FK_FruitSizeForListFilters_FruitSizes_FruitSizeId]
GO
ALTER TABLE [dbo].[FruitSizeForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_FruitSizeForListFilters_PlantGroups_PlantGroupId] FOREIGN KEY([PlantGroupId])
REFERENCES [dbo].[PlantGroups] ([Id])
GO
ALTER TABLE [dbo].[FruitSizeForListFilters] CHECK CONSTRAINT [FK_FruitSizeForListFilters_PlantGroups_PlantGroupId]
GO
ALTER TABLE [dbo].[FruitSizeForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_FruitSizeForListFilters_PlantSections_PlantSectionId] FOREIGN KEY([PlantSectionId])
REFERENCES [dbo].[PlantSections] ([Id])
GO
ALTER TABLE [dbo].[FruitSizeForListFilters] CHECK CONSTRAINT [FK_FruitSizeForListFilters_PlantSections_PlantSectionId]
GO
ALTER TABLE [dbo].[FruitSizeForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_FruitSizeForListFilters_PlantTypes_PlantTypeId] FOREIGN KEY([PlantTypeId])
REFERENCES [dbo].[PlantTypes] ([Id])
GO
ALTER TABLE [dbo].[FruitSizeForListFilters] CHECK CONSTRAINT [FK_FruitSizeForListFilters_PlantTypes_PlantTypeId]
GO
ALTER TABLE [dbo].[FruitTypeForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_FruitTypeForListFilters_FruitTypes_FruitTypeId] FOREIGN KEY([FruitTypeId])
REFERENCES [dbo].[FruitTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FruitTypeForListFilters] CHECK CONSTRAINT [FK_FruitTypeForListFilters_FruitTypes_FruitTypeId]
GO
ALTER TABLE [dbo].[FruitTypeForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_FruitTypeForListFilters_PlantGroups_PlantGroupId] FOREIGN KEY([PlantGroupId])
REFERENCES [dbo].[PlantGroups] ([Id])
GO
ALTER TABLE [dbo].[FruitTypeForListFilters] CHECK CONSTRAINT [FK_FruitTypeForListFilters_PlantGroups_PlantGroupId]
GO
ALTER TABLE [dbo].[FruitTypeForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_FruitTypeForListFilters_PlantSections_PlantSectionId] FOREIGN KEY([PlantSectionId])
REFERENCES [dbo].[PlantSections] ([Id])
GO
ALTER TABLE [dbo].[FruitTypeForListFilters] CHECK CONSTRAINT [FK_FruitTypeForListFilters_PlantSections_PlantSectionId]
GO
ALTER TABLE [dbo].[FruitTypeForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_FruitTypeForListFilters_PlantTypes_PlantTypeId] FOREIGN KEY([PlantTypeId])
REFERENCES [dbo].[PlantTypes] ([Id])
GO
ALTER TABLE [dbo].[FruitTypeForListFilters] CHECK CONSTRAINT [FK_FruitTypeForListFilters_PlantTypes_PlantTypeId]
GO
ALTER TABLE [dbo].[GrowthTypesForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_GrowthTypesForListFilters_GrowthTypes_GrowthTypesId] FOREIGN KEY([GrowthTypesId])
REFERENCES [dbo].[GrowthTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GrowthTypesForListFilters] CHECK CONSTRAINT [FK_GrowthTypesForListFilters_GrowthTypes_GrowthTypesId]
GO
ALTER TABLE [dbo].[GrowthTypesForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_GrowthTypesForListFilters_PlantGroups_PlantGroupId] FOREIGN KEY([PlantGroupId])
REFERENCES [dbo].[PlantGroups] ([Id])
GO
ALTER TABLE [dbo].[GrowthTypesForListFilters] CHECK CONSTRAINT [FK_GrowthTypesForListFilters_PlantGroups_PlantGroupId]
GO
ALTER TABLE [dbo].[GrowthTypesForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_GrowthTypesForListFilters_PlantSections_PlantSectionId] FOREIGN KEY([PlantSectionId])
REFERENCES [dbo].[PlantSections] ([Id])
GO
ALTER TABLE [dbo].[GrowthTypesForListFilters] CHECK CONSTRAINT [FK_GrowthTypesForListFilters_PlantSections_PlantSectionId]
GO
ALTER TABLE [dbo].[GrowthTypesForListFilters]  WITH CHECK ADD  CONSTRAINT [FK_GrowthTypesForListFilters_PlantTypes_PlantTypeId] FOREIGN KEY([PlantTypeId])
REFERENCES [dbo].[PlantTypes] ([Id])
GO
ALTER TABLE [dbo].[GrowthTypesForListFilters] CHECK CONSTRAINT [FK_GrowthTypesForListFilters_PlantTypes_PlantTypeId]
GO
ALTER TABLE [dbo].[MessageAnswers]  WITH CHECK ADD  CONSTRAINT [FK_MessageAnswers_Messages_MessageAnswerId] FOREIGN KEY([MessageAnswerId])
REFERENCES [dbo].[Messages] ([Id])
GO
ALTER TABLE [dbo].[MessageAnswers] CHECK CONSTRAINT [FK_MessageAnswers_Messages_MessageAnswerId]
GO
ALTER TABLE [dbo].[MessageAnswers]  WITH CHECK ADD  CONSTRAINT [FK_MessageAnswers_Messages_MessageId] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageAnswers] CHECK CONSTRAINT [FK_MessageAnswers_Messages_MessageId]
GO
ALTER TABLE [dbo].[MessageReceivers]  WITH CHECK ADD  CONSTRAINT [FK_MessageReceivers_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[MessageReceivers] CHECK CONSTRAINT [FK_MessageReceivers_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[MessageReceivers]  WITH CHECK ADD  CONSTRAINT [FK_MessageReceivers_Messages_MessageId] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MessageReceivers] CHECK CONSTRAINT [FK_MessageReceivers_Messages_MessageId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[NewUserPlants]  WITH CHECK ADD  CONSTRAINT [FK_NewUserPlants_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewUserPlants] CHECK CONSTRAINT [FK_NewUserPlants_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[NewUserPlants]  WITH CHECK ADD  CONSTRAINT [FK_NewUserPlants_Plants_PlantId] FOREIGN KEY([PlantId])
REFERENCES [dbo].[Plants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewUserPlants] CHECK CONSTRAINT [FK_NewUserPlants_Plants_PlantId]
GO
ALTER TABLE [dbo].[PlantDestinations]  WITH CHECK ADD  CONSTRAINT [FK_PlantDestinations_Destinations_DestinationId] FOREIGN KEY([DestinationId])
REFERENCES [dbo].[Destinations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantDestinations] CHECK CONSTRAINT [FK_PlantDestinations_Destinations_DestinationId]
GO
ALTER TABLE [dbo].[PlantDestinations]  WITH CHECK ADD  CONSTRAINT [FK_PlantDestinations_PlantDetails_PlantDetailId] FOREIGN KEY([PlantDetailId])
REFERENCES [dbo].[PlantDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantDestinations] CHECK CONSTRAINT [FK_PlantDestinations_PlantDetails_PlantDetailId]
GO
ALTER TABLE [dbo].[PlantDetails]  WITH CHECK ADD  CONSTRAINT [FK_PlantDetails_Colors_ColorId] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Colors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantDetails] CHECK CONSTRAINT [FK_PlantDetails_Colors_ColorId]
GO
ALTER TABLE [dbo].[PlantDetails]  WITH CHECK ADD  CONSTRAINT [FK_PlantDetails_FruitSizes_FruitSizeId] FOREIGN KEY([FruitSizeId])
REFERENCES [dbo].[FruitSizes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantDetails] CHECK CONSTRAINT [FK_PlantDetails_FruitSizes_FruitSizeId]
GO
ALTER TABLE [dbo].[PlantDetails]  WITH CHECK ADD  CONSTRAINT [FK_PlantDetails_FruitTypes_FruitTypeId] FOREIGN KEY([FruitTypeId])
REFERENCES [dbo].[FruitTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantDetails] CHECK CONSTRAINT [FK_PlantDetails_FruitTypes_FruitTypeId]
GO
ALTER TABLE [dbo].[PlantDetails]  WITH CHECK ADD  CONSTRAINT [FK_PlantDetails_Plants_PlantRef] FOREIGN KEY([PlantRef])
REFERENCES [dbo].[Plants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantDetails] CHECK CONSTRAINT [FK_PlantDetails_Plants_PlantRef]
GO
ALTER TABLE [dbo].[PlantDetailsImages]  WITH CHECK ADD  CONSTRAINT [FK_PlantDetailsImages_PlantDetails_PlantDetailId] FOREIGN KEY([PlantDetailId])
REFERENCES [dbo].[PlantDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantDetailsImages] CHECK CONSTRAINT [FK_PlantDetailsImages_PlantDetails_PlantDetailId]
GO
ALTER TABLE [dbo].[PlantGroups]  WITH CHECK ADD  CONSTRAINT [FK_PlantGroups_PlantTypes_PlantTypeId] FOREIGN KEY([PlantTypeId])
REFERENCES [dbo].[PlantTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantGroups] CHECK CONSTRAINT [FK_PlantGroups_PlantTypes_PlantTypeId]
GO
ALTER TABLE [dbo].[PlantGrowingSeazons]  WITH CHECK ADD  CONSTRAINT [FK_PlantGrowingSeazons_GrowingSeazons_GrowingSeazonId] FOREIGN KEY([GrowingSeazonId])
REFERENCES [dbo].[GrowingSeazons] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantGrowingSeazons] CHECK CONSTRAINT [FK_PlantGrowingSeazons_GrowingSeazons_GrowingSeazonId]
GO
ALTER TABLE [dbo].[PlantGrowingSeazons]  WITH CHECK ADD  CONSTRAINT [FK_PlantGrowingSeazons_PlantDetails_PlantDetailId] FOREIGN KEY([PlantDetailId])
REFERENCES [dbo].[PlantDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantGrowingSeazons] CHECK CONSTRAINT [FK_PlantGrowingSeazons_PlantDetails_PlantDetailId]
GO
ALTER TABLE [dbo].[PlantGrowthTypes]  WITH CHECK ADD  CONSTRAINT [FK_PlantGrowthTypes_GrowthTypes_GrowthTypeId] FOREIGN KEY([GrowthTypeId])
REFERENCES [dbo].[GrowthTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantGrowthTypes] CHECK CONSTRAINT [FK_PlantGrowthTypes_GrowthTypes_GrowthTypeId]
GO
ALTER TABLE [dbo].[PlantGrowthTypes]  WITH CHECK ADD  CONSTRAINT [FK_PlantGrowthTypes_PlantDetails_PlantDetailId] FOREIGN KEY([PlantDetailId])
REFERENCES [dbo].[PlantDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantGrowthTypes] CHECK CONSTRAINT [FK_PlantGrowthTypes_PlantDetails_PlantDetailId]
GO
ALTER TABLE [dbo].[PlantMessages]  WITH CHECK ADD  CONSTRAINT [FK_PlantMessages_Messages_MessageId] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantMessages] CHECK CONSTRAINT [FK_PlantMessages_Messages_MessageId]
GO
ALTER TABLE [dbo].[PlantMessages]  WITH CHECK ADD  CONSTRAINT [FK_PlantMessages_Plants_PlantId] FOREIGN KEY([PlantId])
REFERENCES [dbo].[Plants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantMessages] CHECK CONSTRAINT [FK_PlantMessages_Plants_PlantId]
GO
ALTER TABLE [dbo].[PlantOpinions]  WITH CHECK ADD  CONSTRAINT [FK_PlantOpinions_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PlantOpinions] CHECK CONSTRAINT [FK_PlantOpinions_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[PlantOpinions]  WITH CHECK ADD  CONSTRAINT [FK_PlantOpinions_PlantDetails_PlantDetailId] FOREIGN KEY([PlantDetailId])
REFERENCES [dbo].[PlantDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantOpinions] CHECK CONSTRAINT [FK_PlantOpinions_PlantDetails_PlantDetailId]
GO
ALTER TABLE [dbo].[Plants]  WITH CHECK ADD  CONSTRAINT [FK_Plants_PlantGroups_PlantGroupId] FOREIGN KEY([PlantGroupId])
REFERENCES [dbo].[PlantGroups] ([Id])
GO
ALTER TABLE [dbo].[Plants] CHECK CONSTRAINT [FK_Plants_PlantGroups_PlantGroupId]
GO
ALTER TABLE [dbo].[Plants]  WITH CHECK ADD  CONSTRAINT [FK_Plants_PlantSections_PlantSectionId] FOREIGN KEY([PlantSectionId])
REFERENCES [dbo].[PlantSections] ([Id])
GO
ALTER TABLE [dbo].[Plants] CHECK CONSTRAINT [FK_Plants_PlantSections_PlantSectionId]
GO
ALTER TABLE [dbo].[Plants]  WITH CHECK ADD  CONSTRAINT [FK_Plants_PlantTypes_PlantTypeId] FOREIGN KEY([PlantTypeId])
REFERENCES [dbo].[PlantTypes] ([Id])
GO
ALTER TABLE [dbo].[Plants] CHECK CONSTRAINT [FK_Plants_PlantTypes_PlantTypeId]
GO
ALTER TABLE [dbo].[PlantSections]  WITH CHECK ADD  CONSTRAINT [FK_PlantSections_PlantGroups_PlantGroupId] FOREIGN KEY([PlantGroupId])
REFERENCES [dbo].[PlantGroups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantSections] CHECK CONSTRAINT [FK_PlantSections_PlantGroups_PlantGroupId]
GO
ALTER TABLE [dbo].[PlantSeedlings]  WITH CHECK ADD  CONSTRAINT [FK_PlantSeedlings_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PlantSeedlings] CHECK CONSTRAINT [FK_PlantSeedlings_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[PlantSeedlings]  WITH CHECK ADD  CONSTRAINT [FK_PlantSeedlings_Plants_PlantId] FOREIGN KEY([PlantId])
REFERENCES [dbo].[Plants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantSeedlings] CHECK CONSTRAINT [FK_PlantSeedlings_Plants_PlantId]
GO
ALTER TABLE [dbo].[PlantSeeds]  WITH CHECK ADD  CONSTRAINT [FK_PlantSeeds_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PlantSeeds] CHECK CONSTRAINT [FK_PlantSeeds_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[PlantSeeds]  WITH CHECK ADD  CONSTRAINT [FK_PlantSeeds_Plants_PlantId] FOREIGN KEY([PlantId])
REFERENCES [dbo].[Plants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantSeeds] CHECK CONSTRAINT [FK_PlantSeeds_Plants_PlantId]
GO
ALTER TABLE [dbo].[PlantTags]  WITH CHECK ADD  CONSTRAINT [FK_PlantTags_Plants_PlantId] FOREIGN KEY([PlantId])
REFERENCES [dbo].[Plants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantTags] CHECK CONSTRAINT [FK_PlantTags_Plants_PlantId]
GO
ALTER TABLE [dbo].[PlantTags]  WITH CHECK ADD  CONSTRAINT [FK_PlantTags_Tags_TagId] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlantTags] CHECK CONSTRAINT [FK_PlantTags_Tags_TagId]
GO
ALTER TABLE [dbo].[Regions]  WITH CHECK ADD  CONSTRAINT [FK_Regions_Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Regions] CHECK CONSTRAINT [FK_Regions_Countries_CountryId]
GO
ALTER TABLE [dbo].[TypeOfAvailabilities]  WITH CHECK ADD  CONSTRAINT [FK_TypeOfAvailabilities_Plants_PlantRef] FOREIGN KEY([PlantRef])
REFERENCES [dbo].[Plants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TypeOfAvailabilities] CHECK CONSTRAINT [FK_TypeOfAvailabilities_Plants_PlantRef]
GO
USE [master]
GO
ALTER DATABASE [VFHCatalog] SET  READ_WRITE 
GO
