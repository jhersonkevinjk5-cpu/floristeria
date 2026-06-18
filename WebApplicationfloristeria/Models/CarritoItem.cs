using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationfloristeria.Models
{
    public class CarritoItem
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public string Imagen { get; set; }

        public decimal SubTotal
        {
            get { return Precio * Cantidad; }
        }
    }
}