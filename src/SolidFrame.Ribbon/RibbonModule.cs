using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Logics;
using SolidFrame.Ribbon.UI;

namespace SolidFrame.Ribbon
{
	public class RibbonModule : IModule
	{
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _container;

		public RibbonModule(IRegionManager regionManager, IUnityContainer container)
		{
			_regionManager = regionManager;
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType<ICrudGroupsController, CrudGroupController>(new ContainerControlledLifetimeManager());
			_container.RegisterType<IRibbonViewModel, RibbonViewModel>(new ContainerControlledLifetimeManager());
			_regionManager.RegisterViewWithRegion("RibbonRegion", typeof(RibbonView));
		}
	}
}
