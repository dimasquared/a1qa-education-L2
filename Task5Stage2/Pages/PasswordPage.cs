using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task5Stage2.Pages;

public class PasswordPage : Form
{
    private ITextBox PasswordTxb => ElementFactory.GetTextBox(By.XPath("//input[@type='password']"), "Password");

    private IButton ContinueBtn =>
        ElementFactory.GetButton(By.XPath("//div[contains(@class, 'vkc__EnterPasswordNoUserInfo__buttonWrap')]//button"), "Continue");

    public PasswordPage() : base(By.XPath("//div[contains(@class, 'vkc__EnterPasswordNoUserInfo__titleContainer')]"),
        "Password Page")
    {
    }

    public void InputPassword(string password)
    {
        PasswordTxb.Type(password);
    }

    public void ClickContinueButton()
    {
        ContinueBtn.ClickAndWait();
    }
}