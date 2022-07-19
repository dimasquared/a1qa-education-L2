using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task2Stage2.Pages;

public class ThirdCardPage : Form
{
    public ThirdCardPage() : base(By.XPath("//div[@class = 'personal-details__form-table']"), "Third Card Page")
    {
    }
}