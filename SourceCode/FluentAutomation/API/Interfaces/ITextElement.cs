// <copyright file="ITextElement.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

namespace FluentAutomation.API.Interfaces
{
    public interface ITextElement : IElement
    {
        void SetValueQuickly(string value);
    }
}
