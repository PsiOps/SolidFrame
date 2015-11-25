using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace SolidFrame.Notifications
{
	public class NotificationsModule : IModule
	{
		private readonly IUnityContainer _container;

		public NotificationsModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			// TODO Register the viewmodel
			// TODO Load the View into the notificationregion
		}
	}
}
