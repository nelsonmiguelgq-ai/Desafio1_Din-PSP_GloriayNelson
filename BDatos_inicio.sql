create database BDatos_inicio;
use BDatos_inicio;


create table Categoria(
id_categoria int auto_increment primary key,
nombre varchar(100) unique
);
create table Material(
id_material int auto_increment primary key,
nombre varchar(100) not null,
unidad varchar(50) not null,
precio_unitario double not null,
id_produc int,
constraint fk_product_material
foreign key (id_produc) references Producto(id_produc) on update cascade
);
create table Tipos_Tarea(
id_tip_tarea int auto_increment primary key,
nombre varchar(100),
precio_hora double not null
);
create table Usuario(
id_usuario int auto_increment primary key,
nombre varchar(100) unique not null ,
contraseña varchar(50) unique not null,
rol varchar(100) not null
);
create table Cliente(
id_cliente int auto_increment primary key,
nombre varchar(100) not null,
NIF varchar(50) unique not null,
email varchar(50) not null,
telefono int not null,
direccion varchar(100) not null
);
create table Producto(
id_producto INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(70) NOT NULL,
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
    total DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
id_produc int,
constraint fk_product_presu
foreign key (id_produc) references Producto(id_produc) on update cascade
);
CREATE TABLE Empleado (
    id INT AUTO_INCREMENT PRIMARY KEY,
    dni CHAR(9) NOT NULL,
    nombre VARCHAR(60) NOT NULL,
    telefono VARCHAR(9),
    salario DECIMAL(8,2) NOT NULL DEFAULT 1200.00,
    activo BOOLEAN NOT NULL DEFAULT TRUE
    );
    create table producto_material(-- ESTE ES EL ERROR--
     id_producto_material INT AUTO_INCREMENT PRIMARY KEY,
    id_producto INT NOT NULL,
    id_material INT NOT NULL,
    cantidad DECIMAL(10, 2) NOT NULL DEFAULT 1.00,
    FOREIGN KEY (id_producto) REFERENCES producto(id_producto),
    FOREIGN KEY (id_material) REFERENCES material(id_material),
    UNIQUE (id_producto, id_material)
    );
