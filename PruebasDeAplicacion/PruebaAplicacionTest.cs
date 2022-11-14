using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaDeClases;
using System.Runtime.InteropServices;

namespace DBDatos.Tests
{
    [TestClass()]
    public class PruebasAplicaciontest
    {
        DBPruebas bd;
        Usuario admin;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            bd = new DBPruebas();
            admin = bd.LoggIn("admin@admin.com", "admin");
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }
        [TestMethod()]
        public void PruebasAplicacionTest()
        {
            /*Creamos un usuario normal sin privilegios de administrador*/
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            /*Probamos que el usuario creado "marcos" se ha logeado bien y no es admin*/
            Assert.IsNotNull(marcos);
            Assert.AreEqual(7, bd.ObtenerCantidadLineasLog(admin));
            Assert.IsFalse(marcos.EsAdmin());
            Assert.AreEqual("marcos@guzman.com", marcos.Email);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(10, bd.ObtenerCantidadLineasLog(admin));
            /*Comprobamos que la contraseña del usuario marcos se ha guardado encriptada*/
            Assert.AreNotEqual("Cd2-", marcos.Pass);
            Assert.AreEqual(Utilidades.Encriptar("Cd2-"), marcos.Pass);
            /*Creamos la entrada para ese usuario*/
            bd.CrearEntrada(marcos, "marcos", "Cdw,", "Este es un mensaje creado por marcos");
            Entrada entradaMarcos = bd.RecuperarEntrada(marcos, marcos.ListaEntradasCreadas.Last());
            /*Comprobamos que se ha creado la entrda bien*/
            Assert.IsNotNull(entradaMarcos);
            Assert.AreNotEqual("", entradaMarcos);
            Assert.AreEqual("marcos", entradaMarcos.Usuario);
            Assert.AreEqual("Cdw,", entradaMarcos.Pass);
            Assert.AreEqual("Este es un mensaje creado por marcos", entradaMarcos.Descripcion);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(16, bd.ObtenerCantidadLineasLog(admin));
            /*Actualizamos la entrada del usuario Marcos con el propio usuario*/
            bd.ActualizarEntrada(marcos, marcos.ListaEntradasCreadas.Last(), "marcos2", "DGK2.", "Este mensaje ha sido modificado por el propietario");
            entradaMarcos = bd.RecuperarEntrada(marcos, marcos.ListaEntradasCreadas.Last());
            /*Comprobamos que se ha actualizado correctamente la entrada*/
            Assert.AreEqual("DGK2.", entradaMarcos.Pass);
            Assert.AreEqual(marcos.ListaEntradasCreadas.Last(), entradaMarcos.IdEntrada);
            Assert.AreEqual("marcos2", entradaMarcos.Usuario);
            Assert.AreEqual("Este mensaje ha sido modificado por el propietario", entradaMarcos.Descripcion);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(23, bd.ObtenerCantidadLineasLog(admin));
            /*Actualizamos la entrada del usuario Marcos con el administrador*/
            /*Se crea la entrada al menos con los siguientes parametros:
             * descripcion, usuario, contraseña*/
            bd.ActualizarEntrada(admin, marcos.ListaEntradasCreadas.Last(), "marcos", "Cdw,", "Este mensaje ha sido modificado por el administrador");
            entradaMarcos = bd.RecuperarEntrada(marcos, marcos.ListaEntradasCreadas.Last());
            /*Comprobamos que se ha actualizado correctamente la entrada*/
            Assert.AreEqual("Cdw,", entradaMarcos.Pass);
            Assert.AreEqual(marcos.ListaEntradasCreadas.Last(), entradaMarcos.IdEntrada);
            Assert.AreEqual("marcos", entradaMarcos.Usuario);
            Assert.AreEqual("Este mensaje ha sido modificado por el administrador", entradaMarcos.Descripcion);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(30, bd.ObtenerCantidadLineasLog(admin));
            /*Actualizamos el usuario Marcos con el propio usuario*/
            bd.ActualizarUsuario(marcos, marcos.Email, "hola", "hola", "Jk4.", 1);
            /*Comprobamos que se ha actualizado correctamente el usuario marcos*/
            Assert.AreEqual("hola", marcos.Nombre);
            Assert.AreEqual("hola", marcos.Apellidos);
            Assert.AreEqual(Utilidades.Encriptar("Jk4."), marcos.Pass);
            Assert.AreEqual(1, marcos.Rol);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(35, bd.ObtenerCantidadLineasLog(admin));
            /*Actualizamos el usuario Marcos con el administrador*/
            bd.ActualizarUsuario(admin, marcos.Email, "marcos", "guzman", "Cd2-", 0);
            /*Comprobamos que se ha actualizado correctamente el usuario marcos*/
            Assert.AreEqual("marcos", marcos.Nombre);
            Assert.AreEqual("guzman", marcos.Apellidos);
            Assert.AreEqual(Utilidades.Encriptar("Cd2-"), marcos.Pass);
            Assert.AreEqual(0, marcos.Rol);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(40, bd.ObtenerCantidadLineasLog(admin));
            /*Comprobamos que no se puede crear una entrada con la contraseña del propio usuario*/
            Assert.AreNotEqual(0, bd.CrearEntrada(marcos, "marcos", "Cd2-", "Este es un mensaje creado por marcos"));
            /*Comprobamos que no se ha creado la entrada en la lista de entradas creadas por el usuario
             y comprobamos que no se ha creado una nueva entrada*/
            Assert.AreEqual(1, marcos.ListaEntradasCreadas.Count);
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(43, bd.ObtenerCantidadLineasLog(admin));
            /*Borramos la entrada y el usuario*/
            bd.BorrarUsuarioYEntradasCreadas(admin, "marcos@guzman.com");
            /*Comprobamos que se ha borrado el usuario y sus entradas correspondientes*/
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(51, bd.ObtenerCantidadLineasLog(admin));
            /*Comprobamos que el administrador puede crear entradas*/
            bd.CrearEntrada(admin, admin.Nombre, "Hj7,", "Esta entrada la ha creado el administrador");
            Entrada entradaAdmin = bd.RecuperarEntrada(admin, admin.ListaEntradasCreadas.Last());
            /*Comprobamos que la entrada ha sido creada por el administrador correctamente*/
            Assert.AreEqual(admin.Nombre, entradaAdmin.Usuario);
            Assert.AreEqual("Hj7,", entradaAdmin.Pass);
            Assert.AreEqual("Esta entrada la ha creado el administrador", entradaAdmin.Descripcion);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            /*Comprobamos que el sistema almacena las entradas de log correctamente*/
            Assert.AreEqual(57, bd.ObtenerCantidadLineasLog(admin));
        }
    }
}
    

