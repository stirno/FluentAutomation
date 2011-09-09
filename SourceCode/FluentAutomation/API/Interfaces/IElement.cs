// <copyright file="IElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System.Drawing;

namespace FluentAutomation.API.Interfaces
{
    public interface IElement
    {
        string GetAttributeValue(string attributeName);
        string GetText();
        string GetValue();
        Rectangle GetElementBounds();
        bool IsSelect();
        bool IsText();
        void SetValue(string value);
        void Click();
        void Focus();
        void Hover();
        void DragTo(IElement fieldSelector);
    }
}
