using FluentAutomation.Exceptions;
using OpenQA.Selenium;
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
            I.Select(Option.Value, "QC").From(InputsPage.SelectControlSelector)
             .Assert.Text("Québec").In(InputsPage.SelectControlSelector);
        }

        [Fact]
        public void SelectIndex()
        {
            I.Select(3).From(InputsPage.SelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.SelectControlSelector)
                .Text("Manitoba").In(InputsPage.SelectControlSelector);
        }

        [Fact]
        public void SelectText()
        {
            I.Select("Québec").From(InputsPage.SelectControlSelector)
             .Assert.Value("QC").In(InputsPage.SelectControlSelector);
        }

        [Fact]
        public void SelectClearsOptionBetweenSelections()
        {
            I.Select("Québec").From(InputsPage.SelectControlSelector)
             .Assert.Value("QC").In(InputsPage.SelectControlSelector);

            I.Select("Manitoba").From(InputsPage.SelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.SelectControlSelector)
                .Value("QC").Not.In(InputsPage.SelectControlSelector);
        }

        [Fact]
        public void SelectTextFailed()
        {
            var exception = Assert.Throws<FluentException>(() => I.Select("NonExistentText").From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("NonExistentText"));
        }

        [Fact]
        public void SelectValueFailed()
        {
            var exception = Assert.Throws<FluentException>(() => I.Select(Option.Value, "NonExistentValue").From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("NonExistentValue"));
        }

        [Fact]
        public void SelectIndexFailed()
        {
            var exception = Assert.Throws<FluentException>(() => I.Select(1000).From(InputsPage.SelectControlSelector));
            Assert.True(exception.InnerException.Message.Contains("1000"));
        }

        [Fact]
        public void MultiSelectValue()
        {
            I.Select(Option.Value, "QC", "MB").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Québec").In(InputsPage.MultiSelectControlSelector)
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector)
                .Text("Alberta").Not.In(InputsPage.MultiSelectControlSelector);
        }

        [Fact]
        public void MultiSelectIndex()
        {
            I.Select(2).From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector);

            I.Select(2, 3, 4).From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Manitoba").In(InputsPage.MultiSelectControlSelector)
                .Text("Nouveau-Brunswick").In(InputsPage.MultiSelectControlSelector)
                .Text("Terre-Neuve").In(InputsPage.MultiSelectControlSelector);
        }

        [Fact]
        public void MultiSelectText()
        {
            I.Select("Manitoba").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.MultiSelectControlSelector);

            I.Select(Option.Text, "Nouveau-Brunswick").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Value("NB").In(InputsPage.MultiSelectControlSelector);

            I.Select("Manitoba", "Nouveau-Brunswick", "Terre-Neuve").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.MultiSelectControlSelector)
                .Value("NB").In(InputsPage.MultiSelectControlSelector)
                .Value("NL").In(InputsPage.MultiSelectControlSelector);

            I.Select(Option.Text, "Manitoba", "Nouveau-Brunswick", "Terre-Neuve").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Value("MB").In(InputsPage.MultiSelectControlSelector)
                .Value("NB").In(InputsPage.MultiSelectControlSelector)
                .Value("NL").In(InputsPage.MultiSelectControlSelector);
        }

        [Fact]
        public void MultiSelectClearOptionsBetweenSelections()
        {
            I.Select(Option.Value, "QC", "MB").From(InputsPage.MultiSelectControlSelector)
             .Assert.Text("Québec").In(InputsPage.MultiSelectControlSelector);

            I.Select(Option.Value, "AB").From(InputsPage.MultiSelectControlSelector)
             .Assert
                .Text("Alberta").In(InputsPage.MultiSelectControlSelector)
                .Text("Québec").Not.In(InputsPage.MultiSelectControlSelector);
        }
    }
}
