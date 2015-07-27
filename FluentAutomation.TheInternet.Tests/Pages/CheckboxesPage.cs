namespace FluentAutomation.Tests.Pages
{
	public class CheckboxesPage : PageObject<CheckboxesPage>
	{
		public const string AllCheckboxesSelector = "#checkboxes > input";
		public const string Checkbox1Selector = "#checkboxes > input:nth-child(1)";

		public CheckboxesPage(FluentTest test) : base(test)
		{
			Url = "/checkboxes";
		}
	}
}