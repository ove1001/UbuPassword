
using System;
using System.Collections.Generic;

namespace LibreriaDeClases
{
    public class Usuario
    {
        private Int32 idUsuario;
        private String nombre;
        private String apellidos;
        private String email;
        private String pass;
        private Int32 rol;
        private List<Int32> listaEntradasCreadas;
        private List<Int32> listaEntradasLegibles;

        public Usuario(int idUsuario, string nombre, string apellidos, string email, string passwd, int rol)
        {
            this.IdUsuario = idUsuario;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Email = email;
            this.Pass = passwd;
            this.Rol = rol;
            this.ListaEntradasCreadas = new List<Int32>();
            this.ListaEntradasLegibles = new List<Int32>();
        }
        public Int32 IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Email { get => email; set => email = value; }
        public Int32 Rol { get => rol; set => rol = value; }
        public string Pass { get => pass; set => pass = Utilidades.Encriptar(value); }
        public List<int> ListaEntradasCreadas { get => listaEntradasCreadas; set => listaEntradasCreadas = value; }
        public List<int> ListaEntradasLegibles { get => listaEntradasLegibles; set => listaEntradasLegibles = value; }

        public bool EsAdmin()
        {
            return Rol == 0;
        }

        public bool ComprobarPass(String pass)
        {
            return Utilidades.Encriptar(pass) == Pass;
        }

        public bool EsEntradaPropia(Int32 idEntrada)
        {
            return ListaEntradasCreadas.Contains(idEntrada);  
        }

        public bool EsEntradaLegible(Int32 idEntrada)
        {
            return ListaEntradasLegibles.Contains(idEntrada);
        }

        private Int32 AñadirEntrada(List<Int32> listaEntradas, Int32 idEntrada)
        {
            Int32 flag = -1;
            if (listaEntradas != null)
            {
                if (!listaEntradas.Contains(idEntrada))
                {
                    listaEntradas.Add(idEntrada);
                    flag = 0;
                }
                else flag = 1;
            }
            return flag;
        }

        private Int32 BorrarEntrada(List<Int32> listaEntradas, Int32 idEntrada)
        {
            Int32 flag = -1;
            if (listaEntradas != null)
            {
                if (listaEntradas.Contains(idEntrada))
                {
                    listaEntradas.Remove(idEntrada);
                    flag = 0;
                }
                else flag = 1;
            }
            return flag;
        }

        public Int32 AñadirPropiaEntrada(Int32 idEntrada)
        {
            return AñadirEntrada(ListaEntradasCreadas, idEntrada);
        }

        public Int32 BorrarPropiaEntrada(Int32 idEntrada)
        {
            return BorrarEntrada(ListaEntradasCreadas, idEntrada);
        }

        public Int32 AñadirLecturaEntrada(Int32 idEntrada)
        {
            return AñadirEntrada(ListaEntradasLegibles, idEntrada);
        }

        public Int32 BorrarLecturaEntrada(Int32 idEntrada)
        {
            return BorrarEntrada(ListaEntradasLegibles, idEntrada);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}