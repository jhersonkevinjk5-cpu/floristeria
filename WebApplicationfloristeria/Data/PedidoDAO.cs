using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Data.Interfaces
{
    public class PedidoDAO : IPedido
    {
        private string CadenaCnx;

        public PedidoDAO()
        {
            CadenaCnx = ConfigurationManager.ConnectionStrings["FloristeriaConnection"].ConnectionString;
        }

        public bool Actualizar(Pedido entidad)
        {
            throw new NotImplementedException();
        }

        public Pedido BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pedido> ListarTodo()
        {
            List<Pedido> pedidos = new List<Pedido>();
            using (SqlConnection cnx = new SqlConnection(CadenaCnx))
            {
                cnx.Open();
                string query = @"SELECT p.*, c.Nombres, c.Apellidos 
                                 FROM Pedido p 
                                 INNER JOIN Cliente c ON p.IdCliente = c.IdCliente";

                using (SqlCommand cmd = new SqlCommand(query, cnx))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Pedido pedido = new Pedido(
                                Convert.ToInt32(dr["IdPedido"]),
                                Convert.ToDateTime(dr["FechaPedido"]),
                                Convert.ToInt32(dr["IdCliente"]),
                                Convert.ToInt32(dr["IdUsuario"]),
                                Convert.ToInt32(dr["IdMetodoPago"]),
                                Convert.ToDateTime(dr["FechaEntrega"]),
                                dr["DireccionEntrega"].ToString(),
                                dr["Dedicatoria"].ToString(),
                                Convert.ToDecimal(dr["SubTotal"]),
                                Convert.ToDecimal(dr["IGV"]),
                                Convert.ToDecimal(dr["Total"]),
                                Convert.ToInt32(dr["IdEstado"])
                            );
                            pedido.Cliente = new Cliente
                            {
                                IdCliente = pedido.IdCliente,
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString()
                            };

                            pedidos.Add(pedido);
                        }
                    }
                }
            }
            return pedidos;
        }

        public int Registrar(Pedido entidad)
        {
            throw new NotImplementedException();
        }

        public bool RegistrarPedidoCompleto(Pedido pedido)
        {
            using (SqlConnection cnx = new SqlConnection(CadenaCnx))
            {
                cnx.Open();
                SqlTransaction transaccion = cnx.BeginTransaction();

                try
                {
                    string queryMaestro = @"INSERT INTO Pedido (FechaPedido, IdCliente, IdUsuario, IdMetodoPago, FechaEntrega, DireccionEntrega, Dedicatoria, SubTotal, IGV, Total, IdEstado) 
                                            VALUES (@fechaPedido, @idCliente, @idUsuario, @idMetodoPago, @fechaEntrega, @direccionEntrega, @dedicatoria, @subTotal, @igv, @total, @idEstado);
                                            SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdM = new SqlCommand(queryMaestro, cnx, transaccion);
                    cmdM.Parameters.AddWithValue("@fechaPedido", pedido.FechaPedido);
                    cmdM.Parameters.AddWithValue("@idCliente", pedido.IdCliente);
                    cmdM.Parameters.AddWithValue("@idUsuario", pedido.IdUsuario);
                    cmdM.Parameters.AddWithValue("@idMetodoPago", pedido.IdMetodoPago);
                    cmdM.Parameters.AddWithValue("@fechaEntrega", pedido.FechaEntrega);
                    cmdM.Parameters.AddWithValue("@direccionEntrega", pedido.DireccionEntrega);
                    cmdM.Parameters.AddWithValue("@dedicatoria", pedido.Dedicatoria ?? (object)DBNull.Value);
                    cmdM.Parameters.AddWithValue("@subTotal", pedido.SubTotal);
                    cmdM.Parameters.AddWithValue("@igv", pedido.IGV);
                    cmdM.Parameters.AddWithValue("@total", pedido.Total);
                    cmdM.Parameters.AddWithValue("@idEstado", pedido.IdEstado);

                    int idPedidoGenerado = Convert.ToInt32(cmdM.ExecuteScalar());

                    string queryDetalle = @"INSERT INTO DetallePedido (IdPedido, IdProducto, Cantidad, PrecioUnitario, Importe) 
                                            VALUES (@idPedido, @idProducto, @cantidad, @precioUnitario, @importe)";

                    foreach (var detalle in pedido.DetallePedidos)
                    {
                        SqlCommand cmdD = new SqlCommand(queryDetalle, cnx, transaccion);
                        cmdD.Parameters.AddWithValue("@idPedido", idPedidoGenerado);
                        cmdD.Parameters.AddWithValue("@idProducto", detalle.IdProducto);
                        cmdD.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                        cmdD.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);
                        cmdD.Parameters.AddWithValue("@importe", detalle.Importe);

                        cmdD.ExecuteNonQuery();
                    }

                    transaccion.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    return false;
                }
            }
        }
    }
}