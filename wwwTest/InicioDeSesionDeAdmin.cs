using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace SeleniumTests
{
    [TestClass]
    public class InicioDeSesionDeAdmin
    {
        private static List<IWebDriver> drivers;
        //private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private static string baseURL;
        private bool acceptNextAlert = true;
        
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            drivers = new List<IWebDriver>();
            drivers.Add(new EdgeDriver());
            drivers.Add(new ChromeDriver());
            //driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
        }
        
        
        [TestInitialize]
        public void InitializeTest()
        {
            verificationErrors = new StringBuilder();
        }
        
        [TestCleanup]
        public void CleanupTest()
        {
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [TestMethod]
        public void TheInicioDeSesionDeAdminTest()
        {
            foreach (var driver in drivers)
            {
                driver.Navigate().GoToUrl("https://localhost:44379/Inicio.aspx");
                try
                {
                    Assert.AreEqual("UbuPassword", driver.FindElement(By.Id("LblUbuPassword")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("TxtBx_usuario")).Click();
                driver.FindElement(By.Id("TxtBx_usuario")).Clear();
                driver.FindElement(By.Id("TxtBx_usuario")).SendKeys("admin@admin.com");
                driver.FindElement(By.Id("TxtBx_pass")).Click();
                driver.FindElement(By.Id("TxtBx_pass")).Clear();
                driver.FindElement(By.Id("TxtBx_pass")).SendKeys("admin");
                driver.FindElement(By.Id("BtnAceptar")).Click();
                try
                {
                    Assert.AreEqual("Panel de gestor", driver.FindElement(By.Id("Lbl_panel_gestor")).Text);
                }
                catch (Exception e)
                {
                    verificationErrors.Append(e.Message);
                }
                driver.FindElement(By.Id("Btn_cerrar_sesion")).Click();
                driver.Close();
            }
        }
    }
}
