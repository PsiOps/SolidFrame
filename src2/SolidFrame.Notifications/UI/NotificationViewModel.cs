
using System;

namespace SolidFrame.Notifications.UI
{
	public interface INotificationViewModel
	{
		Guid Id { get; }
		Guid SubjectId { get; }
		string SubjectName { get; }
		string Message { get; }
	}

	public class NotificationViewModel : INotificationViewModel
	{
		public Guid Id { get; private set; }
		public Guid SubjectId { get; private set; }
		public string SubjectName { get; private set; }
		public string Message { get; private set; }

		public NotificationViewModel(Guid notificationId, Guid subjectId, string subjectName, string message)
		{
			Id = notificationId;
			SubjectId = subjectId;
			SubjectName = subjectName;
			Message = message;
		}
	}
}
