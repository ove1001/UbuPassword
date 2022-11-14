using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDatos
{
    public interface ICapaDatos
    {
        Int32 CrearUsuario(Usuario admin, String nombre, String apellidos, String email, String pass, Int32 rol=1);

        Int32 CrearAdmin(Usuario admin, String nombre, String apellidos, String email, String pass);

        Int32 BorrarUsuario(Usuario admin, String emailUsuarioBorrar, bool borrarEntradas = false);

        Int32 BorrarUsuarioYEntradasCreadas(Usuario admin, String emailUsuarioBorrar);

        int ActualizarUsuario(Usuario usuarioModificador, String emailUsuarioAModificar, String nombre, String apellidos, /*String email,*/ String pass, Int32 rol);

        Usuario LoggIn(String email, String pass);

        Int32 ObtenerCantidadUsuarios(Usuario usuario);

        Dictionary<String, Usuario> RecuperarDiccionarioUsuarios(Usuario usuario);

        int CrearEntrada(Usuario usuarioCreador, String usuario, String pass, String descripcion);

        int BorrarEntrada(Usuario usuario, Int32 idEntrada);

        int ActualizarEntrada(Usuario usuarioModificador, Int32 idEntradaAModificar, String usuario, String pass, String descripcion);
       
        Entrada RecuperarEntrada(Usuario usuario, Int32 idEntrada);

        Int32 ObtenerCantidadEntradas(Usuario usuario);

        int AsociarLectorYEntrada(Int32 idEntrada, String emailUsuarioLector, Usuario usuario);

        int DesasociarLectorYEntrrada(Int32 idEntrada, String emailUsuarioLector, Usuario usuario);

        Dictionary<Int32, Entrada> RecuperarDiccionarioEntradas(Usuario usuario);

        Int32 CrearLineaLog(String mensaje, Usuario usuario = null, Entrada entrada = null);

        LineaLog RecuperarLineaLog(Usuario usuario, Int32 numeroLinea);

        LineaLog RecuperarUltimaLineaLog(Usuario usuario);

        String LeerLineaLog(Usuario usuario, Int32 numeroLinea);

        String LeerUltimaLineaLog(Usuario usuario);

        String LeerLineasLogUsuario(Usuario usuario, String emailUsuario);

        String LeerLogCompleto(Usuario usuario);

        String LeerLogEntreFechas(Usuario usuario, DateTime fechaInicio, DateTime fechaFin);

        String LeerLogDesdeFecha(Usuario usuario, DateTime fechaInicio);

        Int32 ObtenerCantidadLineasLog(Usuario usuario);

        Dictionary<Int32, LineaLog> RecuperarDiccionarioLogs(Usuario usuario);

    }

}
