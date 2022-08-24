using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task5Stage2.Pages;

public class LoginPage : Form
{
    private ITextBox LoginTxb => ElementFactory.GetTextBox(By.Id("index_email"), "Login");

    private IButton SignInBtn =>
        ElementFactory.GetButton(By.XPath("//button[contains(@class, 'VkIdForm__signInButton')]"), "Sign In");

    public LoginPage() : base(By.Id("index_login"), "Authorization Page")
    {
    }

    public void InputLogin(string login)
    {
        LoginTxb.Type(login);
    }

    public void ClickSignInButton()
    {
        SignInBtn.ClickAndWait();
    }
}