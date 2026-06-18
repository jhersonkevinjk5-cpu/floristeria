
/*==========================================================
 SISTEMA DE FLORERIA MVC - SQL SERVER
==========================================================*/

CREATE DATABASE BD_Floreria;
GO

USE BD_Floreria;
GO

/* TABLAS */
CREATE TABLE Rol(
 IdRol INT IDENTITY(1,1) PRIMARY KEY,
 NombreRol VARCHAR(30) NOT NULL
);
GO

CREATE TABLE Usuario(
 IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
 Nombres VARCHAR(100) NOT NULL,
 Apellidos VARCHAR(100) NOT NULL,
 Correo VARCHAR(100) UNIQUE NOT NULL,
 Clave VARCHAR(100) NOT NULL,
 IdRol INT NOT NULL,
 Estado BIT NOT NULL DEFAULT 1,
 FOREIGN KEY (IdRol) REFERENCES Rol(IdRol)
);
GO

CREATE TABLE Cliente(
 IdCliente INT IDENTITY(1,1) PRIMARY KEY,
 Nombres VARCHAR(100) NOT NULL,
 Apellidos VARCHAR(100) NOT NULL,
 Telefono VARCHAR(20),
 Correo VARCHAR(100),
 Direccion VARCHAR(250),
 FechaRegistro DATETIME DEFAULT GETDATE()
); 
GO

CREATE TABLE Categoria(
 IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
 NombreCategoria VARCHAR(100) NOT NULL,
 Descripcion VARCHAR(250),
 Estado BIT DEFAULT 1
);
GO

CREATE TABLE Producto( 
 IdProducto INT IDENTITY(1,1) PRIMARY KEY,
 NombreProducto VARCHAR(150) NOT NULL,
 Descripcion VARCHAR(300),
 Precio DECIMAL(10,2) NOT NULL,
 Stock INT NOT NULL DEFAULT 0,
 Imagen VARCHAR(250),
 IdCategoria INT NOT NULL,
 Estado BIT DEFAULT 1,
 FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria)
);
GO

CREATE TABLE Proveedor(
 IdProveedor INT IDENTITY(1,1) PRIMARY KEY,
 RazonSocial VARCHAR(150) NOT NULL,
 Telefono VARCHAR(20),
 Correo VARCHAR(100),
 Direccion VARCHAR(200),
 Estado BIT DEFAULT 1
);
GO

CREATE TABLE Compra(
 IdCompra INT IDENTITY(1,1) PRIMARY KEY,
 FechaCompra DATETIME DEFAULT GETDATE(),
 IdProveedor INT NOT NULL,
 Total DECIMAL(10,2),
 FOREIGN KEY (IdProveedor) REFERENCES Proveedor(IdProveedor)
);
GO

CREATE TABLE DetalleCompra(
 IdDetalleCompra INT IDENTITY(1,1) PRIMARY KEY,
 IdCompra INT NOT NULL,
 IdProducto INT NOT NULL,
 Cantidad INT NOT NULL,
 PrecioCompra DECIMAL(10,2) NOT NULL,
 Importe DECIMAL(10,2) NOT NULL,
 FOREIGN KEY (IdCompra) REFERENCES Compra(IdCompra),
 FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);
GO

CREATE TABLE MetodoPago(
 IdMetodoPago INT IDENTITY(1,1) PRIMARY KEY,          
 NombreMetodoPago VARCHAR(50) NOT NULL
);
GO

CREATE TABLE EstadoPedido(
 IdEstado INT IDENTITY(1,1) PRIMARY KEY,
 NombreEstado VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Pedido(
 IdPedido INT IDENTITY(1,1) PRIMARY KEY,
 FechaPedido DATETIME DEFAULT GETDATE(),
 IdCliente INT NOT NULL,
 IdUsuario INT NOT NULL,
 IdMetodoPago INT NOT NULL,
 FechaEntrega DATE NOT NULL,
 DireccionEntrega VARCHAR(300) NOT NULL,
 Dedicatoria VARCHAR(500),
 SubTotal DECIMAL(10,2) NOT NULL,
 IGV DECIMAL(10,2) NOT NULL,
 Total DECIMAL(10,2) NOT NULL,
 IdEstado INT NOT NULL,
 FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
 FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
 FOREIGN KEY (IdMetodoPago) REFERENCES MetodoPago(IdMetodoPago),
 FOREIGN KEY (IdEstado) REFERENCES EstadoPedido(IdEstado)
);
GO

CREATE TABLE DetallePedido(
 IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
 IdPedido INT NOT NULL,
 IdProducto INT NOT NULL,
 Cantidad INT NOT NULL,
 PrecioUnitario DECIMAL(10,2) NOT NULL,
 Importe DECIMAL(10,2) NOT NULL,
 FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido),
 FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);
GO

CREATE TABLE MovimientoStock(
 IdMovimiento INT IDENTITY(1,1) PRIMARY KEY,
 IdProducto INT NOT NULL,
 TipoMovimiento VARCHAR(20) NOT NULL,
 Cantidad INT NOT NULL,
 FechaMovimiento DATETIME DEFAULT GETDATE(),
 Observacion VARCHAR(250),
 FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);
GO

/******** DATOS INICIALES ********/
-- ROLES
INSERT INTO Rol VALUES 
('Administrador'),
('Vendedor');
GO

-- ESTADOS PEDIDO
INSERT INTO EstadoPedido VALUES 
('Pendiente'),
('En Ruta'),
('Entregado');
GO

-- MÉTODOS DE PAGO
INSERT INTO MetodoPago VALUES 
('Yape'),
('Plin'),
('Efectivo'),
('Transferencia'),
('Tarjeta');
GO
-- USUARIO ADMIN
INSERT INTO Usuario(Nombres,Apellidos,Correo,Clave,IdRol,Estado)
VALUES
('NombAdmin','Admins','admin@floreria.com','12345',1,1);
GO
-- CLIENTE EJEMPLO
INSERT INTO Cliente(Nombres,Apellidos,Telefono,Correo,Direccion)
VALUES
('Geraldine','Client','999999999','client@floreria.com','Av. Primavera 452');
GO

-- CATEGORÍAS
INSERT INTO Categoria(NombreCategoria,Descripcion,Estado)
VALUES
('Ramos','Ramos de flores frescas',1),
('Cajas Florales','Cajas decorativas con flores',1),
('Arreglos Premium','Arreglos exclusivos para eventos',1),
('Bouquets','Bouquets elegantes y personalizados',1),
('Peluches y Regalos','Complementos románticos',1),
('Flores Naturales','Flores frescas individuales',1),
('Eventos','Decoraciones para bodas y fiestas',1),
('Plantas Ornamentales','Plantas decorativas para hogar',1);
GO
-- PRODUCTOS (20)
INSERT INTO Producto(NombreProducto,Descripcion,Precio,Stock,Imagen,IdCategoria,Estado)
VALUES

-- Ramos (1)
('Ramo de Rosas Rojas',
'12 rosas rojas premium envueltas',
85.00,20,'~/Contenido/Multimedia/Productos/ramo_rosas.jpg',1,1),

('Ramo Primavera',
'Mix de flores coloridas frescas',
65.00,15,'~/Contenido/Multimedia/Productos/primavera.jpg',1,1),

('Ramo Elegancia Blanca',
'Rosas blancas y lirios',
95.00,12,'~/Contenido/Multimedia/Productos/blanco.jpg',1,1),

-- Cajas Florales (2)
('Caja Floral Amor',
'Caja decorativa con rosas rosadas',
120.00,10,'~/Contenido/Multimedia/Productos/caja_amor.jpg',2,1),

('Caja Deluxe Rosé',
'Caja premium con flores importadas',
180.00,8,'~/Contenido/Multimedia/Productos/deluxe.jpg',2,1),

-- Arreglos Premium (3)
('Arreglo Luxury Gold',
'Arreglo floral premium elegante',
250.00,5,'~/Contenido/Multimedia/Productos/gold.jpg',3,1),

('Arreglo Cumpleaños Premium',
'Flores + decoración especial',
210.00,7,'~/Contenido/Multimedia/Productos/cumple.jpg',3,1),

('Arreglo Ejecutivo',
'Diseño minimalista premium',
230.00,6,'~/Contenido/Multimedia/Productos/ejecutivo.jpg',3,1),

-- Bouquets (4)
('Bouquet Romantic',
'Bouquet romántico color pastel',
70.00,14,'~/Contenido/Multimedia/Productos/romantic.jpg',4,1),

('Bouquet Sunset',
'Tonos naranjas y amarillos',
78.00,11,'~/Contenido/Multimedia/Productos/sunset.jpg',4,1),

('Bouquet Elegante',
'Flores premium para regalo',
88.00,10,'~/Contenido/Multimedia/Productos/bouquet_elegante.jpg',4,1),

-- Peluches (5)
('Oso Teddy Grande',
'Peluche acompañante para regalo',
55.00,18,'~/Contenido/Multimedia/Productos/oso.jpg',5,1),

('Caja Chocolates Ferrero',
'Chocolate premium acompañante',
35.00,25,'~/Contenido/Multimedia/Productos/ferrero.jpg',5,1),

('Combo Romance',
'Peluche + flores + tarjeta',
145.00,10,'~/Contenido/Multimedia/Productos/combo.jpg',5,1),

-- Flores Naturales (6)
('Rosas Blancas x Unidad',
'Rosa blanca natural fresca',
8.00,50,'~/Contenido/Multimedia/Productos/rosa_blanca.jpg',6,1),

('Girasol Natural',
'Girasol fresco premium',
12.00,40,'~/Contenido/Multimedia/Productos/girasol.jpg',6,1),

('Tulipán Holandés',
'Tulipán importado',
18.00,25,'~/Contenido/Multimedia/Productos/tulipan.jpg',6,1),

-- Eventos (7)
('Decoración Boda Floral',
'Decoración floral completa',
550.00,3,'~/Contenido/Multimedia/Productos/boda.jpg',7,1),

('Decoración Fiesta XV',
'Decoración floral elegante',
420.00,4,'~/Contenido/Multimedia/Productos/xv.jpg',7,1),

-- Plantas (8)
('Orquídea Premium',
'Orquídea decorativa natural',
95.00,12,'~/Contenido/Multimedia/Productos/orquidea.jpg',8,1),

('Mini Bonsai Zen',
'Planta decorativa premium',
110.00,9,'~/Contenido/Multimedia/Productos/bonsai.jpg',8,1);
GO

/******** TRIGGERS ********/
CREATE TRIGGER TR_DescontarStock
ON DetallePedido
AFTER INSERT
AS
BEGIN
 UPDATE P
 SET Stock = Stock - I.Cantidad
 FROM Producto P
 INNER JOIN inserted I ON P.IdProducto=I.IdProducto;
END;
GO

CREATE TRIGGER TR_AumentarStock
ON DetalleCompra
AFTER INSERT
AS
BEGIN
 UPDATE P
 SET Stock = Stock + I.Cantidad
 FROM Producto P
 INNER JOIN inserted I ON P.IdProducto=I.IdProducto;
END;
GO

/******** VISTA ********/
CREATE VIEW vw_ReporteVentas
AS
SELECT
P.IdPedido,
P.FechaPedido,
C.Nombres + ' ' + C.Apellidos AS Cliente,
U.Nombres + ' ' + U.Apellidos AS Usuario,
MP.NombreMetodoPago,
EP.NombreEstado,
P.Total
FROM Pedido P
INNER JOIN Cliente C ON P.IdCliente=C.IdCliente
INNER JOIN Usuario U ON P.IdUsuario=U.IdUsuario
INNER JOIN MetodoPago MP ON P.IdMetodoPago=MP.IdMetodoPago
INNER JOIN EstadoPedido EP ON P.IdEstado=EP.IdEstado;
GO

/*************************************************
RESTRICCIONES DE VALIDACIÓN (CHECK)
Evitan datos incorrectos
**************************************************/

ALTER TABLE Producto
ADD CONSTRAINT CK_Producto_Precio
CHECK (Precio > 0);
GO

ALTER TABLE Producto
ADD CONSTRAINT CK_Producto_Stock
CHECK (Stock >= 0);
GO

ALTER TABLE DetallePedido
ADD CONSTRAINT CK_DetallePedido_Cantidad
CHECK (Cantidad > 0);
GO

ALTER TABLE DetalleCompra
ADD CONSTRAINT CK_DetalleCompra_Cantidad
CHECK (Cantidad > 0);
GO


/*************************************************
RESTRICCIONES UNIQUE
**************************************************/

ALTER TABLE Cliente
ADD CONSTRAINT UQ_Cliente_Correo
UNIQUE(Correo);
GO

ALTER TABLE MetodoPago
ADD CONSTRAINT UQ_MetodoPago
UNIQUE(NombreMetodoPago);
GO
/*************************************************
INDICES
**************************************************/

CREATE INDEX IX_Cliente_Correo
ON Cliente(Correo);
GO

CREATE INDEX IX_Producto_Nombre
ON Producto(NombreProducto);
GO

CREATE INDEX IX_Pedido_Fecha
ON Pedido(FechaPedido);
GO



CREATE VIEW vw_ReporteProductos
AS
SELECT
P.IdProducto,
P.NombreProducto,
p.Descripcion,
P.Precio,
P.Stock,
p.Imagen,
p.IdCategoria,
C.NombreCategoria,
P.Estado
FROM Producto P
INNER JOIN Categoria C
ON P.IdCategoria = C.IdCategoria;
GO

CREATE VIEW vw_ReporteClientes
AS
SELECT
IdCliente,
Nombres,
Apellidos,
Telefono,
Correo,
Direccion,
FechaRegistro
FROM Cliente;
GO


/*************************************************
SP ACTUALIZAR PRODUCTO
**************************************************/
CREATE PROC sp_ActualizarProducto

@IdProducto INT,
@NombreProducto VARCHAR(150),
@Descripcion VARCHAR(300),
@Precio DECIMAL(10,2),
@Stock INT,
@Imagen VARCHAR(250),
@IdCategoria INT

AS
BEGIN

UPDATE Producto
SET
NombreProducto=@NombreProducto,
Descripcion=@Descripcion,
Precio=@Precio,
Stock=@Stock,
Imagen=@Imagen,
IdCategoria=@IdCategoria
WHERE IdProducto=@IdProducto

END
GO

/*************************************************
SP ELIMINAR PRODUCTO
**************************************************/
CREATE PROC sp_EliminarProducto

@IdProducto INT

AS
BEGIN

DELETE FROM Producto
WHERE IdProducto=@IdProducto

END
GO

/*************************************************
SP BUSCAR PRODUCTO
**************************************************/
CREATE PROC sp_BuscarProducto

@IdProducto INT

AS
BEGIN

SELECT *
FROM Producto
WHERE IdProducto=@IdProducto

END
GO



/*************************************************
SP ACTUALIZAR CLIENTE
**************************************************/
CREATE PROC sp_ActualizarCliente

@IdCliente INT,
@Nombres VARCHAR(100),
@Apellidos VARCHAR(100),
@Telefono VARCHAR(20),
@Correo VARCHAR(100),
@Direccion VARCHAR(250)

AS
BEGIN

UPDATE Cliente
SET
Nombres=@Nombres,
Apellidos=@Apellidos,
Telefono=@Telefono,
Correo=@Correo,
Direccion=@Direccion
WHERE IdCliente=@IdCliente

END
GO

/*************************************************
SP ELIMINAR CLIENTE
**************************************************/
CREATE PROC sp_EliminarCliente

@IdCliente INT

AS
BEGIN

DELETE FROM Cliente
WHERE IdCliente=@IdCliente

END
GO



/*************************************************
SP LISTAR PRODUCTOS
**************************************************/
CREATE PROC sp_ListarProductos
AS
BEGIN

SELECT
P.IdProducto,
P.NombreProducto,
P.Descripcion,
P.Precio,
P.Stock,
C.NombreCategoria
FROM Producto P
INNER JOIN Categoria C
ON P.IdCategoria = C.IdCategoria

END
GO

/*************************************************
SP LISTAR CLIENTES
**************************************************/
CREATE PROC sp_ListarClientes
AS
BEGIN

SELECT *
FROM Cliente

END
GO

/*************************************************
SP REGISTRAR CLIENTE
**************************************************/
CREATE PROC sp_RegistrarCliente

@Nombres VARCHAR(100),
@Apellidos VARCHAR(100),
@Telefono VARCHAR(20),
@Correo VARCHAR(100),
@Direccion VARCHAR(250)

AS
BEGIN

INSERT INTO Cliente
(
Nombres,
Apellidos,
Telefono,
Correo,
Direccion
)
VALUES
(
@Nombres,
@Apellidos,
@Telefono,
@Correo,
@Direccion
)

END
GO

/*************************************************
SP REGISTRAR PRODUCTO
**************************************************/
CREATE PROC sp_RegistrarProducto

@NombreProducto VARCHAR(150),
@Descripcion VARCHAR(300),
@Precio DECIMAL(10,2),
@Stock INT,
@Imagen VARCHAR(250),
@IdCategoria INT

AS
BEGIN

INSERT INTO Producto
(
NombreProducto,
Descripcion,
Precio,
Stock,
Imagen,
IdCategoria
)
VALUES
(
@NombreProducto,
@Descripcion,
@Precio,
@Stock,
@Imagen,
@IdCategoria
)

END
GO

/*************************************************
SP LISTAR VENTAS
**************************************************/
CREATE PROC sp_ListarVentas
AS
BEGIN

SELECT *
FROM vw_ReporteVentas

END
GO
/*************************************************
SP Validar_Usuario
**************************************************/
CREATE PROCEDURE USP_Validar_Usuario
(
    @Correo VARCHAR(100),
    @Clave VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        U.IdUsuario,
        U.Nombres,
        U.Apellidos,
        U.Correo,
        U.IdRol,
        R.NombreRol
    FROM Usuario U
    INNER JOIN Rol R
        ON U.IdRol = R.IdRol
    WHERE U.Correo = @Correo
      AND U.Clave = @Clave
      AND U.Estado = 1;
END
GO
/*************************************************
sp_Validar_Cliente
**************************************************/
CREATE PROCEDURE sp_Validar_Cliente
(
    @Nombres VARCHAR(100),
    @Correo VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdCliente,
        Nombres,
        Apellidos,
        Telefono,
        Correo,
        Direccion,
        FechaRegistro
    FROM Cliente
    WHERE Nombres = @Nombres
      AND Correo = @Correo;
END
GO
/*************************************************
sp_RegistrarPedido
**************************************************/

CREATE PROC sp_RegistrarPedido

@IdCliente INT,
@IdUsuario INT,
@IdMetodoPago INT,
@FechaEntrega DATE,
@DireccionEntrega VARCHAR(300),
@Dedicatoria VARCHAR(500),
@SubTotal DECIMAL(10,2),
@IGV DECIMAL(10,2),
@Total DECIMAL(10,2),
@IdEstado INT

AS
BEGIN

INSERT INTO Pedido
(
IdCliente,
IdUsuario,
IdMetodoPago,
FechaEntrega,
DireccionEntrega,
Dedicatoria,
SubTotal,
IGV,
Total,
IdEstado
)
VALUES
(
@IdCliente,
@IdUsuario,
@IdMetodoPago,
@FechaEntrega,
@DireccionEntrega,
@Dedicatoria,
@SubTotal,
@IGV,
@Total,
@IdEstado
)

SELECT SCOPE_IDENTITY()

END
GO