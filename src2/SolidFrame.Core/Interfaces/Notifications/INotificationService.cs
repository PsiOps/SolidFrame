using System;

namespace SolidFrame.Core.Interfaces.Notifications
{
	public interface INotificationService
	{
		void TryRemoveNotification(Guid notificationId, Guid subjectId);
		void AddNotification(Guid notificationId, Guid subjectId, string subjectName, string message);
	}
}