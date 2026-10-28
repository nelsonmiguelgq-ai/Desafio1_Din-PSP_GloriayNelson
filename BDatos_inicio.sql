create database BDatos_inicio;
use BDatos_inicio;

create table Tipos_Tarea(
id_tip_tarea int auto_increment primary key,
nombre varchar(100),
precio_hora double not null
);
drop table tipos_tarea;
drop table usuario;
drop table cliente;
drop table categoria;
drop table producto;
drop table prestamo;
drop table presupuesto;
drop table material;
create table Usuario(
id_usuario int auto_increment primary key,
nombre varchar(100) unique not null ,
contraseña varchar(50) unique not null,
rol varchar(100) not null
);

create table Cliente(
id_usuario int primary key,
nombre varchar(100) not null,
NIF varchar(50) unique not null,
email varchar(50) not null,
telefono int not null,
direccion varchar(100) not null,
 constraint fk_cliente_usuario
 FOREIGN KEY (id_usuario) REFERENCES usuario(id_usuario) ON UPDATE CASCADE ON DELETE CASCADE);


create table Producto(
id_producto INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(70) NOT NULL,
    tipo_material ENUM('Metal', 'Cemento', 'Hormigon', 'plastico') NOT NULL DEFAULT 'Pendiente',
    dimensiones VARCHAR(50),
    proveedor VARCHAR(50) NOT NULL,
    descripcion VARCHAR(255),
    cantidad_stock INT NOT NULL DEFAULT 0,
    precio_venta DECIMAL(15, 2) NOT NULL DEFAULT 0.00,
    precio_proveedor DECIMAL(15, 2) NOT NULL DEFAULT 0.00
);
create table Presupuesto(
 id_presupuesto INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    codigo INT UNIQUE NOT NULL,
    fecha DATE NOT NULL,
    estado ENUM('Pendiente', 'Aprobado', 'Rechazado') NOT NULL DEFAULT 'Pendiente',
    total_materiales DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    total DECIMAL(10, 2) NOT NULL DEFAULT 0.00

);

   