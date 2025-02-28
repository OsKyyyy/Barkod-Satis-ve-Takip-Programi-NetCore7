USE [master]
GO
/****** Object:  Database [Market]    Script Date: 18.01.2025 16:11:14 ******/
CREATE DATABASE [Market]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Market', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Market.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Market_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Market_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Market] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Market].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Market] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Market] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Market] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Market] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Market] SET ARITHABORT OFF 
GO
ALTER DATABASE [Market] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Market] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Market] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Market] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Market] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Market] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Market] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Market] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Market] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Market] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Market] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Market] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Market] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Market] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Market] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Market] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Market] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Market] SET RECOVERY FULL 
GO
ALTER DATABASE [Market] SET  MULTI_USER 
GO
ALTER DATABASE [Market] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Market] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Market] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Market] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Market] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Market] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Market', N'ON'
GO
ALTER DATABASE [Market] SET QUERY_STORE = OFF
GO
USE [Market]
GO
/****** Object:  User [IIS APPPOOL\WebApi]    Script Date: 18.01.2025 16:11:14 ******/
CREATE USER [IIS APPPOOL\WebApi] FOR LOGIN [IIS APPPOOL\WebApi] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [IIS APPPOOL\DefaultAppPool]    Script Date: 18.01.2025 16:11:14 ******/
CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [IIS APPPOOL\api_pool]    Script Date: 18.01.2025 16:11:14 ******/
CREATE USER [IIS APPPOOL\api_pool] FOR LOGIN [IIS APPPOOL\api_pool] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\WebApi]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\DefaultAppPool]
GO
ALTER ROLE [db_owner] ADD MEMBER [IIS APPPOOL\api_pool]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerMovements]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerMovements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[SaleId] [int] NULL,
	[ProcessType] [tinyint] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[ProductInformation] [nvarchar](500) NULL,
	[Note] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_CustomerMovements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nchar](10) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncomeAndExpenses]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IncomeAndExpenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IncomeExpensesTypeId] [int] NOT NULL,
	[Type] [bit] NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NULL,
	[UpdateUserId] [int] NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_IncomeAndExpenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncomeAndExpensesTypes]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IncomeAndExpensesTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_IncomeAndExpensesTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationClaims]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_OperationClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageClaims]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](100) NOT NULL,
	[OperationClaimId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pos]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Basket] [tinyint] NOT NULL,
	[Barcode] [varchar](50) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[ProductUnitPrice] [decimal](10, 2) NOT NULL,
	[ProductUnitPurchasePrice] [decimal](10, 2) NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[CreateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Basket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[PurchasePrice] [decimal](10, 2) NOT NULL,
	[SalePrice] [decimal](10, 2) NOT NULL,
	[PreviousSellingPrice] [decimal](10, 2) NOT NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[UnitType] [nvarchar](20) NULL,
	[Barcode] [nvarchar](50) NOT NULL,
	[Stock] [int] NOT NULL,
	[CriticalStock] [int] NULL,
	[Image] [nvarchar](150) NULL,
	[Favorite] [bit] NOT NULL,
	[Origin] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NULL,
	[UpdateUserId] [int] NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleProducts]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaleId] [int] NOT NULL,
	[Barcode] [varchar](50) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[ProductUnitPrice] [decimal](10, 2) NOT NULL,
	[ProductUnitPurchasePrice] [decimal](10, 2) NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_SaleProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[PaymentType] [tinyint] NOT NULL,
	[ComplateType] [tinyint] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOperationClaims]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOperationClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OperationClaimId] [int] NOT NULL,
 CONSTRAINT [PK_UserOperationClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[PasswordHash] [varbinary](500) NOT NULL,
	[PasswordSalt] [varbinary](500) NOT NULL,
	[Status] [bit] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WholeSalerMovements]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WholeSalerMovements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WholeSalerId] [int] NOT NULL,
	[ProcessType] [tinyint] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[Image] [nvarchar](150) NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_WholeSalerMovements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WholeSalers]    Script Date: 18.01.2025 16:11:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WholeSalers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[AuthorizedPerson] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[Status] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateUserId] [int] NOT NULL,
	[Deleted] [bit] NULL,
 CONSTRAINT [PK_WholeSaler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Market] SET  READ_WRITE 
GO
