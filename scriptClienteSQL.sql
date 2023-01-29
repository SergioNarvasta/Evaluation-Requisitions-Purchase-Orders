IF EXISTS ( SELECT name FROM sysdatabases WHERE name = 'dbparqueo' )
	DROP DATABASE dbparqueo
GO

CREATE DATABASE dbparqueo
GO
USE dbparqueo
GO

CREATE TABLE Cliente
  (	 
       codiClie         int IDENTITY (1,1),
	   nombClie	   		char(31),
	   dniClie			int PRIMARY KEY  not null,
  	   telfClie			varchar(10),
	   placClie			varchar(30),
	   modeClie  		varchar(30),
	   coloClie			varchar(30),
	   fechClie			varchar(30),
	   horaClie			varchar(30)
   ) 

   /*DBCC CHECKIDENT (Cliente, RESEED,0)*/
   /*DBCC CHECKIDENT (Comprobante, RESEED,0)*/

 GO
  CREATE TABLE Comprobante
  (	    id_comp int PRIMARY KEY IDENTITY(1,1),
		fech_comp varchar(10),
		codi_clie varchar(10),
		nomb_clie varchar(50),
		hora_ingreso varchar(50),
		hora_salida varchar(50),
		tiempo_uso varchar(50),
		descuento varchar(50),
		mont_comp decimal(10)
   ) 

   CREATE TABLE ImpresionTemp
  (	   id_comp1 int PRIMARY KEY,
		fech_comp1 varchar(10),
		codi_clie1 varchar(10),
		nomb_clie1 varchar(50),
		hora_ingreso1 varchar(50),
		hora_salida1 varchar(50),
		tiempo_uso1 varchar(50),
		descuento1 varchar(50),
		mont_comp1 decimal(10)
   ) 
  
