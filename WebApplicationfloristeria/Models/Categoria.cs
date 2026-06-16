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
    public class Categoria
    {
        [Key]
        [DisplayName("ID Categoría")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
        [DisplayName("Nombre")]
        public string NombreCategoria { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [DisplayName("Estado")]
        public bool Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

        public Categoria()
        {
            Productos = new List<Producto>();
        }

        public Categoria(int idCategoria, string nombreCategoria,
                        string descripcion, bool estado,
                        ICollection<Producto> productos)
        {
            IdCategoria = idCategoria;
            NombreCategoria = nombreCategoria;
            Descripcion = descripcion;
            Estado = estado;
            Productos = productos;
        }

        public Categoria(int idCategoria, string nombreCategoria)
        {
            IdCategoria = idCategoria;
            NombreCategoria = nombreCategoria;
        }

        public Categoria(int idCategoria)
        {
            IdCategoria = idCategoria;
        }

        public override string ToString()
        {
            return $"{IdCategoria} - {NombreCategoria}";
        }
    }
}