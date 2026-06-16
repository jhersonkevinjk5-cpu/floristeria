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
    public class DetallePedido
    {
        [Key]
        [DisplayName("ID Detalle")]
        public int IdDetalle { get; set; }

        [Required]
        [DisplayName("Pedido")]
        public int IdPedido { get; set; }

        [Required]
        [DisplayName("Producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero")]
        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio unitario es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
        [DisplayName("Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El importe debe ser mayor a cero")]
        [DisplayName("Importe")]
        public decimal Importe { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }

        public DetallePedido()
        {
        }

        public DetallePedido(int idDetalle, int idPedido, int idProducto,
                            int cantidad, decimal precioUnitario, decimal importe)
        {
            IdDetalle = idDetalle;
            IdPedido = idPedido;
            IdProducto = idProducto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Importe = importe;
        }

        public override string ToString()
        {
            return $"Detalle #{IdDetalle} - Cantidad: {Cantidad}";
        }
    }
}