using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using Task2Stage2.Elements;

namespace Task2Stage2.Pages;

public class LoginFormPage : Form
{
    private ITextBox PasswordTxb =>
        ElementFactory.GetTextBox(By.XPath("//input[@placeholder = 'Choose Password']"), "Password");

    private ITextBox EmailTxb =>
        ElementFactory.GetTextBox(By.XPath("//input[@placeholder = 'Your email']"), "Email");

    private ITextBox EmailDomainTxb =>
        ElementFactory.GetTextBox(By.XPath("//input[@placeholder = 'Domain']"), "Email Domain");

    private IButton EmailDomainZoneListOpen =>
        ElementFactory.GetButton(By.XPath("//div[contains(@class, 'dropdown__opener')]"), "Open Email Domain Zone List");

    private IButton EmailDomainZoneCheck =>
        ElementFactory.GetButton(By.XPath("//div[contains(@class, 'dropdown__list-item')][3]"), "Check Email Domain Zone");

    private IButton AcceptTermsConditionsCheckBox =>
        ElementFactory.GetButton(By.XPath("//span[contains(@class, 'checkbox')]"), "Accept Terms & Conditions CheckBox");

    private IButton NextBtn => ElementFactory.GetButton(By.XPath("//a[contains(@class, 'button--secondary')]"), "Next");

    private IButton AcceptCookiesBtn =>
        ElementFactory.GetButton(By.XPath("//div[contains(@class, 'cookies')]//button[text() = 'Not really, no']"),
            "Accept Cookies");

    private TextElement Timer => ElementFactory.GetTextElement(By.XPath("//div[contains(@class, 'timer')]"), "Timer");

    public LoginFormPage() : base(By.XPath("//a[contains(@class, 'login-form__terms-conditions')]"), "Login Form Page")
    {
    }

    public void InputPassword(string password)
    {
        PasswordTxb.ClearAndType(password);
    }

    public void InputEmail(string email)
    {
        EmailTxb.ClearAndType(email);
    }

    public void InputEmailDomain(string emailDomain)
    {
        EmailDomainTxb.ClearAndType(emailDomain);
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
        NextBtn.ClickAndWait();
    }

    public void ClickAcceptCookiesButton()
    {
        AcceptCookiesBtn.WaitAndClick();
    }

    public bool CheckCookiesIsDisplayed()
    {
        return AcceptCookiesBtn.State.IsDisplayed;
    }
    
    public string GetTimerValue()
    {
        return Timer.GetText();
    }
    
    public class HelpForm : Form
    {
        private IButton HideHelpFormBtn =>
            ElementFactory.GetButton(By.XPath("//button[contains(@class, 'help-form__send-to-bottom-button')]"),
                "Hide Help Form");
    
        public HelpForm() : base(By.XPath("//div[@class = 'help-form']"), "Help Form")
        {
        }
    
        public void HideHelpForm()
        {
            HideHelpFormBtn.ClickAndWait();
        }
    }
}