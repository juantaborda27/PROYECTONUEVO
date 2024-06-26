USE [master]
GO
/****** Object:  Database [INVENTARIOTVV_DB]    Script Date: 13/06/2024 11:16:51 ******/
CREATE DATABASE [INVENTARIOTVV_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'INVENTARIOTVV_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\INVENTARIOTVV_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'INVENTARIOTVV_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\INVENTARIOTVV_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [INVENTARIOTVV_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET  MULTI_USER 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET QUERY_STORE = OFF
GO
USE [INVENTARIOTVV_DB]
GO
/****** Object:  Table [dbo].[CATEGORIA]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORIA](
	[IdCategoria] [int] NOT NULL,
	[DescripcionCategoria] [varchar](30) NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLIENTE]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLIENTE](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoCliente] [varchar](10) NOT NULL,
	[NombreCliente] [varchar](40) NULL,
	[Telefono] [varchar](20) NULL,
	[Correo] [varchar](40) NOT NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DocumentoCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COMPRA]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMPRA](
	[IdCompra] [int] NOT NULL,
	[IdUsuario] [int] NULL,
	[IdProveedor] [int] NULL,
	[MontoTotal] [decimal](13, 2) NULL,
	[FechaCompra] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DETALLECOMPRA]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DETALLECOMPRA](
	[IdDetalleCompra] [int] IDENTITY(1,1) NOT NULL,
	[IdCompra] [int] NULL,
	[IdProducto] [int] NULL,
	[PrecioCompra] [decimal](13, 2) NULL,
	[PrecioVenta] [decimal](13, 2) NULL,
	[CantidadProducto] [int] NULL,
	[SubTotal] [decimal](13, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDetalleCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DETALLEVENTA]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DETALLEVENTA](
	[IdDetalleVenta] [int] IDENTITY(1,1) NOT NULL,
	[IdVenta] [int] NULL,
	[IdProducto] [int] NULL,
	[PrecioVenta] [decimal](13, 2) NULL,
	[Cantidad] [int] NULL,
	[SubTotal] [decimal](13, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDetalleVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCTO]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTO](
	[IdProducto] [int] NOT NULL,
	[DescripcionProducto] [varchar](30) NOT NULL,
	[IdCategoria] [int] NULL,
	[CantidadExistente] [int] NULL,
	[PrecioCompra] [decimal](12, 3) NULL,
	[PrecioVenta] [decimal](12, 3) NULL,
	[CantidadMinima] [int] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PROVEEDOR]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PROVEEDOR](
	[IdProveedor] [int] IDENTITY(1,1) NOT NULL,
	[DocumentoProveedor] [int] NOT NULL,
	[NombreProveedor] [varchar](30) NULL,
	[Telefono] [varchar](10) NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](39) NULL,
	[Cedula] [varchar](15) NOT NULL,
	[Nombre] [varchar](40) NULL,
	[Telefono] [varchar](10) NULL,
	[Contraseña] [varchar](15) NOT NULL,
	[Rol] [varchar](20) NOT NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VENTA]    Script Date: 13/06/2024 11:16:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VENTA](
	[IdVenta] [int] NOT NULL,
	[IdUsuario] [int] NULL,
	[DocumentoCliente] [varchar](10) NULL,
	[MontoPago] [decimal](13, 2) NULL,
	[MontoCambio] [decimal](13, 2) NULL,
	[MontoTotal] [decimal](13, 2) NULL,
	[FechaVenta] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (1, N'Bujia', CAST(N'2024-06-04T18:40:45.047' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (2, N'Balinera', CAST(N'2024-06-04T18:40:55.213' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (3, N'Pastillas Freno', CAST(N'2024-06-04T18:41:12.187' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (4, N'Bandas', CAST(N'2024-06-04T18:41:24.170' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (5, N'Llanta', CAST(N'2024-06-04T18:41:45.027' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (6, N'Cilindro', CAST(N'2024-06-04T18:41:56.367' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (7, N'Manigeta Freno', CAST(N'2024-06-04T18:42:20.340' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (8, N'Manigeta Cloche', CAST(N'2024-06-04T18:42:30.910' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (9, N'Aceite 4T', CAST(N'2024-06-12T09:41:47.007' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (10, N'Bandas delanteras', CAST(N'2024-06-12T11:17:21.583' AS DateTime))
INSERT [dbo].[CATEGORIA] ([IdCategoria], [DescripcionCategoria], [FechaCreacion]) VALUES (11, N'Bombillo', CAST(N'2024-06-12T20:29:49.650' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[CLIENTE] ON 

INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (1, N'1066', N'Juan Taborda', N'301585', N'juantaborda@', CAST(N'2024-06-04T18:43:45.047' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (28, N'1066866813', N'Juan Taborda', N'301', N'juantabordaacosta@gmail.com', CAST(N'2024-06-10T23:45:19.263' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (31, N'1099', N'Jose Vidal', N'2727', N'josevidal@gmail.com', CAST(N'2024-06-12T11:16:35.540' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (5, N'1234567890', N'Juan Perez', N'123-456-7890', N'juan.perez@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (30, N'167', N'Damian', N'313', N'damiancamiloquintero@gmail.com', CAST(N'2024-06-11T17:40:56.760' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (17, N'2233445566', N'Miguel Vargas', N'223-344-5566', N'miguel.vargas@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (6, N'2345678901', N'Maria Garcia', N'234-567-8901', N'maria.garcia@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (8, N'4567890123', N'Luisa Fernandez', N'456-789-0123', N'luisa.fernandez@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (9, N'5678901234', N'Jose Ramirez', N'567-890-1234', N'jose.ramirez@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (21, N'6677889900', N'Isabel Morales', N'667-788-9900', N'isabel.morales@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (10, N'6789012345', N'Ana Gomez', N'678-901-2345', N'ana.gomez@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (4, N'7099', N'Martin Acosta', N'311221', N'martin@', CAST(N'2024-06-04T18:44:46.163' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (25, N'77889', N'Libardo', N'316526', N'libardo@gmail', CAST(N'2024-06-06T23:56:16.373' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (22, N'7788990011', N'Gabriel Castro', N'778-899-0011', N'gabriel.castro@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (11, N'7890123456', N'Carlos Rodriguez', N'789-012-3456', N'carlos.rodriguez@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (23, N'8899001122', N'Lucia Ortiz', N'889-900-1122', N'lucia.ortiz@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (12, N'8901234567', N'Marta Lopez', N'890-123-4567', N'marta.lopez@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (13, N'9012345678', N'Javier Sanchez', N'901-234-5678', N'javier.sanchez@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (26, N'9091', N'Emilio', N'322', N'emilio@', CAST(N'2024-06-07T00:03:01.420' AS DateTime))
INSERT [dbo].[CLIENTE] ([IdCliente], [DocumentoCliente], [NombreCliente], [Telefono], [Correo], [FechaCreacion]) VALUES (24, N'9900112233', N'Santiago Medina', N'990-011-2233', N'santiago.medina@example.com', CAST(N'2024-06-05T07:11:01.567' AS DateTime))
SET IDENTITY_INSERT [dbo].[CLIENTE] OFF
GO
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (11, 1, 2, CAST(240000.00 AS Decimal(13, 2)), CAST(N'2024-06-04T21:05:48.917' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (13, 1, 3, CAST(288000.00 AS Decimal(13, 2)), CAST(N'2024-06-04T21:46:42.063' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (15, 1, 4, CAST(240000.00 AS Decimal(13, 2)), CAST(N'2024-06-04T23:28:17.507' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (17, 1, 4, CAST(240000.00 AS Decimal(13, 2)), CAST(N'2024-06-04T23:34:03.947' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (19, 1, 3, CAST(159000.00 AS Decimal(13, 2)), CAST(N'2024-06-05T08:57:03.923' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (20, 1, 16, CAST(2280000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T09:48:33.497' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (21, 1, 16, CAST(190000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T10:16:38.423' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (22, 1, 5, CAST(120000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T11:19:19.903' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (23, 1, 1, CAST(400000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T20:32:41.160' AS DateTime))
INSERT [dbo].[COMPRA] ([IdCompra], [IdUsuario], [IdProveedor], [MontoTotal], [FechaCompra]) VALUES (24, 1, 6, CAST(400000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T20:32:48.810' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[DETALLECOMPRA] ON 

INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (6, 13, 101, CAST(6000.00 AS Decimal(13, 2)), CAST(9000.00 AS Decimal(13, 2)), 40, CAST(240000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (7, 13, 113, CAST(1200.00 AS Decimal(13, 2)), CAST(7000.00 AS Decimal(13, 2)), 40, CAST(48000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (8, 15, 1131, CAST(6000.00 AS Decimal(13, 2)), CAST(12000.00 AS Decimal(13, 2)), 40, CAST(240000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (9, 17, 1131, CAST(6000.00 AS Decimal(13, 2)), CAST(12000.00 AS Decimal(13, 2)), 40, CAST(240000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (10, 19, 101, CAST(3000.00 AS Decimal(13, 2)), CAST(7000.00 AS Decimal(13, 2)), 30, CAST(90000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (11, 19, 103, CAST(2300.00 AS Decimal(13, 2)), CAST(6000.00 AS Decimal(13, 2)), 30, CAST(69000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (12, 20, 7704, CAST(19000.00 AS Decimal(13, 2)), CAST(27000.00 AS Decimal(13, 2)), 120, CAST(2280000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (13, 21, 7704, CAST(19000.00 AS Decimal(13, 2)), CAST(27000.00 AS Decimal(13, 2)), 10, CAST(190000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (14, 22, 101, CAST(3000.00 AS Decimal(13, 2)), CAST(7000.00 AS Decimal(13, 2)), 40, CAST(120000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (15, 23, 111, CAST(40000.00 AS Decimal(13, 2)), CAST(60000.00 AS Decimal(13, 2)), 10, CAST(400000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLECOMPRA] ([IdDetalleCompra], [IdCompra], [IdProducto], [PrecioCompra], [PrecioVenta], [CantidadProducto], [SubTotal]) VALUES (16, 23, 111, CAST(40000.00 AS Decimal(13, 2)), CAST(60000.00 AS Decimal(13, 2)), 10, CAST(400000.00 AS Decimal(13, 2)))
SET IDENTITY_INSERT [dbo].[DETALLECOMPRA] OFF
GO
SET IDENTITY_INSERT [dbo].[DETALLEVENTA] ON 

INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (3, 2, 103, CAST(6000.00 AS Decimal(13, 2)), 2, CAST(12000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (4, 2, 101, CAST(6000.00 AS Decimal(13, 2)), 2, CAST(12000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (5, 3, 1131, CAST(12000.00 AS Decimal(13, 2)), 1, CAST(12000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (6, 4, 1131, CAST(12000.00 AS Decimal(13, 2)), 1, CAST(12000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (9, 6, 101, CAST(7000.00 AS Decimal(13, 2)), 1, CAST(7000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (10, 7, 7704, CAST(27000.00 AS Decimal(13, 2)), 2, CAST(54000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (11, 8, 7704, CAST(27000.00 AS Decimal(13, 2)), 2, CAST(54000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (12, 9, 101, CAST(7000.00 AS Decimal(13, 2)), 5, CAST(35000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (13, 9, 1131, CAST(12000.00 AS Decimal(13, 2)), 3, CAST(36000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (14, 10, 111, CAST(60000.00 AS Decimal(13, 2)), 2, CAST(120000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (15, 11, 1131, CAST(12000.00 AS Decimal(13, 2)), 2, CAST(24000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (16, 12, 1131, CAST(12000.00 AS Decimal(13, 2)), 1, CAST(12000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (17, 13, 101, CAST(7000.00 AS Decimal(13, 2)), 1, CAST(7000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (18, 14, 101, CAST(7000.00 AS Decimal(13, 2)), 1, CAST(7000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (19, 15, 101, CAST(7000.00 AS Decimal(13, 2)), 1, CAST(7000.00 AS Decimal(13, 2)))
INSERT [dbo].[DETALLEVENTA] ([IdDetalleVenta], [IdVenta], [IdProducto], [PrecioVenta], [Cantidad], [SubTotal]) VALUES (20, 16, 101, CAST(7000.00 AS Decimal(13, 2)), 1, CAST(7000.00 AS Decimal(13, 2)))
SET IDENTITY_INSERT [dbo].[DETALLEVENTA] OFF
GO
INSERT [dbo].[PRODUCTO] ([IdProducto], [DescripcionProducto], [IdCategoria], [CantidadExistente], [PrecioCompra], [PrecioVenta], [CantidadMinima], [FechaCreacion]) VALUES (101, N'Boxer', 1, 54, CAST(3000.000 AS Decimal(12, 3)), CAST(7000.000 AS Decimal(12, 3)), 25, CAST(N'2024-06-04T18:46:28.393' AS DateTime))
INSERT [dbo].[PRODUCTO] ([IdProducto], [DescripcionProducto], [IdCategoria], [CantidadExistente], [PrecioCompra], [PrecioVenta], [CantidadMinima], [FechaCreacion]) VALUES (103, N'NKD', 1, 27, CAST(2300.000 AS Decimal(12, 3)), CAST(6000.000 AS Decimal(12, 3)), 25, CAST(N'2024-06-04T18:46:44.907' AS DateTime))
INSERT [dbo].[PRODUCTO] ([IdProducto], [DescripcionProducto], [IdCategoria], [CantidadExistente], [PrecioCompra], [PrecioVenta], [CantidadMinima], [FechaCreacion]) VALUES (111, N'275', 5, 18, CAST(40000.000 AS Decimal(12, 3)), CAST(60000.000 AS Decimal(12, 3)), 5, CAST(N'2024-06-12T20:31:52.350' AS DateTime))
INSERT [dbo].[PRODUCTO] ([IdProducto], [DescripcionProducto], [IdCategoria], [CantidadExistente], [PrecioCompra], [PrecioVenta], [CantidadMinima], [FechaCreacion]) VALUES (113, N'6002', 2, 0, CAST(0.000 AS Decimal(12, 3)), CAST(0.000 AS Decimal(12, 3)), 25, CAST(N'2024-06-04T18:47:50.557' AS DateTime))
INSERT [dbo].[PRODUCTO] ([IdProducto], [DescripcionProducto], [IdCategoria], [CantidadExistente], [PrecioCompra], [PrecioVenta], [CantidadMinima], [FechaCreacion]) VALUES (1131, N'XTZ', 3, 0, CAST(6000.000 AS Decimal(12, 3)), CAST(12000.000 AS Decimal(12, 3)), 173, CAST(N'2024-06-04T18:48:25.897' AS DateTime))
INSERT [dbo].[PRODUCTO] ([IdProducto], [DescripcionProducto], [IdCategoria], [CantidadExistente], [PrecioCompra], [PrecioVenta], [CantidadMinima], [FechaCreacion]) VALUES (7704, N'Mobil', 9, 126, CAST(19000.000 AS Decimal(12, 3)), CAST(27000.000 AS Decimal(12, 3)), 20, CAST(N'2024-06-12T09:47:52.463' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[PROVEEDOR] ON 

INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (1, 998877, N'HA BICICLETAS', N'322098', CAST(N'2024-06-04T18:49:26.150' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (2, 46373, N'AKT', N'4321', CAST(N'2024-06-04T18:49:51.757' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (3, 27737, N'CASSARELLA', N'341122', CAST(N'2024-06-04T18:50:06.257' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (4, 1626, N'JAPAN', N'35511', CAST(N'2024-06-04T18:50:19.353' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (5, 9292, N'COEXITO', N'31122', CAST(N'2024-06-04T18:50:39.510' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (6, 1001, N'Proveedores Industriales SA', N'1234567890', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (7, 1002, N'Comercializadora Global', N'2345678901', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (8, 1003, N'Distribuidora Central', N'3456789012', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (9, 1004, N'Suministros y Servicios', N'4567890123', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (10, 1005, N'Importaciones del Norte', N'5678901234', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (11, 1006, N'Exportadora Universal', N'6789012345', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (12, 1007, N'Tech Supplies Co.', N'7890123456', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (13, 1008, N'Abastecedores Unidos', N'8901234567', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (14, 1009, N'Mercantil del Sur', N'9012345678', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (15, 1010, N'Servicios Integrales', N'0123456789', CAST(N'2024-06-05T07:12:18.777' AS DateTime))
INSERT [dbo].[PROVEEDOR] ([IdProveedor], [DocumentoProveedor], [NombreProveedor], [Telefono], [FechaCreacion]) VALUES (16, 1011, N'Lubrycell', N'311223', CAST(N'2024-06-12T09:46:56.157' AS DateTime))
SET IDENTITY_INSERT [dbo].[PROVEEDOR] OFF
GO
SET IDENTITY_INSERT [dbo].[USUARIO] ON 

INSERT [dbo].[USUARIO] ([IdUsuario], [UserName], [Cedula], [Nombre], [Telefono], [Contraseña], [Rol], [FechaCreacion]) VALUES (1, N'Tabo', N'1066', N'Juan Taborda', N'301585', N'123', N'Administrador', CAST(N'2024-06-04T18:52:12.173' AS DateTime))
INSERT [dbo].[USUARIO] ([IdUsuario], [UserName], [Cedula], [Nombre], [Telefono], [Contraseña], [Rol], [FechaCreacion]) VALUES (2, N'Alvarito', N'10889', N'Alvaro Vidal', N'3661', N'123', N'Administrador', CAST(N'2024-06-04T18:52:41.503' AS DateTime))
INSERT [dbo].[USUARIO] ([IdUsuario], [UserName], [Cedula], [Nombre], [Telefono], [Contraseña], [Rol], [FechaCreacion]) VALUES (3, N'Jose', N'10552', N'Jose Vidal', N'3661', N'123', N'Administrador', CAST(N'2024-06-04T18:53:07.050' AS DateTime))
INSERT [dbo].[USUARIO] ([IdUsuario], [UserName], [Cedula], [Nombre], [Telefono], [Contraseña], [Rol], [FechaCreacion]) VALUES (4, N'Libardo', N'77024', N'Libardo Taborda', N'322', N'123', N'Empleado', CAST(N'2024-06-04T18:53:41.440' AS DateTime))
INSERT [dbo].[USUARIO] ([IdUsuario], [UserName], [Cedula], [Nombre], [Telefono], [Contraseña], [Rol], [FechaCreacion]) VALUES (5, N'Ana', N'1044', N'Ana', N'302', N'123', N'Empleado', CAST(N'2024-06-12T10:11:45.000' AS DateTime))
INSERT [dbo].[USUARIO] ([IdUsuario], [UserName], [Cedula], [Nombre], [Telefono], [Contraseña], [Rol], [FechaCreacion]) VALUES (6, N'Camilo', N'1211', N'Cami', N'322', N'123', N'Empleado', CAST(N'2024-06-12T11:15:54.967' AS DateTime))
SET IDENTITY_INSERT [dbo].[USUARIO] OFF
GO
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (2, 1, N'1066', CAST(18000.00 AS Decimal(13, 2)), CAST(3000.00 AS Decimal(13, 2)), CAST(18000.00 AS Decimal(13, 2)), CAST(N'2024-06-08T23:41:19.787' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (3, 1, N'1066', CAST(13000.00 AS Decimal(13, 2)), CAST(1000.00 AS Decimal(13, 2)), CAST(12000.00 AS Decimal(13, 2)), CAST(N'2024-06-11T07:21:06.270' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (4, 1, N'1066', CAST(13000.00 AS Decimal(13, 2)), CAST(1000.00 AS Decimal(13, 2)), CAST(12000.00 AS Decimal(13, 2)), CAST(N'2024-06-11T07:22:45.700' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (6, 1, N'167', CAST(10000.00 AS Decimal(13, 2)), CAST(3000.00 AS Decimal(13, 2)), CAST(7000.00 AS Decimal(13, 2)), CAST(N'2024-06-11T17:41:19.600' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (7, 1, N'1066866813', CAST(60000.00 AS Decimal(13, 2)), CAST(6000.00 AS Decimal(13, 2)), CAST(54000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T09:50:12.983' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (8, 1, N'1066866813', CAST(60000.00 AS Decimal(13, 2)), CAST(6000.00 AS Decimal(13, 2)), CAST(54000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T10:18:29.283' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (9, 1, N'1066866813', CAST(75000.00 AS Decimal(13, 2)), CAST(4000.00 AS Decimal(13, 2)), CAST(71000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T11:20:21.310' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (10, 1, N'1066866813', CAST(130000.00 AS Decimal(13, 2)), CAST(10000.00 AS Decimal(13, 2)), CAST(120000.00 AS Decimal(13, 2)), CAST(N'2024-06-12T20:35:05.323' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (11, 1, N'1066', CAST(25000.00 AS Decimal(13, 2)), CAST(1000.00 AS Decimal(13, 2)), CAST(24000.00 AS Decimal(13, 2)), CAST(N'2024-06-13T10:48:48.170' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (12, 1, N'1066', CAST(13000.00 AS Decimal(13, 2)), CAST(1000.00 AS Decimal(13, 2)), CAST(12000.00 AS Decimal(13, 2)), CAST(N'2024-06-13T10:51:08.300' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (13, 1, N'1066', CAST(8000.00 AS Decimal(13, 2)), CAST(1000.00 AS Decimal(13, 2)), CAST(7000.00 AS Decimal(13, 2)), CAST(N'2024-06-13T10:55:05.573' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (14, 1, N'1066', CAST(7000.00 AS Decimal(13, 2)), CAST(0.00 AS Decimal(13, 2)), CAST(7000.00 AS Decimal(13, 2)), CAST(N'2024-06-13T10:56:42.377' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (15, 1, N'1066', CAST(8000.00 AS Decimal(13, 2)), CAST(1000.00 AS Decimal(13, 2)), CAST(7000.00 AS Decimal(13, 2)), CAST(N'2024-06-13T10:59:15.857' AS DateTime))
INSERT [dbo].[VENTA] ([IdVenta], [IdUsuario], [DocumentoCliente], [MontoPago], [MontoCambio], [MontoTotal], [FechaVenta]) VALUES (16, 2, N'1066866813', CAST(8000.00 AS Decimal(13, 2)), CAST(1000.00 AS Decimal(13, 2)), CAST(7000.00 AS Decimal(13, 2)), CAST(N'2024-06-13T11:12:40.363' AS DateTime))
GO
ALTER TABLE [dbo].[CATEGORIA] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[CLIENTE] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[COMPRA] ADD  DEFAULT (getdate()) FOR [FechaCompra]
GO
ALTER TABLE [dbo].[DETALLECOMPRA] ADD  DEFAULT ((0)) FOR [PrecioCompra]
GO
ALTER TABLE [dbo].[DETALLECOMPRA] ADD  DEFAULT ((0)) FOR [PrecioVenta]
GO
ALTER TABLE [dbo].[DETALLECOMPRA] ADD  DEFAULT ((0)) FOR [SubTotal]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT ((0)) FOR [CantidadExistente]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT ((0)) FOR [PrecioCompra]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT ((0)) FOR [PrecioVenta]
GO
ALTER TABLE [dbo].[PRODUCTO] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[PROVEEDOR] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[USUARIO] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[VENTA] ADD  DEFAULT ((0)) FOR [MontoPago]
GO
ALTER TABLE [dbo].[VENTA] ADD  DEFAULT ((0)) FOR [MontoCambio]
GO
ALTER TABLE [dbo].[VENTA] ADD  DEFAULT ((0)) FOR [MontoTotal]
GO
ALTER TABLE [dbo].[VENTA] ADD  DEFAULT (getdate()) FOR [FechaVenta]
GO
ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[PROVEEDOR] ([IdProveedor])
GO
ALTER TABLE [dbo].[COMPRA]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
ALTER TABLE [dbo].[DETALLECOMPRA]  WITH CHECK ADD FOREIGN KEY([IdCompra])
REFERENCES [dbo].[COMPRA] ([IdCompra])
GO
ALTER TABLE [dbo].[DETALLECOMPRA]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[PRODUCTO] ([IdProducto])
GO
ALTER TABLE [dbo].[DETALLEVENTA]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[PRODUCTO] ([IdProducto])
GO
ALTER TABLE [dbo].[DETALLEVENTA]  WITH CHECK ADD FOREIGN KEY([IdVenta])
REFERENCES [dbo].[VENTA] ([IdVenta])
GO
ALTER TABLE [dbo].[PRODUCTO]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[CATEGORIA] ([IdCategoria])
GO
ALTER TABLE [dbo].[VENTA]  WITH CHECK ADD FOREIGN KEY([DocumentoCliente])
REFERENCES [dbo].[CLIENTE] ([DocumentoCliente])
GO
ALTER TABLE [dbo].[VENTA]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[USUARIO] ([IdUsuario])
GO
USE [master]
GO
ALTER DATABASE [INVENTARIOTVV_DB] SET  READ_WRITE 
GO
