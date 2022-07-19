using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task2Stage2.Pages;

public class SecondCardPage : Form
{
    public SecondCardPage() : base(By.XPath("//a[@class = 'avatar-and-interests__upload-button']"), "Second Card Page")
    {
    }
}