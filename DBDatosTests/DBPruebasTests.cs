using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaDeClases;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace DBDatos.Tests
{
    [TestClass()]
    public class DBPruebasTests
    {
        ICapaDatos bd;
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
        public void DBPruebasTest()
        {
            bd = new DBPruebas();
            Assert.IsNotNull(bd);
            Usuario admin = bd.LoggIn("admin@admin.com", "admin");
            Assert.IsNotNull(admin);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void CrearUsuarioTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "usuario@usuario.com", "Ef3,", 1);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(9, bd.ObtenerCantidadLineasLog(admin));
            Usuario usuario = bd.LoggIn("usuario@usuario.com", "Ef3,");
            Assert.IsNotNull(usuario);
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            Assert.IsFalse(usuario.EsAdmin());
            Assert.AreEqual(Utilidades.Encriptar("Ef3,"), usuario.Pass);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void CrearUsuarioTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "", "usuario", "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(9, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, null, "usuario", "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "", "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", null, "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(18, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(21, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", null, "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(24, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "usuario", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(27, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "usuario@usuario.com", "");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(30, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "usuario@usuario.com", null);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(33, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void CrearAdminTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "usuario", "usuario", "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(9, bd.ObtenerCantidadLineasLog(admin));
            Usuario usuario = bd.LoggIn("usuario@usuario.com", "Ef3,");
            Assert.IsNotNull(usuario);
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            Assert.IsTrue(usuario.EsAdmin());
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(usuario));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(usuario));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(usuario));
        }

        [TestMethod()]
        public void CrearAdminTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "", "usuario", "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(9, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, null, "usuario", "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "usuario", "", "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "usuario", null, "usuario@usuario.com", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(18, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "usuario", "usuario", "", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(21, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "usuario", "usuario", null, "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(24, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "usuario", "usuario", "usuario", "Ef3,");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(27, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "usuario", "usuario", "usuario@usuario.com", "");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(30, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearAdmin(admin, "usuario", "usuario", "usuario@usuario.com", null);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(33, bd.ObtenerCantidadLineasLog(admin));
            //Console.Write(bd.LeerLogCompleto(admin));
        }

        [TestMethod()]
        public void BorrarUsuarioTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "usuario@usuario.com", "Ef3,", 1);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(9, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "admin2", "admin2", "admin2@admin2.com", "Gh4-", 0);
            Assert.AreEqual(3, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuario(admin, "usuario@usuario.com");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(16, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuario(admin, "admin2@admin2.com");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(20, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void BorrarUsuarioTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "usuario@usuario.com", "Ef3,", 1);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(9, bd.ObtenerCantidadLineasLog(admin));
            Usuario usuario = bd.LoggIn("usuario@usuario.com", "Ef3,");
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario2", "usuario", "usuario2@usuario2.com", "Ef3,", 1);
            Assert.AreEqual(3, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "admin2", "admin2", "admin2@admin2.com", "Gh4-", 0);
            Assert.AreEqual(4, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(18, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuario(usuario, "usuario2@usuario2.com");
            Assert.AreEqual(4, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(21, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuario(usuario, "admin2@admin2.com");
            Assert.AreEqual(4, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(24, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuario(admin, "usuario2@admin2.com");
            Assert.AreEqual(4, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(28, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuario(admin, "admin2@usuario2.com");
            Assert.AreEqual(4, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(32, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void BorrarUsuarioYEntradasCreadasTestOK()
        {
            
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "usuario@usuario.com", "Ef3,", 1);
            Usuario usuario = bd.LoggIn("usuario@usuario.com", "Ef3,");
            bd.CrearEntrada(usuario, "usuario", "usuario", "Entrada prueba");
            Assert.IsNotNull(bd.RecuperarEntrada(usuario, usuario.ListaEntradasCreadas.Last()));
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuarioYEntradasCreadas(admin, "usuario@usuario.com");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(23, bd.ObtenerCantidadLineasLog(admin));
        }
        [TestMethod()]
        public void BorrarUsuarioYEntradasCreadasTestNOK()
        {

            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "usuario", "usuario", "usuario@usuario.com", "Ef3,", 1);
            Usuario usuario = bd.LoggIn("usuario@usuario.com", "Ef3,");
            bd.CrearEntrada(usuario, "usuario", "usuario", "Entrada prueba");
            Assert.IsNotNull(bd.RecuperarEntrada(usuario, usuario.ListaEntradasCreadas.Last()));
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuarioYEntradasCreadas(admin, "usuario@sf.com");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(20, bd.ObtenerCantidadLineasLog(admin));
            bd.BorrarUsuarioYEntradasCreadas(usuario, "usuario@usuario.com");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(24, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void ActualizarUsuarioTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            bd.ActualizarUsuario(admin, marcos.Email, "adolfo", "perez", "Hi3,", 1);
            Assert.AreEqual(marcos.Nombre, "adolfo");
            Assert.AreEqual(marcos.Apellidos, "perez");
            Assert.AreEqual(marcos.Pass, Utilidades.Encriptar("Hi3,"));
            Assert.AreEqual(marcos.Rol, 1);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 12);
            bd.ActualizarUsuario(marcos, marcos.Email, "hola", "hola", "Jk4.", 1);
            Console.Write(bd.LeerLogCompleto(admin));
            Assert.AreEqual(marcos.Nombre, "hola");
            Assert.AreEqual(marcos.Apellidos, "hola");
            Assert.AreEqual(marcos.Pass, Utilidades.Encriptar("Jk4."));
            Assert.AreEqual(marcos.Rol, 1);
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 17);

        }
        [TestMethod()]
        public void ActualizarUsuarioTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            Assert.IsNotNull(marcos);
            Assert.IsNotNull(oscar);
            Assert.AreNotEqual(bd.ActualizarUsuario(oscar, "marcos@guzman.com", "adolfo", "perez", "Hi3,", 1),0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, "marcos@guzman.com", "", "perez",  "Hi3,", 1), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, "marcos@guzman.com", "adolfo", "",  "Hi3,", 1), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, "marcos@guzman.com", "adolfo", "perez",  "", 1), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, "marcos@guzman.com", "adolfo", "perez",  "Hi3,",8), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(null, "marcos@guzman.com", "adolfo", "perez",  "Hi3,", 8), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, null, "adolfo", "perez", "Hi3,", 8), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, "marcos@guzman.com", null, "perez",  "Hi3,", 1), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, "marcos@guzman.com", "adolfo", null,  "Hi3,", 1), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, "marcos@guzman.com", "adolfo", "perez",  null, 1), 0);
            Assert.AreNotEqual(bd.ActualizarUsuario(marcos, "marcos@guzman.com", "adolfo", "perez",  "Hi3,", 8), 0);
            Assert.AreEqual(marcos.Email, "marcos@guzman.com");
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 26);
        }

        [TestMethod()]
        public void LoggInTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 12);

        }
        [TestMethod()]
        public void LoggInTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("", "Cd2-");
            Assert.IsNull(marcos);
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 12);
            marcos = bd.LoggIn("marcos@guzman.com", "");
            Assert.IsNull(marcos);
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 17);
        }

        [TestMethod()]
        public void RecuperarDiccionarioUsuariosTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            Assert.AreEqual(admin, bd.RecuperarDiccionarioUsuarios(admin)["admin@admin.com"]);
        }

        [TestMethod()]
        public void RecuperarDiccionarioUsuariosTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            Assert.AreEqual(admin, bd.RecuperarDiccionarioUsuarios(admin)["admin@admin.com"]);
            Assert.IsNull(bd.RecuperarDiccionarioUsuarios(null));
            Assert.IsNull(bd.RecuperarDiccionarioUsuarios(marcos));
        }

        [TestMethod()]
        public void CrearEntradaTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearEntrada(admin, "marcos", "entrada1", "esta es la primera entrada creada");
            Entrada entradaAdmin = bd.RecuperarEntrada(admin, admin.ListaEntradasCreadas.Last());
            Assert.AreEqual("marcos", entradaAdmin.Usuario);
            Assert.AreEqual("entrada1", entradaAdmin.Pass);
            Assert.AreEqual("esta es la primera entrada creada", entradaAdmin.Descripcion);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
        }
        [TestMethod()]
        public void CrearEntradaTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            bd.CrearEntrada(admin, "", "entrada1", "esta es la primera entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 13);
            bd.CrearEntrada(admin, "marcos", "", "esta es la primera entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 16);
            bd.CrearEntrada(admin, "marcos", "entrada1", "");
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 19);
            bd.CrearEntrada(marcos,"marcos","Cd2-","esta es la primera entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 22);
        }

        [TestMethod()]
        public void BorrarEntradaTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearEntrada(admin, "marcos", "entrada1", "esta es la primera entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 1);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 9);
            bd.BorrarEntrada(admin, admin.ListaEntradasCreadas.Last());
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 0);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 13);

        }
        [TestMethod()]
        public void BorrarEntradaTestNOK()
        {
            /*Creo que esta mal, hay que darle una vuelta*/
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            Usuario marcos = bd.LoggIn("marcos@gmail.com", "Cd2-");
            bd.CrearEntrada(admin, "marcos", "entrada1", "esta es la primera entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 1);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 11);
            bd.BorrarEntrada(marcos, admin.ListaEntradasCreadas.Last());
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 1);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 14);
            bd.BorrarEntrada(admin, 3);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 1);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 18);
        }
        

        [TestMethod()]
        public void ActualizarEntradaTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            bd.CrearEntrada(marcos, "marcos", "Cdw,", "este es un mensaje creado por marcos");
            Entrada entrada1 = bd.RecuperarEntrada(marcos, marcos.ListaEntradasCreadas.Last());
            Assert.IsNotNull(marcos);
            Assert.IsNotNull(marcos.ListaEntradasCreadas);
            Assert.IsNotNull(entrada1);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
            bd.ActualizarEntrada(marcos, marcos.ListaEntradasCreadas.Last(), "marcos2", "DGK2.", "Este mensaje ha sido modificado por el propietario");
            entrada1 = bd.RecuperarEntrada(marcos, marcos.ListaEntradasCreadas.Last());
            Assert.AreEqual(entrada1.Pass, "DGK2.");
            Assert.AreEqual(entrada1.IdEntrada, marcos.ListaEntradasCreadas.Last());
            Assert.AreEqual(entrada1.Usuario, "marcos2");
            Assert.AreEqual(entrada1.Descripcion, "Este mensaje ha sido modificado por el propietario");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(22, bd.ObtenerCantidadLineasLog(admin));
            bd.ActualizarEntrada(admin, marcos.ListaEntradasCreadas.Last(), "marcos3", "DGK2-", "Este mensaje ha sido modificado por un administrador");
            entrada1 = bd.RecuperarEntrada(marcos, marcos.ListaEntradasCreadas.Last());
            Assert.AreEqual(entrada1.Pass, "DGK2-");
            Assert.AreEqual(entrada1.IdEntrada, marcos.ListaEntradasCreadas.Last());
            Assert.AreEqual(entrada1.Usuario, "marcos3");
            Assert.AreEqual(entrada1.Descripcion, "Este mensaje ha sido modificado por un administrador");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(29, bd.ObtenerCantidadLineasLog(admin));
        }
        [TestMethod()]
        public void ActualizarEntradaTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            bd.CrearEntrada(marcos, "marcos", "Cdw,", "este es un mensaje creado por marcos");
            Entrada entrada1 = bd.RecuperarEntrada(marcos, marcos.ListaEntradasCreadas.Last());
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            Assert.IsNotNull(marcos);
            Assert.IsNotNull(marcos.ListaEntradasCreadas);
            Assert.IsNotNull(oscar);
            Assert.IsNotNull(oscar.ListaEntradasCreadas);
            Assert.IsNotNull(entrada1);
            Assert.AreEqual(3, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(18, bd.ObtenerCantidadLineasLog(admin));
            Assert.AreNotEqual(0, bd.ActualizarEntrada(oscar, marcos.ListaEntradasCreadas.Last(), "marcos2", "DGK2.", "Este mensaje ha sido modificado por el propietario"));
            Assert.AreNotEqual(0, bd.ActualizarEntrada(marcos, marcos.ListaEntradasCreadas.Last(), "", "DGK2.", "Este mensaje ha sido modificado por el propietario"));
            Assert.AreNotEqual(0, bd.ActualizarEntrada(marcos, marcos.ListaEntradasCreadas.Last(), "marcos2", "", "Este mensaje ha sido modificado por el propietario"));
            Assert.AreNotEqual(0, bd.ActualizarEntrada(marcos, marcos.ListaEntradasCreadas.Last(), "marcos2", "DGK2.", ""));
            Assert.AreNotEqual(0, bd.ActualizarEntrada(null, marcos.ListaEntradasCreadas.Last(), "", "DGK2.", "Este mensaje ha sido modificado por el propietario"));
            Assert.AreNotEqual(0, bd.ActualizarEntrada(marcos, marcos.ListaEntradasCreadas.Last(), null, "DGK2.", "Este mensaje ha sido modificado por el propietario"));
            Assert.AreNotEqual(0, bd.ActualizarEntrada(marcos, marcos.ListaEntradasCreadas.Last(), "marcos2", null, "Este mensaje ha sido modificado por el propietario"));
            Assert.AreNotEqual(0, bd.ActualizarEntrada(marcos, marcos.ListaEntradasCreadas.Last(), "marcos2", "DGK2.", null));
            Assert.AreEqual(3, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(29, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void RecuperarEntradaTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearEntrada(admin, "marcos", "entrada1", "esta es la primera entrada creada");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(10, bd.ObtenerCantidadLineasLog(admin));
            Entrada entrada = bd.RecuperarEntrada(admin, admin.ListaEntradasCreadas.Last());
            Assert.AreEqual(bd.RecuperarEntrada(admin, admin.ListaEntradasCreadas.Last()), entrada);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(17, bd.ObtenerCantidadLineasLog(admin));            
        }

        [TestMethod()]
        public void RecuperarDiccionarioEntradasTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearEntrada(admin, "marcos", "entrada1", "esta es la primera entrada creada");
            Entrada entrada = bd.RecuperarEntrada(admin, admin.ListaEntradasCreadas.Last());
            Assert.IsNotNull(entrada);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(18, bd.ObtenerCantidadLineasLog(admin));
            Assert.AreEqual(entrada, bd.RecuperarDiccionarioEntradas(admin)[0]);
        }

        [TestMethod()]
        public void RecuperarDiccionarioEntradasTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearEntrada(admin, "marcos", "entrada1", "esta es la primera entrada creada");
            Entrada entrada = bd.RecuperarEntrada(admin, admin.ListaEntradasCreadas.Last());
            Assert.IsNotNull(entrada);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(18, bd.ObtenerCantidadLineasLog(admin));
            Assert.IsNull(bd.RecuperarDiccionarioEntradas(null));
            Assert.IsNull(bd.RecuperarDiccionarioEntradas(marcos));
        }


        [TestMethod()]
        public void RecuperarEntradaTestNOK()
        {
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(9, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearEntrada(admin, "marcos", "entrada1", "esta es la primera entrada creada");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(13, bd.ObtenerCantidadLineasLog(admin));
            Entrada entrada = bd.RecuperarEntrada(admin, 3);
            Assert.IsNull(entrada);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(18, bd.ObtenerCantidadLineasLog(admin));
            Entrada entrada2 = bd.RecuperarEntrada(marcos, admin.ListaEntradasCreadas.Last());
            Assert.IsNull(entrada2);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(23, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void AsociarLectorYEntradaTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            Assert.IsNotNull(oscar);
            bd.CrearEntrada(oscar, "oscar", "entrada1", "esta es la primera entrada creada");
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            bd.CrearEntrada(marcos, "marcos", "entrada2", "esta es la segunda entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 17);

            bd.AsociarLectorYEntrada(1,"oscar@valverde.com", marcos);
            Assert.IsTrue(oscar.ListaEntradasLegibles.Contains(marcos.ListaEntradasCreadas.Last()));
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 23);
        }
        [TestMethod()]
        public void AsociarLectorYEntradaTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            Assert.IsNotNull(oscar);
            bd.CrearEntrada(oscar, "oscar", "entrada1", "esta es la primera entrada creada");
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            bd.CrearEntrada(marcos, "marcos", "entrada2", "esta es la segunda entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 17);

            Assert.AreNotEqual(bd.AsociarLectorYEntrada(4, "oscar@valverde.com", marcos),0);
            Assert.AreNotEqual(bd.AsociarLectorYEntrada(0, "osr@valverde.com", marcos), 0);
            Assert.AreNotEqual(bd.AsociarLectorYEntrada(1, "oscar@valverde.com", oscar), 0);
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 28);
        }

        [TestMethod()]
        public void DesasociarLectorYEntrradaTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            Assert.IsNotNull(oscar);
            bd.CrearEntrada(oscar, "oscar", "entrada1", "esta es la primera entrada creada");
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            bd.CrearEntrada(marcos, "marcos", "entrada2", "esta es la segunda entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 17);

            bd.AsociarLectorYEntrada(1, "oscar@valverde.com", marcos);
            Assert.IsTrue(oscar.ListaEntradasLegibles.Contains(marcos.ListaEntradasCreadas.Last()));
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 23);

            bd.DesasociarLectorYEntrrada(marcos.ListaEntradasCreadas.Last(), "oscar@valverde.com", marcos);
            Assert.IsFalse(oscar.ListaEntradasLegibles.Contains(marcos.ListaEntradasCreadas.Last()));
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 29);

        }
        [TestMethod()]
        public void DesasociarLectorYEntrradaTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            Assert.IsNotNull(oscar);
            bd.CrearEntrada(oscar, "oscar", "entrada1", "esta es la primera entrada creada");
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            bd.CrearEntrada(marcos, "marcos", "entrada2", "esta es la segunda entrada creada");
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadEntradas(admin), 2);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 17);

            bd.AsociarLectorYEntrada(1, "oscar@valverde.com", marcos);
            Assert.IsTrue(oscar.ListaEntradasLegibles.Contains(marcos.ListaEntradasCreadas.Last()));
            Assert.AreEqual(bd.ObtenerCantidadUsuarios(admin), 3);
            Assert.AreEqual(bd.ObtenerCantidadLineasLog(admin), 23);

            Assert.AreNotEqual(bd.DesasociarLectorYEntrrada(4, "oscar@valverde.com", marcos), 0);
            Assert.AreNotEqual(bd.DesasociarLectorYEntrrada(1, "osr@valverde.com", marcos), 0);
            Assert.AreNotEqual(bd.DesasociarLectorYEntrrada(1, "oscar@valverde.com", oscar), 0);
        }

        [TestMethod()]
        public void AñadirLineaLogTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            Int32 numerolog = bd.ObtenerCantidadLineasLog(admin);
            bd.CrearLineaLog("Prueba de log.");
            Assert.AreEqual(bd.RecuperarLineaLog(admin , numerolog).Mensaje, "Prueba de log.");
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            numerolog = bd.ObtenerCantidadLineasLog(admin);
            bd.CrearLineaLog("Prueba de log.", marcos);
            Assert.AreEqual(bd.RecuperarLineaLog(admin, numerolog).EmailUsuario, "marcos@guzman.com");
            bd.CrearEntrada(marcos, "marcos", "entrada2", "esta es la segunda entrada creada");
            Entrada entrada = bd.RecuperarEntrada(marcos, marcos.ListaEntradasCreadas.Last());
            Assert.IsNotNull(entrada);
            numerolog = bd.ObtenerCantidadLineasLog(admin);
            bd.CrearLineaLog("Prueba de log.", marcos, entrada);
            Assert.AreEqual(bd.RecuperarLineaLog(admin, numerolog).IdEntrada, entrada.IdEntrada);

        }
        [TestMethod()]
        public void AñadirLineaLogTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            Int32 numerolog = bd.ObtenerCantidadLineasLog(admin);
            bd.CrearLineaLog(null);
            Assert.AreNotEqual(bd.ObtenerCantidadLineasLog(admin), numerolog + 2);
            bd.CrearLineaLog("");
            Assert.AreNotEqual(bd.ObtenerCantidadLineasLog(admin), numerolog + 3);

        }
        /****************************************************************************************************************************
         * 
         * Se comprueban los metodos de leerlog comprobando que no devuelve cadenas vacias o nulas
         * En caso de solicitarlo un administrador, devuelve cadenas vacias en caso de solicitarlo alguien que no sea administrador
         * esto genera una nueva linea 
         * Las pruebas del log se han ido realizando en todas las pruebas.
         * 
         ****************************************************************************************************************************/

        [TestMethod()]
        public void LeerLineaLogTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            String log = bd.LeerLineaLog(admin, bd.RecuperarDiccionarioLogs(admin).Last().Value.IdLineaLog);
            Assert.AreNotEqual("", log);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(11, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void LeerLineaLogTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            String log = bd.LeerLineaLog(admin, 17);
            Assert.AreEqual(log, "");
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            log = bd.LeerLineaLog(marcos, bd.RecuperarDiccionarioLogs(admin).Last().Value.IdLineaLog);
            Assert.AreEqual(log, "");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(16, bd.ObtenerCantidadLineasLog(admin));

        }

        [TestMethod()]
        public void LeerUltimaLineaLogTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            LineaLog lLog = bd.RecuperarUltimaLineaLog(admin);
            Assert.AreEqual(bd.RecuperarLineaLog(admin, 5), lLog);
            Assert.AreEqual(9, bd.ObtenerCantidadLineasLog(admin));
            String strLLog = bd.LeerUltimaLineaLog(admin);
            Assert.AreEqual(bd.LeerLineaLog(admin, 8), strLLog);
        }

        [TestMethod()]
        public void LeerUltimaLineaLogTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            Assert.AreNotEqual(bd.RecuperarUltimaLineaLog(admin), bd.RecuperarUltimaLineaLog(admin));
            Assert.AreNotEqual(bd.LeerUltimaLineaLog(admin), bd.LeerUltimaLineaLog(admin));
        }


        [TestMethod()]
        public void LeerLineasLogUsuarioTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            Assert.IsNotNull(oscar);
            bd.CrearEntrada(oscar, "oscar", "entrada1", "esta es la primera entrada creada");
            String log = bd.LeerLineasLogUsuario(admin, "oscar@valverde.com");
            Assert.IsNotNull(log);
            Assert.AreNotEqual(log, "");
        }
        [TestMethod()]
        public void LeerLineasLogUsuarioTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            Assert.IsNotNull(oscar);
            bd.CrearEntrada(oscar, "oscar", "entrada1", "esta es la primera entrada creada");
            String log = bd.LeerLineasLogUsuario(admin, "oscar@val.com");
            Assert.AreEqual(log, "");
            log = bd.LeerLineasLogUsuario(oscar, "oscar@valverde.com");
            Assert.AreEqual(log, "");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void LeerLogCompletoTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            String log = bd.LeerLogCompleto(admin);
            Assert.IsNotNull(log);
            Assert.AreNotEqual(log, "");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(10, bd.ObtenerCantidadLineasLog(admin));

        }
        [TestMethod()]
        public void LeerLogCompletoTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            String log = bd.LeerLogCompleto(oscar);
            Assert.AreEqual(log, "");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(13, bd.ObtenerCantidadLineasLog(admin));

        }

        [TestMethod()]
        public void LeerLogEntreFechasTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            String log = bd.LeerLogEntreFechas(admin, DateTime.Today, DateTime.Now);
            Console.Write(DateTime.Today);
            Console.Write(DateTime.Now);
            Assert.IsNotNull(log);
            Assert.AreNotEqual(log, "");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(10, bd.ObtenerCantidadLineasLog(admin));
        }
        [TestMethod()]
        public void LeerLogEntreFechasTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            String log = bd.LeerLogEntreFechas(oscar, DateTime.Today, DateTime.Now);
            Assert.AreEqual(log, "");
            DateTime fechaInvalida = new DateTime(1000, 1, 1);
            log = bd.LeerLogEntreFechas(oscar, fechaInvalida, DateTime.Now);
            Assert.AreEqual(log, "");
            log = bd.LeerLogEntreFechas(oscar, DateTime.Today, fechaInvalida);
            Assert.AreEqual(log, "");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(15, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void LeerLogDesdeFechaTestOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            String log = bd.LeerLogDesdeFecha(admin, DateTime.Today);
            Assert.IsNotNull(log);
            Assert.AreNotEqual(log, "");
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(10, bd.ObtenerCantidadLineasLog(admin));
        }
        [TestMethod()]
        public void LeerLogDesdeFechaTestNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 1);
            Usuario oscar = bd.LoggIn("oscar@valverde.com", "Ab1.");
            DateTime fechaInvalida = new DateTime(3000, 1, 1);
            String log = bd.LeerLogDesdeFecha(oscar, DateTime.Today);
            Assert.AreEqual(log, "");
            log = bd.LeerLogDesdeFecha(admin, fechaInvalida);
            Assert.AreEqual(log, "");
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(14, bd.ObtenerCantidadLineasLog(admin));
        }

        [TestMethod()]
        public void RecuperarDiccionarioEntradasLogsOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearUsuario(admin, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
            Usuario marcos = bd.LoggIn("marcos@guzman.com", "Cd2-");
            Assert.IsNotNull(marcos);
            Assert.AreEqual(2, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            Assert.AreEqual(bd.RecuperarLineaLog(admin,10), bd.RecuperarDiccionarioLogs(admin)[10]);
        }

        [TestMethod()]
        public void RecuperarDiccionarioEntradasLogsNOK()
        {
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(0, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(6, bd.ObtenerCantidadLineasLog(admin));
            bd.CrearEntrada(admin, "marcos", "entrada1", "esta es la primera entrada creada");
            Entrada entrada = bd.RecuperarEntrada(admin, admin.ListaEntradasCreadas.Last());
            Assert.IsNotNull(entrada);
            Assert.AreEqual(1, bd.ObtenerCantidadUsuarios(admin));
            Assert.AreEqual(1, bd.ObtenerCantidadEntradas(admin));
            Assert.AreEqual(12, bd.ObtenerCantidadLineasLog(admin));
            Assert.IsNull(bd.RecuperarDiccionarioEntradas(null));
        }


    }
}