using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using WindowsInput;
using WindowsInput.Native;

namespace Task2Stage2.Pages;

public class SecondCardPage : Form
{
    private IButton UploadImageButton =>
        ElementFactory.GetButton(By.XPath("//a[@class = 'avatar-and-interests__upload-button']"), "UploadImageButton");
    private IButton UnselectAllInterestsCheckBox =>
        ElementFactory.GetButton(By.XPath("//label[@for = 'interest_unselectall']"), "UnselectAllInterestsCheckBox");

    private IButton CheckInterest1 =>
        ElementFactory.GetButton(By.XPath("//label[@for = 'interest_ponies']"), "CheckInterest1");

    private IButton CheckInterest2 =>
        ElementFactory.GetButton(By.XPath("//label[@for = 'interest_postits']"), "CheckInterest2");

    private IButton CheckInterest3 =>
        ElementFactory.GetButton(By.XPath("//label[@for = 'interest_purple']"), "CheckInterest3");

    private IButton NextButton => ElementFactory.GetButton(By.XPath("//button[contains(.,'Next')]"), "Next");

    public SecondCardPage() : base(By.XPath("//a[@class = 'avatar-and-interests__upload-button']"), "Second Card Page")
    {
    }

    public void UploadImage()
    {
        UploadImageButton.Click();
        AqualityServices.ConditionalWait.WaitFor(() => false, TimeSpan.FromSeconds(0.5));
        var imagePath = AppContext.BaseDirectory + @"Resources\DcJwlj4XcAIqUSY.jpg";
        InputSimulator simulator = new InputSimulator();
        simulator.Keyboard.TextEntry(@imagePath);
        simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);

    }

    public void UnselectAllInterestsCheckBoxClick()
    {
        UnselectAllInterestsCheckBox.Click();
    }
    
    public void CheckInterests()
    {
        
        CheckInterest1.Click();
        CheckInterest2.Click();
        CheckInterest3.Click();
    }
    
    public void ClickNextButton()
    {
        NextButton.ClickAndWait();
    }
}