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
    public class Usuario
    {
        [Key]
        [DisplayName("ID Usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombres")]
        public string Nombres { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Apellidos")]
        public string Apellidos { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [DisplayName("Correo")]
        public string Correo { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Clave")]
        public string Clave { get; set; }

        [DisplayName("Rol")]
        public int IdRol { get; set; }

        [DisplayName("Estado")]
        public bool Estado { get; set; }

        public virtual List<Rol> Roles { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }

      
        public Usuario()
        {
            Roles = new List<Rol>();
            Pedidos = new List<Pedido>();
        }

       
        public Usuario(int id, string nombres, string correo)
        {
            IdUsuario = id;
            Nombres = nombres;
            Apellidos = string.Empty;
            Correo = correo;
            Clave = string.Empty;
            Estado = false;
            Roles = new List<Rol>();
            Pedidos = new List<Pedido>();
        }

        public override string ToString()
        {
            return $"{Nombres} {Apellidos}";
        }
    }
}
