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
    public class MovimientoStock
    {
        [Key]
        [DisplayName("ID Movimiento")]
        public int IdMovimiento { get; set; }

        [Required]
        [DisplayName("Producto")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El tipo de movimiento es obligatorio")]
        [StringLength(20)]
        [DisplayName("Tipo de Movimiento")]
        public string TipoMovimiento { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue)]
        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [DisplayName("Fecha de Movimiento")]
        public DateTime FechaMovimiento { get; set; }

        [StringLength(200)]
        [DisplayName("Observación")]
        public string Observacion { get; set; }

        public virtual Producto Producto { get; set; }

        public MovimientoStock()
        {
        }

        public MovimientoStock(int idMovimiento, int idProducto,
            string tipoMovimiento, int cantidad,
            DateTime fechaMovimiento, string observacion)
        {
            IdMovimiento = idMovimiento;
            IdProducto = idProducto;
            TipoMovimiento = tipoMovimiento;
            Cantidad = cantidad;
            FechaMovimiento = fechaMovimiento;
            Observacion = observacion;
        }

        public override string ToString()
        {
            return $"{TipoMovimiento} - {Cantidad}";
        }
    }
}
