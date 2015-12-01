using Example.WPF.Translations.Logics;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using SolidFrame.Core.Interfaces;
using SolidFrame.Core.Interfaces.Translation;

namespace Example.WPF.Translations
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
			_container.RegisterType<ITranslationService, TranslationService>(new ContainerControlledLifetimeManager());
		}
	}
}
