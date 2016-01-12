using Microsoft.Practices.Unity;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Ribbon.Logics;
using SolidFrame.Ribbon.Types;
using SolidFrame.Ribbon.UI;

namespace SolidFrame.Ribbon
{
	public static class RibbonModule
	{
		public static void BootstrapRibbon(this IUnityContainer container)
		{
			container.RegisterType<IRibbonTabFactory, RibbonTabFactory>();
			container.RegisterType<IRibbonControlFactory, RibbonControlFactory>();
			container.RegisterType<ICrudGroupControllerDependencies, CrudGroupControllerDependencies>();
			container.RegisterType<ICrudGroupController, CrudGroupController>(new ContainerControlledLifetimeManager());
			container.RegisterType<IRibbonViewModel, RibbonViewModel>(new ContainerControlledLifetimeManager());
		}
	}
}
