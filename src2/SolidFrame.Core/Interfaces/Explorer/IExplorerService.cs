namespace SolidFrame.Core.Interfaces.Explorer
{
	public interface IExplorerService
	{
		void AddExplorerItem(IExplorerItem item);
		//void AddExplorerItem(IExplorerItem item, IExplorerItem parent);
		//IExplorerItem GetItemByName(string name);
	}
}