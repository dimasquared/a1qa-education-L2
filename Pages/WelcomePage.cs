using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task2Stage2.Pages;

public class WelcomePage : Form
{
    private IButton HereBtn => ElementFactory.GetButton(By.XPath("//a[contains(@class, 'start__link')]"), "Here");
    
    public WelcomePage() : base(By.XPath("//button[contains(@class, 'start__button')]"), "Welcome Page")
    {
    }
    
    public void ClickHereButton()
    {
        HereBtn.ClickAndWait();
    }
}