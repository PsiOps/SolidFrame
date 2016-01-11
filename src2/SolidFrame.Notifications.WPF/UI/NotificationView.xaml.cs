
using SolidFrame.Notifications.UI;

namespace SolidFrame.Notifications.WPF.UI
{
	/// <summary>
	/// Interaction logic for NotificationView.xaml
	/// </summary>
	public partial class NotificationView
	{
		public NotificationView(INotificationListViewModel notificationListViewModel)
		{
			DataContext = notificationListViewModel;
			InitializeComponent();
		}
	}
}
