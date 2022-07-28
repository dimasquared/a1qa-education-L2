using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Task2Stage2.Elements;

public static class ElementFactoryExtensions
{
    public static TextElement GetTextElement(this IElementFactory elementFactory, By elementLocator, string elementName)
    {
        return elementFactory.GetCustomElement(GetTextElementSupplier(), elementLocator, elementName);
    }

    private static ElementSupplier<TextElement> GetTextElementSupplier()
    {
        return (locator, name, state) => new TextElement(locator, name, state);
    }
}