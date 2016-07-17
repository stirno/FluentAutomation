using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Interfaces
{
    public interface ICheckSyntaxProvider
    {
        bool Visible(string element);

        bool Visible(ElementProxy element);

        bool Exist(string element);

        bool Exist(ElementProxy element);

        bool Text(string text, string element);

        bool Text(string text, ElementProxy element);
    }
}
