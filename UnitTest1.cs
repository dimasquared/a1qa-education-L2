using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Task2Stage2.Pages;
using Task2Stage2.Utils;

namespace Task2Stage2;

public class Tests
{
    private string url;
    private string timerStartTime;
    private int requiredNumberOfInterests;
    private string imagePath;
    
    [SetUp]
    public void Setup()
    {
        ISettingsFile config = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\config.json");
        url = config.GetValue<string>("url");
        ISettingsFile testData = new JsonSettingsFile(Environment.CurrentDirectory + @"\Resources\testData.json");
        timerStartTime = testData.GetValue<string>("timerStartTime");
        requiredNumberOfInterests = testData.GetValue<int>("requiredNumberOfInterests");
        imagePath = testData.GetValue<string>("imagePath");
    }

    [Test, Order(1)]
    public void RegistrationTest()
    {
        AqualityServices.Browser.GoTo(url);
        AqualityServices.Browser.Maximize();
        var welcomePage = new WelcomePage();
        Assert.IsTrue(welcomePage.State.WaitForDisplayed(), "Welcome Page is not opened");

        welcomePage.ClickHereButton();
        var firstCardPage = new LoginFormPage();
        Assert.IsTrue(firstCardPage.State.WaitForDisplayed(), "Login Form Page is not opened");

        var email = TextUtil.RandomText('a', 'z', 5);
        firstCardPage.InputEmail(email);
        var password = email + TextUtil.RandomText('A', 'Z', 1) + TextUtil.RandomText('А', 'я', 3) +
                          TextUtil.RandomText('1', '2', 5);
        firstCardPage.InputPassword(password);
        var emailDomain = TextUtil.RandomText('a', 'z', 5);
        firstCardPage.InputEmailDomain(emailDomain);
        firstCardPage.ChooseEmailDomainZone();
        firstCardPage.UncheckAcceptTermsConditionsCheckBox();

        firstCardPage.ClickNextButton();
        var secondCardPage = new AvatarАndInterestsPage();
        Assert.IsTrue(secondCardPage.State.WaitForDisplayed(), "Avatar Аnd Interests Page is not opened");

        secondCardPage.UploadImage(imagePath);
        secondCardPage.UnselectAllInterestsCheckBoxClick();

        var countCheckedInterests = 0;
        var maxNumberOfInterests = secondCardPage.Interests.Count;
        List<int> lastTakeNumbers = new List<int>();

        do
        {
            var index = NumberRangeUtil.GetNextNotRepeatRandomNumber(0, maxNumberOfInterests, lastTakeNumbers);
            var interestName = secondCardPage.GetInterestName(index);
            if (interestName != "interest_selectall" && interestName != "interest_unselectall")
            {
                secondCardPage.CheckInterest(index);
                countCheckedInterests++;
            }
        } while (countCheckedInterests < requiredNumberOfInterests);

        secondCardPage.ClickNextButton();
        var thirdCardPage = new PersonalDetailsPage();
        Assert.IsTrue(thirdCardPage.State.WaitForDisplayed(), "Personal Details Page is not opened");
    }

    [Test, Order(2)]
    public void HideHelpFormTest()
    {
        AqualityServices.Browser.GoTo(url);
        AqualityServices.Browser.Maximize();
        var welcomePage = new WelcomePage();
        Assert.IsTrue(welcomePage.State.WaitForDisplayed(), "Welcome Page is not opened");

        welcomePage.ClickHereButton();
        var helpForm = new LoginFormPage.HelpForm();
        helpForm.HideHelpForm();
        Assert.IsFalse(helpForm.State.WaitForDisplayed(), "Help Form is not hidden");
    }

    [Test, Order(3)]
    public void AcceptCookiesTest()
    {
        AqualityServices.Browser.GoTo(url);
        AqualityServices.Browser.Maximize();
        var welcomePage = new WelcomePage();
        Assert.IsTrue(welcomePage.State.WaitForDisplayed(), "Welcome Page is not opened");

        welcomePage.ClickHereButton();
        var firstCardPage = new LoginFormPage();
        firstCardPage.ClickAcceptCookiesButton();
        Assert.IsFalse(firstCardPage.CheckCookiesIsDisplayed(), "Cookies is displayed");
    }

    [Test, Order(4)]
    public void TimeStartsFromZeroTest()
    {
        AqualityServices.Browser.GoTo(url);
        AqualityServices.Browser.Maximize();
        var welcomePage = new WelcomePage();
        Assert.IsTrue(welcomePage.State.WaitForDisplayed(), "Welcome Page is not opened");

        welcomePage.ClickHereButton();
        var firstCardPage = new LoginFormPage();
        Assert.AreEqual(timerStartTime, firstCardPage.GetTimerValue(), "Timer does not start from 00:00:00");
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