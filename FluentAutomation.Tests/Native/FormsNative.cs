using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    public class FormsNative : FluentTest<IWebDriver>
    {
        public FormsNative()
        {
            Settings.ScreenshotOnFailedAction = true;
            Settings.ScreenshotOnFailedExpect = true;
            Settings.ExpectIsAssert = false;
        }

        private static string testUrl = "http://automation.apphb.com/forms";

        [Fact]
        public void FizzBuzz()
        {
            //for (int i = 1; i <= 100; i++)
            //{
            //    if (i % 3 == 0 && i % 5 == 0)
            //    {
            //        Console.WriteLine("FizzBuzz");
            //    }
            //    if (i % 3 == 0)
            //    {
            //        Console.WriteLine("Fizz");
            //    }
            //    else if (i % 5 == 0)
            //    {
            //        Console.WriteLine("Buzz");
            //    }
            //    else
            //    {
            //        Console.WriteLine(i.ToString());
            //    }
            //}
            
            for (int i = 1; i <= 100; i++) { Console.WriteLine(i % 3 == 0 && i % 5 == 0 ? "FizzBuzz" : i % 3 == 0 ? "Fizz" : i % 5 == 0 ? "Buzz" : i.ToString()); }
        }

        public void Autocomplete_ExpectedResult()
        {
            I.Open(testUrl);
            I.Enter("abcd").In("#form02input02");
            
            // wait for first render of results list
            I.WaitUntil(() => I.Expect.Count(1).Of("ul.typeahead li:eq(0) a"));

            // wait for our keypresses to be processed
            I.Wait(TimeSpan.FromMilliseconds(2500));
            I.Expect.Text("Olercwlc").In("ul.typeahead li:eq(0) a");
        }

        public void CartEditor_BuyMotorcycles()
        {
            I
                .Open(testUrl)
                .Select("Motorcycles").From(".liveExample tr select:eq(0)") // Select by value/text
                .Select(2).From(".liveExample tr select:eq(1)") // Select by index
                .Enter(6).In(".liveExample td.quantity input:eq(0)")
                .Expect
                    .Text("$197.72").In(".liveExample tr span:eq(1)");

            // add second product
            I.Click(".liveExample button:eq(0)");
            I.Select(1).From(".liveExample tr select:eq(2)");
            I.Select(4).From(".liveExample tr select:eq(3)");
            I.Enter(8).In(".liveExample td.quantity input:eq(1)");
            I.Expect.Text("$788.64").In(".liveExample tr span:eq(3)");

            // validate totals
            I.Expect.Text("$986.34").In("p.grandTotal span");

            // remove first product
            I.Click(".liveExample a:eq(0)");

            // validate new total
            I.WaitUntil(() => I.Assert.Text("$788.64").In("p.grandTotal span"));
        }

        public void GoogleInputField()
        {
            I.Open("https://www.google.com/search?sugexp=chrome,mod=6&ix=nh&sourceid=chrome&ie=UTF-8&q=input+text+default+value#hl=en&sclient=psy-ab&q=input+text+value+example&oq=input+text+value+example&aq=f&aqi=q-A1&aql=&gs_l=serp.3..33i29.11385.13741.0.13852.20.13.1.4.4.0.164.1177.10j3.13.0...0.0.pAQWKOmV8pg&pbx=1&bav=on.2,or.r_gc.r_pw.r_cp.r_qf.,cf.osb&fp=356e729b7a15c7f0&ix=nh&biw=1986&bih=650");
            I.Expect.Value("input text value example").In("input[type='text']:first");
            I.Expect.Text("input text value example").In("input[type='text']:first");
        }

        public void MSDNInputField()
        {
            I.Open("http://msdn.microsoft.com/en-us/library/ie/ms535841(v=vs.85).aspx");
            I.Expect.Text("Search Dev Center with Bing").In("#HeaderSearchTextBox");
            I.Expect.Value("Search Dev Center with Bing").In("#HeaderSearchTextBox");
        }
    }
}
