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
    public class Proveedor
    {
        [Key]
        [DisplayName("ID Proveedor")]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "La razón social es obligatoria")]
        [StringLength(100)]
        [DisplayName("Razón Social")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(15)]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        [StringLength(100)]
        [DisplayName("Correo")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200)]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [DisplayName("Estado")]
        public bool Estado { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }

        public Proveedor()
        {
            Compras = new List<Compra>();
        }

        public Proveedor(int idProveedor, string razonSocial,
                         string telefono, string correo,
                         string direccion, bool estado)
        {
            IdProveedor = idProveedor;
            RazonSocial = razonSocial;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
            Estado = estado;
            Compras = new List<Compra>();
        }

        public override string ToString()
        {
            return RazonSocial;
        }
    }
}
