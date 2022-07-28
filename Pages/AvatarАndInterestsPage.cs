using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using Task2Stage2.Utils;

namespace Task2Stage2.Pages;

public class AvatarАndInterestsPage : Form
{
    private IButton UploadImageBtn =>
        ElementFactory.GetButton(By.XPath("//a[contains(@class, 'avatar-and-interests__upload-button')]"), "Upload Image");

    private IButton UnselectAllInterestsCheckBox =>
        ElementFactory.GetButton(By.XPath("//label[@for = 'interest_unselectall']"), "Unselect All Interests CheckBox");

    public IList<IButton> Interests =>
        ElementFactory.FindElements<IButton>(By.XPath("//label[contains(@for, 'interest_')]")).ToList();

    private IButton NextBtn => ElementFactory.GetButton(By.XPath("//button[contains(.,'Next')]"), "Next");

    public AvatarАndInterestsPage() : base(By.XPath("//a[contains(@class, 'avatar-and-interests__upload-button')]"), "Avatar Аnd Interests Page")
    {
    }

    public void UploadImage(string imagePath)
    {
        UploadImageBtn.Click();
        AqualityServices.ConditionalWait.WaitFor(() => false, TimeSpan.FromSeconds(0.5));
        UploadFileUtil.UploadFile(imagePath);
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
        NextBtn.ClickAndWait();
    }
}