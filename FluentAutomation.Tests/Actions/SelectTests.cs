using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
    public class SelectTests : BaseTest
    {
        public SelectTests()
            : base()
        {
            InputsPage.Go();
        }

        [Fact]
        public void SelectValue()
        {
            I.Select(Option.Value, "QC").From(InputsPage.SelectControlSelector);
            I.Assert.Value("QC").In(InputsPage.SelectControlSelector);
            I.Assert.Text("Québec").In(InputsPage.SelectControlSelector);
        }

        [Fact]
        public void MultiSelectValue()
        {
            I.Select(Option.Value, "QC", "MB").From(InputsPage.MultiSelectControlSelector);
            I.Assert.Value("QC").In(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Québec").In(InputsPage.MultiSelectControlSelector);
        }

        [Fact]
        public void SelectIndex()
        {
            I.Select(3).From(InputsPage.SelectControlSelector);
            I.Assert.Value("MB").In(InputsPage.SelectControlSelector);
            I.Assert.Text("Manitoba").In(InputsPage.SelectControlSelector);
        }

        [Fact]
        public void SelectText()
        {
            I.Select("Québec").From(InputsPage.SelectControlSelector);
            I.Assert.Value("QC").In(InputsPage.SelectControlSelector);
            I.Assert.Text("Québec").In(InputsPage.SelectControlSelector);
        }
    }
}
