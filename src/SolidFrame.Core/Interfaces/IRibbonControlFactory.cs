namespace SolidFrame.Core.Interfaces
{
	public interface IRibbonControlFactory
	{
		IRibbonButtonControl CreateRibbonButton();
		IRibbonControlGroup CreateRibbonControlGroup(string name);
	}
}