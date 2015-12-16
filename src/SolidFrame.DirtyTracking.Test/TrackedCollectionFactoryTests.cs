using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.DirtyTracking.Logics;
using SolidFrame.DirtyTracking.Test.Stubs;
using System.Collections.ObjectModel;

namespace SolidFrame.DirtyTracking.Test
{
	[TestFixture]
	public class TrackedCollectionFactoryTests
	{
		private TrackedCollectionFactory<ITrackableModel, ITrackableViewModel> _trackedCollectionFactory;
		private Mock<ITrackerFactory<ITrackableModel, ITrackableViewModel>> _trackerFactoryMock;
		private Mock<ITracker<ITrackableModel, ITrackableViewModel>> _trackerMock;

		[SetUp]
		public void BeforeEach()
		{
			_trackerMock = new Mock<ITracker<ITrackableModel, ITrackableViewModel>>();
			_trackerFactoryMock = new Mock<ITrackerFactory<ITrackableModel, ITrackableViewModel>>();
			_trackerFactoryMock.Setup(f => f.Create()).Returns(_trackerMock.Object);
			_trackedCollectionFactory = new TrackedCollectionFactory<ITrackableModel, ITrackableViewModel>(_trackerFactoryMock.Object);
			_trackedCollectionFactory.Create(new Collection<ITrackableModel>());
		}

		[Test]
		public void It_calls_the_TrackerFactory_Create_method()
		{
			_trackerFactoryMock.Verify(f => f.Create(), Times.Once);
		}
	}
}
