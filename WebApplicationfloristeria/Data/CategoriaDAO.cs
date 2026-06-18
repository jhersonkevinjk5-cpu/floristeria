using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplicationfloristeria.Data.Interfaces;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Data
{
    public class CategoriaDAO : ICategoria
    {
        private string CadenaCnx;

        public CategoriaDAO()
        {
            CadenaCnx = ConfigurationManager.ConnectionStrings["FloristeriaConnection"].ConnectionString;
        }

        public bool Actualizar(int id)
        {
            return false;
        }

        public bool Actualizar(Categoria entidad)
        {
            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Categoria SET NombreCategoria = @nombre, Descripcion = @descripcion, Estado = @estado WHERE IdCategoria = @id";

            cmd.Parameters.AddWithValue("@nombre", entidad.NombreCategoria);
            cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion);
            cmd.Parameters.AddWithValue("@estado", entidad.Estado);
            cmd.Parameters.AddWithValue("@id", entidad.IdCategoria);

            int filasAfectadas = cmd.ExecuteNonQuery();
            cnx.Close();

            return filasAfectadas > 0;
        }

        public Categoria BuscarPorId(int id)
        {
            Categoria categoria = null;
            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Categoria WHERE IdCategoria = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                categoria = new Categoria();
                categoria.IdCategoria = Convert.ToInt32(dr["IdCategoria"]);
                categoria.NombreCategoria = dr["NombreCategoria"].ToString();
                categoria.Descripcion = dr["Descripcion"].ToString();
                categoria.Estado = Convert.ToBoolean(dr["Estado"]);
            }
            dr.Close();
            cnx.Close();
            return categoria;
        }

        public bool Eliminar(int id)
        {
            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Categoria WHERE IdCategoria = @id";
            cmd.Parameters.AddWithValue("@id", id);

            int filasAfectadas = cmd.ExecuteNonQuery();
            cnx.Close();

            return filasAfectadas > 0;
        }

        public List<Categoria> ListarTodo()
        {
            List<Categoria> categorias = new List<Categoria>();

            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Categoria";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Categoria categoria = new Categoria();
                categoria.IdCategoria = Convert.ToInt32(dr["IdCategoria"]);
                categoria.NombreCategoria = dr["NombreCategoria"].ToString();
                categoria.Descripcion = dr["Descripcion"].ToString();
                categoria.Estado = Convert.ToBoolean(dr["Estado"]);

                categorias.Add(categoria);
            }
            dr.Close();
            cnx.Close();
            return categorias;
        }

        public int Registrar(Categoria entidad)
        {
            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Categoria (NombreCategoria, Descripcion, Estado) VALUES (@nombre, @descripcion, @estado)";

            cmd.Parameters.AddWithValue("@nombre", entidad.NombreCategoria);
            cmd.Parameters.AddWithValue("@descripcion", entidad.Descripcion);
            cmd.Parameters.AddWithValue("@estado", entidad.Estado);

            int filasAfectadas = cmd.ExecuteNonQuery();
            cnx.Close();

            return filasAfectadas;
        }
    }
}