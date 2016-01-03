
using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.DirtyTracking.Test.Stubs;
using SolidFrame.DirtyTracking.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SolidFrame.DirtyTracking.Test
{
	[TestFixture]
	public class DescribeTrackedCollectionConstruction
	{
		private TrackedCollection<TrackableModel, ITrackableViewModel> _trackedCollection; 
		private Mock<ITracker<TrackableModel, ITrackableViewModel>> _trackerMock;
		private TrackableModel _model;
		private ICollection<TrackableModel> _models;

		[SetUp]
		public void BeforeEach()
		{
			_trackerMock = new Mock<ITracker<TrackableModel, ITrackableViewModel>>();

			_model = new TrackableModel("test", 1);
			_models = new Collection<TrackableModel> { _model };

			_trackerMock.Setup(t => t.ConvertAndTrack(_models)).Returns<IEnumerable<TrackableModel>>(models =>
			{
				var viewModels = new Collection<ITrackableViewModel>();

				foreach (var model in models)
				{
					viewModels.Add(new TrackableViewModel(model));
				}

				return viewModels;
			}).Verifiable();

			_trackedCollection = new TrackedCollection<TrackableModel, ITrackableViewModel>(_models, _trackerMock.Object);
		}

		[Test]
		public void It_uses_the_tracker_to_convert_and_track_models()
		{
			_trackerMock.Verify(t => t.ConvertAndTrack(_models));
			Assert.AreEqual(1, _trackedCollection.Count);
		}
	}

	[TestFixture]
	public class DescribeTrackedCollectionAddTracked
	{
		private TrackedCollection<TrackableModel, ITrackableViewModel> _trackedCollection;
		private Mock<ITracker<TrackableModel, ITrackableViewModel>> _trackerMock;
		private TrackableModel _model;
		private ICollection<TrackableModel> _models;

		[SetUp]
		public void BeforeEach()
		{
			_trackerMock = new Mock<ITracker<TrackableModel, ITrackableViewModel>>();

			_model = new TrackableModel("test", 1);
			_models = new Collection<TrackableModel>();

			_trackerMock.Setup(t => t.ConvertAndTrack(_model)).Returns<TrackableModel>(model => new TrackableViewModel(model)).Verifiable();

			_trackerMock.Setup(t => t.ConvertAndTrack(_models)).Returns<IEnumerable<TrackableModel>>(models =>
			{
				var viewModels = new Collection<ITrackableViewModel>();

				foreach (var model in models)
				{
					viewModels.Add(new TrackableViewModel(model));
				}

				return viewModels;
			}).Verifiable();

			_trackedCollection = new TrackedCollection<TrackableModel, ITrackableViewModel>(_models, _trackerMock.Object);

			_trackedCollection.AddTracked(_model);
		}

		[Test]
		public void It_uses_the_tracker_to_convert_and_track_model_and_adds()
		{
			Assert.AreEqual(1, _trackedCollection.Count);
			_trackerMock.Verify(t => t.ConvertAndTrack(_model));
			Assert.AreEqual(_model.Id, _trackedCollection.Single().Id);
		}
	}

	[TestFixture]
	public class DescribeTrackedCollectionRemoveTrackedById
	{
		private TrackedCollection<TrackableModel, ITrackableViewModel> _trackedCollection;
		private Mock<ITracker<TrackableModel, ITrackableViewModel>> _trackerMock;
		private TrackableModel _model;
		private ICollection<TrackableModel> _models;
		private ITrackableViewModel _row;

		[SetUp]
		public void BeforeEach()
		{
			_trackerMock = new Mock<ITracker<TrackableModel, ITrackableViewModel>>();

			_model = new TrackableModel("test", 1);
			_models = new Collection<TrackableModel>{_model};

			_trackerMock.Setup(t => t.ConvertAndTrack(_model)).Returns<TrackableModel>(model => new TrackableViewModel(model)).Verifiable();

			_trackerMock.Setup(t => t.ConvertAndTrack(_models)).Returns<IEnumerable<TrackableModel>>(models =>
			{
				var viewModels = new Collection<ITrackableViewModel>();

				foreach (var model in models)
				{
					viewModels.Add(new TrackableViewModel(model));
				}

				return viewModels;
			}).Verifiable();

			_trackerMock.Setup(t => t.UnTrack(It.IsAny<ITrackableViewModel>()));

			_trackedCollection = new TrackedCollection<TrackableModel, ITrackableViewModel>(_models, _trackerMock.Object);

			_row = _trackedCollection.Single();

			_trackedCollection.RemoveTrackedById(_row.Id);
		}

		[Test]
		public void It_uses_the_tracker_to_untrack_and_removes()
		{
			_trackerMock.Verify(t => t.UnTrack(_row));
			Assert.AreEqual(0, _trackedCollection.Count);
		}
	}

	[TestFixture]
	public class DescribeTrackedCollectionGetDirtyModelsAndIsDirtyAndClean
	{
		private TrackedCollection<TrackableModel, ITrackableViewModel> _trackedCollection;
		private Mock<ITracker<TrackableModel, ITrackableViewModel>> _trackerMock;
		private TrackableModel _model;
		private ICollection<TrackableModel> _models;

		[SetUp]
		public void BeforeEach()
		{
			_trackerMock = new Mock<ITracker<TrackableModel, ITrackableViewModel>>();

			_model = new TrackableModel("test", 1);
			_models = new Collection<TrackableModel>{_model};

			_trackerMock.Setup(t => t.ConvertAndTrack(_model)).Returns<TrackableModel>(model => new TrackableViewModel(model)).Verifiable();

			_trackerMock.Setup(t => t.ConvertAndTrack(_models)).Returns<IEnumerable<TrackableModel>>(models =>
			{
				var viewModels = new Collection<ITrackableViewModel>();

				foreach (var model in models)
				{
					viewModels.Add(new TrackableViewModel(model));
				}

				return viewModels;
			}).Verifiable();

			_trackerMock.Setup(t => t.GetDirtyModels()).Verifiable();
			_trackerMock.SetupGet(t => t.IsDirty).Verifiable();

			_trackedCollection = new TrackedCollection<TrackableModel, ITrackableViewModel>(_models, _trackerMock.Object);
		}

		[Test]
		public void It_delegates_the_GetDirtyModels_call_to_the_tracker()
		{
			_trackedCollection.GetDirtyModels();

			_trackerMock.Verify(t => t.GetDirtyModels(), Times.Once);
		}

		[Test]
		public void It_delegates_the_IsDirty_call_to_the_tracker()
		{
			var isDirty = _trackedCollection.IsDirty;

			_trackerMock.Verify(t => t.IsDirty, Times.Once);
		}

		[Test]
		public void It_delegates_the_Clean_call_to_the_tracker()
		{
			_trackedCollection.Clean();

			_trackerMock.Verify(t => t.Clean(), Times.Once);
		}

		[Test]
		public void It_raises_the_IsDirtyChanged_event_if_raised_by_Tracker()
		{
			var eventRaised = false;

			_trackedCollection.IsDirtyChanged += state =>
			{
				eventRaised = true;
			};

			_trackerMock.Raise(t => t.IsDirtyChanged += null, true);

			Assert.IsTrue(eventRaised);
		}
	}
}
