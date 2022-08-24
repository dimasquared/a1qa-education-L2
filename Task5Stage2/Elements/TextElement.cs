using Aquality.Selenium.Core.Elements;
using OpenQA.Selenium;
using Element = Aquality.Selenium.Elements.Element;

namespace Task5Stage2.Elements;

public class TextElement : Element
{
    protected override string ElementType { get; }

    public TextElement(By locator, string name, ElementState state) : base(locator, name, state)
    {
    }
}