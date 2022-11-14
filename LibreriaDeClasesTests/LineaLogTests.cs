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
    public class LineaLogTests
    {

        [TestInitialize()]
        public void MyTestInitialize()
        {
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        [TestMethod()]
        public void LineaLogTest()
        {
            String mensaje = "Insertando linea log de prueba.";
            LineaLog lineaLog = new LineaLog(mensaje, 0, "prueba@prueba.com", 1);
            Assert.IsNotNull(lineaLog);
            Assert.AreEqual(lineaLog.IdLineaLog, 0);
            Assert.AreEqual(lineaLog.EmailUsuario, "prueba@prueba.com");
            Assert.AreEqual(lineaLog.IdEntrada, 1);
            Assert.AreEqual(lineaLog.Mensaje, mensaje);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            String mensaje = "Insertando linea log de prueba.";
            LineaLog lineaLog = new LineaLog(mensaje, 0, "prueba@prueba.com", 1);
            Assert.AreNotEqual(lineaLog.ToString(), "");
            Assert.AreEqual(lineaLog.ToString(), "0 : " + lineaLog.Fecha.ToString() + " | Insertando linea log de prueba. | Usuario: prueba@prueba.com | Entrada: 1");
        }
    }
}