USE [master]
GO
/****** Object:  Database [inventariodb]    Script Date: 24/05/2023 09:41:23 a. m. ******/
CREATE DATABASE [inventariodb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'inventariodb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\inventariodb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'inventariodb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\inventariodb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [inventariodb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [inventariodb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [inventariodb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [inventariodb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [inventariodb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [inventariodb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [inventariodb] SET ARITHABORT OFF 
GO
ALTER DATABASE [inventariodb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [inventariodb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [inventariodb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [inventariodb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [inventariodb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [inventariodb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [inventariodb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [inventariodb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [inventariodb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [inventariodb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [inventariodb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [inventariodb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [inventariodb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [inventariodb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [inventariodb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [inventariodb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [inventariodb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [inventariodb] SET RECOVERY FULL 
GO
ALTER DATABASE [inventariodb] SET  MULTI_USER 
GO
ALTER DATABASE [inventariodb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [inventariodb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [inventariodb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [inventariodb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [inventariodb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [inventariodb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'inventariodb', N'ON'
GO
ALTER DATABASE [inventariodb] SET QUERY_STORE = ON
GO
ALTER DATABASE [inventariodb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [inventariodb]
GO
/****** Object:  Table [dbo].[BodegaProducto]    Script Date: 24/05/2023 09:41:23 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BodegaProducto](
	[TipoBodega] [varchar](100) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[UnidadMedida] [varchar](100) NOT NULL,
	[Precio] [numeric](18, 2) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[InventarioInicial] [int] NOT NULL,
	[CantidadEntradas] [int] NOT NULL,
	[CantidadSalidas] [int] NOT NULL,
	[CantidadFinal] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 24/05/2023 09:41:24 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] NOT NULL,
	[DescripcionRol] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 24/05/2023 09:41:24 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Nombre] [varchar](100) NOT NULL,
	[Login] [char](20) NOT NULL,
	[Passwords] [char](50) NOT NULL,
	[IdRol] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[BodegaProducto] ([TipoBodega], [Nombre], [UnidadMedida], [Precio], [Fecha], [InventarioInicial], [CantidadEntradas], [CantidadSalidas], [CantidadFinal]) VALUES (N'Alimentos', N'jio', N'P', CAST(9.00 AS Numeric(18, 2)), CAST(N'2023-04-03T20:30:15.903' AS DateTime), 0, 9, 9, 10)
INSERT [dbo].[BodegaProducto] ([TipoBodega], [Nombre], [UnidadMedida], [Precio], [Fecha], [InventarioInicial], [CantidadEntradas], [CantidadSalidas], [CantidadFinal]) VALUES (N'Alimentos', N'asd', N'Lata ', CAST(10.00 AS Numeric(18, 2)), CAST(N'2023-04-03T22:25:57.887' AS DateTime), 0, 2, 0, 0)
INSERT [dbo].[BodegaProducto] ([TipoBodega], [Nombre], [UnidadMedida], [Precio], [Fecha], [InventarioInicial], [CantidadEntradas], [CantidadSalidas], [CantidadFinal]) VALUES (N'Alimentos', N'Banano', N'Una caja', CAST(1.00 AS Numeric(18, 2)), CAST(N'2023-04-03T22:26:51.200' AS DateTime), 0, 25, 0, 0)
INSERT [dbo].[BodegaProducto] ([TipoBodega], [Nombre], [UnidadMedida], [Precio], [Fecha], [InventarioInicial], [CantidadEntradas], [CantidadSalidas], [CantidadFinal]) VALUES (N'Alimentos', N'Coca-Cola', N'Litro (L)', CAST(1000.00 AS Numeric(18, 2)), CAST(N'2023-04-03T22:28:27.897' AS DateTime), 2000, 2, 0, 0)
INSERT [dbo].[BodegaProducto] ([TipoBodega], [Nombre], [UnidadMedida], [Precio], [Fecha], [InventarioInicial], [CantidadEntradas], [CantidadSalidas], [CantidadFinal]) VALUES (N'Limpieza', N'Toallas', N'Caja', CAST(1200.00 AS Numeric(18, 2)), CAST(N'2023-04-03T00:00:00.000' AS DateTime), 25, 1, 0, 26)
INSERT [dbo].[BodegaProducto] ([TipoBodega], [Nombre], [UnidadMedida], [Precio], [Fecha], [InventarioInicial], [CantidadEntradas], [CantidadSalidas], [CantidadFinal]) VALUES (N'Aseo_Personal', N'Shampo', N'Litro (L)', CAST(2000.00 AS Numeric(18, 2)), CAST(N'2023-04-03T00:00:00.000' AS DateTime), 5, 2, 0, 7)
INSERT [dbo].[BodegaProducto] ([TipoBodega], [Nombre], [UnidadMedida], [Precio], [Fecha], [InventarioInicial], [CantidadEntradas], [CantidadSalidas], [CantidadFinal]) VALUES (N'Alimentos', N'kaka', N'Mililitro (ml) ', CAST(1.00 AS Numeric(18, 2)), CAST(N'2023-05-23T20:45:25.457' AS DateTime), 1, 1, 0, 0)
INSERT [dbo].[BodegaProducto] ([TipoBodega], [Nombre], [UnidadMedida], [Precio], [Fecha], [InventarioInicial], [CantidadEntradas], [CantidadSalidas], [CantidadFinal]) VALUES (N'Limpieza', N'kaka limpieza', N'Gramos (g)', CAST(2.00 AS Numeric(18, 2)), CAST(N'2023-05-23T21:01:10.877' AS DateTime), 40, 20, 0, 0)
GO
INSERT [dbo].[Rol] ([IdRol], [DescripcionRol]) VALUES (1, N'Administrador')
GO
INSERT [dbo].[Usuario] ([Nombre], [Login], [Passwords], [IdRol], [Estado]) VALUES (N'Administrador', N'Admin               ', N'123456                                            ', 1, 1)
GO
USE [master]
GO
ALTER DATABASE [inventariodb] SET  READ_WRITE 
GO
