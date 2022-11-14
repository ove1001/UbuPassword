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
    public class EntradaTests
    {
        Entrada entrada1;
        Entrada entrada2;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            entrada1 = new Entrada(1, "oscar", "Ab1.", "Hola que tal", "oscar@valverde.com");
            entrada2 = new Entrada(2, "marcos", "Cd2-", "Hola que tal", "marcos@guzman.com");
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        [TestMethod()]
        public void EntradaTest()
        {
            Entrada entradaPrueba = new Entrada(3, "prueba", "Ab1.", "prueba", "prueba@prueba.com");
            Assert.IsInstanceOfType(entradaPrueba, typeof(Entrada));
            Assert.AreEqual(entradaPrueba.IdEntrada, 3);
            Assert.AreEqual(entradaPrueba.Usuario, "prueba");
            Assert.AreEqual(entradaPrueba.Pass, "Ab1.");
            Assert.AreEqual(entradaPrueba.Descripcion, "prueba");
            Assert.AreEqual(entradaPrueba.EmailUsuarioCreador, "prueba@prueba.com");
            Assert.IsNotNull(entradaPrueba.ListaEmailsUsuariosLectores);
            Assert.AreEqual(entradaPrueba.ListaEmailsUsuariosLectores.LongCount(), 0);
        }

        [TestMethod()]
        public void AñadirUsuarioLectorTest()
        {
            /* añadimos el usuarios a lista de Email Usuarios Lectores */
            entrada1.AñadirUsuarioLector("marcos@guzman.com");
            entrada2.AñadirUsuarioLector("oscar@valverde.com");
            /*Comprobamos que oscar no se puede añadir a si mismo en la lista de usuarios lectores suya */
            Assert.IsFalse(entrada1.EsLector("oscar@valverde.com"));
            /*Comprobamos que oscar puede añadir a marcos en su lista de usuarios lectores*/
            Assert.IsTrue(entrada1.EsLector("marcos@guzman.com"));
            /*Comprobamos que marcos no se puede añadri a si mismo en la lista de usuarios lectores suya */
            Assert.IsFalse(entrada2.EsLector("marcos@guzman.com"));
            /*Comprobamos que marcos puede añadir a oscar en su lista de usuarios lectores*/
            Assert.IsTrue(entrada2.EsLector("oscar@valverde.com"));
        }

        [TestMethod()]
        public void BorrarUsuarioLectorTest()
        {
            Assert.IsFalse(entrada1.EsLector("oscar@valverde.com"));
            Assert.IsFalse(entrada1.EsLector("marcos@guzman.com"));
            /*Se borra el usuario marcos de la lista de usuarios lectores de la entrada de oscar */
            entrada1.AñadirUsuarioLector("oscar@valverde.com");
            entrada1.BorrarUsuarioLector("marcos@guzman.com");
            /*EL CREADOR NO SE PUEDE AÑADIR COMO LECTOR*/
            Assert.IsFalse(entrada1.EsLector("oscar@valverde.com"));
            Assert.IsFalse(entrada1.EsLector("marcos@guzman.com"));

            entrada1.AñadirUsuarioLector("marcos@guzman.com");
            entrada1.BorrarUsuarioLector("oscar@valverde.com");
            /*Se comprueba que la lista de usuarios lectores no contiene a Oscar*/
            Assert.IsTrue(entrada1.EsLector("marcos@guzman.com"));
            Assert.IsFalse(entrada1.EsLector("oscar@valverde.com"));

            entrada1.BorrarUsuarioLector("marcos@guzman.com");
            Assert.IsFalse(entrada1.EsLector("marcos@guzman.com"));
        }

        [TestMethod()]
        public void EsCreadorTest()
        {
            /*Se comprueba si el usuario oscar es creador de la entrada 1*/
            Assert.IsTrue(entrada1.EsCreador("oscar@valverde.com"));
            /*Se comprueba si el usuario oscar es creador de la entrada 2*/
            Assert.IsFalse(entrada1.EsCreador("marcos@guzman.com"));
        }

        [TestMethod()]
        public void EsLectorTest()
        {
            Assert.IsFalse(entrada1.EsLector("oscar@valverde.com"));
            Assert.IsFalse(entrada1.EsLector("marcos@guzman.com"));
            /*Se borra el usuario marcos de la lista de usuarios lectores de oscar */
            entrada1.AñadirUsuarioLector("marcos@guzman.com");
            /*Se comprueba que el usuario marcos es lector de la entrada de oscar*/
            Assert.IsTrue(entrada1.EsLector("marcos@guzman.com"));
            /*Se comprueba que el usuario oscar no puede ser lector de su propia entrada*/
            Assert.IsFalse(entrada1.EsLector("oscar@valverde.com"));
        }

        [TestMethod()]
        public void UsuariosLectoresTest()
        {
            Assert.IsFalse(entrada1.EsLector("oscar@valverde.com"));
            Assert.IsFalse(entrada1.EsLector("marcos@guzman.com"));
            Assert.AreEqual("", entrada1.UsuariosLectores());
            /*Se borra el usuario marcos de la lista de usuarios lectores de oscar */
            entrada1.AñadirUsuarioLector("marcos@guzman.com");
            /*Se comprueba que el usuario marcos es lector de la entrada de oscar*/
            Assert.IsTrue(entrada1.EsLector("marcos@guzman.com"));
            /*Se comprueba que el usuario oscar no puede ser lector de su propia entrada*/
            Assert.IsFalse(entrada1.EsLector("oscar@valverde.com"));
            Assert.AreNotEqual("", entrada1.UsuariosLectores());
        }

        [TestMethod()]
        public void CloneTest()
        {
            /* Compruebo si es igual la entrada1 que el clon del entrada1 */
            Entrada entrada = (Entrada) entrada1.Clone();
            Assert.IsFalse(entrada == entrada1);
            Assert.AreEqual(entrada.Usuario , entrada1.Usuario);
            Assert.AreEqual(entrada.Pass , entrada1.Pass);
            Assert.AreEqual(entrada.IdEntrada , entrada1.IdEntrada);
            Assert.AreEqual(entrada.ListaEmailsUsuariosLectores , entrada1.ListaEmailsUsuariosLectores);
            Assert.AreEqual(entrada.ListaEmailsUsuariosLectores.LongCount() , entrada1.ListaEmailsUsuariosLectores.LongCount());
        }
    }
}