using Example.WPF.Person;
using Example.WPF.Resources;
using Example.WPF.Translations;
using Prism.Modularity;
using SolidFrame.Explorer;
using SolidFrame.Resources;
using SolidFrame.Ribbon;
using SolidFrame.Validation;

namespace Example.WPF.Client.Extensions
{
	public static class BootstrapperExtensions
	{
		public static ModuleCatalog AddFrameworkModules(this ModuleCatalog moduleCatalog)
		{
			return moduleCatalog
				.AddModule(typeof(RibbonModule))
				.AddModule(typeof(ExplorerModule), "ClientResourcesModule", "PersonModule")
				.AddModule(typeof(ValidationModule), "CoreResourceModule")
				.AddModule(typeof(CoreResourceModule));
		}

		public static ModuleCatalog AddFrameworkExtensionModules(this ModuleCatalog moduleCatalog)
		{
			return moduleCatalog
				.AddModule(typeof(TranslationModule));
		}

		public static ModuleCatalog AddClientModules(this ModuleCatalog moduleCatalog)
		{
			return moduleCatalog
				.AddModule(typeof(ClientResourcesModule))
				.AddModule(typeof(PersonModule));
		}
	}
}
