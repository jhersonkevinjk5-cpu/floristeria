using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplicationfloristeria.Models;
using WebApplicationfloristeria.Data.Interfaces;

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
            return false;
        }

        public bool Actualizar(Cliente entidad)
        {
            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Cliente SET Nombres = @nombres, Apellidos = @apellidos, Telefono = @telefono, Correo = @correo, Direccion = @direccion, FechaRegistro = @fechaRegistro WHERE IdCliente = @id";

            cmd.Parameters.AddWithValue("@nombres", entidad.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", entidad.Apellidos);
            cmd.Parameters.AddWithValue("@telefono", entidad.Telefono);
            cmd.Parameters.AddWithValue("@correo", entidad.Correo);
            cmd.Parameters.AddWithValue("@direccion", entidad.Direccion);
            cmd.Parameters.AddWithValue("@fechaRegistro", entidad.FechaRegistro);
            cmd.Parameters.AddWithValue("@id", entidad.IdCliente);

            int filasAfectadas = cmd.ExecuteNonQuery();
            cnx.Close();

            return filasAfectadas > 0;
        }

        public Cliente BuscarPorId(int id)
        {
            Cliente cliente = null;
            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Cliente WHERE IdCliente = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cliente = new Cliente(
                    Convert.ToInt32(dr["IdCliente"]),
                    dr["Nombres"].ToString(),
                    dr["Apellidos"].ToString(),
                    dr["Telefono"].ToString(),
                    dr["Correo"].ToString(),
                    dr["Direccion"].ToString(),
                    Convert.ToDateTime(dr["FechaRegistro"])
                );
            }
            dr.Close();
            cnx.Close();
            return cliente;
        }

        public bool Eliminar(int id)
        {
            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM Cliente WHERE IdCliente = @id";
            cmd.Parameters.AddWithValue("@id", id);

            int filasAfectadas = cmd.ExecuteNonQuery();
            cnx.Close();

            return filasAfectadas > 0;
        }
        public List<Cliente> ListarTodo()
        {
            List<Cliente> clientes = new List<Cliente>();

            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Cliente";

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cliente cliente = new Cliente(
                    Convert.ToInt32(dr["IdCliente"]),
                    dr["Nombres"].ToString(),
                    dr["Apellidos"].ToString(),
                    dr["Telefono"].ToString(),
                    dr["Correo"].ToString(),
                    dr["Direccion"].ToString(),
                    Convert.ToDateTime(dr["FechaRegistro"])
                );

                clientes.Add(cliente);
            }
            dr.Close();
            cnx.Close();
            return clientes;
        }

        public int Registrar(Cliente entidad)
        {
            SqlConnection cnx = new SqlConnection(CadenaCnx);
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Cliente (Nombres, Apellidos, Telefono, Correo, Direccion, FechaRegistro) VALUES (@nombres, @apellidos, @telefono, @correo, @direccion, @fechaRegistro)";

            cmd.Parameters.AddWithValue("@nombres", entidad.Nombres);
            cmd.Parameters.AddWithValue("@apellidos", entidad.Apellidos);
            cmd.Parameters.AddWithValue("@telefono", entidad.Telefono);
            cmd.Parameters.AddWithValue("@correo", entidad.Correo);
            cmd.Parameters.AddWithValue("@direccion", entidad.Direccion);
            cmd.Parameters.AddWithValue("@fechaRegistro", entidad.FechaRegistro);

            int filasAfectadas = cmd.ExecuteNonQuery();
            cnx.Close();

            return filasAfectadas;
        }


        public Cliente Validar(string correo, string nombre)
        {
            Cliente cliente = null;

            using (SqlConnection cn = new SqlConnection(CadenaCnx))
            {
                SqlCommand cmd = new SqlCommand("sp_Validar_Cliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombres", nombre);
                cmd.Parameters.AddWithValue("@Correo", correo);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    cliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(dr["IdCliente"]),
                        Nombres = dr["Nombres"].ToString(),
                        Apellidos = dr["Apellidos"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"])
                    };
                }
            }

            return cliente;
        }
    }
}