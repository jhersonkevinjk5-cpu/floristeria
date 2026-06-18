using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationfloristeria.Models;

namespace WebApplicationfloristeria.Data.Interfaces
{
    internal interface ICliente : ICRUD<Cliente , int>
    {
        Cliente Validar(string correo, string nombre);
    }
}
