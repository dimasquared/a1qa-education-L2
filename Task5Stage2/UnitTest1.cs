using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Task5Stage2.Pages;

namespace Task5Stage2;

public class Tests
{
    private string url;
    private string password;
    private string login;

    [SetUp]
    public void Setup()
    {
        ISettingsFile config = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\config.json");
        url = config.GetValue<string>("url");

        ISettingsFile testData = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\testData.json");
        login = testData.GetValue<string>("login");
        password = testData.GetValue<string>("password");
    }

    [Test]
    public void Test1()
    {
        AqualityServices.Browser.Maximize();
        AqualityServices.Browser.GoTo(url);
        var loginPage = new LoginPage();
        loginPage.InputLogin(login);
        loginPage.ClickSignInButton();
        var passwordPage = new PasswordPage();
        passwordPage.InputPassword(password);
        passwordPage.ClickContinueButton();
        var feedPage = new FeedPage();
        feedPage.ClickMyProfileButton();
        var myProfilePage = new MyProfilePage();
        Assert.IsTrue(myProfilePage.State.WaitForDisplayed(), "My Profile Page is not opened");
        
        
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