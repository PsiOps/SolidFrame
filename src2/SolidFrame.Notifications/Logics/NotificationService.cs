using SolidFrame.Core.Interfaces.Notifications;
using SolidFrame.Notifications.Types;
using SolidFrame.Notifications.UI;
using System;
using System.Linq;

namespace SolidFrame.Notifications.Logics
{
	public class NotificationService : INotificationService
	{
		private readonly INotificationListViewModel _notificationListViewModel;
		private readonly INotificationFactory _notificationFactory;

		public NotificationService(INotificationServiceDependencies dependencies)
		{
			_notificationListViewModel = dependencies.NotificationListViewModel;
			_notificationFactory = dependencies.NotificationFactory;
		}

		public void TryRemoveNotification(Guid notificationId, Guid subjectId)
		{
			var notificationToRemove =
				_notificationListViewModel.ItemsSource.SingleOrDefault(n => n.Id == notificationId && n.SubjectId == subjectId);

			if (notificationToRemove == null) return;

			_notificationListViewModel.ItemsSource.Remove(notificationToRemove);
		}

		public void AddNotification(Guid notificationId, Guid subjectId, string subjectName, string message)
		{
			var notification = _notificationFactory.Create(notificationId, subjectId, subjectName, message);

			_notificationListViewModel.ItemsSource.Add(notification);
		}
	}
}
