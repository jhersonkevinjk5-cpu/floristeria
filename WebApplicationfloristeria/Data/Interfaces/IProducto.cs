using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Data.Interfaces
{
    internal interface IProducto :ICRUD<Producto , int>
    {
        (List<Producto>, int) BuscarPorDescripcion(string descripcion, bool Paginado = false, int numPag = 1, int resultadoPorPagina = 5);
    }
}
