using Prism.Modularity;
using Prism.Regions;
using SolidFrame.Explorer.WPF.UI;
using SolidFrame.Resources;

namespace SolidFrame.Explorer.WPF
{
	public class ExplorerModule : IModule
	{
		private readonly IRegionManager _regionManager;

		public ExplorerModule(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_regionManager.RegisterViewWithRegion(Regions.Explorer, typeof(ExplorerView));
		}
	}
}
