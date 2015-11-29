using Microsoft.Practices.Unity;
using Prism.Modularity;
using SolidFrame.Core.Interfaces;

namespace Example.WPF.TranslationService
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
			_container.RegisterType<ITranslationService, Logics.TranslationService>(new ContainerControlledLifetimeManager());
		}
    }
}
