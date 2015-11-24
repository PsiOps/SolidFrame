namespace SolidFrame.Core.Interfaces
{
	public interface ICanBeBusy
	{
		bool IsBusy { get; }
		string IsBusyText { get; }
	}
}