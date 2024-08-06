using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace CalculatorAppiumTesting
{
    public class CalculatorTest

    {
        private AndroidDriver driver;   
        private AppiumLocalService service; 

        [SetUp]
        public void Setup()
        {
            service  = new AppiumServiceBuilder()
                     .WithIPAddress ("127.0.0.1")  
                     .UsingPort(4723)
                     .Build ();

            AppiumOptions options = new AppiumOptions();
                          options.App = @"C:\Users\Maria\Desktop\1\com.example.androidappsummator.apk";
                          options.PlatformName = "Android";
                          options.DeviceName = "Pixel 7 API 34";
                          options.AutomationName = "UIAutomator2";

          driver = new AndroidDriver(service,options);    

        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit ();  
            driver.Dispose();
            service.Dispose();

        }


        [Test]
        public void TestValidSummation()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("3");

            var secondInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("5");

            var calcButton = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("8"), "Summation is incorrect");

        }

        [Test]
        public void TestInvalidSummation()
        {
            var firstInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText1"));
            firstInput.Clear();
            firstInput.SendKeys("3");

            var secondInput = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editText2"));
            secondInput.Clear();
            secondInput.SendKeys("");

            var calcButton = driver.FindElement(MobileBy.ClassName("android.widget.Button"));
            calcButton.Click();

            var result = driver.FindElement(MobileBy.Id("com.example.androidappsummator:id/editTextSum"));

            Assert.That(result.Text, Is.EqualTo("error"), "Summation is incorrect");

        }
    }
}