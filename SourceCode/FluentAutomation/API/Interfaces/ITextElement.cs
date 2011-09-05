using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API.Interfaces
{
    public interface ITextElement : IElement
    {
        void SetValueQuickly(string value);
    }
}
