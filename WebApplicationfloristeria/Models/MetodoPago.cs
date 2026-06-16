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
    public class MetodoPago
    {
        [Key]
        [DisplayName("ID Método de Pago")]
        public int IdMetodoPago { get; set; }

        [Required(ErrorMessage = "El nombre del método de pago es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [DisplayName("Método de Pago")]
        public string NombreMetodoPago { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }

        public MetodoPago()
        {
            Pedidos = new List<Pedido>();
        }

        public MetodoPago(int idMetodoPago, string nombreMetodoPago)
        {
            IdMetodoPago = idMetodoPago;
            NombreMetodoPago = nombreMetodoPago;
            Pedidos = new List<Pedido>();
        }

        public override string ToString()
        {
            return NombreMetodoPago;
        }
    }
}
