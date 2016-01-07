using Microsoft.Practices.Unity;
using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Explorer.Types;
using SolidFrame.Explorer.UI;
using System.Collections.Generic;

namespace SolidFrame.Explorer
{
	public static class ExplorerModule
	{
		public static void RegisterTypes(this IUnityContainer container)
		{
			container.RegisterType<IEnumerable<IDocumentConfiguration>, IDocumentConfiguration[]>();
			container.RegisterType<IExplorerViewModel, ExplorerViewModel>(new ContainerControlledLifetimeManager());
			container.RegisterType<IExplorerItemFactory, ExplorerItemFactory>();
			container.RegisterType<IExplorerViewModelDependencies, ExplorerViewModelDependencies>();
		}
	}
}