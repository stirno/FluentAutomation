using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation.Interfaces
{
    public interface IElement
    {
        string Selector { get; }
        string TagName { get; }
        IElementAttributeSelector Attributes { get; }

        string Value { get; }
        string Text { get; }

        IEnumerable<string> SelectedOptionValues { get; }
        IEnumerable<string> SelectedOptionTextCollection { get; }

        bool IsText { get; }
        bool IsSelect { get; }
        bool IsMultipleSelect { get; }

        int PosX { get; }
        int PosY { get; }
        int Width { get; }
        int Height { get; }
    }

    public interface IElementAttributeSelector
    {
        string Get(string name);
    }
}
