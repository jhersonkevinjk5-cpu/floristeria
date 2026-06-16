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
    public class DetalleCompra
    {
        [Key]
        [DisplayName("ID Detalle Compra")]
        public int IdDetalleCompra { get; set; }

        [Required]
        public int IdCompra { get; set; }

        [Required]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue,
            ErrorMessage = "La cantidad debe ser mayor a cero")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio de compra es obligatorio")]
        [Range(0.01, double.MaxValue,
            ErrorMessage = "El precio debe ser mayor a cero")]
        public decimal PrecioCompra { get; set; }

        [Required]
        [Range(0.01, double.MaxValue,
            ErrorMessage = "El importe debe ser mayor a cero")]
        public decimal Importe { get; set; }

        public virtual Compra Compra { get; set; }
        public virtual Producto Producto { get; set; }


        public DetalleCompra()
        {
        }

        public DetalleCompra(int idDetalleCompra, int idCompra,
                            int idProducto, int cantidad,
                            decimal precioCompra, decimal importe)
        {
            IdDetalleCompra = idDetalleCompra;
            IdCompra = idCompra;
            IdProducto = idProducto;
            Cantidad = cantidad;
            PrecioCompra = precioCompra;
            Importe = importe;
        }

        public override string ToString()
        {
            return $"{Producto?.NombreProducto} x {Cantidad} = {Importe}";
        }
    }
}
