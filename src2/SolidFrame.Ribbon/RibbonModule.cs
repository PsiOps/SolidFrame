using Microsoft.Practices.Unity;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Ribbon.Logics;
using SolidFrame.Ribbon.Types;
using SolidFrame.Ribbon.UI;

namespace SolidFrame.Ribbon
{
	public class RibbonModule
	{
		private readonly IUnityContainer _container;

		public RibbonModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType<IRibbonTabFactory, RibbonTabFactory>();
			_container.RegisterType<IRibbonControlFactory, RibbonControlFactory>();
			_container.RegisterType<ICrudGroupControllerDependencies, CrudGroupControllerDependencies>();
			_container.RegisterType<ICrudGroupController, CrudGroupController>(new ContainerControlledLifetimeManager());
			_container.RegisterType<IRibbonViewModel, RibbonViewModel>(new ContainerControlledLifetimeManager());
		}
	}
}
