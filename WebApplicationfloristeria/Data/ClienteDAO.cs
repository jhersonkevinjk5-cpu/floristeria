using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Data.Interfaces
{
    public class ClienteDAO : ICliente
    {
        private string CadenaCnx;
        public ClienteDAO()

        {
            CadenaCnx = ConfigurationManager.ConnectionStrings["FloristeriaConnection"].ConnectionString;
        }
        public bool Actualizar(int id)
        {
            throw new NotImplementedException();
        }

        public bool Actualizar(Cliente entidad)
        {
            throw new NotImplementedException();
        }

        public Cliente BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> ListarTodo()
        {
            throw new NotImplementedException();
        }

        public int Registrar(Cliente entidad)
        {
            throw new NotImplementedException();
        }

        public Cliente Validar(string nombre, string Clave)
        {
            Cliente cliente = null;
            using (SqlConnection cnx = new SqlConnection(CadenaCnx))
            {
                cnx.Open();
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "USP_Validar_Cliente";
                    cmd.Parameters.Add("@nombres",
                        SqlDbType.VarChar, 100).Value = nombre;

                    cmd.Parameters.Add("@correo",
                        SqlDbType.VarChar, 100).Value = Clave;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (cliente == null)
                            {
                                cliente = new Cliente(
                                    Convert.ToInt32(dr["IdUsuario"]),
                                    dr["Nombres"].ToString(),
                                    dr["Correo"].ToString()
                                 );
                            }
                        }
                    }
                }
            }
            return cliente;
        }
    }
}