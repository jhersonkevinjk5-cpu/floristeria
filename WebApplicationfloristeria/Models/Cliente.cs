using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using WebApplicationfloristeria.Data.Interfaces;
namespace WebApplicationfloristeria.Models
{
    //-------------------------------------------------------------
    // Implementar constructores
    // implementar public override string tOstring(){}
    // Implentar Los componentModel
    // Implementar los DataAnnotations
    // implementar restrinciones deacuerdo sea el caso
    //-------------------------------------------------------------
    public class Cliente
    {
        [Key]
        [DisplayName("ID Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombres")]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(15)]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [DisplayName("Correo")]
        public string Correo { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [Required]
        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }

        public Cliente()
        {
            Pedidos = new List<Pedido>();
        }

        public Cliente(int idCliente, string nombres, string apellidos,
                       string telefono, string correo,
                       string direccion, DateTime fechaRegistro)
        {
            IdCliente = idCliente;
            Nombres = nombres;
            Apellidos = apellidos;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
            FechaRegistro = fechaRegistro;
            Pedidos = new List<Pedido>();
        }

        public Cliente(int idCliente, string nombres, string correo)
        {
            IdCliente = idCliente;
            Nombres = nombres;
            Correo = correo;
        }

        public override string ToString()
        {
            return $"{Nombres} {Apellidos}";
        }
    }
}
