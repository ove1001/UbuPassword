using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibreriaDeClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaDeClases.Tests
{
    [TestClass()]
    public class UsuarioTests
    {
        Usuario usuarioAdmin;
        Usuario usuarioNormal;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            usuarioAdmin = new Usuario(1, "oscar", "valverde", "oscar@valverde.com", "Ab1.", 0);
            usuarioNormal = new Usuario(2, "marcos", "guzman", "marcos@guzman.com", "Cd2-", 1);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }


        [TestMethod()]
        public void UsuarioTest()
        {
            Usuario usuario = new Usuario(3, "prueba", "prueba", "prueba@prueba.com", "Ab1.", 0);
            Assert.IsInstanceOfType(usuario, typeof(Usuario));
            Assert.IsNotNull(usuario);
            Assert.AreEqual(usuario.IdUsuario, 3);
            Assert.AreEqual(usuario.Nombre, "prueba");
            Assert.AreEqual(usuario.Apellidos, "prueba");
            Assert.AreEqual(usuario.Email, "prueba@prueba.com");
            Assert.AreEqual(usuario.Pass, Utilidades.Encriptar("Ab1."));
            Assert.AreEqual(usuario.Rol, 0);
            Assert.IsNotNull(usuario.ListaEntradasCreadas);
            Assert.AreEqual(usuario.ListaEntradasCreadas.LongCount(),0);
            Assert.IsNotNull(usuario.ListaEntradasLegibles);
            Assert.AreEqual(usuario.ListaEntradasLegibles.LongCount(), 0);
        }

        [TestMethod()]
        public void EsAdminTest()
        {
            Assert.IsTrue(usuarioAdmin.EsAdmin());
            Assert.IsFalse(usuarioNormal.EsAdmin());
        }

        [TestMethod()]
        public void ComprobarPassTest()
        {
            Assert.IsTrue(usuarioAdmin.ComprobarPass("Ab1."));
            Assert.IsFalse(usuarioAdmin.ComprobarPass("Cd2-"));
            Assert.IsTrue(usuarioNormal.ComprobarPass("Cd2-"));
            Assert.IsFalse(usuarioNormal.ComprobarPass("Ab1."));
        }

        [TestMethod()]
        public void EsEntradaPropiaTest()
        {
            usuarioAdmin.AñadirPropiaEntrada(0);
            Assert.IsTrue(usuarioAdmin.EsEntradaPropia(0));
            usuarioAdmin.AñadirLecturaEntrada(1);
            Assert.IsFalse(usuarioAdmin.EsEntradaPropia(1));
        }

        [TestMethod()]
        public void EsEntradaLegibleTest()
        {
            usuarioNormal.AñadirPropiaEntrada(0);
            Assert.IsFalse(usuarioNormal.EsEntradaLegible(0));
            usuarioNormal.AñadirLecturaEntrada(1);
            Assert.IsTrue(usuarioNormal.EsEntradaLegible(1));
        }

        [TestMethod()]
        public void AñadirPropiaEntradaTest()
        {
            usuarioAdmin.AñadirPropiaEntrada(0);
            Assert.IsTrue(usuarioAdmin.EsEntradaPropia(0));
            usuarioAdmin.AñadirLecturaEntrada(1);
            Assert.IsFalse(usuarioAdmin.EsEntradaPropia(1));
        }

        [TestMethod()]
        public void BorrarPropiaEntradaTest()
        {
            usuarioAdmin.AñadirPropiaEntrada(0);
            Assert.IsTrue(usuarioAdmin.EsEntradaPropia(0));
            usuarioAdmin.BorrarPropiaEntrada(0);
            Assert.IsFalse(usuarioAdmin.EsEntradaPropia(0));
        }

        [TestMethod()]
        public void AñadirLecturaEntradaTest()
        {
            usuarioNormal.AñadirLecturaEntrada(0);
            Assert.IsTrue(usuarioNormal.EsEntradaLegible(0));
            usuarioNormal.AñadirPropiaEntrada(1);
            Assert.IsFalse(usuarioNormal.EsEntradaLegible(1));
        }

        [TestMethod()]
        public void BorrarLecturaEntradaTest()
        {
            usuarioNormal.AñadirLecturaEntrada(0);
            Assert.IsTrue(usuarioNormal.EsEntradaLegible(0));
            usuarioNormal.BorrarLecturaEntrada(0);
            Assert.IsFalse(usuarioNormal.EsEntradaLegible(0));
        }

        [TestMethod()]
        public void CloneTest()
        {
            Usuario usuario = (Usuario) usuarioAdmin.Clone();
            Assert.IsFalse(usuario == usuarioAdmin);
            Assert.AreEqual(usuario.IdUsuario, usuarioAdmin.IdUsuario);
            Assert.AreEqual(usuario.Nombre , usuarioAdmin.Nombre);
            Assert.AreEqual(usuario.Apellidos, usuarioAdmin.Apellidos);
            Assert.AreEqual(usuario.Email, usuarioAdmin.Email);
            Assert.AreEqual(usuario.Pass, usuarioAdmin.Pass);
            Assert.AreEqual(usuario.Rol, usuarioAdmin.Rol);
            Assert.AreEqual(usuario.ListaEntradasCreadas.LongCount(), usuarioAdmin.ListaEntradasCreadas.LongCount());
            Assert.AreEqual(usuario.ListaEntradasLegibles.LongCount(), usuarioAdmin.ListaEntradasLegibles.LongCount());
        }
    }
}