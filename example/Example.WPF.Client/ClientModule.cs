using Microsoft.Practices.Unity;

namespace Example.WPF.Client
{
	public static class ClientModule
	{
		public static void BootstrapClient(this IUnityContainer container)
		{
			container.RegisterType<IExampleExplorerItems, ExampleExplorerItems>();
		}
	}
}
