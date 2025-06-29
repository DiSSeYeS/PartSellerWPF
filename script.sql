USE [master]
GO
/****** Object:  Database [ComponentsSellerDB]    Script Date: 26.06.2025 20:41:10 ******/
CREATE DATABASE [ComponentsSellerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ComponentsSellerDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ComponentsSellerDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ComponentsSellerDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ComponentsSellerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ComponentsSellerDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ComponentsSellerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ComponentsSellerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ComponentsSellerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ComponentsSellerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ComponentsSellerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ComponentsSellerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ComponentsSellerDB] SET  MULTI_USER 
GO
ALTER DATABASE [ComponentsSellerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ComponentsSellerDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ComponentsSellerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ComponentsSellerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ComponentsSellerDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ComponentsSellerDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ComponentsSellerDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [ComponentsSellerDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ComponentsSellerDB]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Case]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Case](
	[ID] [int] IDENTITY(8000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[SupplyLength] [int] NOT NULL,
	[GPULength] [int] NOT NULL,
	[CoolerLength] [int] NOT NULL,
	[FormFactorID] [int] NOT NULL,
 CONSTRAINT [PK__Case__3214EC277E8F9D4D] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chipset]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chipset](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Chipset] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoolerType]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoolerType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CoolerType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cooling]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cooling](
	[ID] [int] IDENTITY(7000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[CoolerTypeID] [int] NOT NULL,
	[Socket] [nvarchar](50) NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[RPM] [int] NOT NULL,
 CONSTRAINT [PK__Cooling__3214EC279D137156] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CPU]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CPU](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Voltage] [int] NOT NULL,
	[SocketID] [int] NOT NULL,
	[Cores] [int] NOT NULL,
	[Threads] [int] NOT NULL,
	[FrequencyGHz] [decimal](3, 2) NOT NULL,
	[L1] [int] NOT NULL,
	[L2] [int] NOT NULL,
	[HasTurboBoost] [int] NOT NULL,
	[MaxFrequency] [decimal](3, 2) NULL,
 CONSTRAINT [PK__CPU__3214EC2766C196F3] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disk]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disk](
	[ID] [int] IDENTITY(6000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[DiskTypeID] [int] NOT NULL,
	[Space] [int] NOT NULL,
 CONSTRAINT [PK__Disk__3214EC27B5324DBD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiskType]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiskType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DiskType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormFactor]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormFactor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FormFactor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GPU]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPU](
	[ID] [int] IDENTITY(2000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Voltage] [int] NOT NULL,
	[VideoMemoryGB] [int] NOT NULL,
	[MemoryFrequencyMHz] [decimal](18, 0) NOT NULL,
	[CoreFrequencyMHz] [decimal](18, 0) NOT NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Length] [int] NULL,
 CONSTRAINT [PK__GPU__3214EC27D965EFF0] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motherboard]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motherboard](
	[ID] [int] IDENTITY(5000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[SocketID] [int] NOT NULL,
	[ChipsetID] [int] NOT NULL,
	[RAMTypeID] [int] NOT NULL,
	[RAMSlots] [int] NOT NULL,
	[MaxRAMCountGB] [int] NOT NULL,
	[MaxRAMFrequencyMHz] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[SATASlots] [int] NOT NULL,
	[M2Slots] [int] NOT NULL,
	[NVMe] [int] NOT NULL,
	[FormFactorID] [int] NULL,
 CONSTRAINT [PK__Motherbo__3214EC2751912A03] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TotalPrice] [decimal](18, 0) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK__Order__3214EC27A7A99DCE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Part]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Part](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CPUID] [int] NULL,
	[GPUID] [int] NULL,
	[SupplyID] [int] NULL,
	[RAMID] [int] NULL,
	[MotherboardID] [int] NULL,
	[DiskID] [int] NULL,
	[CoolingID] [int] NULL,
	[CaseID] [int] NULL,
	[Image] [nvarchar](300) NULL,
	[QuantityInStock] [int] NOT NULL,
 CONSTRAINT [PK__Part__3214EC271670842D] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PartID] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RAM]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RAM](
	[ID] [int] IDENTITY(4000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[RAMTypeID] [int] NOT NULL,
	[MemoryCountGB] [int] NOT NULL,
	[MemoryFrequencyMHz] [int] NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK__RAM__3214EC27DB8D8A4A] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RAMType]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RAMType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RAMType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Socket]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Socket](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Socket] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[ID] [int] NOT NULL,
	[PartID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supply]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supply](
	[ID] [int] IDENTITY(3000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Wattage] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[FormFactorID] [int] NULL,
 CONSTRAINT [PK__Supply__3214EC278A8EC012] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupportedFormFactorMotherboard]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportedFormFactorMotherboard](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CaseID] [int] NOT NULL,
	[FormFactorID] [int] NOT NULL,
 CONSTRAINT [PK_SupportedFormFactorMotherboard] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupportedFormFactorSupply]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportedFormFactorSupply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CaseID] [int] NOT NULL,
	[FormFactorID] [int] NOT NULL,
 CONSTRAINT [PK_SupportedFormFactor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupportedSockets]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportedSockets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CoolerID] [int] NOT NULL,
	[SocketID] [int] NOT NULL,
 CONSTRAINT [PK_SupportedSockets] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 26.06.2025 20:41:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK__User__3214EC272851B6FF] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([ID], [Name]) VALUES (1, N'Intel')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (2, N'AMD')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (3, N'NVIDIA')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (4, N'ASUS')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (5, N'Gigabyte')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (6, N'MSI')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (7, N'ASRock')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (8, N'Biostar')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (9, N'be quiet!')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (10, N'Corsair')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (11, N'Seasonic')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (12, N'Cooler Master')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (13, N'ThermalTake')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (14, N'EVGA')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (15, N'FSP')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (16, N'NZXT')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (17, N'Aerocool')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (18, N'Kingston')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (19, N'G.Skill')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (20, N'Crucial')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (21, N'HyperX')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (22, N'Patriot')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (23, N'Team Group')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (24, N'Samsung')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (25, N'ADATA')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (26, N'GeIL')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (27, N'Noctua')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (28, N'Deepcool')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (29, N'Arctic')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (30, N'Noctua')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (31, N'Lian Li')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (32, N'Fractal Design')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (33, N'Phanteks')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (34, N'Cougar')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (35, N'Western Digital')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (36, N'Seagate')
INSERT [dbo].[Brand] ([ID], [Name]) VALUES (37, N'Toshiba')
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Case] ON 

INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8000, 16, N'H510', 210, 460, 428, 180, 381, 165, 1)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8001, 32, N'Meshify C', 210, 440, 395, 175, 315, 170, 1)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8002, 31, N'PC-O11 Dynamic', 272, 446, 445, 190, 420, 155, 2)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8003, 10, N'4000D Airflow', 230, 453, 453, 180, 360, 170, 1)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8004, 33, N'Eclipse P400A', 210, 470, 470, 180, 380, 160, 1)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8005, 12, N'MasterBox MB520', 210, 480, 480, 185, 400, 160, 1)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8006, 9, N'Pure Base 500DX', 232, 463, 463, 180, 369, 190, 1)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8007, 13, N'Core P3', 266, 490, 490, 200, 450, 160, 3)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8008, 28, N'MATREXX 55', 210, 450, 450, 180, 350, 160, 1)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [Length], [SupplyLength], [GPULength], [CoolerLength], [FormFactorID]) VALUES (8009, 34, N'MX330-G', 200, 440, 440, 175, 320, 155, 1)
SET IDENTITY_INSERT [dbo].[Case] OFF
GO
SET IDENTITY_INSERT [dbo].[Chipset] ON 

INSERT [dbo].[Chipset] ([ID], [Name]) VALUES (1, N'B550')
INSERT [dbo].[Chipset] ([ID], [Name]) VALUES (2, N'B560')
INSERT [dbo].[Chipset] ([ID], [Name]) VALUES (3, N'Z690')
INSERT [dbo].[Chipset] ([ID], [Name]) VALUES (4, N'X570')
INSERT [dbo].[Chipset] ([ID], [Name]) VALUES (5, N'Z590')
INSERT [dbo].[Chipset] ([ID], [Name]) VALUES (6, N'B660')
INSERT [dbo].[Chipset] ([ID], [Name]) VALUES (7, N'H510')
SET IDENTITY_INSERT [dbo].[Chipset] OFF
GO
SET IDENTITY_INSERT [dbo].[CoolerType] ON 

INSERT [dbo].[CoolerType] ([ID], [Type]) VALUES (1, N'Tower')
INSERT [dbo].[CoolerType] ([ID], [Type]) VALUES (2, N'Water')
INSERT [dbo].[CoolerType] ([ID], [Type]) VALUES (3, N'Case')
SET IDENTITY_INSERT [dbo].[CoolerType] OFF
GO
SET IDENTITY_INSERT [dbo].[Cooling] ON 

INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7000, 12, N'Hyper 212', 1, N'LGA1700/AM4', 120, 158, 120, 2000)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7001, 27, N'NH-D15', 1, N'LGA1700/AM4', 150, 165, 150, 1500)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7002, 10, N'iCUE H150i ELITE', 2, N'LGA1700/AM4', 277, 120, 397, 2400)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7003, 9, N'Dark Rock Pro 4', 1, N'LGA1700/AM4', 136, 163, 146, 1500)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7004, 28, N'GAMMAXX 400', 1, N'LGA1700/AM4', 120, 155, 130, 1600)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7005, 29, N'P12 PWM PST', 3, NULL, 120, 120, 25, 1800)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7006, 27, N'NF-A14 PWM', 3, NULL, 140, 140, 25, 1500)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7007, 10, N'LL120 RGB', 3, NULL, 120, 120, 25, 1500)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7008, 9, N'Silent Wings 3', 3, NULL, 120, 120, 25, 1600)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [CoolerTypeID], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7009, 31, N'UNI FAN SL120', 3, NULL, 120, 120, 25, 1900)
SET IDENTITY_INSERT [dbo].[Cooling] OFF
GO
SET IDENTITY_INSERT [dbo].[CPU] ON 

INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1000, 1, N'Core i5-12400F', 65, 4, 6, 12, CAST(2.00 AS Decimal(3, 2)), 480, 7, 1, CAST(4.00 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1001, 2, N'Ryzen 5 5600X', 65, 2, 6, 12, CAST(3.70 AS Decimal(3, 2)), 512, 3, 1, CAST(4.60 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1002, 1, N'Core i7-12700K', 125, 4, 12, 20, CAST(3.00 AS Decimal(3, 2)), 960, 12, 1, CAST(5.00 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1003, 2, N'Ryzen 7 5800X', 105, 2, 8, 16, CAST(3.00 AS Decimal(3, 2)), 512, 4, 1, CAST(4.00 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1004, 1, N'Core i9-12900K', 125, 4, 16, 24, CAST(3.00 AS Decimal(3, 2)), 1280, 14, 1, CAST(5.00 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1005, 2, N'Ryzen 9 5950X', 105, 2, 16, 32, CAST(3.40 AS Decimal(3, 2)), 512, 8, 1, CAST(4.90 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1006, 1, N'Core i3-12100', 60, 4, 4, 8, CAST(3.00 AS Decimal(3, 2)), 320, 5, 1, CAST(4.00 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1007, 2, N'Ryzen 3 4100', 65, 2, 4, 8, CAST(3.00 AS Decimal(3, 2)), 512, 2, 1, CAST(4.00 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1008, 1, N'Xeon E-2336', 80, 5, 6, 12, CAST(2.00 AS Decimal(3, 2)), 384, 3, 1, CAST(4.00 AS Decimal(3, 2)))
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [SocketID], [Cores], [Threads], [FrequencyGHz], [L1], [L2], [HasTurboBoost], [MaxFrequency]) VALUES (1009, 2, N'Ryzen Threadripper 3960X', 280, 13, 24, 48, CAST(3.00 AS Decimal(3, 2)), 512, 12, 1, CAST(4.00 AS Decimal(3, 2)))
SET IDENTITY_INSERT [dbo].[CPU] OFF
GO
SET IDENTITY_INSERT [dbo].[Disk] ON 

INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6000, 24, N'970 EVO Plus 500GB', 2, 500)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6001, 35, N'Blue SN570 1TB', 2, 1000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6002, 36, N'BarraCuda 2TB', 1, 2000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6003, 18, N'NV1 1TB', 2, 1000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6004, 20, N'MX500 500GB', 2, 500)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6005, 37, N'P300 3TB', 1, 3000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6006, 25, N'XPG SX8200 Pro 1TB', 2, 1000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6007, 35, N'Black 4TB', 1, 4000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6008, 24, N'980 PRO 1TB', 2, 1000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [DiskTypeID], [Space]) VALUES (6009, 36, N'FireCuda 2TB', 1, 2000)
SET IDENTITY_INSERT [dbo].[Disk] OFF
GO
SET IDENTITY_INSERT [dbo].[DiskType] ON 

INSERT [dbo].[DiskType] ([ID], [Type]) VALUES (1, N'HDD SATA')
INSERT [dbo].[DiskType] ([ID], [Type]) VALUES (2, N'SSD M.2')
SET IDENTITY_INSERT [dbo].[DiskType] OFF
GO
SET IDENTITY_INSERT [dbo].[FormFactor] ON 

INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (1, N'Mid Tower')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (2, N'Full Tower')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (3, N'Open Frame')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (4, N'ATX')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (5, N'SFX')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (6, N'TFX')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (7, N'Flex ATX')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (9, N'Micro-ATX')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (10, N'Mini-ITX')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (11, N'E-ATX')
INSERT [dbo].[FormFactor] ([ID], [Type]) VALUES (12, N'XL-ATX')
SET IDENTITY_INSERT [dbo].[FormFactor] OFF
GO
SET IDENTITY_INSERT [dbo].[GPU] ON 

INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2000, 3, N'GeForce RTX 3060', 170, 12, CAST(15000 AS Decimal(18, 0)), CAST(1320 AS Decimal(18, 0)), 50, 112, 242)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2001, 2, N'Radeon RX 6600', 132, 8, CAST(14000 AS Decimal(18, 0)), CAST(2044 AS Decimal(18, 0)), 40, 120, 240)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2002, 3, N'GeForce RTX 3070', 220, 8, CAST(14000 AS Decimal(18, 0)), CAST(1500 AS Decimal(18, 0)), 50, 112, 242)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2003, 2, N'Radeon RX 6700 XT', 230, 12, CAST(16000 AS Decimal(18, 0)), CAST(2424 AS Decimal(18, 0)), 50, 120, 267)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2004, 3, N'GeForce RTX 3080', 320, 10, CAST(19000 AS Decimal(18, 0)), CAST(1440 AS Decimal(18, 0)), 60, 112, 285)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2005, 2, N'Radeon RX 6800', 250, 16, CAST(16000 AS Decimal(18, 0)), CAST(1815 AS Decimal(18, 0)), 50, 120, 267)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2006, 3, N'GeForce RTX 3050', 130, 8, CAST(14000 AS Decimal(18, 0)), CAST(1552 AS Decimal(18, 0)), 40, 98, 199)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2007, 2, N'Radeon RX 6500 XT', 107, 4, CAST(18000 AS Decimal(18, 0)), CAST(2610 AS Decimal(18, 0)), 40, 111, 167)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2008, 3, N'GeForce RTX 3090', 350, 24, CAST(19500 AS Decimal(18, 0)), CAST(1395 AS Decimal(18, 0)), 60, 138, 313)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemoryGB], [MemoryFrequencyMHz], [CoreFrequencyMHz], [Width], [Height], [Length]) VALUES (2009, 2, N'Radeon RX 6900 XT', 300, 16, CAST(16000 AS Decimal(18, 0)), CAST(2015 AS Decimal(18, 0)), 50, 120, 267)
SET IDENTITY_INSERT [dbo].[GPU] OFF
GO
SET IDENTITY_INSERT [dbo].[Motherboard] ON 

INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5000, 4, N'ROG Strix B550-F Gaming', 2, 1, 2, 4, 128, 5100, 305, 244, 6, 2, 1, 4)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5001, 5, N'B660M DS3H AX', 4, 6, 2, 4, 128, 4800, 305, 244, 4, 1, 1, 4)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5002, 6, N'MAG B550 TOMAHAWK', 2, 1, 2, 4, 128, 5100, 305, 244, 6, 1, 1, 4)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5003, 7, N'Z690 Phantom Gaming 4', 4, 3, 2, 4, 128, 6400, 305, 244, 6, 3, 1, 4)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5004, 4, N'TUF Gaming X570-PRO', 2, 4, 2, 4, 128, 5100, 305, 244, 6, 2, 1, 4)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5005, 5, N'Z590 AORUS ELITE', 5, 5, 2, 4, 128, 5333, 305, 244, 6, 2, 1, 4)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5006, 6, N'MPG X570S EDGE MAX WIFI', 2, 4, 2, 4, 128, 5100, 305, 244, 6, 2, 1, 4)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5007, 7, N'B560M-HDV', 5, 2, 2, 2, 128, 5066, 226, 185, 4, 1, 0, 9)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5008, 4, N'Prime H510M-K', 5, 7, 2, 2, 64, 3200, 226, 185, 4, 1, 0, 9)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [SocketID], [ChipsetID], [RAMTypeID], [RAMSlots], [MaxRAMCountGB], [MaxRAMFrequencyMHz], [Width], [Height], [SATASlots], [M2Slots], [NVMe], [FormFactorID]) VALUES (5009, 8, N'B550MH', 2, 1, 2, 2, 128, 5100, 226, 185, 6, 1, 1, 9)
SET IDENTITY_INSERT [dbo].[Motherboard] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (1, 1, CAST(571000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (2, 2, CAST(145000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (3, 3, CAST(67000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (4, 4, CAST(95000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (5, 5, CAST(128000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (6, 6, CAST(75000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (7, 7, CAST(80000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (8, 8, CAST(132000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (9, 9, CAST(70000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (10, 10, CAST(37000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (11, 11, CAST(54001 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (13, 11, CAST(35000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-05-10' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (14, 10, CAST(0 AS Decimal(18, 0)), N'Отменён', CAST(N'2025-06-26' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (15, 11, CAST(36000 AS Decimal(18, 0)), N'Отменён', CAST(N'2025-06-26' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (16, 6, CAST(43000 AS Decimal(18, 0)), N'Получен', CAST(N'2025-05-15' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (17, 11, CAST(127000 AS Decimal(18, 0)), N'Получен', CAST(N'2025-06-26' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (18, 6, CAST(0 AS Decimal(18, 0)), N'Корзина', CAST(N'2025-05-15' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (19, 11, CAST(0 AS Decimal(18, 0)), N'Корзина', CAST(N'2025-06-26' AS Date))
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderItem] ON 

INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (2, 1, 2, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (3, 1, 5, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (4, 2, 3, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (5, 2, 4, 2)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (6, 2, 6, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (7, 3, 7, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (8, 3, 8, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (9, 4, 9, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (10, 4, 10, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (11, 5, 1, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (12, 5, 3, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (13, 6, 2, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (14, 6, 4, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (15, 7, 5, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (16, 7, 6, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (17, 8, 7, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (18, 8, 8, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (19, 9, 9, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (20, 9, 10, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (54, 11, 1, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (55, 11, 50, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (56, 11, 4, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (57, 11, 41, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (58, 2, 9, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (69, 13, 2, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (71, 15, 1, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (76, 16, 8, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (77, 16, 51, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (78, 16, 9, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (80, 17, 12, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (81, 17, 42, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (82, 17, 51, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (83, 17, 34, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (84, 17, 82, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (85, 17, 6, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (86, 17, 60, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (87, 17, 71, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (88, 17, 1, 1)
SET IDENTITY_INSERT [dbo].[OrderItem] OFF
GO
SET IDENTITY_INSERT [dbo].[Part] ON 

INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (1, 1000, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://c.dns-shop.ru/thumb/st4/fit/300/300/424deb3b18841a9597bc65d4ace552bf/200e4a08e74afcc3cf1d54d47b758cbbf14c71973a64009553347d2b234b5af4.jpg', 10)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (2, NULL, 2000, NULL, NULL, NULL, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLiu2azaW5fyNW-CmTvCRe-yG2moparEMDzw&s', 14)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (3, NULL, NULL, 3000, NULL, NULL, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTMsbu4kn1zrGInPjI29-_Dw8idl4ZvzKN5nw&s', 15)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (4, NULL, NULL, NULL, 4000, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/5425309/img_id707834447690517666.jpeg/9hq', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (5, NULL, NULL, NULL, NULL, 5000, NULL, NULL, NULL, N'https://tehpos.ru/image/cache/catalog/asus/90mb14s0-m0eay0_7-800x800.webp', 22)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (6, NULL, NULL, NULL, NULL, NULL, 6000, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/5235429/img_id3365231712071416759.jpeg/orig', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (7, NULL, NULL, NULL, NULL, NULL, NULL, 7000, NULL, N'https://cdn.citilink.ru/F1_AR7ahOYAh52xMTi-CCA6UJn1VehaYZGiBP6rrB2U/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/a6e36e07-8d3d-4286-a86a-907f0809dd91.jpg', 14)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8000, N'https://cdn.citilink.ru/cgmnFCb73XEmtBQisk9vdZxxtIYhPMKXii4VLoMEwNU/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/e8448e16-43d1-4f8d-9e22-05efb76f9ac0.jpg', 24)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (9, 1001, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://cdn.citilink.ru/cnpoNsMhxliVIa4uJnqSmzi8juv59JhyVoHpGsoxk2Y/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/bd7752f0-19a1-471d-ac80-de15d55ab131.jpg', 26)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (10, NULL, 2001, NULL, NULL, NULL, NULL, NULL, NULL, N'https://main-cdn.sbermegamarket.ru/big1/hlr-system/191/361/238/581/911/0/100029084416b0.jpg', 11)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (11, NULL, 2002, NULL, NULL, NULL, NULL, NULL, NULL, N'https://microless.com/cdn/products/5e5ea2a6966de05ce10425aac60268d3-hi.jpg', 6)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (12, NULL, 2003, NULL, NULL, NULL, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLKiruLjXuHCbHEA5tQ0Wti2vcEiYtVNxPnw&s', 7)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (13, NULL, 2004, NULL, NULL, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/5249393/2a0000019397aaa3f3ba425246c9a022ac2c/orig', 8)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (14, NULL, 2005, NULL, NULL, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/14117740/2a00000195dd6b1b25ea649e47fbe5c6f251/orig', 9)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (15, NULL, 2006, NULL, NULL, NULL, NULL, NULL, NULL, N'https://www.coxo.ru/upload/iblock/493/59yr2d26dk7n6rik0ahd2jebamj4jpv7/01eea616_7444_11ec_a422_00155d7d1c00_e3189e12_0dbd_11ed_a423_00155d7d1c00.resize1.jpg', 12)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (16, NULL, 2007, NULL, NULL, NULL, NULL, NULL, NULL, N'https://www.tradeinn.com/f/13877/138779396_7/gigabyte-%D0%92%D0%B8%D0%B4%D0%B5%D0%BE%D0%BA%D0%B0%D1%80%D1%82%D0%B0-radeon-rx-6500-xt-gaming-oc-4gb-gddr6.webp', 14)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (17, NULL, 2008, NULL, NULL, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/3698270/img_id2395272528643465327.jpeg/orig', 15)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (18, NULL, 2009, NULL, NULL, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/5231998/2a00000194ac6a2056b46a4bf00ec60169d8/orig', 16)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (19, 1002, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://torg-pc.ru/upload/iblock/7c6/86wovtakerj2hywwirvp4pol831i5zil/Intel-Core-i7-12700K-oem.jpg', 15)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (20, 1003, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/4568822/2a00000191a090c568ea34135e689b50c584/orig', 15)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (21, 1004, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://cdn.citilink.ru/OiWhShSX7iJBaDrnwlzrqFR4JnokrnK5kYV8VSZt8tQ/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/e90e41f9-4dae-47f1-8040-d7a2af409407.jpg', 8)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (22, 1005, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/12618610/2a000001909b84ae9ceab13066491e0a8d08/orig', 9)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (23, 1006, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://cdn.citilink.ru/DQ6pKIl_4pVDiV_u6-FoyKXvb3tMI7q7bfLZ6Y6Tbxw/resizing_type:fit/gravity:sm/width:1200/height:1200/plain/product-images/48279131-dfb4-4c5e-a8ff-b30a468279cd.jpg', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (24, 1007, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/1749547/img_id4252784984655254261.jpeg/orig', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (25, 1008, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://cdn.citilink.ru/0bcpbQFnlbg2_ilBNp1fo0b1_05L7CoiSMMiV6OWsbo/resizing_type:fit/gravity:sm/width:1200/height:1200/plain/product-images/57f12876-3d7f-4627-8710-3fe8ddefb070.jpg', 2)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (26, 1009, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'https://www.sp-computer.ru/upload/iblock/366/3665e90fbc26660becdf23912a586394.jpg', 2)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (27, NULL, NULL, 3001, NULL, NULL, NULL, NULL, NULL, N'https://img.oldi.ru/upload/resaiz_images_catalog/big/102/3347626/3347626_1.jpg', 7)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (28, NULL, NULL, 3002, NULL, NULL, NULL, NULL, NULL, N'https://m.onlinetrade.ru/img/items/m/seasonic_focus_gx_650_ssr_650fx_650w_atx_gold_1499011_3.jpg', 8)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (29, NULL, NULL, 3003, NULL, NULL, NULL, NULL, NULL, N'https://microless.com/cdn/products/37c8e373fe5a36ce9fa1c9f44296a0ef-hi.jpg', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (30, NULL, NULL, 3004, NULL, NULL, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTt5IDPEc3qX11DhZCTlMZERCmIGh8sqJ8IjA&s', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (31, NULL, NULL, 3005, NULL, NULL, NULL, NULL, NULL, N'https://microless.com/cdn/products/645986ae5343bd9e900a4fc8782a1d76-hi.jpg', 7)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (32, NULL, NULL, 3006, NULL, NULL, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXgq-suDFXTKQm09gLRSzWX8hfZ9H1y0QKLA&s', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (33, NULL, NULL, 3007, NULL, NULL, NULL, NULL, NULL, N'https://cdn.citilink.ru/J6ODlwgrXth7neFUxhKjlAxT1PW-pvEnFrekW0pOrEQ/resizing_type:fit/gravity:sm/width:1200/height:1200/plain/product-images/dc910105-b318-4b26-9c47-c77fce018424.jpg', 8)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (34, NULL, NULL, 3008, NULL, NULL, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQxU6yIavsXDjwgdtQAFcunoKUc44MawW_PTg&s', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (35, NULL, NULL, 3009, NULL, NULL, NULL, NULL, NULL, N'https://microless.com/cdn/products/7c31278f87c8c831f68e63782c0b9cdd-hi.jpg', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (36, NULL, NULL, NULL, 4001, NULL, NULL, NULL, NULL, N'https://28bit.ru/wa-data/public/shop/products/34/76/27634/images/66428/66428.970.jpeg', 11)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (37, NULL, NULL, NULL, 4002, NULL, NULL, NULL, NULL, N'https://www.wite.ru/images/cms/data/1nastya/ram/G.Skill/TRIDENT/f4-3600c18d-16gtzr1.jpg', 16)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (38, NULL, NULL, NULL, 4003, NULL, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTU-O6sOc8DudUDc7r8AN-_w8NpafV4uFYJkA&s', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (39, NULL, NULL, NULL, 4004, NULL, NULL, NULL, NULL, N'https://cdn.citilink.ru/WShqeq-GYeZGmWp4ApC5LOXGe0Tm8PCmntaAj24qo74/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/656468f1-3de5-4af7-93b7-40bc841b6295.jpg', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (40, NULL, NULL, NULL, 4005, NULL, NULL, NULL, NULL, N'https://28bit.ru/wa-data/public/shop/products/41/93/19341/images/32639/32639.970.jpeg', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (41, NULL, NULL, NULL, 4006, NULL, NULL, NULL, NULL, N'https://m.onlinetrade.ru/img/items/m/team_group_operativnaya_pamyat_dimm_teamgroup_t_force_delta_rgb_32gb_16gb_x2_ddr5_6000_black_ff3d532g6000hc38adc01__2188940_1.jpg', 15)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (42, NULL, NULL, NULL, 4007, NULL, NULL, NULL, NULL, N'https://static.onlinetrade.ru/img/items/m/operativnaya_pamyat_crucial_ddr4_16gb_3200mhz_pc_25600_ct16g4dfra32a__1443257_1.jpg', 17)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (43, NULL, NULL, NULL, 4008, NULL, NULL, NULL, NULL, N'https://microless.com/cdn/products/cde688049d8ab96e4b38f102df4f86be-hi.jpg', 18)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (44, NULL, NULL, NULL, 4009, NULL, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/10352132/2a0000019565a6dafe7abf34bbb42e372ba7/orig', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (45, NULL, NULL, NULL, NULL, 5001, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-OFE8pjU46-PW0bQiCJoMTZhCQR1A9BOf_g&s', 9)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (46, NULL, NULL, NULL, NULL, 5002, NULL, NULL, NULL, N'https://tehpos.ru/image/cache/catalog/msi/mag-b550-tomahawk-800x800.webp', 22)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (47, NULL, NULL, NULL, NULL, 5003, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEcevINlbzh_fOlIZLq-LL-CPd4pRa6Pk0hA&s', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (48, NULL, NULL, NULL, NULL, 5004, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/4249638/img_id1829794763802994813.jpeg/orig', 7)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (49, NULL, NULL, NULL, NULL, 5005, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSSMDPkCR1EhlO51-RfqPq-_QmdNFPkdcMoJg&s', 8)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (50, NULL, NULL, NULL, NULL, 5006, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4pc_pklF7iUlFO2tlTixoywneSe-lMnrRQw&s', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (51, NULL, NULL, NULL, NULL, 5007, NULL, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/4547325/img_id5721744565067354512.png/9hq', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (52, NULL, NULL, NULL, NULL, 5008, NULL, NULL, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ84XG1eQGWOGOHssteujGMhZmeH4CR-Q-bpw&s', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (53, NULL, NULL, NULL, NULL, 5009, NULL, NULL, NULL, N'https://images.mltrade.ru/images/photo/3/4/a/525054-078088-2.webp', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (54, NULL, NULL, NULL, NULL, NULL, 6001, NULL, NULL, N'https://www.ixbt.com/img/r30/00/02/55/47/wd-blue-sn570-1tb-big.jpg', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (55, NULL, NULL, NULL, NULL, NULL, 6002, NULL, NULL, N'https://www.regard.ru/api/photo/goods/685450.jpg', 6)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (56, NULL, NULL, NULL, NULL, NULL, 6003, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/5231953/img_id4357598414217211659.jpeg/orig', 18)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (57, NULL, NULL, NULL, NULL, NULL, 6004, NULL, NULL, N'https://www.regard.ru/api/photo/goods/260032.jpg', 9)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (58, NULL, NULL, NULL, NULL, NULL, 6005, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/4615030/img_id4879065212110981708.jpeg/orig', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (59, NULL, NULL, NULL, NULL, NULL, 6006, NULL, NULL, N'https://main-cdn.sbermegamarket.ru/big1/hlr-system/-17/796/425/899/261/611/100024459194b0.jpg', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (60, NULL, NULL, NULL, NULL, NULL, 6007, NULL, NULL, N'https://roxton-rus.ru/image/cache/catalog/available/9/ge-catalog-wd-WD4005FZBX-1200x800.jpg', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (61, NULL, NULL, NULL, NULL, NULL, 6008, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/12087486/2a0000018fee1244616200171aa143fe9644/orig', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (62, NULL, NULL, NULL, NULL, NULL, 6009, NULL, NULL, N'https://avatars.mds.yandex.net/get-mpic/12016886/2a000001942f2ab0456f552b0b47eb037f69/orig', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (63, NULL, NULL, NULL, NULL, NULL, NULL, 7001, NULL, N'https://avatars.mds.yandex.net/get-mpic/7471903/2a00000193ae39aa03461d1f9ba9e0e0c0d6/orig', 2)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (64, NULL, NULL, NULL, NULL, NULL, NULL, 7002, NULL, N'https://assets.corsair.com/image/upload/c_pad,q_auto,h_1024,w_1024,f_auto/products/Custom-Cooling/CW-9060075-WW/-base-elite-lcd-xt-cooler-config-Gallery-H150i-ELITE-LCD-XT-01.webp', 6)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (65, NULL, NULL, NULL, NULL, NULL, NULL, 7003, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdQ_QYx1Y87hv-3BeL41GDl_LiotmnpJTikg&s', 1)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (66, NULL, NULL, NULL, NULL, NULL, NULL, 7004, NULL, N'https://cdn.citilink.ru/SyHaKD_0WVtNp1sWrb4O6EQ0wcuqc3Nkk14BZunfrmk/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/7a201d40-2cd7-485f-bb6e-f997b3234466.jpg', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (67, NULL, NULL, NULL, NULL, NULL, NULL, 7005, NULL, N'https://3logic.ru/pimg/pim/1000/1208188.jpg', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (68, NULL, NULL, NULL, NULL, NULL, NULL, 7006, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQtyjanqo9-CFfYKoY-d9M8Zbgc-X3vu1CG5A&s', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (69, NULL, NULL, NULL, NULL, NULL, NULL, 7007, NULL, N'https://assets.corsair.com/image/upload/f_auto,q_auto/content/CO-9050072-WW-LL120-RGB-01-RAINBOW.png', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (70, NULL, NULL, NULL, NULL, NULL, NULL, 7008, NULL, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-0Bg6WRBsVIf8tHL6RCmY1CWzV-krOztA1w&s', 16)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (71, NULL, NULL, NULL, NULL, NULL, NULL, 7009, NULL, N'https://ae04.alicdn.com/kf/Sf711d49955bd4da28b7d4894da986d2cW.jpg', 18)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (72, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8001, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSbZLB0sKFsmNWHcsWiE817Oou23RuVk_IDQ&s', 22)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (73, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8002, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSPGr_sH2etUb7pNKTXT9DCBNSciTrLBHZUMw&s', 12)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (74, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8003, N'https://assets.corsair.com/image/upload/c_pad,q_auto,h_1024,w_1024,f_auto/products/Cases/base-4000d-airflow-config/Gallery/4000D_AF_BLACK_01.webp', 0)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (75, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8004, N'https://cdn1.ozone.ru/s3/multimedia-y/c600/6117399562.jpg', 1)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (76, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8005, N'https://28bit.ru/wa-data/public/shop/products/00/86/18600/images/31284/31284.970.jpeg', 4)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (77, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8006, N'https://microless.com/cdn/products/4639ed5b06f73416a2a4f8415fc8b5a8-hi.jpg', 3)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (78, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8007, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcScC286Ed457l4uRqznaMgkmp9UJCN2mi2_JQ&s', 5)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (79, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8008, N'https://cdn.deepcool.com/public/ProductFile/DEEPCOOL/Cases/Matrexx_55_V3_ADD_RGB_3F/Gallery/608X760/01.jpg?fm=webp&q=60', 17)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID], [Image], [QuantityInStock]) VALUES (80, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8009, N'https://avatars.mds.yandex.net/get-mpic/10815509/2a0000018b2649ae18220930b6fda6066bc0/orig', 3)
SET IDENTITY_INSERT [dbo].[Part] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (1, 1, CAST(85000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (2, 2, CAST(60000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (3, 3, CAST(65000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (4, 4, CAST(95000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (5, 5, CAST(55000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (6, 6, CAST(75000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (7, 7, CAST(80000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (8, 8, CAST(65000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (9, 9, CAST(70000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (10, 10, CAST(90000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (12, 11, CAST(42002 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (13, 13, CAST(35000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (14, 14, CAST(35000 AS Decimal(18, 0)), N'Не оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (15, 15, CAST(36000 AS Decimal(18, 0)), N'Не оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (16, 16, CAST(43000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (17, 17, CAST(127000 AS Decimal(18, 0)), N'Оплачен')
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (1, 1, CAST(36000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (2, 2, CAST(35000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (3, 3, CAST(5000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (4, 4, CAST(6000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (5, 5, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (6, 6, CAST(5000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (7, 7, CAST(3000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (8, 8, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (9, 9, CAST(25000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (10, 10, CAST(30000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (11, 11, CAST(44000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (12, 12, CAST(32000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (14, 13, CAST(72000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (16, 14, CAST(53000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (18, 15, CAST(52000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (19, 16, CAST(33000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (20, 17, CAST(110000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (22, 18, CAST(78000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (23, 19, CAST(14000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (24, 20, CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (26, 21, CAST(18000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (27, 22, CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (28, 23, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (29, 24, CAST(24000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (30, 25, CAST(11000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (31, 26, CAST(13000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (32, 27, CAST(6000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (33, 28, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (34, 29, CAST(7000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (35, 30, CAST(8000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (36, 31, CAST(11000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (37, 32, CAST(7000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (38, 33, CAST(4000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (39, 34, CAST(8000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (40, 35, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (41, 36, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (42, 37, CAST(8000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (43, 38, CAST(8000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (44, 39, CAST(6000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (45, 40, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (46, 41, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (47, 42, CAST(7000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (48, 43, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (49, 44, CAST(11000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (50, 45, CAST(11000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (51, 46, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (52, 47, CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (53, 48, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (54, 49, CAST(11000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (55, 50, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (56, 51, CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (57, 52, CAST(8000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (58, 53, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (59, 54, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (60, 55, CAST(14000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (61, 56, CAST(20000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (62, 57, CAST(5000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (63, 58, CAST(21000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (64, 59, CAST(8000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (65, 60, CAST(28000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (66, 61, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (67, 62, CAST(13000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (68, 63, CAST(18000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (69, 64, CAST(7000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (70, 65, CAST(24000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (71, 66, CAST(6000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (72, 67, CAST(14000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (73, 68, CAST(7000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (74, 69, CAST(8000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (75, 70, CAST(5000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (76, 71, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (77, 72, CAST(9000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (78, 73, CAST(11000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (79, 74, CAST(14000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (80, 75, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (81, 76, CAST(15000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (82, 77, CAST(10000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (83, 78, CAST(9900 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (84, 79, CAST(12000 AS Decimal(18, 0)))
INSERT [dbo].[Product] ([ID], [PartID], [Price]) VALUES (85, 80, CAST(9000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[RAM] ON 

INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4000, 10, N'Vengeance LPX 16GB', 2, 16, 3200, 2)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4001, 18, N'Fury Beast 32GB', 2, 32, 3200, 2)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4002, 19, N'Trident Z RGB 16GB', 2, 16, 3600, 2)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4003, 20, N'Ballistix 8GB', 2, 8, 3000, 1)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4004, 21, N'Predator 32GB', 2, 32, 3600, 2)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4005, 22, N'Viper Steel 16GB', 2, 16, 3600, 1)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4006, 23, N'T-Force Delta RGB 32GB', 2, 32, 3600, 2)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4007, 24, N'DDR4 16GB', 2, 16, 2666, 1)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4008, 25, N'XPG Spectrix D60G 16GB', 2, 16, 3600, 2)
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [RAMTypeID], [MemoryCountGB], [MemoryFrequencyMHz], [Count]) VALUES (4009, 26, N'Orion RGB 32GB', 2, 32, 3600, 2)
SET IDENTITY_INSERT [dbo].[RAM] OFF
GO
SET IDENTITY_INSERT [dbo].[RAMType] ON 

INSERT [dbo].[RAMType] ([ID], [Type]) VALUES (1, N'DDR3')
INSERT [dbo].[RAMType] ([ID], [Type]) VALUES (2, N'DDR4')
INSERT [dbo].[RAMType] ([ID], [Type]) VALUES (3, N'DDR5')
INSERT [dbo].[RAMType] ([ID], [Type]) VALUES (4, N'DDR1')
INSERT [dbo].[RAMType] ([ID], [Type]) VALUES (5, N'DDR2')
INSERT [dbo].[RAMType] ([ID], [Type]) VALUES (6, N'DDR6')
INSERT [dbo].[RAMType] ([ID], [Type]) VALUES (7, N'DDR7')
SET IDENTITY_INSERT [dbo].[RAMType] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [Name]) VALUES (1, N'user')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (2, N'employee')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (3, N'admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Socket] ON 

INSERT [dbo].[Socket] ([ID], [Name]) VALUES (1, N'AM5')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (2, N'AM4')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (3, N'LGA 1851')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (4, N'LGA 1700')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (5, N'LGA 1200')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (6, N'LGA 1151-v2')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (7, N'LGA 1151')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (8, N'AM3+')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (9, N'sWRX8')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (10, N'TR4')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (11, N'LGA 2066')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (12, N'FM2+')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (13, N'sTRX4')
INSERT [dbo].[Socket] ([ID], [Name]) VALUES (14, N'Case')
SET IDENTITY_INSERT [dbo].[Socket] OFF
GO
SET IDENTITY_INSERT [dbo].[Supply] ON 

INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3000, 9, N'Pure Power 11 400W', 400, 150, 86, 140, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3001, 10, N'RM550x', 550, 150, 86, 160, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3002, 11, N'Focus GX-650', 650, 150, 86, 140, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3003, 12, N'MWE Gold 750', 750, 150, 86, 140, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3004, 13, N'Toughpower GF1 850W', 850, 150, 86, 150, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3005, 14, N'SuperNOVA 1000 G5', 1000, 150, 86, 150, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3006, 15, N'Hydro G Pro 850W', 850, 150, 86, 150, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3007, 5, N'P850GM', 850, 150, 86, 150, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3008, 16, N'C650', 650, 150, 86, 140, 4)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length], [FormFactorID]) VALUES (3009, 17, N'Lux RGB 550W', 550, 150, 86, 140, 4)
SET IDENTITY_INSERT [dbo].[Supply] OFF
GO
SET IDENTITY_INSERT [dbo].[SupportedFormFactorMotherboard] ON 

INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (1, 8000, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (2, 8001, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (3, 8002, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (4, 8003, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (5, 8004, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (6, 8005, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (7, 8006, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (8, 8007, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (9, 8008, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (10, 8009, 4)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (11, 8000, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (12, 8001, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (13, 8002, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (14, 8003, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (15, 8004, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (16, 8005, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (17, 8006, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (18, 8007, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (19, 8008, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (20, 8009, 9)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (21, 8000, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (22, 8001, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (23, 8002, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (24, 8003, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (25, 8004, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (26, 8005, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (27, 8006, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (28, 8007, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (29, 8008, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (30, 8009, 10)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (31, 8002, 11)
INSERT [dbo].[SupportedFormFactorMotherboard] ([ID], [CaseID], [FormFactorID]) VALUES (32, 8002, 12)
SET IDENTITY_INSERT [dbo].[SupportedFormFactorMotherboard] OFF
GO
SET IDENTITY_INSERT [dbo].[SupportedFormFactorSupply] ON 

INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (2, 8000, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (3, 8000, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (4, 8001, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (5, 8001, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (6, 8002, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (7, 8002, 2)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (8, 8002, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (9, 8003, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (10, 8003, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (11, 8004, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (12, 8004, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (13, 8005, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (14, 8005, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (15, 8006, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (16, 8006, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (17, 8007, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (18, 8007, 2)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (19, 8007, 3)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (20, 8007, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (21, 8007, 5)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (22, 8007, 6)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (23, 8008, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (24, 8008, 4)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (25, 8009, 1)
INSERT [dbo].[SupportedFormFactorSupply] ([ID], [CaseID], [FormFactorID]) VALUES (26, 8009, 4)
SET IDENTITY_INSERT [dbo].[SupportedFormFactorSupply] OFF
GO
SET IDENTITY_INSERT [dbo].[SupportedSockets] ON 

INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (1, 7000, 2)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (2, 7000, 4)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (3, 7000, 5)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (4, 7000, 6)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (5, 7000, 7)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (6, 7000, 8)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (7, 7001, 1)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (8, 7001, 2)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (9, 7001, 3)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (10, 7001, 4)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (11, 7001, 5)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (12, 7001, 6)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (13, 7001, 7)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (14, 7001, 9)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (15, 7001, 10)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (16, 7001, 11)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (17, 7002, 1)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (18, 7002, 2)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (19, 7002, 3)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (20, 7002, 4)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (21, 7002, 5)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (22, 7002, 6)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (23, 7002, 7)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (24, 7002, 11)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (25, 7003, 2)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (26, 7003, 4)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (27, 7003, 5)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (28, 7003, 6)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (29, 7003, 7)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (30, 7003, 10)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (31, 7003, 11)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (32, 7004, 2)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (33, 7004, 4)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (34, 7004, 5)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (35, 7004, 6)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (36, 7004, 7)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (37, 7004, 8)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (38, 7004, 12)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (39, 7005, 14)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (40, 7006, 14)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (41, 7007, 14)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (42, 7008, 14)
INSERT [dbo].[SupportedSockets] ([ID], [CoolerID], [SocketID]) VALUES (43, 7009, 14)
SET IDENTITY_INSERT [dbo].[SupportedSockets] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (1, N'Иван Иванов', N'ivan@example.com', N'password123', 1)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (2, N'Петр Петров', N'petr@example.com', N'qwerty456', 2)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (3, N'Алексей Сидоров', N'alex@example.com', N'pass789', 1)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (4, N'Мария Кузнецова', N'maria@example.com', N'mariak123', 1)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (5, N'Анна Смирнова', N'anna@example.com', N'anna456', 1)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (6, N'Дмитрий Попов', N'dmitry@example.com', N'dima789', 2)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (7, N'Елена Новикова', N'elena@example.com', N'elena123', 3)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (8, N'Сергей Волков', N'sergey@example.com', N'volk456', 1)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (9, N'Ольга Козлова', N'olga@example.com', N'olga789', 1)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (10, N'Николай Морозов', N'nikolay@example.com', N'moroz123', 1)
INSERT [dbo].[User] ([ID], [Name], [Email], [Password], [RoleID]) VALUES (11, N'testtest', N'test', N'test', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Case]  WITH CHECK ADD  CONSTRAINT [FK_Case_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Case_Brand]
GO
ALTER TABLE [dbo].[Case]  WITH CHECK ADD  CONSTRAINT [FK_Case_FormFactor] FOREIGN KEY([FormFactorID])
REFERENCES [dbo].[FormFactor] ([ID])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Case_FormFactor]
GO
ALTER TABLE [dbo].[Cooling]  WITH CHECK ADD  CONSTRAINT [FK_Cooling_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Cooling] CHECK CONSTRAINT [FK_Cooling_Brand]
GO
ALTER TABLE [dbo].[Cooling]  WITH CHECK ADD  CONSTRAINT [FK_Cooling_CoolerType] FOREIGN KEY([CoolerTypeID])
REFERENCES [dbo].[CoolerType] ([ID])
GO
ALTER TABLE [dbo].[Cooling] CHECK CONSTRAINT [FK_Cooling_CoolerType]
GO
ALTER TABLE [dbo].[CPU]  WITH CHECK ADD  CONSTRAINT [FK_CPU_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[CPU] CHECK CONSTRAINT [FK_CPU_Brand]
GO
ALTER TABLE [dbo].[CPU]  WITH CHECK ADD  CONSTRAINT [FK_CPU_Socket] FOREIGN KEY([SocketID])
REFERENCES [dbo].[Socket] ([ID])
GO
ALTER TABLE [dbo].[CPU] CHECK CONSTRAINT [FK_CPU_Socket]
GO
ALTER TABLE [dbo].[Disk]  WITH CHECK ADD  CONSTRAINT [FK_Disk_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Disk] CHECK CONSTRAINT [FK_Disk_Brand]
GO
ALTER TABLE [dbo].[Disk]  WITH CHECK ADD  CONSTRAINT [FK_Disk_DiskType] FOREIGN KEY([DiskTypeID])
REFERENCES [dbo].[DiskType] ([ID])
GO
ALTER TABLE [dbo].[Disk] CHECK CONSTRAINT [FK_Disk_DiskType]
GO
ALTER TABLE [dbo].[GPU]  WITH CHECK ADD  CONSTRAINT [FK_GPU_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[GPU] CHECK CONSTRAINT [FK_GPU_Brand]
GO
ALTER TABLE [dbo].[Motherboard]  WITH CHECK ADD  CONSTRAINT [FK_Motherboard_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Motherboard] CHECK CONSTRAINT [FK_Motherboard_Brand]
GO
ALTER TABLE [dbo].[Motherboard]  WITH CHECK ADD  CONSTRAINT [FK_Motherboard_Chipset] FOREIGN KEY([ChipsetID])
REFERENCES [dbo].[Chipset] ([ID])
GO
ALTER TABLE [dbo].[Motherboard] CHECK CONSTRAINT [FK_Motherboard_Chipset]
GO
ALTER TABLE [dbo].[Motherboard]  WITH CHECK ADD  CONSTRAINT [FK_Motherboard_FormFactor] FOREIGN KEY([FormFactorID])
REFERENCES [dbo].[FormFactor] ([ID])
GO
ALTER TABLE [dbo].[Motherboard] CHECK CONSTRAINT [FK_Motherboard_FormFactor]
GO
ALTER TABLE [dbo].[Motherboard]  WITH CHECK ADD  CONSTRAINT [FK_Motherboard_RAMType] FOREIGN KEY([RAMTypeID])
REFERENCES [dbo].[RAMType] ([ID])
GO
ALTER TABLE [dbo].[Motherboard] CHECK CONSTRAINT [FK_Motherboard_RAMType]
GO
ALTER TABLE [dbo].[Motherboard]  WITH CHECK ADD  CONSTRAINT [FK_Motherboard_Socket] FOREIGN KEY([SocketID])
REFERENCES [dbo].[Socket] ([ID])
GO
ALTER TABLE [dbo].[Motherboard] CHECK CONSTRAINT [FK_Motherboard_Socket]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK__Order__UserId__5AEE82B9] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK__Order__UserId__5AEE82B9]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK__OrderItem__Order__59063A47] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK__OrderItem__Order__59063A47]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK__Part__CoolingID__5441852A] FOREIGN KEY([CoolingID])
REFERENCES [dbo].[Cooling] ([ID])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK__Part__CoolingID__5441852A]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK__Part__CPUID__5535A963] FOREIGN KEY([CPUID])
REFERENCES [dbo].[CPU] ([ID])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK__Part__CPUID__5535A963]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK__Part__DiskID__5629CD9C] FOREIGN KEY([DiskID])
REFERENCES [dbo].[Disk] ([ID])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK__Part__DiskID__5629CD9C]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK__Part__GPUID__571DF1D5] FOREIGN KEY([GPUID])
REFERENCES [dbo].[GPU] ([ID])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK__Part__GPUID__571DF1D5]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK__Part__Motherboar__5812160E] FOREIGN KEY([MotherboardID])
REFERENCES [dbo].[Motherboard] ([ID])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK__Part__Motherboar__5812160E]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK__Part__RAMID__59063A47] FOREIGN KEY([RAMID])
REFERENCES [dbo].[RAM] ([ID])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK__Part__RAMID__59063A47]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK__Part__SupplyID__59FA5E80] FOREIGN KEY([SupplyID])
REFERENCES [dbo].[Supply] ([ID])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK__Part__SupplyID__59FA5E80]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK_Part_Case] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Case] ([ID])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK_Part_Case]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK__Payment__OrderID__5BE2A6F2] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK__Payment__OrderID__5BE2A6F2]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK__Product__PartID__5BE2A6F2] FOREIGN KEY([PartID])
REFERENCES [dbo].[Part] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK__Product__PartID__5BE2A6F2]
GO
ALTER TABLE [dbo].[RAM]  WITH CHECK ADD  CONSTRAINT [FK_RAM_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[RAM] CHECK CONSTRAINT [FK_RAM_Brand]
GO
ALTER TABLE [dbo].[RAM]  WITH CHECK ADD  CONSTRAINT [FK_RAM_RAMType] FOREIGN KEY([RAMTypeID])
REFERENCES [dbo].[RAMType] ([ID])
GO
ALTER TABLE [dbo].[RAM] CHECK CONSTRAINT [FK_RAM_RAMType]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Part] FOREIGN KEY([PartID])
REFERENCES [dbo].[Part] ([ID])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Part]
GO
ALTER TABLE [dbo].[Supply]  WITH CHECK ADD  CONSTRAINT [FK_Supply_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Supply] CHECK CONSTRAINT [FK_Supply_Brand]
GO
ALTER TABLE [dbo].[Supply]  WITH CHECK ADD  CONSTRAINT [FK_Supply_FormFactor] FOREIGN KEY([FormFactorID])
REFERENCES [dbo].[FormFactor] ([ID])
GO
ALTER TABLE [dbo].[Supply] CHECK CONSTRAINT [FK_Supply_FormFactor]
GO
ALTER TABLE [dbo].[SupportedFormFactorMotherboard]  WITH CHECK ADD  CONSTRAINT [FK_SupportedFormFactorMotherboard_FormFactor] FOREIGN KEY([FormFactorID])
REFERENCES [dbo].[FormFactor] ([ID])
GO
ALTER TABLE [dbo].[SupportedFormFactorMotherboard] CHECK CONSTRAINT [FK_SupportedFormFactorMotherboard_FormFactor]
GO
ALTER TABLE [dbo].[SupportedFormFactorSupply]  WITH CHECK ADD  CONSTRAINT [FK_SupportedFormFactor_Case] FOREIGN KEY([CaseID])
REFERENCES [dbo].[Case] ([ID])
GO
ALTER TABLE [dbo].[SupportedFormFactorSupply] CHECK CONSTRAINT [FK_SupportedFormFactor_Case]
GO
ALTER TABLE [dbo].[SupportedFormFactorSupply]  WITH CHECK ADD  CONSTRAINT [FK_SupportedFormFactor_FormFactor] FOREIGN KEY([FormFactorID])
REFERENCES [dbo].[FormFactor] ([ID])
GO
ALTER TABLE [dbo].[SupportedFormFactorSupply] CHECK CONSTRAINT [FK_SupportedFormFactor_FormFactor]
GO
ALTER TABLE [dbo].[SupportedSockets]  WITH CHECK ADD  CONSTRAINT [FK_SupportedSockets_Cooling] FOREIGN KEY([CoolerID])
REFERENCES [dbo].[Cooling] ([ID])
GO
ALTER TABLE [dbo].[SupportedSockets] CHECK CONSTRAINT [FK_SupportedSockets_Cooling]
GO
ALTER TABLE [dbo].[SupportedSockets]  WITH CHECK ADD  CONSTRAINT [FK_SupportedSockets_Socket] FOREIGN KEY([SocketID])
REFERENCES [dbo].[Socket] ([ID])
GO
ALTER TABLE [dbo].[SupportedSockets] CHECK CONSTRAINT [FK_SupportedSockets_Socket]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [ComponentsSellerDB] SET  READ_WRITE 
GO
