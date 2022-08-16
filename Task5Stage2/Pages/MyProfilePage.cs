using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using Task5Stage2.Elements;

namespace Task5Stage2.Pages;

public class MyProfilePage : Form
{
    private TextElement PageOwnerTxt =>
        ElementFactory.GetTextElement(By.XPath("//h1[contains(@class, 'page_name')]"), "Page Owner");

    private TextElement PostAuthorTxt => ElementFactory.GetTextElement(
        By.XPath(
            "//div[@id='page_wall_posts']//div[contains(@class, 'post page_block all own')][1]//h5[contains(@class, 'post_author')]//a"),
        "Post Author");

    private TextElement MessageOnTheWallTxt => ElementFactory.GetTextElement(
        By.XPath(
            "//div[@id='page_wall_posts']//div[contains(@id, 'post')][1]//div[contains(@class, 'wall_post_text')]"),
        "Message On The Wall");

    private TextElement PostCommentTxt => ElementFactory.GetTextElement(
        By.XPath(
            "//div[@id='page_wall_posts']//div[contains(@id, 'post')][1]//child::div[contains(@id, 'replies')]//div[contains(@class, 'wall_reply_text')]"),
        "Comment To The Post");

    private TextElement PostCommentAuthorTxt => ElementFactory.GetTextElement(
        By.XPath(
            "//div[@id='page_wall_posts']//div[contains(@id, 'post')][1]//child::div[contains(@id, 'replies')]//div[contains(@class, 'reply_author')]//a"),
        "Comment's Author");

    private IButton ShowCommentBtn =>
        ElementFactory.GetButton(By.XPath("//a[contains(@class,'replies_next')]"), "Show Comment");

    private IButton LikePostBtn => ElementFactory.GetButton(
        By.XPath(
            "//div[@id='page_wall_posts']//div[contains(@id, 'post')][1]//child::div[contains(@class, 'PostButtonReactionsContainer')]"),
        "Like Post");

    private ILink ImageOnTheWallLnk =>
        ElementFactory.GetLink(
            By.XPath("//div[@id='page_wall_posts']//div[contains(@id, 'post')][1]//a[@aria-label = 'photo']"), "Image On The Wall");

    private IButton MoreMenuBtn => ElementFactory.GetButton(
        By.XPath("//div[contains (@class, 'pv_bottom_actions')]//a[contains(@class, 'pv_actions_more')]"), "More Menu");

    private ILink ImageDownloadLnk = ElementFactory.GetLink(By.XPath("//div[contains (@class, 'pv_bottom_actions')]//a[contains(@class, 'more_shown')]//a[@id='pv_more_act_download']"), "Image Download");

    private IButton CloseImageBtn =
        ElementFactory.GetButton(By.XPath("//div[@id = 'pv_box']//div[contains(@class, 'pv_close_btn')]"),
            "Close Image");
    
    public MyProfilePage() : base(By.Id("page_current_info"), "My Profile Page")
    {
    }

    public string GetPostAuthor()
    {
        return PostAuthorTxt.GetText();
    }

    public string GetPageOwner()
    {
        return PageOwnerTxt.GetText();
    }

    public string GetMessageOnTheWall()
    {
        return MessageOnTheWallTxt.GetText();
    }

    public void ShowNewComment()
    {
        ShowCommentBtn.ClickAndWait();
    }

    public string GetPostCommentAuthor()
    {
        return PostCommentAuthorTxt.GetText();
    }

    public string GetPostCommentText()
    {
        return PostCommentTxt.GetText();
    }

    public void LikePost()
    {
        LikePostBtn.ClickAndWait();
    }

    public bool CheckPostDeleted()
    {
        return MessageOnTheWallTxt.State.IsDisplayed;
    }

    public string GetImageUrlFromTheWall()
    {
       ImageOnTheWallLnk.ClickAndWait();
       MoreMenuBtn.Click();
       var imgUrl = ImageDownloadLnk.Href;
       CloseImageBtn.ClickAndWait();
       return imgUrl;
    }

    public void WaitForEditPostLoad()
    {
        AqualityServices.ConditionalWait.WaitFor(() => ImageOnTheWallLnk.State.IsClickable, TimeSpan.FromSeconds(1));
    }
}