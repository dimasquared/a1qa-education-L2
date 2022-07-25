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
        ElementFactory.GetButton(By.XPath("//a[@class = 'avatar-and-interests__upload-button']"), "Upload Image Button");

    private IButton UnselectAllInterestsCheckBox =>
        ElementFactory.GetButton(By.XPath("//label[@for = 'interest_unselectall']"), "UnselectAllInterests CheckBox");

    public IList<IButton> Interests =>
        ElementFactory.FindElements<IButton>(By.XPath("//label[contains(@for, 'interest_')]")).ToList();

    private IButton NextButton => ElementFactory.GetButton(By.XPath("//button[contains(.,'Next')]"), "Next");

    public SecondCardPage() : base(By.XPath("//a[@class = 'avatar-and-interests__upload-button']"), "Second Card Page")
    {
    }

    public void UploadImage()
    {
        UploadImageButton.Click();
        AqualityServices.ConditionalWait.WaitFor(() => false, TimeSpan.FromSeconds(0.5));
        var imagePath = Environment.CurrentDirectory + @"\Resources\DcJwlj4XcAIqUSY.jpg";
        InputSimulator simulator = new InputSimulator();
        simulator.Keyboard.TextEntry(@imagePath);
        Thread.Sleep(500);
        simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
    }

    public void UnselectAllInterestsCheckBoxClick()
    {
        UnselectAllInterestsCheckBox.Click();
    }

    public string GetInterestName(int index)
    {
        return Interests[index].GetAttribute("for");
    }

    public void CheckInterest(int index)
    {
        Interests[index].Click();
    }

    public void ClickNextButton()
    {
        NextButton.ClickAndWait();
    }
}