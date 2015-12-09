using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using SolidFrame.Core.Interfaces.Notifications;
using SolidFrame.Notifications.Logics;
using SolidFrame.Notifications.Types;
using SolidFrame.Notifications.UI;
using SolidFrame.Resources;

namespace SolidFrame.Notifications
{
	public class NotificationsModule : IModule
	{
		private readonly IUnityContainer _container;
		private readonly RegionManager _regionManager;

		public NotificationsModule(IUnityContainer container, RegionManager regionManager)
		{
			_container = container;
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_container.RegisterType<INotificationListViewModel, NotificationListViewModel>(new ContainerControlledLifetimeManager());
			_container.RegisterType<INotificationService, NotificationService>(new ContainerControlledLifetimeManager());
			_container.RegisterType<INotificationServiceDependencies, NotificationServiceDependencies>();
			_container.RegisterType<INotificationFactory, NotificationFactory>();

			_regionManager.RegisterViewWithRegion(Regions.Notification, typeof (NotificationView));
		}
	}
}
