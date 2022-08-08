using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task5Stage2.Pages;

public class FeedPage : Form
{
    private ILink MyProfileBtn => ElementFactory.GetLink(By.Id("l_pr"), "My Profile");

    public FeedPage() : base(By.Id("stories_feed_items"), "Feed Page")
    {
    }
    
    public void ClickMyProfileButton()
    {
        MyProfileBtn.ClickAndWait();
    }
}