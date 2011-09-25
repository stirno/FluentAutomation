// <copyright file="MatchConditions.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;

namespace FluentAutomation.API.Enumerations
{
    [Flags]
    public enum MatchConditions
    {
        None = 0,
        Visible = 1,
        Hidden = 2
    }
}
