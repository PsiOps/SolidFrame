using SolidFrame.Ribbon.UI;

namespace SolidFrame.Ribbon.Types
{
	public interface IRibbonServiceDependencies
	{
		IRibbonViewModel RibbonViewModel { get; }
	}

	public class RibbonServiceDependencies : IRibbonServiceDependencies
	{
		public RibbonServiceDependencies(IRibbonViewModel ribbonViewModel)
		{
			RibbonViewModel = ribbonViewModel;
		}

		public IRibbonViewModel RibbonViewModel { get; private set; }
	}
}
