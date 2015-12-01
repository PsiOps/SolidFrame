using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Ribbon.UI;

namespace SolidFrame.Ribbon.Logics
{
	public interface IRibbonTabFactory
	{
		IRibbonTab Create(string name);
	}

	public class RibbonTabFactory : IRibbonTabFactory
	{
		public IRibbonTab Create(string name)
		{
			return new RibbonTab(name);
		}
	}
}
