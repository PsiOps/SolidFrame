using Microsoft.Practices.Unity;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.DirtyTracking.Logics;

namespace SolidFrame.DirtyTracking
{
	public static class DirtyTrackingModule
	{
		public static void RegisterTypes(this IUnityContainer container)
		{
			container.RegisterType(typeof(ITrackedCollectionFactory<,>), typeof(TrackedCollectionFactory<,>));
			container.RegisterType(typeof(ITrackerFactory<,>), typeof(TrackerFactory<,>));
		}
	}
}
 
