using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task5Stage2.Pages;

public class FeedPage : Form
{
    private IButton MyProfileBtn => ElementFactory.GetButton(By.XPath("//li[@id='l_pr']//a"), "My Profile");

    public FeedPage() : base(By.Id("stories_feed_items"), "Feed Page")
    {
    }
    
    public void ClickMyProfileButton()
    {
        MyProfileBtn.ClickAndWait();
    }
}