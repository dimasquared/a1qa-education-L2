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
    private TextElement PostTimeTxt => ElementFactory.GetTextElement(
        By.XPath(
            "//div[@id='page_wall_posts']//div[contains(@id, 'post')][1]//child::div[contains(@class, 'post_date')]//span[contains(@class, 'rel_date')]"),
        "PostTime");

    private IButton ShowCommentBtn => ElementFactory.GetButton(By.XPath("//a[contains(@class,'replies_next')]"), "Show Comment");

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
    
    public string GetPostTime()
    {
        return PostTimeTxt.GetAttribute("time");
    }
}