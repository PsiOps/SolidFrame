using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Ribbon.Controls;
using SolidFrame.Ribbon.Types;

namespace SolidFrame.Ribbon.Logics
{
	public class RibbonControlFactory : IRibbonControlFactory
	{
		public IRibbonButtonControl CreateRibbonButton(string name)
		{
			return new RibbonButton(name);
		}

		public IRibbonControlGroup CreateRibbonControlGroup(string name)
		{
			return new RibbonControlGroup(name);
		}
	}
}
