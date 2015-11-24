using SolidFrame.Core.Interfaces;

namespace SolidFrame.Ribbon.Interfaces
{
	public interface ICrudGroupController : IRibbonControlGroupController
	{
		IRibbonControl AddButton { get; }
	}
}