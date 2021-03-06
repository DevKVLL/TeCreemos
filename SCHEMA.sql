USE [master]
GO
/****** Object:  Database [TeCreemos]    Script Date: 17/01/2022 01:47:41 p. m. ******/
CREATE DATABASE [TeCreemos] ON  PRIMARY 
( NAME = N'TeCreemos', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.DEVKARLA\MSSQL\DATA\TeCreemos.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TeCreemos_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.DEVKARLA\MSSQL\DATA\TeCreemos_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TeCreemos] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TeCreemos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TeCreemos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TeCreemos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TeCreemos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TeCreemos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TeCreemos] SET ARITHABORT OFF 
GO
ALTER DATABASE [TeCreemos] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TeCreemos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TeCreemos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TeCreemos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TeCreemos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TeCreemos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TeCreemos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TeCreemos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TeCreemos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TeCreemos] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TeCreemos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TeCreemos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TeCreemos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TeCreemos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TeCreemos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TeCreemos] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TeCreemos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TeCreemos] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TeCreemos] SET  MULTI_USER 
GO
ALTER DATABASE [TeCreemos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TeCreemos] SET DB_CHAINING OFF 
GO
USE [TeCreemos]
GO
/****** Object:  Table [dbo].[CatClientes]    Script Date: 17/01/2022 01:47:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CatClientes](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[FstApellido] [varchar](50) NOT NULL,
	[ScndApellido] [varchar](50) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Telefono] [varchar](13) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[ClaveElector] [varchar](18) NOT NULL,
 CONSTRAINT [PK_CatClientes] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CuentaAhorro]    Script Date: 17/01/2022 01:47:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CuentaAhorro](
	[IdCuenta] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[FechaApertura] [date] NOT NULL,
	[NoCuenta] [varchar](16) NULL,
	[SaldoActual] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_CuentaAhorro] PRIMARY KEY CLUSTERED 
(
	[IdCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogTransacciones]    Script Date: 17/01/2022 01:47:41 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogTransacciones](
	[IdLog] [int] IDENTITY(1,1) NOT NULL,
	[IdCuenta] [int] NOT NULL,
	[Operacion] [varchar](20) NOT NULL,
	[Cantidad] [decimal](18, 2) NOT NULL,
	[SaldoAnterior] [decimal](18, 2) NOT NULL,
	[SaldoRestante] [decimal](18, 2) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [time](0) NOT NULL,
 CONSTRAINT [PK_LogTransacciones] PRIMARY KEY CLUSTERED 
(
	[IdLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CuentaAhorro]  WITH CHECK ADD  CONSTRAINT [FK_CuentaAhorro_CatClientes] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[CatClientes] ([IdCliente])
GO
ALTER TABLE [dbo].[CuentaAhorro] CHECK CONSTRAINT [FK_CuentaAhorro_CatClientes]
GO
ALTER TABLE [dbo].[LogTransacciones]  WITH CHECK ADD  CONSTRAINT [FK_LogTransacciones_CuentaAhorro] FOREIGN KEY([IdCuenta])
REFERENCES [dbo].[CuentaAhorro] ([IdCuenta])
GO
ALTER TABLE [dbo].[LogTransacciones] CHECK CONSTRAINT [FK_LogTransacciones_CuentaAhorro]
GO
USE [master]
GO
ALTER DATABASE [TeCreemos] SET  READ_WRITE 
GO
