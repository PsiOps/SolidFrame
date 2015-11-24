using SolidFrame.Ribbon.Logics;

namespace SolidFrame.Core.Interfaces
{
	public interface ICrudGroupController : IRibbonControlGroupController
	{
		IRibbonButtonControl AddButton { get; }
	}
}