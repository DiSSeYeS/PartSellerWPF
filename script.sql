USE [master]
GO
/****** Object:  Database [ComponentsSellerDB]    Script Date: 21.04.2025 17:45:57 ******/
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
/****** Object:  Table [dbo].[Brand]    Script Date: 21.04.2025 17:45:57 ******/
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
/****** Object:  Table [dbo].[Case]    Script Date: 21.04.2025 17:45:57 ******/
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
	[SupplyWidth] [int] NOT NULL,
	[SupplyHeight] [int] NOT NULL,
 CONSTRAINT [PK__Case__3214EC277E8F9D4D] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cooling]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cooling](
	[ID] [int] IDENTITY(7000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Speed] [int] NOT NULL,
	[Socket] [nvarchar](50) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Length] [int] NULL,
	[RPM] [int] NULL,
 CONSTRAINT [PK__Cooling__3214EC279D137156] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CPU]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CPU](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Voltage] [int] NOT NULL,
	[Socket] [nvarchar](50) NOT NULL,
	[Cores] [int] NULL,
	[Frequency] [int] NULL,
 CONSTRAINT [PK__CPU__3214EC2766C196F3] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disk]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disk](
	[ID] [int] IDENTITY(6000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Space] [int] NOT NULL,
 CONSTRAINT [PK__Disk__3214EC27B5324DBD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GPU]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPU](
	[ID] [int] IDENTITY(2000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Voltage] [int] NOT NULL,
	[VideoMemory] [int] NOT NULL,
	[MemoryFrequency] [decimal](18, 0) NOT NULL,
	[CoreFrequency] [decimal](18, 0) NOT NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Length] [int] NULL,
 CONSTRAINT [PK__GPU__3214EC27D965EFF0] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Motherboard]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Motherboard](
	[ID] [int] IDENTITY(5000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Socket] [nvarchar](50) NOT NULL,
	[Chipset] [nvarchar](50) NULL,
	[RAMType] [nvarchar](50) NOT NULL,
	[RAMSlots] [int] NULL,
	[Voltage] [int] NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
 CONSTRAINT [PK__Motherbo__3214EC2751912A03] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 21.04.2025 17:45:57 ******/
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
/****** Object:  Table [dbo].[OrderItem]    Script Date: 21.04.2025 17:45:57 ******/
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
/****** Object:  Table [dbo].[Part]    Script Date: 21.04.2025 17:45:57 ******/
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
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 21.04.2025 17:45:57 ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PartID] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Image] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RAM]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RAM](
	[ID] [int] IDENTITY(4000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[MemoryCount] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__RAM__3214EC27DB8D8A4A] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supply]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supply](
	[ID] [int] IDENTITY(3000,1) NOT NULL,
	[BrandID] [int] NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Wattage] [int] NOT NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Length] [int] NULL,
 CONSTRAINT [PK__Supply__3214EC278A8EC012] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 21.04.2025 17:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
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

INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8000, 16, N'H510', 210, 460, 210, 180)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8001, 32, N'Meshify C', 210, 440, 210, 175)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8002, 31, N'PC-O11 Dynamic', 272, 446, 225, 190)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8003, 10, N'4000D Airflow', 230, 453, 225, 180)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8004, 33, N'Eclipse P400A', 210, 470, 210, 180)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8005, 12, N'MasterBox MB520', 210, 480, 210, 185)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8006, 9, N'Pure Base 500DX', 232, 463, 225, 180)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8007, 13, N'Core P3', 266, 490, 240, 200)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8008, 28, N'MATREXX 55', 210, 450, 210, 180)
INSERT [dbo].[Case] ([ID], [BrandID], [Model], [Width], [Height], [SupplyWidth], [SupplyHeight]) VALUES (8009, 34, N'MX330-G', 200, 440, 200, 175)
SET IDENTITY_INSERT [dbo].[Case] OFF
GO
SET IDENTITY_INSERT [dbo].[Cooling] ON 

INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7000, 12, N'Hyper 212', N'Воздушное', 2000, N'LGA1700/AM4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7001, 27, N'NH-D15', N'Воздушное', 1500, N'LGA1700/AM4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7002, 10, N'iCUE H150i ELITE', N'Жидкостное', 2400, N'LGA1700/AM4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7003, 9, N'Dark Rock Pro 4', N'Воздушное', 1500, N'LGA1700/AM4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7004, 28, N'GAMMAXX 400', N'Воздушное', 1600, N'LGA1700/AM4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7005, 29, N'P12 PWM PST', N'Корпусное', 1800, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7006, 30, N'NF-A14 PWM', N'Корпусное', 1500, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7007, 10, N'LL120 RGB', N'Корпусное', 1500, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7008, 9, N'Silent Wings 3', N'Корпусное', 1600, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Cooling] ([ID], [BrandID], [Model], [Type], [Speed], [Socket], [Width], [Height], [Length], [RPM]) VALUES (7009, 31, N'UNI FAN SL120', N'Корпусное', 1900, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Cooling] OFF
GO
SET IDENTITY_INSERT [dbo].[CPU] ON 

INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1000, 1, N'Core i5-12400F', 65, N'LGA1700', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1001, 2, N'Ryzen 5 5600X', 65, N'AM4', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1002, 1, N'Core i7-12700K', 125, N'LGA1700', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1003, 2, N'Ryzen 7 5800X', 105, N'AM4', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1004, 1, N'Core i9-12900K', 125, N'LGA1700', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1005, 2, N'Ryzen 9 5950X', 105, N'AM4', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1006, 1, N'Core i3-12100', 60, N'LGA1700', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1007, 2, N'Ryzen 3 4100', 65, N'AM4', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1008, 1, N'Xeon E-2336', 80, N'LGA1200', NULL, NULL)
INSERT [dbo].[CPU] ([ID], [BrandID], [Model], [Voltage], [Socket], [Cores], [Frequency]) VALUES (1009, 2, N'Ryzen Threadripper 3960X', 280, N'sTRX4', NULL, NULL)
SET IDENTITY_INSERT [dbo].[CPU] OFF
GO
SET IDENTITY_INSERT [dbo].[Disk] ON 

INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6000, 24, N'970 EVO Plus 500GB', N'SSD', 500)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6001, 35, N'Blue SN570 1TB', N'SSD', 1000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6002, 36, N'BarraCuda 2TB', N'HDD', 2000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6003, 18, N'NV1 1TB', N'SSD', 1000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6004, 20, N'MX500 500GB', N'SSD', 500)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6005, 37, N'P300 3TB', N'HDD', 3000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6006, 25, N'XPG SX8200 Pro 1TB', N'SSD', 1000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6007, 35, N'Black 4TB', N'HDD', 4000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6008, 24, N'980 PRO 1TB', N'SSD', 1000)
INSERT [dbo].[Disk] ([ID], [BrandID], [Model], [Type], [Space]) VALUES (6009, 36, N'FireCuda 2TB', N'SSD', 2000)
SET IDENTITY_INSERT [dbo].[Disk] OFF
GO
SET IDENTITY_INSERT [dbo].[GPU] ON 

INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2000, 3, N'GeForce RTX 3060', 170, 12, CAST(1875 AS Decimal(18, 0)), CAST(1320 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2001, 2, N'Radeon RX 6600', 132, 8, CAST(1750 AS Decimal(18, 0)), CAST(2044 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2002, 3, N'GeForce RTX 3070', 220, 8, CAST(1750 AS Decimal(18, 0)), CAST(1500 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2003, 2, N'Radeon RX 6700 XT', 230, 12, CAST(2000 AS Decimal(18, 0)), CAST(2424 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2004, 3, N'GeForce RTX 3080', 320, 10, CAST(1188 AS Decimal(18, 0)), CAST(1440 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2005, 2, N'Radeon RX 6800', 250, 16, CAST(2000 AS Decimal(18, 0)), CAST(1815 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2006, 3, N'GeForce RTX 3050', 130, 8, CAST(1750 AS Decimal(18, 0)), CAST(1552 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2007, 2, N'Radeon RX 6500 XT', 107, 4, CAST(2250 AS Decimal(18, 0)), CAST(2610 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2008, 3, N'GeForce RTX 3090', 350, 24, CAST(1219 AS Decimal(18, 0)), CAST(1395 AS Decimal(18, 0)), NULL, NULL, NULL)
INSERT [dbo].[GPU] ([ID], [BrandID], [Model], [Voltage], [VideoMemory], [MemoryFrequency], [CoreFrequency], [Width], [Height], [Length]) VALUES (2009, 2, N'Radeon RX 6900 XT', 300, 16, CAST(2000 AS Decimal(18, 0)), CAST(2015 AS Decimal(18, 0)), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[GPU] OFF
GO
SET IDENTITY_INSERT [dbo].[Motherboard] ON 

INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5000, 4, N'ROG Strix B550-F Gaming', N'AM4', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5001, 5, N'B660M DS3H AX', N'LGA1700', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5002, 6, N'MAG B550 TOMAHAWK', N'AM4', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5003, 7, N'Z690 Phantom Gaming 4', N'LGA1700', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5004, 4, N'TUF Gaming X570-PRO', N'AM4', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5005, 5, N'Z590 AORUS ELITE', N'LGA1200', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5006, 6, N'MPG X570S EDGE MAX WIFI', N'AM4', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5007, 7, N'B560M-HDV', N'LGA1200', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5008, 4, N'Prime H510M-K', N'LGA1200', NULL, N'DDR4', NULL, NULL, NULL, NULL)
INSERT [dbo].[Motherboard] ([ID], [BrandID], [Model], [Socket], [Chipset], [RAMType], [RAMSlots], [Voltage], [Width], [Height]) VALUES (5009, 8, N'B550MH', N'AM4', NULL, N'DDR4', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Motherboard] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (1, 1, CAST(85000 AS Decimal(18, 0)), N'Завершен', CAST(N'2024-03-11' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (2, 2, CAST(120000 AS Decimal(18, 0)), N'В обработке', CAST(N'2025-04-08' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (3, 3, CAST(65000 AS Decimal(18, 0)), N'Отправлен', CAST(N'2025-02-22' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (4, 4, CAST(95000 AS Decimal(18, 0)), N'Завершен', CAST(N'2024-05-03' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (5, 5, CAST(110000 AS Decimal(18, 0)), N'В обработке', CAST(N'2025-04-09' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (6, 6, CAST(75000 AS Decimal(18, 0)), N'Отправлен', CAST(N'2024-12-25' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (7, 7, CAST(80000 AS Decimal(18, 0)), N'Завершен', CAST(N'2023-12-30' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (8, 8, CAST(130000 AS Decimal(18, 0)), N'В обработке', CAST(N'2025-02-12' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (9, 9, CAST(70000 AS Decimal(18, 0)), N'Отправлен', CAST(N'2025-01-21' AS Date))
INSERT [dbo].[Order] ([ID], [UserId], [TotalPrice], [Status], [Date]) VALUES (10, 10, CAST(90000 AS Decimal(18, 0)), N'Завершен', CAST(N'2025-01-11' AS Date))
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderItem] ON 

INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (1, 1, 1, 1)
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
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (21, 10, 1, 1)
INSERT [dbo].[OrderItem] ([ID], [OrderID], [ProductID], [Quantity]) VALUES (22, 10, 2, 1)
SET IDENTITY_INSERT [dbo].[OrderItem] OFF
GO
SET IDENTITY_INSERT [dbo].[Part] ON 

INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (1, 1000, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (2, NULL, 2000, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (3, NULL, NULL, 3000, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (4, NULL, NULL, NULL, 4000, NULL, NULL, NULL, NULL)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (5, NULL, NULL, NULL, NULL, 5000, NULL, NULL, NULL)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (6, NULL, NULL, NULL, NULL, NULL, 6000, NULL, NULL)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (7, NULL, NULL, NULL, NULL, NULL, NULL, 7000, NULL)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8000)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (9, 1001, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Part] ([ID], [CPUID], [GPUID], [SupplyID], [RAMID], [MotherboardID], [DiskID], [CoolingID], [CaseID]) VALUES (10, NULL, 2001, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Part] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (1, 1, CAST(85000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (2, 2, CAST(60000 AS Decimal(18, 0)), N'Частичная оплата')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (3, 3, CAST(65000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (4, 4, CAST(95000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (5, 5, CAST(55000 AS Decimal(18, 0)), N'Частичная оплата')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (6, 6, CAST(75000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (7, 7, CAST(80000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (8, 8, CAST(65000 AS Decimal(18, 0)), N'Частичная оплата')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (9, 9, CAST(70000 AS Decimal(18, 0)), N'Оплачен')
INSERT [dbo].[Payment] ([ID], [OrderID], [Amount], [Status]) VALUES (10, 10, CAST(90000 AS Decimal(18, 0)), N'Оплачен')
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (1, 1, CAST(18000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (2, 2, CAST(35000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (3, 3, CAST(4500 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (4, 4, CAST(6000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (5, 5, CAST(12000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (6, 6, CAST(5000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (7, 7, CAST(3000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (8, 8, CAST(7000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (9, 9, CAST(25000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[Product] ([ID], [PartID], [Price], [Image]) VALUES (10, 10, CAST(30000 AS Decimal(18, 0)), NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[RAM] ON 

INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4000, 10, N'Vengeance LPX 16GB', N'DDR4', N'16GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4001, 18, N'Fury Beast 32GB', N'DDR4', N'32GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4002, 19, N'Trident Z RGB 16GB', N'DDR4', N'16GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4003, 20, N'Ballistix 8GB', N'DDR4', N'8GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4004, 21, N'Predator 32GB', N'DDR4', N'32GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4005, 22, N'Viper Steel 16GB', N'DDR4', N'16GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4006, 23, N'T-Force Delta RGB 32GB', N'DDR4', N'32GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4007, 24, N'DDR4 16GB', N'DDR4', N'16GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4008, 25, N'XPG Spectrix D60G 16GB', N'DDR4', N'16GB')
INSERT [dbo].[RAM] ([ID], [BrandID], [Model], [Type], [MemoryCount]) VALUES (4009, 26, N'Orion RGB 32GB', N'DDR4', N'32GB')
SET IDENTITY_INSERT [dbo].[RAM] OFF
GO
SET IDENTITY_INSERT [dbo].[Supply] ON 

INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3000, 9, N'Pure Power 11 400W', 400, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3001, 10, N'RM550x', 550, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3002, 11, N'Focus GX-650', 650, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3003, 12, N'MWE Gold 750', 750, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3004, 13, N'Toughpower GF1 850W', 850, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3005, 14, N'SuperNOVA 1000 G5', 1000, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3006, 15, N'Hydro G Pro 850W', 850, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3007, 5, N'P850GM', 850, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3008, 16, N'C650', 650, NULL, NULL, NULL)
INSERT [dbo].[Supply] ([ID], [BrandID], [Model], [Wattage], [Width], [Height], [Length]) VALUES (3009, 17, N'Lux RGB 550W', 550, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Supply] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (1, N'Иван Иванов', N'ivan@example.com', N'password123')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (2, N'Петр Петров', N'petr@example.com', N'qwerty456')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (3, N'Алексей Сидоров', N'alex@example.com', N'pass789')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (4, N'Мария Кузнецова', N'maria@example.com', N'mariak123')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (5, N'Анна Смирнова', N'anna@example.com', N'anna456')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (6, N'Дмитрий Попов', N'dmitry@example.com', N'dima789')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (7, N'Елена Новикова', N'elena@example.com', N'elena123')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (8, N'Сергей Волков', N'sergey@example.com', N'volk456')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (9, N'Ольга Козлова', N'olga@example.com', N'olga789')
INSERT [dbo].[User] ([ID], [Name], [Email], [Password]) VALUES (10, N'Николай Морозов', N'nikolay@example.com', N'moroz123')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Case]  WITH CHECK ADD  CONSTRAINT [FK_Case_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Case] CHECK CONSTRAINT [FK_Case_Brand]
GO
ALTER TABLE [dbo].[Cooling]  WITH CHECK ADD  CONSTRAINT [FK_Cooling_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Cooling] CHECK CONSTRAINT [FK_Cooling_Brand]
GO
ALTER TABLE [dbo].[CPU]  WITH CHECK ADD  CONSTRAINT [FK_CPU_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[CPU] CHECK CONSTRAINT [FK_CPU_Brand]
GO
ALTER TABLE [dbo].[Disk]  WITH CHECK ADD  CONSTRAINT [FK_Disk_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Disk] CHECK CONSTRAINT [FK_Disk_Brand]
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
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([PartID])
REFERENCES [dbo].[Part] ([ID])
GO
ALTER TABLE [dbo].[RAM]  WITH CHECK ADD  CONSTRAINT [FK_RAM_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[RAM] CHECK CONSTRAINT [FK_RAM_Brand]
GO
ALTER TABLE [dbo].[Supply]  WITH CHECK ADD  CONSTRAINT [FK_Supply_Brand] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brand] ([ID])
GO
ALTER TABLE [dbo].[Supply] CHECK CONSTRAINT [FK_Supply_Brand]
GO
USE [master]
GO
ALTER DATABASE [ComponentsSellerDB] SET  READ_WRITE 
GO
