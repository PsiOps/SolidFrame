using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Logics;
using SolidFrame.Ribbon.UI;

namespace SolidFrame.Ribbon.Types
{
	public interface ICrudControlServiceDependencies
	{
		IRibbonTabFactory RibbonTabFactory { get; }
		IRibbonControlFactory RibbonControlFactory { get; }
		IRibbonViewModel RibbonViewModel { get; }
	}

	public class CrudControlServiceDependencies : ICrudControlServiceDependencies
	{
		public CrudControlServiceDependencies(IRibbonControlFactory ribbonControlFactory, IRibbonViewModel ribbonViewModel, IRibbonTabFactory ribbonTabFactory)
		{
			RibbonControlFactory = ribbonControlFactory;
			RibbonViewModel = ribbonViewModel;
			RibbonTabFactory = ribbonTabFactory;
		}

		public IRibbonTabFactory RibbonTabFactory { get; private set; }
		public IRibbonControlFactory RibbonControlFactory { get; private set; }
		public IRibbonViewModel RibbonViewModel { get; private set; }
	}
}