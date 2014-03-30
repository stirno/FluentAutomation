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
            I.Select(Option.Value, "QC").From(InputsPage.SelectControlSelector);
            I.Assert.Text("Québec").In(InputsPage.SelectControlSelector);
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
        }

        [Fact]
        public void SelectTextFailed()
        {
            var exception = Assert.Throws<NoSuchElementException>(() =>
            {
                I.Select("NonExistentText").From(InputsPage.SelectControlSelector);
            });

            Assert.True(exception.InnerException.Message.Contains("NonExistentText"));
        }

        [Fact]
        public void SelectValueFailed()
        {
            var exception = Assert.Throws<NoSuchElementException>(() =>
            {
                I.Select(Option.Value, "NonExistentValue").From(InputsPage.SelectControlSelector);
            });

            Assert.True(exception.InnerException.Message.Contains("NonExistentValue"));
        }

        [Fact]
        public void SelectIndexFailed()
        {
            var exception = Assert.Throws<FluentException>(() =>
            {
                I.Select(1000).From(InputsPage.SelectControlSelector);
            });

            Assert.True(exception.InnerException.Message.Contains("1000"));
        }

        [Fact]
        public void MultiSelectValue()
        {
            I.Select(Option.Value, "QC", "MB").From(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Québec").In(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Manitoba").In(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Alberta").Not.In(InputsPage.MultiSelectControlSelector);
        }

        [Fact]
        public void MultiSelectIndex()
        {
            I.Select(3, 4, 5).From(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Manitoba").In(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Nouveau-Brunswick").In(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Terre-Neuve").In(InputsPage.MultiSelectControlSelector);
        }

        [Fact]
        public void MultiSelectText()
        {
            I.Select("Manitoba", "Nouvea-Brunswick", "Terra-Neuve").From(InputsPage.MultiSelectControlSelector);
            I.Assert.Value("MB").In(InputsPage.MultiSelectControlSelector);
            I.Assert.Value("NB").In(InputsPage.MultiSelectControlSelector);
            I.Assert.Value("NL").In(InputsPage.MultiSelectControlSelector);
        }

        [Fact]
        public void MultiSelectClearOptionsBetweenSelections()
        {
            I.Select(Option.Value, "QC", "MB").From(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Québec").In(InputsPage.MultiSelectControlSelector);

            I.Select(Option.Value, "AB").From(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Alberta").In(InputsPage.MultiSelectControlSelector);
            I.Assert.Text("Québec").Not.In(InputsPage.MultiSelectControlSelector);
        }

    }
}
