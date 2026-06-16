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