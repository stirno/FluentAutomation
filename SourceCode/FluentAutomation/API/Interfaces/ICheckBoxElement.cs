namespace FluentAutomation.API.Interfaces
{
	/// <summary>
	/// Interface for <select />
	/// </summary>
	public interface ICheckBoxElement : IElement
	{
		/// <summary>
		/// Gets the checked status.
		/// </summary>
		/// <returns></returns>
		bool Checked { get; set; }
	}
}