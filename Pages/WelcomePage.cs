using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task2Stage2.Pages;

public class WelcomePage : Form
{
    private IButton HereButton => ElementFactory.GetButton(By.XPath("//a[@class = 'start__link']"), "HERE Button");
    
    public WelcomePage() : base(By.XPath("//button[@class = 'start__button']"), "Welcome Page")
    {
    }
    
    public void ClickHereButton()
    {
        HereButton.ClickAndWait();
    }
}