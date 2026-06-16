using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Data.Interfaces
{
    internal interface IUsuario : ICRUD<Usuario, int>
    {
        Usuario Validar(string nombre, string Clave);
    }
}
