using SolidFrame.Notifications.Logics;
using SolidFrame.Notifications.UI;

namespace SolidFrame.Notifications.Types
{
	public interface INotificationServiceDependencies
	{
		INotificationListViewModel NotificationListViewModel { get; }
		INotificationFactory NotificationFactory { get; }
	}

	public class NotificationServiceDependencies : INotificationServiceDependencies
	{
		public NotificationServiceDependencies(INotificationListViewModel notificationListViewModel, INotificationFactory notificationFactory)
		{
			NotificationListViewModel = notificationListViewModel;
			NotificationFactory = notificationFactory;
		}

		public INotificationListViewModel NotificationListViewModel { get; private set; }
		public INotificationFactory NotificationFactory { get; private set; }
	}
}
