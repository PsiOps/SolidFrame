using Example.WPF.Client.Extensions;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
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

		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<ShellView>();
		}

		protected override void InitializeModules()
		{
			base.InitializeModules();
			Application.Current.MainWindow = (ShellView)Shell;
			Application.Current.MainWindow.Show();
		}
	}
}
