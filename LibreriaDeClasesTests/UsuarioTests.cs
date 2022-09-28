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
        [TestMethod()]
        public void UsuarioTest()
        {
            Usuario u = new Usuario(1, "Oscar", "Valverde", "ove1001@alu.ubu.es", "pass");
            Assert.IsNotNull(u);
            Assert.AreEqual(u.Nombre, "Oscar");
            u.Nombre = "Osc";
            Assert.AreEqual(u.Nombre, "Osc");
        }
    }
}