
using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Logics;

namespace SolidFrame.Ribbon.Types
{
	public interface IRibbonViewModelDependencies
	{
		IRibbonTabFactory RibbonTabFactory { get; }
		ICrudGroupController CrudGroupController { get; }
	}

	public class RibbonViewModelDependencies : IRibbonViewModelDependencies
	{
		public RibbonViewModelDependencies(IRibbonTabFactory ribbonTabFactory, ICrudGroupController crudGroupController)
		{
			RibbonTabFactory = ribbonTabFactory;
			CrudGroupController = crudGroupController;
		}

		public IRibbonTabFactory RibbonTabFactory { get; private set; }
		public ICrudGroupController CrudGroupController { get; private set; }
	}
}
