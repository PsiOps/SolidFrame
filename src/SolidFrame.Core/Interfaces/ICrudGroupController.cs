namespace SolidFrame.Core.Interfaces
{
	public interface ICrudGroupController : IRibbonControlGroupController
	{
		IRibbonControl AddButton { get; }
	}
}