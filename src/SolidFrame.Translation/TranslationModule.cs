using Microsoft.Practices.Unity;
using Prism.Modularity;
using SolidFrame.Core.Interfaces;
using SolidFrame.Translation.Logics;
using SolidFrame.Translation.Types;

namespace SolidFrame.Translation
{
	public class TranslationModule : IModule
	{
		private readonly IUnityContainer _container;

		public TranslationModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType<ITranslationServiceDependencies, TranslationServiceDependencies>();
			_container.RegisterType<ITranslationService, TranslationService>();
		}
	}
}
