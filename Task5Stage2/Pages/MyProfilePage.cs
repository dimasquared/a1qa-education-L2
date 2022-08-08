using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task5Stage2.Pages;

public class MyProfilePage : Form
{
    public MyProfilePage() : base(By.Id("page_current_info"), "My Profile Page")
    {
    }
}