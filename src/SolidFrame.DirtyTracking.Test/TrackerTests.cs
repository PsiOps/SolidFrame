
using Moq;
using NUnit.Framework;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.DirtyTracking.Logics;
using SolidFrame.DirtyTracking.Test.Stubs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SolidFrame.DirtyTracking.Test
{
	[TestFixture]
	public class DescribeConvertAndTrack
	{
		private ITracker<TrackableModel, ITrackableViewModel> _tracker;
		private Mock<IRowViewModelFactory<TrackableModel, ITrackableViewModel>> _rowViewModelFactoryMock; 

		[SetUp]
		public void BeforeEach()
		{
			_rowViewModelFactoryMock = new Mock<IRowViewModelFactory<TrackableModel, ITrackableViewModel>>();
			_rowViewModelFactoryMock.Setup(f => f.Create(It.IsAny<TrackableModel>())).Returns<TrackableModel>(m => new TrackableViewModel(m));

			_tracker = new Tracker<TrackableModel, ITrackableViewModel>(_rowViewModelFactoryMock.Object);

			var models = new Collection<TrackableModel>
			{
				new TrackableModel("test1", 1),
				new TrackableModel("test2", 2),
				new TrackableModel("test3", 3),
				new TrackableModel("test4", 4)
			};

			_tracker.ConvertAndTrack(models);
		}

		[Test]
		public void It_calls_RowViewModelFactory_Create_for_each_model()
		{
			_rowViewModelFactoryMock.Verify(f => f.Create(It.IsAny<TrackableModel>()), Times.Exactly(4));
		}
	}

	[TestFixture]
	public class DescribePropertyChangeTracking
	{
		private ITracker<TrackableModel, ITrackableViewModel> _tracker;
		private Mock<IRowViewModelFactory<TrackableModel, ITrackableViewModel>> _rowViewModelFactoryMock;
		private IEnumerable<ITrackableViewModel> _rows;

		[SetUp]
		public void BeforeEach()
		{
			_rowViewModelFactoryMock = new Mock<IRowViewModelFactory<TrackableModel, ITrackableViewModel>>();
			_rowViewModelFactoryMock.Setup(f => f.Create(It.IsAny<TrackableModel>())).Returns<TrackableModel>(m => new TrackableViewModel(m));

			_tracker = new Tracker<TrackableModel, ITrackableViewModel>(_rowViewModelFactoryMock.Object);

			var models = new Collection<TrackableModel>
			{
				new TrackableModel("test1", 1),
				new TrackableModel("test2", 2),
				new TrackableModel("test3", 3),
				new TrackableModel("test4", 4)
			};

			_rows = _tracker.ConvertAndTrack(models);
		}

		[Test]
		public void It_retrieves_dirty_rows()
		{
			var row = _rows.First();

			row.Name = "Changed";

			var dirtyModels = _tracker.GetDirtyModels().ToList();

			Assert.AreEqual(1, dirtyModels.Count);
			Assert.AreEqual(row.Id, dirtyModels.Single().Id);
		}

		[Test]
		public void It_gets_dirty()
		{
			var row = _rows.First();

			row.Name = "Changed";

			Assert.IsTrue(_tracker.IsDirty);
		}

		[Test]
		public void It_raises_IsDirtyChanged_event_when_first_row_is_made_Dirty()
		{
			var eventRaised = false;

			_tracker.IsDirtyChanged += state => eventRaised = true;

			var row = _rows.First();

			row.Name = "Changed";

			Assert.IsTrue(eventRaised);
		}

		[Test]
		public void It_does_not_raise_IsDirtyChanged_when_second_row_is_made_Dirty()
		{
			bool eventRaised;

			_tracker.IsDirtyChanged += state => eventRaised = true;

			var firstRow = _rows.First();
			var secondRow = _rows.Skip(1).First();

			firstRow.Name = "Changed";

			eventRaised = false;

			secondRow.Name = "AlsoChanged";

			Assert.IsFalse(eventRaised);
		}

		[Test]
		public void It_does_not_retrieve_rows_made_clean()
		{
			var row = _rows.First();

			row.Name = "Changed";

			var dirtyModels = _tracker.GetDirtyModels().ToList();

			Assert.AreEqual(1, dirtyModels.Count);
			Assert.AreEqual(row.Id, dirtyModels.Single().Id);

			row.Name = "test1";

			Assert.AreEqual(0, _tracker.GetDirtyModels().Count());
		}

		[Test]
		public void It_does_not_raise_IsDirtyChanged_when_row_is_made_clean_but_Dirty_rows_remain()
		{
			bool eventRaised;

			_tracker.IsDirtyChanged += state => eventRaised = true;

			var firstRow = _rows.First();
			var secondRow = _rows.Skip(1).First();

			firstRow.Name = "Changed";
			secondRow.Name = "AlsoChanged";

			eventRaised = false;

			firstRow.Name = "test1";

			Assert.IsFalse(eventRaised);

		}

		[Test]
		public void It_raises_IsDirtyChanged_event_when_last_Dirty_row_is_made_clean()
		{
			bool eventRaised;

			_tracker.IsDirtyChanged += state => eventRaised = true;

			var firstRow = _rows.First();
			var secondRow = _rows.Skip(1).First();

			firstRow.Name = "Changed";
			secondRow.Name = "AlsoChanged";
			firstRow.Name = "test1";

			eventRaised = false;

			secondRow.Name = "test2";

			Assert.IsTrue(eventRaised);
		}
	}

	[TestFixture]
	public class DescribeUnTracking
	{
		private ITracker<TrackableModel, ITrackableViewModel> _tracker;
		private Mock<IRowViewModelFactory<TrackableModel, ITrackableViewModel>> _rowViewModelFactoryMock;
		private IEnumerable<ITrackableViewModel> _rows;

		[SetUp]
		public void BeforeEach()
		{
			_rowViewModelFactoryMock = new Mock<IRowViewModelFactory<TrackableModel, ITrackableViewModel>>();
			_rowViewModelFactoryMock.Setup(f => f.Create(It.IsAny<TrackableModel>())).Returns<TrackableModel>(m => new TrackableViewModel(m));

			_tracker = new Tracker<TrackableModel, ITrackableViewModel>(_rowViewModelFactoryMock.Object);

			var models = new Collection<TrackableModel>
			{
				new TrackableModel("test1", 1),
				new TrackableModel("test2", 2),
				new TrackableModel("test3", 3),
				new TrackableModel("test4", 4)
			};

			_rows = _tracker.ConvertAndTrack(models);
		}

		[Test]
		public void It_no_longer_tracks_untracked_viewmodel_property_changes()
		{
			var row = _rows.First();

			_tracker.UnTrack(row);

			row.Name = "Changed";

			Assert.AreEqual(0, _tracker.GetDirtyModels().Count());
		}
	}


	[TestFixture]
	public class Describecleaning
	{
		private ITracker<TrackableModel, ITrackableViewModel> _tracker;
		private Mock<IRowViewModelFactory<TrackableModel, ITrackableViewModel>> _rowViewModelFactoryMock;
		private IEnumerable<ITrackableViewModel> _rows;

		[SetUp]
		public void BeforeEach()
		{
			_rowViewModelFactoryMock = new Mock<IRowViewModelFactory<TrackableModel, ITrackableViewModel>>();
			_rowViewModelFactoryMock.Setup(f => f.Create(It.IsAny<TrackableModel>())).Returns<TrackableModel>(m => new TrackableViewModel(m));

			_tracker = new Tracker<TrackableModel, ITrackableViewModel>(_rowViewModelFactoryMock.Object);

			var models = new Collection<TrackableModel>
			{
				new TrackableModel("test1", 1),
				new TrackableModel("test2", 2),
				new TrackableModel("test3", 3),
				new TrackableModel("test4", 4)
			};

			_rows = _tracker.ConvertAndTrack(models);
		}

		[Test]
		public void It_syncs_the_models_with_the_current_row_properties()
		{
			var row = _rows.First();

			_tracker.UnTrack(row);

			row.Name = "Changed";

			Assert.AreEqual(0, _tracker.GetDirtyModels().Count());
		}
	}

}
