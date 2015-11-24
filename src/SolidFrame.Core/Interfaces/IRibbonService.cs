
namespace SolidFrame.Core.Interfaces
{
	public interface IRibbonService
	{
		void Register(IListViewModel document);
		void UnRegister(IListViewModel document);
	}
}