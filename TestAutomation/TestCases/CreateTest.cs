using ExcelReader;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
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
    public class CreateTest : BaseTest
    {
        [Test, TestCaseSource("getData")]
        public void createTest(string runmode, string browser, string portfolioName)
        {
            if (!DataUtil.isTestRunnable(xls, "CreateTest") || runmode.Equals("N"))
                Assert.Ignore("Runmode is No");

            openBrowser(browser);

            LaunchPage lPage = new LaunchPage(driver);
            PageFactory.InitElements(driver, lPage);
            LoginPage loginPage = lPage.goToLoginPage();
            object resultPage = loginPage.doLogin("gunjangarg1604@rediffmail.com", "qtp@1234");
            if (resultPage is LoginPage)
            {
                Assert.Fail("Could not login");
            }

            MyPortFolioPage myPortFolioPage = (MyPortFolioPage)resultPage;
            myPortFolioPage.createNewPortFolio(portfolioName);

            bool res = myPortFolioPage.verifyNewPortFolio(portfolioName);
            Assert.IsTrue(res, "Value not updated in the dropdown");

        }

        public static object[] getData()
        {
            return DataUtil.getData(xls, "CreateTest");
        }

        [TearDown]
        public void quit()
        {
            if (driver != null)
                driver.Quit();
        }
    }
}
