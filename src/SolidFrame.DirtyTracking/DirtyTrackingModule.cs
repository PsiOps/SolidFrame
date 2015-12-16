using Microsoft.Practices.Unity;
using Prism.Modularity;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.DirtyTracking.Logics;

namespace SolidFrame.DirtyTracking
{
	public class DirtyTrackingModule : IModule
	{
		private readonly IUnityContainer _container;

		public DirtyTrackingModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType(typeof (ITrackedCollectionFactory<,>), typeof (TrackedCollectionFactory<,>));
			_container.RegisterType(typeof (ITrackerFactory<,>), typeof (TrackerFactory<,>));
		}
	}
}
 
