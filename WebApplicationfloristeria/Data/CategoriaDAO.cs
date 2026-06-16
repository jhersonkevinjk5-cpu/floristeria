using System;
using System.Collections.Generic;
using System.Configuration;
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
            throw new NotImplementedException();
        }

        public bool Actualizar(Categoria entidad)
        {
            throw new NotImplementedException();
        }

        public Categoria BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> ListarTodo()
        {
            List<Categoria> categorias = new List<Categoria>();

            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM Categoria";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Categoria categoria = new Categoria(
                    Convert.ToInt32(dr["IdCategoria"]),
                    dr["NombreCategoria"].ToString()
                    );
                categorias.Add( categoria );
            }
            dr.Close(); cnx.Close();
            return categorias;
        }

        public int Registrar(Categoria entidad)
        {
            throw new NotImplementedException();
        }
    }
}