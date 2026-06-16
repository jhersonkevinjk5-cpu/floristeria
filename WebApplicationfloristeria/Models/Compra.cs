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
    public class Compra
    {
        [Key]
        [DisplayName("ID Compra")]
        public int IdCompra { get; set; }

        [Required(ErrorMessage = "La fecha de compra es obligatoria")]
        [DisplayName("Fecha de Compra")]
        public DateTime FechaCompra { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un proveedor")]
        [DisplayName("Proveedor")]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "El total es obligatorio")]
        [Range(0.01, double.MaxValue,
            ErrorMessage = "El total debe ser mayor que cero")]
        public decimal Total { get; set; }

        public virtual Proveedor Proveedor { get; set; }
        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; }

        public Compra()
        {
            DetalleCompras = new List<DetalleCompra>();
        }

        public Compra(int idCompra, DateTime fechaCompra,
                     int idProveedor, decimal total)
        {
            IdCompra = idCompra;
            FechaCompra = fechaCompra;
            IdProveedor = idProveedor;
            Total = total;
            DetalleCompras = new List<DetalleCompra>();
        }

        public override string ToString()
        {
            return $"Compra N° {IdCompra} - {FechaCompra:dd/MM/yyyy}";
        }
    }
}

