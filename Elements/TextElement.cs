using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using Element = Aquality.Selenium.Elements.Element;

namespace Task2Stage2.Elements;

public class TextElement : Element, IElement
{
    public TextElement(By locator, string name, ElementState state) : base(locator, name, state)
    {
    }

    protected override string ElementType { get; }
}