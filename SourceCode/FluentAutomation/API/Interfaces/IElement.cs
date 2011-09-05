using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API.Interfaces
{
    public interface IElement
    {
        string GetAttributeValue(string attributeName);
        string GetText();
        string GetValue();
        bool IsSelect();
        bool IsText();
        void SetValue(string value);
        void Click();
        void Focus();
        void Hover();
    }
}
