using Prism.Modularity;
using Prism.Regions;
using SolidFrame.Ribbon.WPF.UI;

namespace SolidFrame.Ribbon.WPF
{
	public class RibbonModule : IModule
	{
		private readonly IRegionManager _regionManager;

		public RibbonModule(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_regionManager.RegisterViewWithRegion("RibbonRegion", typeof(RibbonView));
		}
	}
}
