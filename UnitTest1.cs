using Aquality.Selenium.Browsers;
using Task2Stage2.Pages;
using Task2Stage2.Utils;

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

        string email = TextUtil.RandomText('a', 'z', 5);
        firstCardPage.InputEmail(email);
        string password = email + TextUtil.RandomText('A', 'Z', 1) + TextUtil.RandomText('А', 'я', 3) +
                          TextUtil.RandomText('1', '2', 5);
        firstCardPage.InputPassword(password);
        string emailDomain = TextUtil.RandomText('a', 'z', 5);
        firstCardPage.InputEmailDomain(emailDomain);
        firstCardPage.ChooseEmailDomainZone();
        firstCardPage.UncheckAcceptTermsConditionsCheckBox();

        firstCardPage.ClickNextButton();
        var secondCardPage = new SecondCardPage();
        Assert.IsTrue(secondCardPage.State.WaitForDisplayed(), "Second Card Page is not opened");

        secondCardPage.UploadImage();
        secondCardPage.UnselectAllInterestsCheckBoxClick();

        var countCheckedInterests = 0;
        NumberRangeUtil indexRange = new NumberRangeUtil(0, secondCardPage.Interests.Count);

        do
        {
            var index = indexRange.GetNextNotRepeatRandomNumber();
            var interestName = secondCardPage.GetInterestName(index);
            if (interestName != "interest_selectall" && interestName != "interest_unselectall")
            {
                secondCardPage.CheckInterest(index);
                countCheckedInterests++;
            }
        } while (countCheckedInterests < 3);

        secondCardPage.ClickNextButton();
        var thirdCardPage = new ThirdCardPage();
        Assert.IsTrue(thirdCardPage.State.WaitForDisplayed(), "Third Card Page is not opened");
    }

    [Test, Order(2)]
    public void TestCase2()
    {
        AqualityServices.Browser.GoTo("https://userinyerface.com/");
        AqualityServices.Browser.Maximize();
        var welcomePage = new WelcomePage();
        Assert.IsTrue(welcomePage.State.WaitForDisplayed(), "Welcome Page is not opened");

        welcomePage.ClickHereButton();
        var helpForm = new FirstCardPage.HelpForm();
        helpForm.HideHelpForm();
        Assert.IsFalse(helpForm.State.WaitForDisplayed(), "Help Form is not hidden");
    }

    [Test, Order(3)]
    public void TestCase3()
    {
        AqualityServices.Browser.GoTo("https://userinyerface.com/");
        AqualityServices.Browser.Maximize();
        var welcomePage = new WelcomePage();
        Assert.IsTrue(welcomePage.State.WaitForDisplayed(), "Welcome Page is not opened");

        welcomePage.ClickHereButton();
        var firstCardPage = new FirstCardPage();
        firstCardPage.ClickAcceptCookiesButton();
        Assert.IsFalse(firstCardPage.CheckCookiesIsDisplayed(), "Cookies is displayed");
    }

    [Test, Order(4)]
    public void TestCase4()
    {
        AqualityServices.Browser.GoTo("https://userinyerface.com/");
        AqualityServices.Browser.Maximize();
        var welcomePage = new WelcomePage();
        Assert.IsTrue(welcomePage.State.WaitForDisplayed(), "Welcome Page is not opened");

        welcomePage.ClickHereButton();
        var firstCardPage = new FirstCardPage();
        Assert.AreEqual("00:00:00", firstCardPage.GetTimerValue(), "Timer does not start from 00:00:00");
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