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
			_container.RegisterType<ICrudControlService, CrudControlService>();
			_container.RegisterType<ICrudGroupController, CrudControlService>();
			_container.RegisterType<IRibbonViewModel, RibbonViewModel>();
			_regionManager.RegisterViewWithRegion("RibbonRegion", typeof(RibbonView));
		}
	}
}
