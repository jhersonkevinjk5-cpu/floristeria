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
    public class Rol
    {
        [Key]
        [DisplayName("ID Rol")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50)]
        [DisplayName("Nombre del Rol")]
        public string NombreRol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        // Constructor vacío
        public Rol()
        {
            Usuarios = new List<Usuario>();
        }

        // Constructor con parámetros
        public Rol(int id, string nombre)
        {
            IdRol = id;
            NombreRol = nombre;
            Usuarios = new List<Usuario>();
        }

        public override string ToString()
        {
            return NombreRol;
        }
    }
}
