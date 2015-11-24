using SolidFrame.Core.Interfaces;

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
			throw new System.NotImplementedException();
		}
	}
}
