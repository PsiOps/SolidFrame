using Example.WPF.Person;
using Example.WPF.Resources;
using Example.WPF.Translations;
using Prism.Modularity;
using SolidFrame.Explorer.WPF;
using SolidFrame.Notifications.WPF;
using SolidFrame.Ribbon.WPF;

namespace Example.WPF.Client.Extensions
{
	public static class BootstrapperExtensions
	{
		public static ModuleCatalog AddFrameworkModules(this ModuleCatalog moduleCatalog)
		{
			return moduleCatalog
				.AddModule(typeof (RibbonModule))
				.AddModule(typeof (NotificationsModule))
				.AddModule(typeof (ExplorerModule), dependsOn: "TranslationModule")
				;
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
