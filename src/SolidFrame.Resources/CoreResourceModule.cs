using Microsoft.Practices.Unity;
using Prism.Modularity;
using SolidFrame.Resources.Helpers;

namespace SolidFrame.Resources
{
	public class CoreResourceModule : IModule
	{
		private readonly IUnityContainer _container;

		public CoreResourceModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType<IPropertyNameHelper, PropertyNameHelper>();
		}
	}
}
