using Prism.Modularity;
using Prism.Regions;
using SolidFrame.Notifications.WPF.UI;
using SolidFrame.Resources;

namespace SolidFrame.Notifications.WPF
{
	public class NotificationsModule : IModule
	{
		private readonly RegionManager _regionManager;

		public NotificationsModule(RegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		public void Initialize()
		{
			_regionManager.RegisterViewWithRegion(Regions.Notification, typeof (NotificationView));
		}
	}
}
