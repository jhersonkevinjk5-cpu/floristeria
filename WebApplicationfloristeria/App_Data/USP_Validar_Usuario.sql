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