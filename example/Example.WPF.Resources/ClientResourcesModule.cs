using Example.WPF.Resources.Web;
using Example.WPF.Resources.Web.Configurations;
using Microsoft.Practices.Unity;
using Prism.Modularity;

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
			_container.RegisterType<IPersonResourceConfiguration, PersonResourceConfiguration>();
			_container.RegisterType<IPersonResource, PersonResource>();
		}
	}
}
