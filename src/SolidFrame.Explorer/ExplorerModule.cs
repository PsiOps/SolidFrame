﻿using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using SolidFrame.Explorer.Types;
using SolidFrame.Explorer.UI;
using SolidFrame.Resources;

namespace SolidFrame.Explorer
{
	public class ExplorerModule : IModule
	{
		private readonly IRegionManager _regionManager;
		private readonly IUnityContainer _container;

		public ExplorerModule(IRegionManager regionManager, IUnityContainer container)
		{
			_regionManager = regionManager;
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType<IExplorerViewModel, ExplorerViewModel>();
			_container.RegisterType<IExplorerItemFactory, ExplorerItemFactory>();
			_container.RegisterType<IExplorerViewModelDependencies, ExplorerViewModelDependencies>();
			_regionManager.RegisterViewWithRegion(Regions.Explorer, typeof(ExplorerView));
		}
	}
}