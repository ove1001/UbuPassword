using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDatos
{
    internal interface ICapaDatos
    {
        public bool CrearUsuario(String emailAdmin, string nombre, string apellidos, string email, string passwd, short rol);


    }
}
