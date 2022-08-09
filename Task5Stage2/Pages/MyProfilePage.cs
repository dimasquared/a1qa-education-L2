using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using Task5Stage2.Elements;

namespace Task5Stage2.Pages;

public class MyProfilePage : Form
{
    private TextElement PageOwner =>
        ElementFactory.GetTextElement(By.XPath("//h1[contains(@class, 'page_name')]"), "Page Owner");

    private TextElement PostAuthor => ElementFactory.GetTextElement(
        By.XPath(
            "//div[@id='page_wall_posts']//div[contains(@id, 'post')][1]//h5[contains(@class, 'post_author')]//a"),
        "Post Author");

    private TextElement MessageOnTheWall => ElementFactory.GetTextElement(By.XPath("//div[@id='page_wall_posts']//div[contains(@id, 'post')][1]//div[contains(@class, 'wall_post_text')]"), "Message On The Wall");

    public MyProfilePage() : base(By.Id("page_current_info"), "My Profile Page")
    {
    }

    public string GetPostAuthor()
    {
        return PostAuthor.GetText();
    }
    
    public string GetPageOwner()
    {
        return PageOwner.GetText();
    }
    
    public string GetMessageOnTheWall()
    {
        return MessageOnTheWall.GetText();
    }
}