using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task2Stage2.Pages;

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