using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaDeClases
{

    internal class DBPruebas
    {
        /*
        * La lista de usuarios se guarda en la base de datos 
        * La lista de entradas se guarda en la base de datos
        * La lista de lineas de log se guardan en la base de datos
        * 
        * Generamos doble enlace para tener un acceso eficiente
        * El Usuario guardará una lista con punteros a los objetos Entrada que ha creado y que tiene acceso
        * La Entrada guardará una lista con punteros a los objetos Usuario creador y lectores
        * 
        */

        //El primer usuario tendrá que estar generado en la base de datos
        //generarlo en el constructor

        private static Int16 idUsuarioActual=0;
        private static Int16 idEntradaActual = 0;

        //private Dictionary diccionarioUsers : key id - value objeto usuario
        //private Dictionary diccionarioEntradas : key id - value objeto entrada

        private static Int16 ObtenerSiguienteIdUsuarioLibre()
        {
            return idUsuarioActual++;
        }

        private static Int16 ObtenerSiguienteIdEntradaLibre()
        {
            return idUsuarioActual++;
        }

        public void crearUsuario(Usuario Admin, short idUsuario, string nombre, string apellidos, string email, string passwd, short rol)
        {

        }

        public void BorrarUsuario(Usuario Admin, short idUsuario, string nombre, string apellidos, string email, string passwd, short rol)
        {

        }

        public void BuscarUsuario()
        {

        }

        //factopria para crear entradas
        public Entrada CrearEntrada(Usuario Creador /*datos*/)
        {
            Entrada entrada = new Entrada();

            return entrada;
        }


        public void BorrarEntrada(Usuario Creador, Entrada entrada)
        {

        }

        /* No se pueden buscra entyradas
         * Solo se puede acceder a las entradas a través del acceso 
         * que tienen los usuarios al ser creadores o lectores
         * Todo el flujo se maneja por doble enlace
        public void BuscarEntrada()
        {

        }
        */


        public void AñadirLineaLog(String linea)
        {


        }
    }


   
