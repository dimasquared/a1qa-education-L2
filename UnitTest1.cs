using Aquality.Selenium.Browsers;
using Task2Stage2.Pages;

namespace Task2Stage2;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test, Order(1)]
    public void TestCase1()
    {
        
        AqualityServices.Browser.GoTo("https://userinyerface.com/");
        AqualityServices.Browser.Maximize();
        var welcomePage = new WelcomePage();
        Assert.IsTrue(welcomePage.State.WaitForDisplayed(), "Welcome Page is not opened");
        
        welcomePage.ClickHereButton();
        var firstCardPage = new FirstCardPage();
        Assert.IsTrue(firstCardPage.State.WaitForDisplayed(), "First Card Page is not opened");

        firstCardPage.InputPassword();
        firstCardPage.InputEmail();
        firstCardPage.InputEmailDomain();
        firstCardPage.ChooseEmailDomainZone();
        firstCardPage.UncheckAcceptTermsConditionsCheckBox();

        firstCardPage.ClickNextButton();
        var secondCardPage = new SecondCardPage();
        Assert.IsTrue(secondCardPage.State.WaitForDisplayed(), "Second Card Page is not opened");

        secondCardPage.UploadImage();
        secondCardPage.UnselectAllInterestsCheckBoxClick();
        secondCardPage.CheckInterests();
        
        secondCardPage.ClickNextButton();
        var thirdCardPage = new ThirdCardPage();
        Assert.IsTrue(thirdCardPage.State.WaitForDisplayed(), "Third Card Page is not opened");
    }

    [Test, Order(2)]
    public void TestCase2()
    {
        
    }

    [TearDown]
    public void CleanUp()
    {
        if (AqualityServices.IsBrowserStarted)
        {
            AqualityServices.Browser.Quit();
        }
    }
}