use bdatos_inicio;

select * from clientes;

DROP TABLE IF EXISTS Presupuesto_Detalle;
DROP TABLE IF EXISTS Presupuesto;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Tipos_Tarea;
DROP TABLE IF EXISTS categoria;
DROP TABLE IF EXISTS material;
DROP TABLE IF EXISTS empleado;
DROP TABLE producto;



DROP TABLE IF EXISTS Presupuesto_Detalle;
DROP TABLE IF EXISTS Presupuesto;
DROP TABLE IF EXISTS material;
DROP TABLE IF EXISTS Producto;
DROP TABLE IF EXISTS Cliente;
DROP TABLE IF EXISTS Usuario;
DROP TABLE IF EXISTS Tipos_Tarea;

-- ==========================================================
-- CREACIÓN DE TABLAS
-- ==========================================================

-- Tabla 1: Tipos de Tarea
CREATE TABLE Tipos_Tarea(
    id_tip_tarea INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    precio_hora DOUBLE NOT NULL
);


CREATE TABLE Usuario(
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) UNIQUE NOT NULL,
    contraseña VARCHAR(50) NOT NULL,
    rol VARCHAR(100) NOT NULL
);


CREATE TABLE Cliente(
    id_usuario int AUTO_INCREMENT  PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    NIF VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(50) NOT NULL,
    telefono INT NOT NULL,
    direccion VARCHAR(100) NOT NULL,
    CONSTRAINT fk_cliente_usuario
    FOREIGN KEY (id_usuario) REFERENCES Usuario(id_usuario) 
    ON UPDATE CASCADE ON DELETE CASCADE
);

-- Tabla 4: Productos / Materiales
CREATE TABLE Producto(
    id_producto INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(70) NOT NULL,
	tipo_material ENUM('Pendiente', 'Metal', 'Cemento', 'Hormigon', 'plastico') NOT NULL DEFAULT 'Pendiente',
    dimensiones VARCHAR(50),
    proveedor VARCHAR(50) NOT NULL,
    descripcion VARCHAR(255),
    cantidad_stock INT NOT NULL DEFAULT 0,
    precio_venta DECIMAL(15, 2) NOT NULL DEFAULT 0.00,
    precio_proveedor DECIMAL(15, 2) NOT NULL DEFAULT 0.00
);


CREATE TABLE Presupuesto(
    id_presupuesto INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    codigo INT UNIQUE NOT NULL,
    fecha DATE NOT NULL,
    estado ENUM('Pendiente', 'Aprobado', 'Rechazado') NOT NULL DEFAULT 'Pendiente',
    total_materiales DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    total DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    CONSTRAINT fk_presupuesto_cliente
    FOREIGN KEY (id_cliente) REFERENCES Cliente(id_usuario)
    ON UPDATE CASCADE ON DELETE RESTRICT
);

-- Tabla 6: Detalle de Presupuesto (Relación N a N entre Presupuesto y Producto con atributo propio)
CREATE TABLE Presupuesto_Detalle(
    id_presupuesto INT,
    id_producto INT,
    cantidad INT NOT NULL DEFAULT 1, -- Atributo propio de la relación exigido en los apuntes
    precio_aplicado DECIMAL(15, 2) NOT NULL,
    PRIMARY KEY (id_presupuesto, id_producto),
    CONSTRAINT fk_detalle_presupuesto
    FOREIGN KEY (id_presupuesto) REFERENCES Presupuesto(id_presupuesto)
    ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_detalle_producto
    FOREIGN KEY (id_producto) REFERENCES Producto(id_producto)
    ON UPDATE CASCADE ON DELETE RESTRICT
);

-- Inserción de Tipos de Tarea
INSERT INTO Tipos_Tarea (nombre, precio_hora) VALUES 
('Instalación básica', 35.50),
('Reparación de estructuras', 50.00),
('Consultoría técnica', 60.00);

-- Inserción de Usuarios
INSERT INTO Usuario (nombre, contraseña, rol) VALUES 
('admin', '1234', 'Administrador'),
('carlos_cli', 'pass123', 'Cliente'),
('laura_cli', 'pass456', 'Cliente');

-- Inserción de Clientes (Vinculados al ID de Usuario 2 y 3)
INSERT INTO Cliente (id_usuario, nombre, NIF, email, telefono, direccion) VALUES 
(2, 'Carlos Pérez', '12345678A', 'carlos@email.com', 600112233, 'Calle Mayor 10, Madrid'),
(3, 'Laura Gómez', '87654321B', 'laura@email.com', 611223344, 'Avenida de la Paz 22, Barcelona');

-- Inserción de Productos
INSERT INTO Producto (nombre, tipo_material, dimensiones, proveedor, descripcion, cantidad_stock, precio_venta, precio_proveedor) VALUES 
('Viga de Acero HEB', 'Metal', '3m x 20cm', 'Metalurgia S.A.', 'Viga estructural de alta resistencia', 50, 120.00, 85.00),
('Saco Cemento Rápido', 'Cemento', '25kg', 'Cementos del Norte', 'Cemento de fraguado rápido', 200, 7.50, 4.20),
('Bloque Hormigón', 'Hormigon', '40x20x20', 'Áridos y Más', 'Bloque macizo para muros', 500, 2.10, 1.15),
('Tubo PVC Reforzado', 'plastico', '6m x 100mm', 'Plásticos Técnicos', 'Tubería para canalización', 150, 15.00, 8.00);

-- Inserción de Presupuestos
INSERT INTO Presupuesto (id_cliente, codigo, fecha, estado, total_materiales, total) VALUES 
(2, 1001, '2026-03-01', 'Aprobado', 350.00, 450.00),
(3, 1002, '2026-03-05', 'Pendiente', 105.00, 150.00);

-- Inserción en la tabla intermedia (Presupuesto_Detalle)
INSERT INTO Presupuesto_Detalle (id_presupuesto, id_producto, cantidad, precio_aplicado) VALUES 
(1, 1, 2, 120.00), -- 2 vigas de acero en el presupuesto 1
(1, 2, 15, 7.50), -- 15 sacos de cemento en el presupuesto 1
(2, 3, 30, 2.10); -- 30 bloques de hormigón en el presupuesto 2