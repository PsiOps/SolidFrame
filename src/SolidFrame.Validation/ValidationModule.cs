using Microsoft.Practices.Unity;
using Prism.Modularity;
using System;

namespace SolidFrame.Validation
{
	public class ValidationModule : IModule
	{
		private readonly IUnityContainer _container;

		public ValidationModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			throw new NotImplementedException();
		}
	}
}
