namespace SolidFrame.Core.Interfaces.General
{
	public interface ICanBeBusy
	{
		bool IsBusy { get; }
		string IsBusyText { get; }
	}
}