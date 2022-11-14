using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        private Dictionary<Int32, LineaLog> diccionarioLogs;

        private static bool imprimirLogPorConsola = true;

        private int IdUsuarioActual { get => idUsuarioActual; set => idUsuarioActual = value; }
        private int IdEntradaActual { get => idEntradaActual; set => idEntradaActual = value; }
        private int IdLineaLog { get => idLineaLog; set => idLineaLog = value; }
        private Dictionary<string, Usuario> DiccionarioUsuarios { get => diccionarioUsuarios; set => diccionarioUsuarios = value; }
        private Dictionary<int, Entrada> DiccionarioEntradas { get => diccionarioEntradas; set => diccionarioEntradas = value; }
        private Dictionary<int, LineaLog> DiccionarioLogs { get => diccionarioLogs; set => diccionarioLogs = value; }

        public DBPruebas()
        {
            this.IdUsuarioActual = 0;
            this.IdEntradaActual = 0;
            this.IdLineaLog = 0;
            this.DiccionarioUsuarios = new Dictionary<String, Usuario>();
            this.DiccionarioEntradas = new Dictionary<Int32, Entrada>();
            this.DiccionarioLogs = new Dictionary<Int32, LineaLog>();
            CrearAdminInicial();
        }

        /***********************************************************/

        private Int32 ObtenerSiguienteIdUsuarioLibre()
        {
            return IdUsuarioActual++;
        }

        private Int32 ObtenerSiguienteIdEntradaLibre()
        {
            return IdEntradaActual++;
        }

        private Int32 ObtenerSiguienteIdLineaLogLibre()
        {
            return IdLineaLog++;
        }

        /***********************************************************/

        private int AnadirUsuarioPorEmail(String emailUsuarioNuevo, Usuario usuarioNuevo, Usuario usuario)
        {
            int flag = -1;
            String mensaje = "";
            try
            {
                DiccionarioUsuarios.Add(emailUsuarioNuevo, usuarioNuevo);
                flag = 0;
                mensaje = "El usuario " + emailUsuarioNuevo + " ha sido añadido correctamente.";
            }
            catch (System.ArgumentException ex)
            {
                mensaje = "El usuario " + emailUsuarioNuevo + " ya existe.";
            }
            finally
            {
            if (usuarioNuevo != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            }
            return flag;
        }

        private int BorrarUsuarioPorEmail(String emailUsuarioBorrar, Usuario usuario)
        {
            int flag = -1;
            String mensaje = "";
            try
            {
                DiccionarioUsuarios.Remove(emailUsuarioBorrar);
                flag = 0;
                mensaje = "El usuario " + emailUsuarioBorrar + " ha sido borrado correctamente.";
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                mensaje = "El usuario " + emailUsuarioBorrar + " no ha sido borrado porque no existía.";
            }
            finally
            {
                if (usuario != null)
                    CrearLineaLog(mensaje, usuario);
                else
                    CrearLineaLog(mensaje);
            }
            return flag;
        }

        private Usuario BuscarUsuarioPorEmail(String emailUsuaurioBuscado, Usuario usuario)
        {
            Usuario usuarioBuscado = null;
            String mensaje = "";
            try
            {
                usuarioBuscado = DiccionarioUsuarios[emailUsuaurioBuscado];
                mensaje = "El usuario " + emailUsuaurioBuscado + " ha sido recuperado de la base de datos correctamente.";
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                mensaje = "El usuario " + emailUsuaurioBuscado + " no ha podido ser recuperadode la base de datos, ya que no existe.";
            }
            finally
            {
                if (usuario != null)
                    CrearLineaLog(mensaje, usuario);
                else
                    CrearLineaLog(mensaje);
            }
            return usuarioBuscado;
        }

        public Dictionary<String, Usuario> RecuperarDiccionarioUsuarios(Usuario usuario)
        {
            String mensaje = "";
            Dictionary<String, Usuario> diccUsuarios = null;
            if (usuario != null)
            {
                if (usuario.EsAdmin())
                {
                    diccUsuarios = DiccionarioUsuarios;
                    mensaje = "Se ha recuperado el registro completo de usuarios.";
                }
                else
                {
                    mensaje = "El usuario " + usuario.Email + "sin privilegios ha intentado recuperar el registro completo de usuarios.";
                    //diccUsuarios = new Dictionary<String, Usuario>(DiccionarioUsuarios);
                }
            }
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return diccUsuarios;
        }

        /***********************************************************/

        private int AnadirEntradaPorId(Int32 idEntradaNueva, Entrada entradaNueva, Usuario usuario)
        {
            int flag = -1;
            String mensaje = "";
            try
            {
                DiccionarioEntradas.Add(idEntradaNueva, entradaNueva);
                flag = 0;
                mensaje = "La entrada ID=" + idEntradaNueva + " Usuario=" + entradaNueva.Usuario + " Contraseña=" + entradaNueva.Pass + " Descripcion=" + entradaNueva.Descripcion + " ha sido añadida correctamente.";
            }
            catch (System.ArgumentException ex)
            {
                mensaje = "La entrada ID=" + idEntradaNueva + " ya existía.";
            }
            finally
            {
                if (usuario != null)
                    CrearLineaLog(mensaje, usuario);
                else
                    CrearLineaLog(mensaje);
            }
            return flag;
        }

        private int BorrarEntradaPorId(Int32 idEntradaBorrar, Usuario usuario)
        {
            int flag = -1;
            Entrada entrada = null;
            String mensaje = "";
            try
            {
                entrada = DiccionarioEntradas[idEntradaBorrar];
                DiccionarioEntradas.Remove(idEntradaBorrar);
                flag = 0;
                mensaje = "La entrada ID=" + idEntradaBorrar + " Usuario=" + entrada.Usuario + " Contraseña=" + entrada.Pass + " Descripcion=" + entrada.Descripcion + " ha sido borrada correctamente.";
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                mensaje = "La entrada ID=" + idEntradaBorrar + " no ha podido ser borrada, ya que no existe.";
            }
            finally
            {
                if (usuario != null)
                    CrearLineaLog(mensaje, usuario);
                else
                    CrearLineaLog(mensaje);
            }
            return flag;
        }

        private Entrada BuscarEntradaPorId(Int32 idEntradaBuscada, Usuario usuario)
        {
            Entrada entrada = null;
            String mensaje = "";
            try
            {
                entrada = DiccionarioEntradas[idEntradaBuscada];
                mensaje = "La entrada ID=" + idEntradaBuscada + " Usuario=" + entrada.Usuario + " Contraseña=" + entrada.Pass + " Descripcion=" + entrada.Descripcion + " ha sido recuperada correctamente.";
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                mensaje = "La entrada ID=" + idEntradaBuscada + " no ha podido ser recuperada, ya que no existe.";
            }
            finally
            {
                if (usuario != null)
                    CrearLineaLog(mensaje, usuario);
                else
                    CrearLineaLog(mensaje);
            }
            return entrada;
        }

        public Dictionary<Int32, Entrada> RecuperarDiccionarioEntradas(Usuario usuario)
        {
            String mensaje = "";
            Dictionary<Int32, Entrada> dicEntradas = null;
            if (usuario != null)
            {
                if (usuario.EsAdmin())
                {
                    dicEntradas = DiccionarioEntradas;
                    mensaje = "Se ha recuperado el registro completo de entradas.";
                }
                else
                {
                    mensaje = "El usuario " + usuario.Email + "sin privilegios ha intentado recuperar el registro completo de entradas.";
                    //diccUsuarios = new Dictionary<String, Usuario>(DiccionarioUsuarios);
                }
            }
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return dicEntradas;
        }


        /***********************************************************/

        private int AñadirLineaLog(String mensajeLineaLog, String emailUsuario = null, int idEntrada = -1)
        {
            int flag = -1;
            if (mensajeLineaLog != null && mensajeLineaLog != "")
            {
                Int32 idLinea = ObtenerSiguienteIdLineaLogLibre();
                LineaLog lineaLog = new LineaLog(mensajeLineaLog, idLinea, emailUsuario, idEntrada);
                try
                {
                    DiccionarioLogs.Add(idLinea, lineaLog);
                    flag = 0;
                }
                catch (System.ArgumentException ex)
                {
                    flag = 1;
                }
                finally
                {
                    if (imprimirLogPorConsola)
                        Console.Write(lineaLog.ToString()+"\r\n");
                }
            }
            return flag;
        }

        private LineaLog BuscarLineaLog(Int32 numeroLineaLog, Usuario usuario)
        {
            LineaLog lineaLog = null;
            String mensaje = "";
            try
            {
                lineaLog = DiccionarioLogs[numeroLineaLog];
                mensaje = "El usuario ha leido la linea de log " + numeroLineaLog + " correctamente.";
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                mensaje = "El usuario ha intentado leer una linea de log inexistente";
            }

            finally
            {
                if (usuario != null)
                    CrearLineaLog(mensaje, usuario);
                else
                    CrearLineaLog(mensaje);
            }
            return lineaLog;
        }

        public Dictionary<Int32, LineaLog> RecuperarDiccionarioLogs(Usuario usuario)
        {
            String mensaje = "";
            Dictionary<Int32, LineaLog> dicLogs = null;
            if (usuario != null)
            {
                if (usuario.EsAdmin())
                {
                    dicLogs = DiccionarioLogs;
                    mensaje = "Se ha recuperado el registro completo de logs.";
                }
                else
                {
                    mensaje = "El usuario " + usuario.Email + "sin privilegios ha intentado recuperar el registro completo de logs.";
                    //diccUsuarios = new Dictionary<String, Usuario>(DiccionarioUsuarios);
                }
            }

            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return dicLogs;
        }

        /***********************************************************/

        //El primer usuario tendrá que estar generado en la base de datos al instanciarla
        private void CrearAdminInicial()
        {
            String nombre = "admin";
            String apellidos = "admin";
            String email = "admin@admin.com";
            String pass = "admin";
            Int32 rol = 0;

            Int32 idUsuario = ObtenerSiguienteIdUsuarioLibre();
            Usuario usuario = new Usuario(idUsuario, nombre, apellidos, email, pass, rol);
            DiccionarioUsuarios.Add(email, usuario);
            CrearLineaLog("El usuario " + email + " ha sido añadido correctamente.", usuario);
        }

        /***********************************************************/


        /*
         * Pasamos un objeto usuario para asegurar que se ha pasado por el método de loggin,
         * ya que es el único método publico capaz de recuperar un objeto usuario de la base de datos
         */
        public Int32 CrearUsuario(Usuario admin, String nombre, String apellidos, String email, String pass, Int32 rol = 1)
        {
            Int32 flag = -1;
            String mensaje = "";
            if (admin != null && 
                nombre != null && nombre != "" &&
                apellidos != null && apellidos != "" &&
                email != null && email != "" &&
                pass != null && pass != "" &&
                rol >= 0 && rol <= 1)
            {
                if (admin.EsAdmin())
                {
                    if (new EmailAddressAttribute().IsValid(email))
                    {
                        if (pass.Length > 0)
                        {
                            if (nombre.Length > 0 && apellidos.Length > 0 && pass.Length > 0)
                            {
                                Int32 idUsuario = ObtenerSiguienteIdUsuarioLibre();
                                //Crear un nuevo usuario
                                Usuario usuario = new Usuario(idUsuario, nombre, apellidos, email, pass, rol);
                                //Guardarlo en la base datos
                                AnadirUsuarioPorEmail(email, usuario, admin);
                                flag = 0;
                            }
                            else
                            {
                                flag = 4;
                                mensaje = "El usuario " + admin.Email + " ha intentado crear un usuario con datos personales invalidos.";
                            }
                        }
                        else
                        {
                            flag = 3;
                            mensaje = "El usuario " + admin.Email + " ha intentado crear un usuario con contraseña vacia.";
                        }
                    }
                    else
                    {
                        flag = 2;
                        mensaje = "El usuario " + admin.Email + " ha intentado crear un usuario con un correo invalido.";
                    }
                }
                else
                {
                    flag = 1;
                    mensaje = "El usuario " + admin.Email + " ha intentado crear un usuario sin ser administrador.";
                }
            }
            else
                mensaje = "Se ha intentado crear un usuario con algún dato invalido.";
            if (admin != null)
                CrearLineaLog(mensaje, admin);
            else
                CrearLineaLog(mensaje);
            return flag;
        }

        public Int32 CrearAdmin(Usuario admin, String nombre, String apellidos, String email, String pass)
        {
            return CrearUsuario(admin, nombre, apellidos, email, pass, 0);
        }

        public Int32 BorrarUsuario(Usuario admin, String emailUsuarioBorrar, bool borrarEntradas = false)
        {
            Int32 flag = -1;
            String mensaje = "";
            if (admin != null && 
                emailUsuarioBorrar != null && emailUsuarioBorrar != "")
            {
                if (admin.EsAdmin())
                {
                    Usuario usuario = BuscarUsuarioPorEmail(emailUsuarioBorrar, admin);
                    if (usuario != null)
                    {
                        //Borrar usuario de base de datos
                        if (borrarEntradas == true)
                        {
                            foreach (int idEntrada in usuario.ListaEntradasCreadas.ToList())
                            {
                                BorrarEntrada(admin, idEntrada);
                            }
                        }
                        BorrarUsuarioPorEmail(emailUsuarioBorrar, admin);
                        flag = 0;
                    }
                    else
                    {
                        flag = 2;
                        mensaje = "El usuario " + admin.Email + " ha intentado borrar un usuario inexistente.";
                    }
                }
                else
                {
                    flag = 1;
                    mensaje = "El usuario " + admin.Email + " ha intentado borrar un usuario sin ser administrador.";
                }
            }
            else
                mensaje = "Se ha intentado borrar un usuario con datos nulos";
            if (admin != null)
                CrearLineaLog(mensaje, admin);
            else
                CrearLineaLog(mensaje);
            return flag;
        }

        public Int32 BorrarUsuarioYEntradasCreadas(Usuario admin, String emailUsuarioBorrar)
        {
            return BorrarUsuario(admin, emailUsuarioBorrar, true);
        }

        public int ActualizarUsuario(Usuario usuarioModificador, String emailUsuarioAModificar, String nombreNuevo, String apellidosNuevo, /*String emailNuevo,*/ String passNueva, Int32 nuevoRol)
        {
            Int32 flag = -1;
            String mensaje = "";
            if (usuarioModificador != null && 
                emailUsuarioAModificar != "" && emailUsuarioAModificar != null &&
                nombreNuevo != "" && nombreNuevo != null && 
                apellidosNuevo != "" && apellidosNuevo != null && 
                //emailNuevo != "" && emailNuevo != null && 
                passNueva != "" && passNueva != null && 
                nuevoRol >= 0 && nuevoRol <= 1)
            {
                if (usuarioModificador.EsAdmin() || usuarioModificador.Email == emailUsuarioAModificar)
                {
                    Usuario usuarioAModificar = BuscarUsuarioPorEmail(emailUsuarioAModificar, usuarioModificador);
                    if (usuarioAModificar != null)
                    {
                        usuarioAModificar.Nombre = nombreNuevo;
                        usuarioAModificar.Apellidos = apellidosNuevo;
                        usuarioAModificar.Pass = passNueva;
                        if (usuarioModificador.EsAdmin())
                            usuarioAModificar.Rol = nuevoRol;
                            /*
                            usuarioAModificar.Email = emailNuevo;
                            BorrarUsuarioPorEmail(emailUsuarioAModificar, usuarioAModificar);
                            AnadirUsuarioPorEmail(emailUsuarioAModificar, usuarioAModificar, usuarioAModificar);
                            */
                        flag = 0;
                        mensaje = "El usuario " + emailUsuarioAModificar + " ha sido modificado correctamente.";
                    }
                    else
                    {
                        flag = 2;
                        mensaje = "No se puede modificar el usuario " + emailUsuarioAModificar + " ya que no existe en el sistema.";
                    }
                }
                else
                {
                    flag = 1;
                    mensaje = "Se ha intentado modificar al usuario " + emailUsuarioAModificar + " sin ser él mismo o un adminitrador.";
                }
            }
            else
                mensaje = "Se ha intentado modificar un usario con datos invalidos";
            if (usuarioModificador != null)
                CrearLineaLog(mensaje, usuarioModificador);
            else
                CrearLineaLog(mensaje);
            return flag;
        }
                 
        public Usuario LoggIn(String email, String pass)
        {
            Usuario usuario = BuscarUsuarioPorEmail(email, null);
            String mensaje = "";
            if (usuario != null && usuario.ComprobarPass(pass))
            {
                mensaje = "El usuario " + email + " se ha logeado correctamente.";
            }
            else
            {
                usuario = null;
                mensaje = "El usuario " + email + " no se ha logeado correctamente.";
            }
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return usuario;
        }

        public Int32 ObtenerCantidadUsuarios(Usuario usuario)
        {
            Int32 cantidad = -1;
            if (usuario != null && usuario.EsAdmin())
            {
                cantidad = RecuperarDiccionarioUsuarios(usuario).Count();
            }
            return cantidad;
        }

        /***********************************************************/

        public int CrearEntrada(Usuario usuarioCreador, String usuario, String pass, String descripcion)
        {
            Int32 flag = -1;
            String mensaje = "";
            Entrada entrada = null;
            if (usuarioCreador != null)
            {
                if (usuario != "" && usuario != null &&
                    pass != "" && pass != null &&
                    descripcion != "" && descripcion != null)
                {
                    /*
                     * Requisito propio extra
                     * 
                     * Comprobar la contraseña introducida respecto a la contraseña del usuario
                     * Si coincide la contraseña, no iniciamos la creación del objeto 
                     * y arrojamos un código de berror
                     * 
                     */

                    if (!usuarioCreador.ComprobarPass(pass))
                    {
                        Int32 idEntrada = ObtenerSiguienteIdEntradaLibre();
                        //Creación de entrada
                        entrada = new Entrada(idEntrada, usuario, pass, descripcion, usuarioCreador.Email);
                        //Almacenar entrada en DB 
                        AnadirEntradaPorId(idEntrada, entrada, usuarioCreador);
                        //Linkado de entrada a su creador
                        usuarioCreador.AñadirPropiaEntrada(idEntrada);
                        flag = 0;
                    }
                    else
                    {
                        flag = 2;
                        mensaje = "Se ha intentado crear una entrada con la contraseña del usuario.";
                    }
                }
                else
                {
                    flag = 1;
                    mensaje = "Se ha intentado crear una entrada con algún dato vacío";
                }
            }
            else
                mensaje = "Se ha intentado crear una entrada sin usuario asociado";
            if (usuarioCreador != null)
                if (entrada != null)
                    CrearLineaLog(mensaje, usuarioCreador, entrada);
                else
                    CrearLineaLog(mensaje, usuarioCreador);
            else
                CrearLineaLog(mensaje);
            return flag;
        }

        public int BorrarEntrada(Usuario usuario, Int32 idEntrada)
        {
            Int32 flag = -1;
            String mensaje = "";
            Entrada entrada = null;
            if (usuario != null)
            {
                Usuario usuarioCreador = null;
                entrada = BuscarEntradaPorId(idEntrada, usuario);
                if (entrada != null) {
                    if (usuario.EsEntradaPropia(idEntrada))
                    {
                        usuarioCreador = usuario;
                    }
                    else if (usuario.EsAdmin())
                    {
                        usuarioCreador = BuscarUsuarioPorEmail(entrada.EmailUsuarioCreador, usuario);
                    }
                    if(usuarioCreador != null)
                    {
                        foreach (String emailUsuario in entrada.ListaEmailsUsuariosLectores)
                        {
                            Usuario user = BuscarUsuarioPorEmail(emailUsuario, usuario);
                            user.BorrarLecturaEntrada(idEntrada);
                        }
                        usuarioCreador.BorrarPropiaEntrada(idEntrada);
                        BorrarEntradaPorId(idEntrada, usuario);
                        flag = 0;
                    }
                    else
                    {
                        flag = 2;
                        mensaje = "Se ha intentado borrar la entrada " + idEntrada + " sin ser su propietario o un administrador.";
                    }
                }
                else
                {
                    flag = 1;
                    mensaje = "Se ha intentado borrar una entrada inexistene.";
                }
            }
            else
                mensaje = "Se ha intentado borrar una entrada con datos inválidos";
            if (usuario != null)
                if (entrada != null)
                    CrearLineaLog(mensaje, usuario, entrada);
                else
                    CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return flag;
        }

        public int ActualizarEntrada(Usuario usuarioModificador, Int32 idEntradaAModificar, String usuario, String pass, String descripcion)
        {
            Int32 flag = -1;
            String mensaje = "";
            Entrada entradaAModificar = null;
            if (usuarioModificador != null)
            {
                if (usuario != "" && usuario != null &&
                    pass != "" && pass != null &&
                    descripcion != "" && descripcion != null)
                {
                    if (usuarioModificador.EsAdmin() || usuarioModificador.EsEntradaPropia(idEntradaAModificar))
                    {
                        entradaAModificar = BuscarEntradaPorId(idEntradaAModificar, usuarioModificador);
                        if (entradaAModificar != null)
                        {
                            if (!usuarioModificador.ComprobarPass(pass))
                            {
                                mensaje = "Se ha modificado los datos de la entrada ID = " + idEntradaAModificar + " Usuario = " + entradaAModificar.Usuario + " Contraseña = " + entradaAModificar.Pass + " Descripcion = " + entradaAModificar.Descripcion;
                                entradaAModificar.Usuario = usuario;
                                entradaAModificar.Pass = pass;
                                entradaAModificar.Descripcion = descripcion;
                                mensaje = " por los datos Usuario = " + entradaAModificar.Usuario + " Contraseña = " + entradaAModificar.Pass + " Descripcion = " + entradaAModificar.Descripcion;
                                flag = 0;
                            }
                            else
                            {
                                flag = 3;
                                mensaje = "Se ha intentado modificar la entrada " + idEntradaAModificar + " con la contraseña del usuario.";
                            }
                        }
                        else
                        {
                            flag = 2;
                            mensaje = "Se ha intentado modificar la entrada " + idEntradaAModificar + " con datos inválidos.";
                        }
                    }
                    else
                    {
                        flag = 1;
                        mensaje = "Se ha intentado modificar la entrada " + idEntradaAModificar + " sin ser su propietario o un administrador.";
                    }
                }
                else
                {
                    flag = 1;
                    mensaje = "Se ha intentado modificar una entrada con algún dato vacío";
                }
            }
            else
                mensaje = "Se ha intentado modificar una entrada sin usuario asociado";
            if (usuarioModificador != null)
                if (entradaAModificar != null)
                    CrearLineaLog(mensaje, usuarioModificador, entradaAModificar);
                else
                    CrearLineaLog(mensaje, usuarioModificador);
            else
                CrearLineaLog(mensaje);
            return flag;
        }

        public Entrada RecuperarEntrada(Usuario usuario, Int32 idEntrada)
        {
            String mensaje = "";
            Entrada entrada = null;
            if (usuario != null)
            {
                /*
                 * Los administradores pueden recuprar cualquier entrada, 
                 * aunque no estén especificados explícitamente
                 */
                //Si es el propietario o un admin devuelvo objeto real.
                entrada = BuscarEntradaPorId(idEntrada, usuario);
                if (entrada != null)
                {
                    if (entrada.EsLector(usuario.Email))
                    {
                        // si solo tiene permiso de lectura, devuelvo una copia
                        entrada = (Entrada)entrada.Clone();
                        mensaje = "Se ha recuperado la entrada " + idEntrada + " correctamente como lector.";
                    }
                    //si no es lector ni creador ni es admin devulvo null
                    else if (!entrada.EsCreador(usuario.Email) && !usuario.EsAdmin())
                    {
                        entrada = null;
                        mensaje = "Se ha intentado recuperar la entrada " + idEntrada + " sin ser su propietario o un administrador.";
                    }
                    else
                        mensaje = "Se ha recuperado la entrada " + idEntrada + " correctamente como creador o administrador.";
                }
                else
                    mensaje = "Se ha intentado recuperar una entrada inexistente.";
            }
            else
                mensaje = "Se ha intentado recuperar una entrada con datos inválidos.";
            if (usuario != null)
                if (entrada != null)
                    CrearLineaLog(mensaje, usuario, entrada);
                else
                    CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return entrada;
        }

        public Int32 ObtenerCantidadEntradas(Usuario usuario)
        {
            Int32 cantidad = -1;
            if (usuario != null && usuario.EsAdmin())
            {
                cantidad = RecuperarDiccionarioEntradas(usuario).Count();
            }
            return cantidad;
        }

        /***********************************************************/

        public int AsociarLectorYEntrada(Int32 idEntrada, String emailUsuarioLector, Usuario usuario) 
        {
            int flag = -1;
            String mensaje = "";
            Entrada entrada = RecuperarEntrada(usuario, idEntrada);
            if (entrada != null &&
                usuario != null &&
                emailUsuarioLector != null && emailUsuarioLector != "")
            {
                if (usuario.EsEntradaPropia(idEntrada) || usuario.EsAdmin())
                {
                    Usuario lector = BuscarUsuarioPorEmail(emailUsuarioLector, usuario);
                    //Los admin no pueden ser lectores, ya que tienen acceso a todo
                    //Los creadores no pueden definirse como lectores
                    if (lector != null && !lector.EsAdmin() && lector != usuario)
                    {
                        lector.AñadirLecturaEntrada(idEntrada);
                        entrada.AñadirUsuarioLector(emailUsuarioLector);
                        flag = 0;
                        mensaje = "Se ha asociado correctamente la lectura de la entrada " + idEntrada + " al usuario " + emailUsuarioLector;
                    }
                    else
                    {
                        flag = 2;
                        mensaje = "Se ha intentado asociar la lectura de la entrada " + idEntrada + " al usuario invalido " + emailUsuarioLector;
                    }
                }
                else
                {
                    flag = 1;
                    mensaje = "Se ha intentado asociar la lectura de la entrada " + idEntrada + " al usuario " + emailUsuarioLector + " sin ser su propietario o un administrador.";
                }               
            }
            else
                mensaje = "Se ha intentado asociar la lectura de una entrada a un usuario con algún dato erroneo.";
            if (usuario != null)
                if (entrada != null)
                    CrearLineaLog(mensaje, usuario, entrada);
                else
                    CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return flag;
        }

        public int DesasociarLectorYEntrrada(Int32 idEntrada, String emailUsuarioLector, Usuario usuario)
        {
            int flag = -1;
            String mensaje = "";
            Entrada entrada = RecuperarEntrada(usuario, idEntrada);
            if (entrada != null &&
                usuario != null &&
                emailUsuarioLector != null && emailUsuarioLector != "")
            {
                if (usuario.EsEntradaPropia(idEntrada) || usuario.EsAdmin())
                {
                    Usuario lector = BuscarUsuarioPorEmail(emailUsuarioLector, usuario);
                    if (lector != null && lector.EsEntradaLegible(idEntrada))
                    {
                        lector.BorrarLecturaEntrada(idEntrada);
                        entrada.BorrarUsuarioLector(emailUsuarioLector);
                        flag = 0;
                        mensaje = "Se ha desasociado correctamente la lectura de la entrada " + idEntrada + " al usuario " + emailUsuarioLector;
                    }
                    else
                    {
                        flag = 2;
                        mensaje = "Se ha intentado desasociar la lectura de la entrada " + idEntrada + " al usuario invalido " + emailUsuarioLector;
                    }
                }
                else
                {
                    flag = 1;
                    mensaje = "Se ha intentado desasociar la lectura de la entrada " + idEntrada + " al usuario " + emailUsuarioLector + " sin ser su propietario o un administrador.";
                }
            }
            else
                mensaje = "Se ha intentado desasociar la lectura de una entrada a un usuario con algún dato erroneo.";
            if (usuario != null)
                if (entrada != null)
                    CrearLineaLog(mensaje, usuario, entrada);
                else
                    CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return flag;
        }

        /***********************************************************/

        public Int32 CrearLineaLog(String mensaje, Usuario usuario = null, Entrada entrada = null)
        {
            Int32 flag = -1;
            if (mensaje != null && mensaje != "")
            {
                if (usuario != null)
                    if (entrada != null)
                        flag = AñadirLineaLog(mensaje, usuario.Email, entrada.IdEntrada);
                    else
                        flag = AñadirLineaLog(mensaje, usuario.Email);
                else
                    flag = AñadirLineaLog(mensaje);
            }
            return flag;
        }

        public LineaLog RecuperarLineaLog(Usuario usuario, Int32 numeroLinea)
        {
            LineaLog lineaLog = null;
            String mensaje = "";
            if (usuario != null)
            {
                if (usuario.EsAdmin())
                {
                    lineaLog = BuscarLineaLog(numeroLinea, usuario);
                    if ( lineaLog == null)
                        mensaje = "Se ha intentado recuperar la linea de log " + numeroLinea + " pero no existe.";
                }
                else
                    mensaje = "El usuario " + usuario.Email + " ha intentado recuperar el log sin los permisos necesarios.";
            }
            else
                mensaje = "Se ha intentado recuperar el log con datos invalidos";
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return lineaLog;
        }

        public LineaLog RecuperarUltimaLineaLog(Usuario usuario)
        {
            return RecuperarLineaLog(usuario, IdLineaLog - 1);
        }

        public String LeerLineaLog(Usuario usuario, Int32 numeroLinea)
        {
            String log = "";
            String mensaje = "";
            if (usuario != null)
            {
                if (usuario.EsAdmin())
                {
                    LineaLog lineaLog = BuscarLineaLog(numeroLinea, usuario);
                    if (lineaLog != null)
                        log = lineaLog.ToString();
                    else
                        mensaje = "Se ha intentado leer la linea de log " + numeroLinea + " pero no existe.";
                }
                else
                    mensaje = "El usuario " + usuario.Email + " ha intentado leer el log sin los permisos necesarios.";
            }
            else
                mensaje = "Se ha intentado leer el log con datos invalidos";
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return log;
        }

        public String LeerUltimaLineaLog(Usuario usuario)
        {
            return LeerLineaLog(usuario, IdLineaLog-1);
        }

        public String LeerLineasLogUsuario(Usuario usuario, String emailUsuario)
        {
            String log = "";
            String mensaje = "";
            if (usuario != null && emailUsuario != null && emailUsuario != "")
            {
                if (usuario.EsAdmin())
                {
                    foreach (KeyValuePair<int, LineaLog> lineaLog in RecuperarDiccionarioLogs(usuario))
                    {
                        if (lineaLog.Value.EmailUsuario == emailUsuario)
                            log += lineaLog.Key.ToString() + " : " + lineaLog.Value.ToString();
                    }
                }
                else
                    mensaje = "El usuario " + usuario.Email + " ha intentado leer el log sin los permisos necesarios.";
            }
            else
                mensaje = "Se ha intentado leer el log con datos invalidos";
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return log;
        }

        public String LeerLogCompleto(Usuario usuario)
        {
            String log = "";
            String mensaje = "";
            if (usuario != null)
            {
                if (usuario.EsAdmin())
                {
                    foreach (KeyValuePair<int, LineaLog> lineaLog in RecuperarDiccionarioLogs(usuario))
                    {
                        log += lineaLog.Key.ToString() + " : " + lineaLog.Value.ToString() + "\r\n";
                    }
                }
                else
                    mensaje = "El usuario " + usuario.Email + " ha intentado leer el log sin los permisos necesarios.";
            }
            else
                mensaje = "Se ha intentado leer el log con datos invalidos";
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return log;
        }

        public String LeerLogEntreFechas(Usuario usuario, DateTime fechaInicio, DateTime fechaFin)
        {
            String log = "";
            String mensaje = "";
            if (usuario != null && fechaInicio != null && fechaFin != null && fechaFin > fechaInicio)
            {
                if (usuario.EsAdmin())
                {
                    foreach (KeyValuePair<int, LineaLog> lineaLog in RecuperarDiccionarioLogs(usuario))
                    {
                        if (lineaLog.Value.Fecha >= fechaInicio && lineaLog.Value.Fecha <= fechaFin)
                            log += lineaLog.Key.ToString() + " : " + lineaLog.Value.ToString();
                    }
                }
                else
                    mensaje = "El usuario " + usuario.Email + " ha intentado leer el log sin los permisos necesarios.";
            }
            else
                mensaje = "Se ha intentado leer el log con datos invalidos";
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return log;
        }

        public String LeerLogDesdeFecha(Usuario usuario, DateTime fechaInicio)
        {
            return LeerLogEntreFechas(usuario, fechaInicio, DateTime.Now);
        }
    
        public Int32 ObtenerCantidadLineasLog(Usuario usuario)
        {
            Int32 cantidad = -1;
            String mensaje = "";
            if (usuario != null)
            {
                if (usuario.EsAdmin())
                {
                    cantidad = RecuperarDiccionarioLogs(usuario).Count();
                }
                else
                    mensaje = "El usuario " + usuario.Email + " ha intentado leer el log sin los permisos necesarios.";
            }
            else
                mensaje = "Se ha intentado leer el log con datos invalidos";
            if (usuario != null)
                CrearLineaLog(mensaje, usuario);
            else
                CrearLineaLog(mensaje);
            return cantidad;
        }
    }
}

   
