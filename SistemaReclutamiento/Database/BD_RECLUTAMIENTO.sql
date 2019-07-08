use master
go

CREATE DATABASE BD_RECLUTAMIENTO
go
USE BD_RECLUTAMIENTO
go
if(not exists(select 1 from sys.tables where name='TipoDocumento'))
	CREATE TABLE dbo.TipoDocumento(
		[tipoDocumentoId] [int] identity(1,1) NOT NULL,
		[tipoDocumentoDescripcion] [varchar](50) NULL,
		PRIMARY KEY ([tipoDocumentoId]) 	
	)
GO
if(not exists(select 1 from sys.tables where name ='Ubigeo'))
	CREATE TABLE dbo.Ubigeo(
		[ubigeoId] [int] IDENTITY(1,1) NOT NULL,
		[ubigeoNumero] [varchar](10) NULL,
		[ubigeoDistrito] [varchar](50) NULL,
		[ubigeoProvincia] [varchar](50) NULL,
		[ubigeoDepartamento] [varchar](50)NULL,
		PRIMARY KEY ([ubigeoId]) 	
	)
GO
if (not exists(select 1 from sys.tables where name = 'Persona'))
    CREATE TABLE dbo.Persona (
		[personaId] [int] IDENTITY(1,1) NOT NULL,
		[ubigeoId] [int] NULL,
		[tipoDocumentoId] int NOT NULL,
		[personaNroDocumento] [varchar](8) NOT NULL UNIQUE,
		[personaNombre] [varchar](100) NOT NULL,
		[personaApellidoPaterno] [varchar](100) NOT NULL,
		[personaApellidoMaterno] [varchar](100) NOT NULL,
		[personaEmail] [varchar](100) NULL UNIQUE,
		[personaEstado] [int] NULL,
		[personaFechaNacimiento] [datetime] NULL,
		[personaDireccion] [varchar](50) NULL,
		[personaContacto1] [varchar](12) NULL,
		[personaContacto2] [varchar](12) NULL,
		[personaTelefono] [varchar](10) NULL,
		[personaSexo] [varchar](1) NULL,
		[personaEstadoCivil] [varchar](15) NULL,      
		PRIMARY KEY ([personaId]),
		FOREIGN KEY ([ubigeoId]) references Ubigeo,
		FOREIGN KEY ([tipoDocumentoId]) references TipoDocumento
)
go


if (not exists(select 1 from sys.tables where name = 'PesonaCv'))
    CREATE TABLE dbo.PersonaCv (
		[personaCvId] [int] IDENTITY(1,1) NOT NULL,
		[personaId] [int] NOT NULL,
		[archivoRuta] [varchar](100) NOT NULL,
		[archivoExtension] [varchar](10) NOT NULL,
		[archivoNombre] [varchar](20) NULL,     
       PRIMARY KEY ([personaCvId]),
	   FOREIGN KEY ([personaId]) REFERENCES Persona
)
go



if (not exists(select 1 from sys.tables where name = 'Usuario'))
    CREATE TABLE dbo.Usuario (
     	[usuarioId] [int] IDENTITY(1,1) NOT NULL,
		[personaId] [int] NOT NULL,
		[usuarioEmail] [varchar](100) NOT NULL,
		[usuarioContrasenia] [varchar](100) NOT NULL,
		[usuarioToken] [varchar](100) NULL,
		[usuarioAvatarExtension] [varchar](250) NULL,
		[usuarioAvatarNombre] [varchar](20) NULL,
		[usuarioAvatarRuta] [varchar](50) NULL,
		[usuarioEstado] [int] NULL,
		[usuarioFechaCreacion] [datetime] NULL,
		[usuarioValidado] [int] NOT NULL,
       PRIMARY KEY ([usuarioId]),
	   FOREIGN KEY ([personaId]) REFERENCES Persona       
)
go

/*Contraseña de usuario(copiar con espacios para no tener problemas) admin --> DqegpAKsMK4=                                                                                        */