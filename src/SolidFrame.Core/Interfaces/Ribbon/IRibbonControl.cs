
namespace SolidFrame.Core.Interfaces.Ribbon
{
	public interface IRibbonControl
	{
		string Name { get; }
		bool IsEnabled { get; set; }
	}
}