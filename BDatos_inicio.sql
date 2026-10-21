create database BDatos_inicio;
use BDatos_inicio;

create table Producto(
id_produc int auto_increment primary key,
nombre varchar(100) not null
);
create table Categoria(
id_categoria int auto_increment primary key
);
create table Material(
id_material int auto_increment primary key
);
create table Tipos_Tarea(
id_tip_tarea int auto_increment primary key
);
create table Usuario(
id_usuario int auto_increment primary key,
nombre varchar(100) not null
);