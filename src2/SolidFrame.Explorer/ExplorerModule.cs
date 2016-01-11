using Microsoft.Practices.Unity;
using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Explorer.Logics;
using SolidFrame.Explorer.Types;
using SolidFrame.Explorer.UI;
using System.Collections.Generic;

namespace SolidFrame.Explorer
{
	public static class ExplorerModule
	{
		public static void RegisterExplorerTypes(this IUnityContainer container)
		{
			container.RegisterType<IEnumerable<IDocumentConfiguration>, IDocumentConfiguration[]>();
			container.RegisterType<IExplorerViewModel, ExplorerViewModel>(new ContainerControlledLifetimeManager());
			container.RegisterType<IExplorerItemViewModelFactory, ExplorerItemViewModelFactory>();
			container.RegisterType<IExplorerServiceDependencies, ExplorerServiceDependencies>();
		}
	}
}