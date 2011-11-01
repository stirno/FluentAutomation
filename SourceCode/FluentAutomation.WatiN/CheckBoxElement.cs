using FluentAutomation.API.Interfaces;
using WatiN.Core;

namespace FluentAutomation.WatiN
{
	public class CheckBoxElement : Element, ICheckBoxElement
	{
		private CheckBox _element = null;

		public CheckBoxElement(CheckBox element)
			: base(element)
		{
			_element = element;
		}

		public bool Checked
		{
			get { return _element.Checked; }
			set { _element.Checked = value; }
		}
	}
}