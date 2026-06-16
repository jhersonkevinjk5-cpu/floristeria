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
    public class EstadoPedido
    {
        [Key]
        [DisplayName("ID Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "El nombre del estado es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [DisplayName("Estado del Pedido")]
        public string NombreEstado { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }

        public EstadoPedido()
        {
            Pedidos = new List<Pedido>();
        }

        public EstadoPedido(int idEstado, string nombreEstado)
        {
            IdEstado = idEstado;
            NombreEstado = nombreEstado;
            Pedidos = new List<Pedido>();
        }

        public override string ToString()
        {
            return NombreEstado;
        }
    }
}
