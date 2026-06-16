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

    public class UsuarioDAO : IUsuario
    {
        private string CadenaCnx;
        public UsuarioDAO()

        {
            CadenaCnx = ConfigurationManager.ConnectionStrings["FloristeriaConnection"].ConnectionString;
        }
        public bool Actualizar(int id)
        {
            throw new NotImplementedException();
        }

        public bool Actualizar(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> ListarTodo()
        {
            throw new NotImplementedException();
        }

        public int Registrar(Usuario entidad)
        {
            throw new NotImplementedException();
        }

        public Usuario Validar(string nombre, string Clave)
        {
            Usuario usuario = null;
            using (SqlConnection cnx = new SqlConnection(CadenaCnx))
            {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand()) 
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "USP_Validar_Usuario";
                    cmd.Parameters.Add("@Correo",
                        SqlDbType.VarChar, 100).Value = nombre;

                    cmd.Parameters.Add("@Clave",
                        SqlDbType.VarChar, 100).Value = Clave;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (usuario == null)
                            {
                                usuario = new Usuario(
                                    Convert.ToInt32(dr["IdUsuario"]),
                                    dr["Nombres"].ToString(),
                                    dr["Correo"].ToString()
                                 );
                            }
                            usuario.Roles.Add(
                                new Rol(
                                    Convert.ToInt32(dr["IdRol"]),
                                    dr["NombreRol"].ToString())
                                );
                        }
                    }
                }
            }
                return usuario;
        }
    }
}