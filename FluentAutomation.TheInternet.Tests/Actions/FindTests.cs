using FluentAutomation.Exceptions;
using FluentAutomation.Tests.Pages;
using Xunit;

namespace FluentAutomation.Tests.Actions
{
	public class FindTests : BaseTest
	{
		public FindTests()
		{
			CheckboxesPage.Go();
		}

		[Fact]
		public void FindElement()
		{
			var element = I.Find(CheckboxesPage.Checkbox1Selector).Element;
			Assert.Equal("checkbox", element.Attributes.Get("type"));
		}

		[Fact]
		public void TestFindElementThrowsIfElementNotFound()
		{
			Assert.Throws<FluentElementNotFoundException>(() => I.Find("doesntexist").Element);
		}

		[Fact]
		public void FindMultipleElements()
		{
			var elements = I.FindMultiple(CheckboxesPage.AllCheckboxesSelector);
			Assert.True(elements.Elements.Count > 1);

			// How to access the multiple elements? I would expect this syntax to work:
			// elements.Elements.ForEach(element =>
			// {
			// 	Assert.Equal("checkbox", element.Attributes.Get("type"));
			// });
		}

		[Fact]
		public void TestFindMultipleThrowsIfElementNotFound()
		{
			Assert.Throws<FluentElementNotFoundException>(() => I.FindMultiple("doesntexist").Element);
		}

		[Fact]
		public void AttemptToFindFakeElement()
		{
			var exception = Assert.Throws<FluentElementNotFoundException>(() => I.Find("#fake-control").Element.ToString());
			// accessing Element executes the Find
			Assert.True(exception.Message.Contains("Unable to find"));
		}
	}
}