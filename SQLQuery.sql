create table Material(
MaterialId int primary key identity,
Razon varchar(100)
);
go
create table MaterialDetalle(
DetalleId int primary key identity,
MaterialId int references Material(MaterialId),
Material varchar(50),
Cantidad int
);