using FluentAutomation.Exceptions;
using FluentAutomation.Tests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Base
{
    public class PageObjectTests : BaseTest
    {
        [Fact]
        public void SwitchPageObject()
        {
            SwitchPage.Go();

            var switchPage = InputsPage.Switch<SwitchPage>();
            Assert.True(switchPage.Url.EndsWith("Switch"));

            InputsPage.Go();

            // throw because we aren't on the SwitchPage and nothing is navigating us there
            Assert.Throws<FluentException>(() => InputsPage.Switch<SwitchPage>());
        }
    }
}
