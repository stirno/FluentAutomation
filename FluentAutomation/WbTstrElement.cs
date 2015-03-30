using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class WbTstrElement : IElement
    {
        public WbTstrElement(string selector)
        {
            Selector = selector;
            TagName = string.Empty;
            Value = string.Empty;
            Text = string.Empty;
            SelectedOptionValues = Enumerable.Empty<string>();
            SelectedOptionTextCollection = Enumerable.Empty<string>();
        }

        /*-------------------------------------------------------------------*/

        public string Selector { get; set; }

        public string TagName { get; set; }

        public IElementAttributeSelector Attributes { get; set; }

        public string Value { get; set; }

        public string Text { get; set; }

        public IEnumerable<string> SelectedOptionValues { get; set; }

        public IEnumerable<string> SelectedOptionTextCollection { get; set; }

        public bool IsText { get; set; }

        public bool IsSelect { get; set; }

        public bool IsMultipleSelect { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}