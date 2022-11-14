
using System;
using System.Collections.Generic;

namespace LibreriaDeClases
{
    public class Entrada
    {
        private Int32 idEntrada;
        private String usuario;
        private String pass;
        //applicacion donde se usan las credenciales
        private String descripcion;
        private String emailUsuarioCreador;
        private List<String> listaEmailsUsuariosLectores;
        //fecha de creacion de entrada

        public Entrada(Int32 idEntrada, string usuario, string pass, string descripcion, String emailUsuarioCreador)
        {
            this.IdEntrada = idEntrada;
            this.Usuario = usuario;
            this.Pass = pass;
            this.Descripcion = descripcion;
            this.EmailUsuarioCreador = emailUsuarioCreador;
            this.ListaEmailsUsuariosLectores = new List<String>();
            //listaEmailsUsuariosLectores.Add(emailUsuarioCreador);
        }

        public Int32 IdEntrada { get => idEntrada; set => idEntrada = value; }
        public String Usuario { get => usuario; set => usuario = value; }
        public String Pass { get => pass; set => pass = value; }
        public String Descripcion { get => descripcion; set => descripcion = value; }
        public String EmailUsuarioCreador { get => emailUsuarioCreador; set => emailUsuarioCreador = value; }
        public List<string> ListaEmailsUsuariosLectores { get => listaEmailsUsuariosLectores; set => listaEmailsUsuariosLectores = value; }

        public int AñadirUsuarioLector(String emailUsuarioLector)
        {
            int flag = -1;
            if (emailUsuarioLector != null)
            {
                if (!EsCreador(emailUsuarioLector))
                {
                    ListaEmailsUsuariosLectores.Add(emailUsuarioLector);
                    flag = 0;
                }
                else flag = 1;
            }
            return flag;
        }
        public int BorrarUsuarioLector(String emailUsuarioLector)
        {
            int flag = -1;
            if (emailUsuarioLector != null)
            {
                if (EsLector(emailUsuarioLector))
                {
                    ListaEmailsUsuariosLectores.Remove(emailUsuarioLector);
                    flag = 0;
                }
                else flag = 1;
            }
            return flag;
        }

        public bool EsCreador(String emailUsuario)
        {
            bool flag = false;
            if (emailUsuario != null)
            {
                if (EmailUsuarioCreador == emailUsuario)
                {
                    flag = true;
                }
            }
            return flag;
        }
        public bool EsLector(String emailUsuario)
        {
            bool flag = false;
            if(emailUsuario != null)
            {
                if (ListaEmailsUsuariosLectores.Contains(emailUsuario))
                {
                    flag = true;
                }
            }
            return flag;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public String UsuariosLectores()
        {
            String usuariosLectores = "";
            foreach(String emailUsuario in ListaEmailsUsuariosLectores)
            {
                usuariosLectores += emailUsuario + "\n\r ";
            }
            return usuariosLectores;
        }

    }

}
