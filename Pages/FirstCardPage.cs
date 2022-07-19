using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task2Stage2.Pages;

public class FirstCardPage : Form
{
    public FirstCardPage() : base(By.XPath("//a[@class = 'login-form__terms-conditions']"), "First Card Page")
    {
    }
}