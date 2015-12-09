
namespace SolidFrame.Notifications.UI
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
