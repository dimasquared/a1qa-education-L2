using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using Task2Stage2.Elements;

namespace Task2Stage2.Pages;

public class FirstCardPage : Form
{
    private ITextBox PasswordTextBox =>
        ElementFactory.GetTextBox(By.XPath("//input[@placeholder = 'Choose Password']"), "PasswordTextBox");

    private ITextBox EmailTextBox =>
        ElementFactory.GetTextBox(By.XPath("//input[@placeholder = 'Your email']"), "EmailTextBox");

    private ITextBox EmailDomainTextBox =>
        ElementFactory.GetTextBox(By.XPath("//input[@placeholder = 'Domain']"), "EmailDomainTextBox");

    private IButton EmailDomainZoneListOpen =>
        ElementFactory.GetButton(By.XPath("//div[@class = 'dropdown__opener']"), "EmailDomainZoneListOpen");

    private IButton EmailDomainZoneCheck =>
        ElementFactory.GetButton(By.XPath("//div[@class = 'dropdown__list-item'][3]"), "EmailDomainZoneCheck");

    private IButton AcceptTermsConditionsCheckBox =>
        ElementFactory.GetButton(By.XPath("//span[@class = 'checkbox']"), "Accept Terms & Conditions CheckBox");

    private IButton NextButton => ElementFactory.GetButton(By.XPath("//a[@class = 'button--secondary']"), "Next");

    private IButton AcceptCookiesButton =>
        ElementFactory.GetButton(By.XPath("//div[@class = 'cookies']//button[text() = 'Not really, no']"),
            "Accept Cookies Button");

    private TextElement Timer => ElementFactory.GetTextElement(By.XPath("//div[contains(@class, 'timer')]"), "Timer");

    public FirstCardPage() : base(By.XPath("//a[@class = 'login-form__terms-conditions']"), "First Card Page")
    {
    }

    public void InputPassword(string password)
    {
        PasswordTextBox.ClearAndType(password);
    }

    public void InputEmail(string email)
    {
        EmailTextBox.ClearAndType(email);
    }

    public void InputEmailDomain(string emailDomain)
    {
        EmailDomainTextBox.ClearAndType(emailDomain);
    }

    public void ChooseEmailDomainZone()
    {
        EmailDomainZoneListOpen.Click();
        EmailDomainZoneCheck.Click();
    }

    public void UncheckAcceptTermsConditionsCheckBox()
    {
        AcceptTermsConditionsCheckBox.Click();
    }

    public void ClickNextButton()
    {
        NextButton.ClickAndWait();
    }

    public void ClickAcceptCookiesButton()
    {
        AcceptCookiesButton.WaitAndClick();
    }

    public bool CheckCookiesIsDisplayed()
    {
        return AcceptCookiesButton.State.IsDisplayed;
    }
    
    public string GetTimerValue()
    {
        return Timer.GetText();
    }
    
    public class HelpForm : Form
    {
        private IButton HideHelpFormButton =>
            ElementFactory.GetButton(By.XPath("//button[contains(@class, 'help-form__send-to-bottom-button')]"),
                "HideHelpFormButton");
    
        public HelpForm() : base(By.XPath("//div[@class = 'help-form']"), "Help Form")
        {
        }
    
        public void HideHelpForm()
        {
            HideHelpFormButton.ClickAndWait();
        }
    }
}