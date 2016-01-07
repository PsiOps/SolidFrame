namespace SolidFrame.Core.Interfaces.Ribbon
{
	public interface ICrudGroupController : IRibbonControlGroupsController
	{
		IRibbonButtonControl AddButton { get; }
		IRibbonButtonControl SaveButton { get; }
	}
}