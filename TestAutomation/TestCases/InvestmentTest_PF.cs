using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using POM.Pages;
using POM.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestAutomation.TestCases
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    public class InvestmentTest_PF : BaseTest
    {
        [Test]
        public void investmentTest_PF()
        {
            string expResult = "Failure";
            string loginStatus = "";

            openBrowser("Mozilla");

            if (!DataUtil.isTestRunnable(xls, "CreateTest"))
                Assert.Ignore("Runmode is No");

            LaunchPage lPage = new LaunchPage(driver);
            PageFactory.InitElements(driver, lPage);
            LoginPage loginPage = lPage.goToLoginPage();
            object resultPage = loginPage.doLogin("gunjangarg1604@rediffmail.com", "qtp@1234");
            if (resultPage is LoginPage)
            {
                loginStatus = "Failure";
            }
            else if (resultPage is RegisterPage)
            {
                loginStatus = "Failure";
            }
            Assert.AreEqual(expResult, loginStatus);

            if (loginStatus == "Success")
            {
                MyPortFolioPage myPortFolioPage = (MyPortFolioPage)resultPage;

                myPortFolioPage.selectPortFolio("PortA");
                myPortFolioPage.addStock("Reliance Industries Ltd", "13/02/2016", "200", "500");
                myPortFolioPage.verifyPortFolio("PortA");

                //check if stock exists
                bool result = myPortFolioPage.verifyStock("Reliance Industries Ltd.");
                Assert.IsTrue(result, "Stock not found - Reliance Industries Ltd.");
                Console.WriteLine(result);

                Thread.Sleep(3000);
                myPortFolioPage.getMenu().logout();
            }
        }

        [TearDown]
        public void quit()
        {
            if (driver != null)
                driver.Quit();
        }
    }
}
