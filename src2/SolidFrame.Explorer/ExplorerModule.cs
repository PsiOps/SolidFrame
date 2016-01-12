using Microsoft.Practices.Unity;
using SolidFrame.Core.Interfaces.Explorer;
using SolidFrame.Explorer.Logics;
using SolidFrame.Explorer.Types;
using SolidFrame.Explorer.UI;

namespace SolidFrame.Explorer
{
	public static class ExplorerModule
	{
		public static void BootstrapExplorer(this IUnityContainer container)
		{
			container.RegisterType<IExplorerViewModel, ExplorerViewModel>(new ContainerControlledLifetimeManager());
			container.RegisterType<IExplorerItemViewModelFactory, ExplorerItemViewModelFactory>();
			container.RegisterType<IExplorerServiceDependencies, ExplorerServiceDependencies>();
			container.RegisterType<IExplorerService, ExplorerService>();
		}
	}
}