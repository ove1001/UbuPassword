//using DBDatos;

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
            this.listaEmailsUsuariosLectores = new List<String>();
        }

        public Int32 IdEntrada { get => idEntrada; set => idEntrada = value; }
        public String Usuario { get => usuario; set => usuario = value; }
        public String Pass { get => pass; set => pass = value; }
        public String Descripcion { get => descripcion; set => descripcion = value; }
        public String EmailUsuarioCreador { get => emailUsuarioCreador; set => emailUsuarioCreador = value; }

        /* requisito propio 
        public bool comprobarPassSecreto(Usuario usuario, String pass)
        {          
            return !usuario.comprobarPass(pass);
        }
        */

        public int añadirUsuarioLector(String emailUsuarioAñadidor, String emailUsuarioLector)
        {
            if (emailUsuarioAñadidor == EmailUsuarioCreador)
            {
                idUsuariosLectores.Add(iDusuarioLector);
                iDusuarioLector.AñadirAccesoEntrada(this);
                return 0;
            }
            return 1;

        }

    }

}
