namespace SolidFrame.Core.Interfaces.Ribbon
{
	public interface IRibbonControlFactory
	{
		IRibbonButtonControl CreateRibbonButton(string name);
		IRibbonControlGroup CreateRibbonControlGroup(string name);
	}
}