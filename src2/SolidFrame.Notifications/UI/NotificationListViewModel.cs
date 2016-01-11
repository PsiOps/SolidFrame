using SolidFrame.Core.Interfaces.Translation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Notifications.UI
{
	public interface INotificationListViewModel : ITranslate
	{
		ICollection<INotificationViewModel> ItemsSource { get; set; }
	}

	public class NotificationListViewModel : INotificationListViewModel
	{
		public NotificationListViewModel()
		{
			ItemsSource = new ObservableCollection<INotificationViewModel>();

			Translations = new Dictionary<string, string>
			{
				{"SubjectName", "Naam"},
				{"Message", "Melding"}
			};
		}

		public ICollection<INotificationViewModel> ItemsSource { get; set; }

		public IDictionary<string, string> Translations { get; set; }
	}
}
