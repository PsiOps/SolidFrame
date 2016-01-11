
using SolidFrame.Notifications.UI;
using System;

namespace SolidFrame.Notifications.Logics
{
	public interface INotificationFactory
	{
		INotificationViewModel Create(Guid notificationId, Guid subjectId, string subjectName, string message);
	}

	public class NotificationFactory : INotificationFactory
	{
		public INotificationViewModel Create(Guid notificationId, Guid subjectId, string subjectName, string message)
		{
			return new NotificationViewModel(notificationId, subjectId, subjectName, message);
		}
	}
}
