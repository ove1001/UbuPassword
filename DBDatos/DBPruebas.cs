using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBDatos
{

    public class DBPruebas : ICapaDatos
    {
        /*
         * La lista de usuarios se guarda en la base de datos 
         * La lista de entradas se guarda en la base de datos
         * La lista de lineas de log se guardan en la base de datos
         * 
         * Generamos doble enlace para tener un acceso eficiente
         * 
         */

        private Int32 idUsuarioActual;
        private Int32 idEntradaActual;
        private Int32 idLineaLog;
        //Acceso temporal intermedio. Si no está en el diccionario, lo buscamos en la base de datos y lo guardamos en el diccionario
        private Dictionary<String, Usuario> diccionarioUsuarios;
        private Dictionary<Int32, Entrada> diccionarioEntradas;
        private Dictionary<Int32, LineaLog> diccionarioLogs; //¿¿Usar mejor lista??

        public DBPruebas()
        {
            this.idUsuarioActual = 0;
            this.idEntradaActual = 0;
            this.idLineaLog = 0;
            this.diccionarioUsuarios = new Dictionary<String, Usuario>();
            this.diccionarioEntradas = new Dictionary<Int32, Entrada>();
            this.diccionarioLogs = new Dictionary<Int32, LineaLog>();
            CrearAdminInicial();
        }

        private Int32 ObtenerSiguienteIdUsuarioLibre()
        {
            return idUsuarioActual++;
        }

        private Int32 ObtenerSiguienteIdEntradaLibre()
        {
            return idUsuarioActual++;
        }

        private Int32 ObtenerSiguienteIdLineaLogLibre()
        {
            return idLineaLog++;
        }

        /*
         * El primer usuario tendrá que estar generado en la base de datos al instanciarla
         */
        private Int32 CrearAdminInicial(
            String nombre = "admin",
            String apellidos = "admin",
            String email = "admin@admin.com",
            String pass = "admin",
            Int32 rol = 0
            )
        {
            Int32 flag = -1;
            if (nombre != null && apellidos != null &&
                email != null && pass != null)
            {
                if (new EmailAddressAttribute().IsValid(email))
                {
                    if (nombre.Length > 0 && apellidos.Length > 0 && pass.Length > 0)
                    {
                        Int32 idUsuario = ObtenerSiguienteIdUsuarioLibre();
                        //Crear un nuevo usuario
                        Usuario usuario = new Usuario(idUsuario, nombre, apellidos, email, pass, rol);
                        //Guardarlo en la base datos
                        diccionarioUsuarios.Add(email, usuario);
                        flag = 0;
                    }
                    else flag = 3;
                }
                else flag = 2;
            }
            return flag;
        }

        /*
         * Pasamos el objeto UsuarioCreador para asegurar que se ha pasado por el método de loggin,
         * ya que es el único método publico capaz de recuperar un objeto usuario de la base de datos
         */
        public Int32 CrearUsuario(Usuario admin, String nombre, String apellidos, String email, String pass, Int32 rol = 1)
        {
            Int32 flag = -1;
            if (admin != null && nombre != null && apellidos != null &&
                email != null && pass != null)
            {
                if (admin.EsAdmin())
                {
                    if (new EmailAddressAttribute().IsValid(email))
                    {
                        if (nombre.Length > 0 && apellidos.Length > 0 && pass.Length > 0)
                        {
                            Int32 idUsuario = ObtenerSiguienteIdUsuarioLibre();
                            //Crear un nuevo usuario
                            Usuario usuario = new Usuario(idUsuario, nombre, apellidos, email, pass, rol);
                            //Guardarlo en la base datos
                            diccionarioUsuarios.Add(email, usuario);
                            flag = 0;
                        }
                        else flag = 3;
                    }
                    else flag = 2;
                }
                else flag = 1;
            }
            return flag;
        }

        public Int32 CrearAdmin(Usuario admin, String nombre, String apellidos, String email, String pass)
        {
            return CrearUsuario(admin, nombre, apellidos, email, pass, 0);
        }

        /*
         * Pasamos el objeto UsuarioCreador para asegurar que se ha pasado por el método de loggin,
         * ya que es el único método publico capaz de recuperar un objeto usuario de la base de datos
         */
        public Int32 BorrarUsuario(Usuario admin, String emailUsuarioBorrar)
        {
            Int32 flag = -1;
            if (admin != null && emailUsuarioBorrar != null)
            {
                if (admin.EsAdmin())
                {
                    Usuario usuario = BuscarUsuarioPorEmail(emailUsuarioBorrar);
                    if (usuario != null)
                    {
                        //Borrar usuario de base de datos
                        diccionarioUsuarios.Remove(emailUsuarioBorrar);
                        flag = 0;
                    }
                    else flag = 2;
                }
                else flag = 1;
            }
            return flag;
        }

        /*
        public bool ActualizarUsuario(Usuario usuario)
        {
            //Actualizar datos de usuario en base de datos
        }
        */


        private Usuario BuscarUsuarioPorEmail(String email)
        {
            Usuario usuario = diccionarioUsuarios[email];
            return usuario;
        }

        public Usuario LoggIn(String email, String pass)
        {
            Usuario usuario = BuscarUsuarioPorEmail(email);
            if (usuario != null && usuario.comprobarPass(pass))
            {
                return usuario;
            }
            return null;
        }

        /*
         * Pasamos el objeto UsuarioCreador para asegurar que se ha pasado por el método de loggin,
         * ya que es el único método publico capaz de recuperar un objeto usuario de la base de datos
         */
        public int CrearEntrada(Usuario usuarioCreador, String usuario, String pass, String descripcion, /*datos*/)
        {
            Int32 flag = -1;
            if (usuarioCreador != null && usuario.Length > 0 && pass.Length > 0 && descripcion.Length > 0)
            {
                /*
                 * Requisito propio extra
                 * 
                 * Comprobar la contraseña introducida respecto a la contraseña del usuario
                 * Si coincide la contraseña, no iniciamos la creación del objeto 
                 * y arrojamos un código de berror
                 * 
                 */

                if (!usuarioCreador.comprobarPass(pass))
                {
                    Int32 idEntrada = ObtenerSiguienteIdEntradaLibre();
                    //Creación de entrada
                    Entrada entrada = new Entrada(idEntrada, usuario, pass, descripcion, usuarioCreador.Email);
                    //Almacenar entrada en DB 
                    diccionarioEntradas.Add(idEntrada, entrada);
                    //Linkado de entrada a su creador
                    usuarioCreador.AñadirPropiaEntrada(idEntrada);
                    flag = 0;
                }
                else flag = 1;
            }
            return flag;
        }

        public int BorrarEntrada(Usuario usuario, Int32 idEntrada)
        {
            Int32 flag = -1;
            if (usuario != null)
            {
                if (usuario.EsEntradaPropia(idEntrada))
                {
                    Entrada entrada = BuscarEntradaPorId(idEntrada);
                    foreach (String emailUsuario in entrada.ListaEmailsUsuariosLectores)
                    {
                        Usuario user = BuscarUsuarioPorEmail(emailUsuario);
                    }
                }
                else flag = 1;

            }
            return flag;
        }

        /*
        public void ActualizarEntrada(Int32 idUsuario, Int32 idEntrada)
        {

        }
        */

        private Entrada BuscarEntradaPorId(Int32 id)
        {
            Entrada entrada = diccionarioEntradas[id];
            return entrada;
        }

        public Entrada RecuperarEntrada(Usuario usuario, Int32 idEntrada)
        {
            Entrada entrada = null;
            if (usuario != null)
            {
                /*
                 * Los administradores pueden recuprar cualquier entrada, 
                 * aunque no estén especificados explícitamente
                 */
                //Si es el propietario devuelvo objeto real.
                entrada = BuscarEntradaPorId(idEntrada);
                if (entrada != null)
                {
                    if (entrada.EsLector(usuario.Email))
                    {
                        // si solo tiene permiso de lectura, devuelvo una copia
                        entrada = (Entrada)entrada.Clone();
                    }
                    //si no es lector ni creador
                    else if (!entrada.EsCreador(usuario.Email))
                    {
                        entrada = null;
                    }
                }
            }
            return entrada;
        }


        /*
         * Pasamos el objeto UsuarioCreador para asegurar que se ha pasado por el método de loggin,
         * ya que es el único método publico capaz de recuperar un objeto usuario de la base de datos
         */
        public int AñadirLectorAEntrada(Int32 idEntrada, Usuario usuarioCreador, String emailUsuarioLector) {
            int flag = -1;
            if (usuarioCreador != null)
            {
                if (usuarioCreador.EsEntradaPropia(idEntrada))
                {
                    Entrada entrada = RecuperarEntrada(usuarioCreador, idEntrada);
                    Usuario lector = BuscarUsuarioPorEmail(emailUsuarioLector);
                    if (lector != null)
                    {
                        lector.AñadirLecturaEntrada(idEntrada);
                        entrada.AñadirUsuarioLector(emailUsuarioLector);
                        flag = 0;
                    }
                    else flag = 2;
                }
                else flag = 1;
            }
            return flag;
        }

        /*
         * Pasamos el objeto UsuarioCreador para asegurar que se ha pasado por el método de loggin,
         * ya que es el único método publico capaz de recuperar un objeto usuario de la base de datos
         */
        public int BorrarLectorAEntrada(Int32 idEntrada, Usuario usuarioCreador, String emailUsuarioLector)
        {
            int flag = -1;
            if (usuarioCreador != null)
            {
                if (usuarioCreador.EsEntradaPropia(idEntrada))
                {
                    Entrada entrada = RecuperarEntrada(usuarioCreador, idEntrada);
                    Usuario lector = BuscarUsuarioPorEmail(emailUsuarioLector);
                    if (lector != null)
                    {
                        lector.BorrarLecturaEntrada(idEntrada);
                        entrada.BorrarUsuarioLector(emailUsuarioLector);
                        flag = 0;
                    }
                    else flag = 2;
                }
                else flag = 1;
            }
            return flag;
        }

        public void AñadirLineaLog(String linea)
        {


        }

        public void LeerLineaLog(String linea)
        {


        }

        public void LeerLog(String linea)
        {


        }
    }
}

   
