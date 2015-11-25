namespace SolidFrame.Core.Interfaces
{
	public interface ICrudGroupsController : IRibbonControlGroupsController
	{
		IRibbonButtonControl AddButton { get; }
	}
}