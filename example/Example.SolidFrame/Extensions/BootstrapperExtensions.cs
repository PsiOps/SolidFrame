using Example.WPF.Person;
using Example.WPF.Resources;
using Prism.Modularity;
using SolidFrame.Explorer;
using SolidFrame.Ribbon;
using SolidFrame.Translation;

namespace Example.WPF.Client.Extensions
{
	public static class BootstrapperExtensions
	{
		public static ModuleCatalog AddFrameworkModules(this ModuleCatalog moduleCatalog)
		{
			return moduleCatalog
				.AddModule(typeof(TranslationModule))
				.AddModule(typeof(RibbonModule))
				.AddModule(typeof(ExplorerModule), "ClientResourcesModule");
		}

		public static ModuleCatalog AddFrameworkExtensionModules(this ModuleCatalog moduleCatalog)
		{
			return moduleCatalog;
		}

		public static ModuleCatalog AddClientModules(this ModuleCatalog moduleCatalog)
		{
			return moduleCatalog
				.AddModule(typeof(ClientResourcesModule))
				.AddModule(typeof(PersonModule));
		}
	}
}
