using System;
using System.Collections.Generic;
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
    public class Producto
    {
        [Display(Name = "Código")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del Producto")]
        public string NombreProducto { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Display(Name = "Precio (S/.)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio")]
        [Display(Name = "Stock Disponible")]
        public int Stock { get; set; }

        public string Imagen { get; set; }
        public bool Estado { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }
        public virtual ICollection<MovimientoStock> MovimientoStocks { get; set; }

        public Producto()
        {
            DetallePedidos = new HashSet<DetallePedido>();
            DetalleCompras = new HashSet<DetalleCompra>();
            MovimientoStocks = new HashSet<MovimientoStock>();
        }

        public Producto(int idProducto, string nombreProducto, string descripcion, decimal precio, int stock, string imagen, Categoria categoria)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            Imagen = imagen;
            Categoria = categoria;
        }

        public Producto(int idProducto, string nombreProducto, string descripcion, decimal precio, int stock, string imagen, bool estado, Categoria categoria)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            Imagen = imagen;
            Estado = estado;
            Categoria = categoria;
        }

        public Producto(int idProducto, string descripcion)
        {
            IdProducto = idProducto;
            Descripcion = descripcion;
        }

        public override string ToString()
        {
            return $"{NombreProducto} - Precio: {Precio:C} - Stock: {Stock}";
        }
    }
}
