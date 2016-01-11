using Microsoft.Practices.Unity;
using SolidFrame.Core.Interfaces.Notifications;
using SolidFrame.Notifications.Logics;
using SolidFrame.Notifications.Types;
using SolidFrame.Notifications.UI;

namespace SolidFrame.Notifications
{
	public static class NotificationsModule
	{
		public static void RegisterNotificationTypes(this IUnityContainer container)
		{
			container.RegisterType<INotificationListViewModel, NotificationListViewModel>(new ContainerControlledLifetimeManager());
			container.RegisterType<INotificationService, NotificationService>(new ContainerControlledLifetimeManager());
			container.RegisterType<INotificationServiceDependencies, NotificationServiceDependencies>();
			container.RegisterType<INotificationFactory, NotificationFactory>();
		}
	}
}
