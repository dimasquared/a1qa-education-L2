using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task2Stage2.Page;

public class WelcomeForm : Form
{
    public WelcomeForm() : base(By.XPath("button[@class = 'start__button']"), "Welcome Page")
    {
    }
}