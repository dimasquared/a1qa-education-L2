using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

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

    private IButton EmailDomainZoneCheck => ElementFactory.GetButton(By.XPath("//div[@class = 'dropdown__list-item'][3]"), "EmailDomainZoneCheck");

    private IButton AcceptTermsConditionsCheckBox =>
        ElementFactory.GetButton(By.XPath("//span[@class = 'checkbox']"), "Accept Terms & Conditions CheckBox");

    private IButton NextButton => ElementFactory.GetButton(By.XPath("//a[@class = 'button--secondary']"), "Next");

    public FirstCardPage() : base(By.XPath("//a[@class = 'login-form__terms-conditions']"), "First Card Page")
    {
    }

    public void InputPassword()
    {
        PasswordTextBox.ClearAndType("Яqwerty12345");
    }

    public void InputEmail()
    {
        EmailTextBox.ClearAndType("qwerty");
    }

    public void InputEmailDomain()
    {
        EmailDomainTextBox.ClearAndType("qwerty");
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
}