CREATE DATABASE FinalECC;


use FinalECC;

CREATE TABLE Serie (
    id INT IDENTITY(1,1) PRIMARY KEY,
	titulo VARCHAR(250),
	sinopsis VARCHAR(5000),
	director VARCHAR(100),
	duracion INT NOT NULL,
	fechaEstreno DATETIME DEFAULT GETDATE(),
	usuarioRegistro VARCHAR(12) DEFAULT SUSER_SNAME(),
	registroActivo BIT DEFAULT 1
);


CREATE TABLE Usuario (
  id INT IDENTITY(1,1) PRIMARY KEY,
  usuario VARCHAR(12) NOT NULL UNIQUE,
  clave VARCHAR(250) NOT NULL,
  rol VARCHAR(20),
  registroActivo BIT DEFAULT 1,
);


USE [master]
GO
CREATE LOGIN [usrECC] WITH PASSWORD=N'1234567', 
   DEFAULT_DATABASE=[LabECC], 
   CHECK_EXPIRATION=OFF, 
   CHECK_POLICY=ON
GO
USE [LabECC]
GO
CREATE USER [usrECC] FOR LOGIN [usrECC]
GO
USE [LabECC]
GO
ALTER ROLE [db_owner] ADD MEMBER [usrECC]
GO

create proc paSerieListar @parametro varchar(100)
as
select id, titulo, sinopsis, director, duracion, fechaEstreno, usuarioRegistro
from Serie where registroActivo=1
and titulo+sinopsis like '%'+@parametro+'%'

insert into Serie 
values ('Lucy2','pelicula de ciencia ficcion','juan',95,GETDATE(),'AAA4',1)

insert into Usuario 
values ('edwin','hola1234.','administrador',1)

exec paSerieListar '2'

select * from Serie 
