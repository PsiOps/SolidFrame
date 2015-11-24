
using SolidFrame.Ribbon.Logics;

namespace SolidFrame.Ribbon.Types
{
	public interface IRibbonViewModelDependencies
	{
		IRibbonTabFactory RibbonTabFactory { get; }
	}

	public class RibbonViewModelDependencies : IRibbonViewModelDependencies
	{
		public RibbonViewModelDependencies(IRibbonTabFactory ribbonTabFactory)
		{
			RibbonTabFactory = ribbonTabFactory;
		}

		public IRibbonTabFactory RibbonTabFactory { get; private set; }
	}
}
