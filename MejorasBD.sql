USE BDatos_inicio;

CREATE TABLE tipo_opcion (
    id_tipo_opcion INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE opcion (
    id_opcion INT AUTO_INCREMENT PRIMARY KEY,
    id_tipo_opcion INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    precio DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    FOREIGN KEY (id_tipo_opcion) REFERENCES tipo_opcion(id_tipo_opcion)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    UNIQUE (id_tipo_opcion, nombre)
);

CREATE TABLE rejilla (
    id_rejilla INT AUTO_INCREMENT PRIMARY KEY,
    largo_cm INT NOT NULL,
    ancho_cm INT NOT NULL,
    medida_a DECIMAL(5, 2),
    medida_b DECIMAL(5, 2),
    precio DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    UNIQUE (largo_cm, ancho_cm)
);

CREATE TABLE presupuesto_nave (
    id_presupuesto_nave INT AUTO_INCREMENT PRIMARY KEY,
    id_presupuesto INT NOT NULL UNIQUE,
    ancho_nave DECIMAL(6, 2) NOT NULL,
    largo_nave DECIMAL(6, 2) NOT NULL,
    num_pasillos INT NOT NULL DEFAULT 1,
    ancho_pasillo DECIMAL(5, 2) NOT NULL,
    num_filas INT NOT NULL,
    cuadras_por_fila INT NULL,
    m2_por_cerdo DECIMAL(3, 2) NULL,
    cerdos_por_cuadra INT NULL,
    num_cuadras INT NULL,
    CHECK (m2_por_cerdo IS NULL OR m2_por_cerdo IN (0.65, 0.68, 0.74)),
    FOREIGN KEY (id_presupuesto) REFERENCES presupuesto(id_presupuesto)
        ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE presupuesto_opcion (
    id_presupuesto_opcion INT AUTO_INCREMENT PRIMARY KEY,
    id_presupuesto INT NOT NULL,
    id_tipo_opcion INT NOT NULL,
    id_opcion INT NOT NULL,
    precio DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    FOREIGN KEY (id_presupuesto) REFERENCES presupuesto(id_presupuesto)
        ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (id_tipo_opcion) REFERENCES tipo_opcion(id_tipo_opcion)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    FOREIGN KEY (id_opcion) REFERENCES opcion(id_opcion)
        ON UPDATE CASCADE ON DELETE RESTRICT,
    UNIQUE (id_presupuesto, id_tipo_opcion)
);

CREATE TABLE presupuesto_rejilla (
    id_presupuesto_rejilla INT AUTO_INCREMENT PRIMARY KEY,
    id_presupuesto INT NOT NULL,
    id_rejilla INT NULL,
    largo_cm INT NOT NULL,
    ancho_cm INT NOT NULL,
    cantidad INT NOT NULL DEFAULT 1,
    precio_unitario DECIMAL(10, 2) NOT NULL DEFAULT 0.00,
    total DECIMAL(15, 2) NOT NULL DEFAULT 0.00,
    FOREIGN KEY (id_presupuesto) REFERENCES presupuesto(id_presupuesto)
        ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (id_rejilla) REFERENCES rejilla(id_rejilla)
        ON UPDATE CASCADE ON DELETE RESTRICT
);

INSERT INTO tipo_opcion (nombre) VALUES
('Altura de cuadras'),
('Puerta'),
('Separaciones'),
('Herrajes'),
('Tolva');

INSERT INTO opcion (id_tipo_opcion, nombre) VALUES
(1, '90 cm'),
(1, '1 metro'),
(2, 'Hasta el suelo'),
(2, 'Con marco'),
(3, 'Lisas'),
(3, '1 ventana'),
(3, '2 ventanas'),
(4, 'Galvanizado'),
(4, 'Inox'),
(5, 'Plástico'),
(5, 'Hormigón');

INSERT INTO rejilla (largo_cm, ancho_cm, medida_a, medida_b) VALUES
(200, 40, 7.5, 1.8),
(180, 40, 7.5, 1.8),
(150, 50, 7.5, 1.8),
(120, 50, 7.5, 1.8),
(100, 50, 7.5, 1.8);