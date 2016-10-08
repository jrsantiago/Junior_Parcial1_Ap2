create table Materiales(
IdMaterial int primary key identity,
Descripcion varchar(50),
Precio float,
);
go
create table Solicitudes(
IdSolicitud int primary key identity,
Fecha varchar(50),
Razon varchar(300),
Total float
);
go
create table SolicitudesDetalle(
Id int primary key identity,
IdSolicutud int references Solicitudes(IdSolicitud),
IdMaterial int references Materiales(IdMaterial),
Cantidad int,
Precio float
);