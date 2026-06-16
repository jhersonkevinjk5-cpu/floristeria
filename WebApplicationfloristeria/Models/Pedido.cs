using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WebApplicationfloristeria.Models
{
    //-------------------------------------------------------------
    // Implementar constructores
    // implementar public override string tOstring(){}
    // Implentar Los componentModel
    // Implementar los DataAnnotations
    // implementar restrinciones deacuerdo sea el caso
    //-------------------------------------------------------------
    public class Pedido
    {
        [Key]
        [DisplayName("ID Pedido")]
        public int IdPedido { get; set; }

        [Required]
        [DisplayName("Fecha del Pedido")]
        public DateTime FechaPedido { get; set; }

        [Required]
        [DisplayName("Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [DisplayName("Usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [DisplayName("Método de Pago")]
        public int IdMetodoPago { get; set; }

        [Required]
        [DisplayName("Fecha de Entrega")]
        public DateTime FechaEntrega { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Dirección de Entrega")]
        public string DireccionEntrega { get; set; }

        [StringLength(300)]
        [DisplayName("Dedicatoria")]
        public string Dedicatoria { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Sub Total")]
        public decimal SubTotal { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("IGV")]
        public decimal IGV { get; set; }

        [Range(0, double.MaxValue)]
        [DisplayName("Total")]
        public decimal Total { get; set; }

        [Required]
        [DisplayName("Estado")]
        public int IdEstado { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual EstadoPedido EstadoPedido { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }

        public Pedido()
        {
            DetallePedidos = new List<DetallePedido>();
        }

        public Pedido(int idPedido, DateTime fechaPedido, int idCliente,
            int idUsuario, int idMetodoPago, DateTime fechaEntrega,
            string direccionEntrega, string dedicatoria,
            decimal subTotal, decimal igv, decimal total, int idEstado)
        {
            IdPedido = idPedido;
            FechaPedido = fechaPedido;
            IdCliente = idCliente;
            IdUsuario = idUsuario;
            IdMetodoPago = idMetodoPago;
            FechaEntrega = fechaEntrega;
            DireccionEntrega = direccionEntrega;
            Dedicatoria = dedicatoria;
            SubTotal = subTotal;
            IGV = igv;
            Total = total;
            IdEstado = idEstado;

            DetallePedidos = new List<DetallePedido>();
        }

        public override string ToString()
        {
            return $"Pedido N° {IdPedido} - Total: {Total}";
        }
    }
}
