using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Task2Stage2.Pages;

public class PersonalDetailsPage : Form
{
    public PersonalDetailsPage() : base(By.XPath("//div[contains(@class, 'personal-details__form-table')]"), "Personal Details Page")
    {
    }
}