using Example.WPF.Client.Extensions;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using SolidFrame.Core.Interfaces.Explorer;
using SolidFrame.DirtyTracking;
using SolidFrame.Explorer;
using SolidFrame.Notifications;
using SolidFrame.Ribbon;
using SolidFrame.Validation;
using System.Windows;

namespace Example.WPF.Client
{
	public class Bootstrapper : UnityBootstrapper
	{
		protected override void ConfigureModuleCatalog()
		{
			base.ConfigureModuleCatalog();

			var moduleCatalog = (ModuleCatalog)ModuleCatalog;

			moduleCatalog
				.AddFrameworkModules()
				.AddFrameworkExtensionModules()
				.AddClientModules();
		}

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

			Container.BootstrapDirtyTracking();
			Container.BootstrapExplorer();
			Container.BootstrapNotifications();
			Container.BootstrapRibbon();
			Container.BootstrapValidation();

			Container.BootstrapClient();
		}

		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<ShellView>();
		}

		protected override void InitializeModules()
		{
			base.InitializeModules();

			LoadExplorer();

			Application.Current.MainWindow = (ShellView)Shell;
			Application.Current.MainWindow.Show();
		}

		private void LoadExplorer()
		{
			var explorerItems = Container.Resolve<IExampleExplorerItems>();

			Container.Resolve<IExplorerService>().AddExplorerItem(explorerItems.TopNode);
		}
	}
}
