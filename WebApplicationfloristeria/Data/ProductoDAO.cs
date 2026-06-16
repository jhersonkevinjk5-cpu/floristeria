using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using WebApplicationfloristeria.Data.Interfaces;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Data
{
    public class ProductoDAO : IProducto
    {
        private string CadenaCnx;
        public ProductoDAO()

            {
            CadenaCnx = ConfigurationManager.ConnectionStrings["FloristeriaConnection"].ConnectionString;
            }
        public bool Actualizar(Producto p)
        {
            bool Ok = false;

            using (SqlConnection cnx = new SqlConnection(CadenaCnx))
            {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"UPDATE Producto
                                        SET
                                            NombreProducto =@n,
                                            Descripcion = @d,
                                            Precio = @p,
                                            Stock = @s,
                                            Imagen = @img,
                                            IdCategoria = @c_id,
                                            Estado = @sd
                                        WHERE IdProducto = @id";
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = p.IdProducto;
                    cmd.Parameters.Add("@n", System.Data.SqlDbType.VarChar, 150).Value = p.NombreProducto;
                    cmd.Parameters.Add("@d", System.Data.SqlDbType.VarChar, 300).Value = p.Descripcion;
                    SqlParameter parametroPrecio = cmd.Parameters.Add("@p", System.Data.SqlDbType.Decimal);
                    parametroPrecio.Precision = 10;
                    parametroPrecio.Scale = 2;
                    parametroPrecio.Value = p.Precio;
                    cmd.Parameters.Add("@s", System.Data.SqlDbType.Int).Value = p.Stock;
                    cmd.Parameters.Add("@img", System.Data.SqlDbType.VarChar, 250).Value = p.Imagen;
                    cmd.Parameters.Add("@c_id", System.Data.SqlDbType.Int).Value = p.Categoria.IdCategoria;
                    cmd.Parameters.Add("@sd", System.Data.SqlDbType.Bit).Value = p.Estado;
                    Ok = cmd.ExecuteNonQuery() ==1;
                }
            }
            return Ok;
        }

        public (List<Producto>, int) BuscarPorDescripcion(string descripcion, bool Paginado = false, int numPag = 1, int resultadoPorPagina = 5)
        {
            throw new NotImplementedException();
        }

        public Producto BuscarPorId(int id)
        {
            Producto prodBuscado = new Producto(-2, "---------Producto NO Encontrado---------");

            using (SqlConnection cnx = new SqlConnection(CadenaCnx))
            {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT *
                                        FROM vw_ReporteProductos
                                        WHERE IdProducto = @id";
                    cmd.Parameters.Add("@id" , System.Data.SqlDbType.Int).Value=id;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                                prodBuscado = new Producto(
                                Convert.ToInt32(dr["IdProducto"]),
                                dr["NombreProducto"].ToString(),
                                dr["Descripcion"].ToString(),
                                Convert.ToDecimal(dr["Precio"]),
                                Convert.ToInt32(dr["Stock"]),
                                dr["Imagen"].ToString(),
                                Convert.ToBoolean(dr["Estado"]),
                            new Categoria(
                                        Convert.ToInt32(dr["IdCategoria"]),
                                        dr["NombreCategoria"].ToString()
                                        )
                                
                                );

                        }
                    }
                }
            }
            return prodBuscado;
        }

        public bool Eliminar(int id)
        {
        bool Ok = false;

            using (SqlConnection cnx = new SqlConnection(CadenaCnx))
            {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"DELETE 
                                        FROM
                                        Producto
                                        WHERE IdProducto = @id";
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    Ok = cmd.ExecuteNonQuery() == 1;
                }
            }
            return Ok;
        }

        public List<Producto> ListarTodo()
        {
            List<Producto> listaproductos = new List<Producto>();

            using (SqlConnection cnx = new SqlConnection(CadenaCnx)) {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"
                                        SELECT
                                            P.IdProducto,
                                            P.NombreProducto,
                                            P.Descripcion,
                                            P.Precio,
                                            P.Stock,
                                            P.Imagen,
                                            P.IdCategoria,
                                            C.NombreCategoria
                                        FROM Producto P
                                        INNER JOIN Categoria C
                                            ON P.IdCategoria = C.IdCategoria";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Producto productos = new Producto(
                                Convert.ToInt32(dr["IdProducto"]),
                                dr["NombreProducto"].ToString(),
                                dr["Descripcion"].ToString(),
                                Convert.ToDecimal(dr["Precio"]),
                                Convert.ToInt32(dr["Stock"]),
                                dr["Imagen"].ToString(),
                                new Categoria(
                                        Convert.ToInt32(dr["IdCategoria"]),
                                        dr["NombreCategoria"].ToString()
                                        )
                             );
                            listaproductos.Add(productos);
                        }
                    }
                }
            } 
            return listaproductos;
        }

        public int Registrar(Producto p)
        {
            int idGenerado = -1;

            using (SqlConnection cnx = new SqlConnection(CadenaCnx))
            {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_RegistrarProducto";
                    cmd.Parameters.Add("@NombreProducto", System.Data.SqlDbType.VarChar, 150).Value = p.NombreProducto;
                    cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar, 300).Value = p.Descripcion;
                    SqlParameter parametroPrecio = cmd.Parameters.Add("@Precio", System.Data.SqlDbType.Decimal);
                    parametroPrecio.Precision = 10;
                    parametroPrecio.Scale = 2;
                    parametroPrecio.Value = p.Precio;
                    cmd.Parameters.Add("@Stock", System.Data.SqlDbType.Int).Value = p.Stock;
                    cmd.Parameters.Add("@Imagen", System.Data.SqlDbType.VarChar, 250).Value = p.Imagen;
                    cmd.Parameters.Add("@IdCategoria", System.Data.SqlDbType.Int).Value = p.Categoria.IdCategoria;
                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return idGenerado;
        }
    }
}