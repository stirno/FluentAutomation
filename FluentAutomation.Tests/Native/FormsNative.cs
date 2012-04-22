using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Tests
{
    public class FormsNative : FluentTest
    {
        private static string testUrl = "http://automation.apphb.com/forms";

        public void Autocomplete_ExpectedResult()
        {
            I.Open(testUrl);
            I.Enter("abcd").In("#form02input02");
            I.WaitUntil(() => I.Expect.Count(1).Of("ul.typeahead li:eq(0) a"));
            I.Expect.Text("Olercwlc").In("ul.typeahead li:eq(0) a");
        }
    }
}
