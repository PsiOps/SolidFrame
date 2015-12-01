using Microsoft.Practices.Unity;
using Prism.Modularity;
using SolidFrame.Core.Interfaces.Document;

namespace Example.WPF.Resources
{
	public class ClientResourcesModule : IModule
	{
		private readonly IUnityContainer _container;

		public ClientResourcesModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType<IDocumentCategoryCatalog, ExampleDocumentCategoryCatalog>();
		}
	}
}
