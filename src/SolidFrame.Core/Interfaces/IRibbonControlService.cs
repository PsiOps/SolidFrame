namespace SolidFrame.Core.Interfaces
{
	public interface IRibbonControlService
	{
		void Register(IListViewModel document);
		void UnRegister(IListViewModel document);
	}
}