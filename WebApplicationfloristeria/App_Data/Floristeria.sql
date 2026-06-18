
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
INSERT INTO Rol VALUES ('Administrador'),('Vendedor');
INSERT INTO EstadoPedido VALUES ('Pendiente'),('En Ruta'),('Entregado');
INSERT INTO MetodoPago VALUES ('Yape'),('Plin'),('Efectivo'),('Transferencia'),('Tarjeta');

INSERT INTO Usuario(Nombres,Apellidos,Correo,clave,IdRol,Estado)
VALUES('Admin','Sistema','admin@floreria.com','123456',1,1); 

INSERT INTO Categoria(NombreCategoria,Descripcion,Estado)
VALUES
('Ramos','Ramos de flores',1),
('Cajas','Cajas florales',1),
('Arreglos','Arreglos especiales',1);
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
