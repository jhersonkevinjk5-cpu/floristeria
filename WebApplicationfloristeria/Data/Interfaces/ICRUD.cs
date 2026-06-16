using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationfloristeria.Data.Interfaces
{
    internal interface ICRUD <T ,ID>
    {
        ID Registrar(T entidad);
        T BuscarPorId(ID id);
        bool Actualizar(T entidad);
        bool Eliminar(ID id);
        List<T> ListarTodo();
    }
}
