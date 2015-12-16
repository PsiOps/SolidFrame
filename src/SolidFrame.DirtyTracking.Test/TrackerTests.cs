﻿
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
		private ITracker<ITrackableModel, ITrackableViewModel> _tracker;
		private Mock<IRowViewModelFactory<ITrackableModel, ITrackableViewModel>> _rowViewModelFactoryMock; 

		[SetUp]
		public void BeforeEach()
		{
			_rowViewModelFactoryMock = new Mock<IRowViewModelFactory<ITrackableModel, ITrackableViewModel>>();
			_rowViewModelFactoryMock.Setup(f => f.Create(It.IsAny<ITrackableModel>())).Returns<ITrackableModel>(m => new TrackableViewModel(m));

			_tracker = new Tracker<ITrackableModel, ITrackableViewModel>(_rowViewModelFactoryMock.Object);

			var models = new Collection<ITrackableModel>
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
			_rowViewModelFactoryMock.Verify(f => f.Create(It.IsAny<ITrackableModel>()), Times.Exactly(4));
		}
	}

	[TestFixture]
	public class DescribePropertyChangeTracking
	{
		private ITracker<ITrackableModel, ITrackableViewModel> _tracker;
		private Mock<IRowViewModelFactory<ITrackableModel, ITrackableViewModel>> _rowViewModelFactoryMock;
		private IEnumerable<ITrackableViewModel> _rows;

		[SetUp]
		public void BeforeEach()
		{
			_rowViewModelFactoryMock = new Mock<IRowViewModelFactory<ITrackableModel, ITrackableViewModel>>();
			_rowViewModelFactoryMock.Setup(f => f.Create(It.IsAny<ITrackableModel>())).Returns<ITrackableModel>(m => new TrackableViewModel(m));

			_tracker = new Tracker<ITrackableModel, ITrackableViewModel>(_rowViewModelFactoryMock.Object);

			var models = new Collection<ITrackableModel>
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
	}

	[TestFixture]
	public class DescribeUnTracking
	{
		private ITracker<ITrackableModel, ITrackableViewModel> _tracker;
		private Mock<IRowViewModelFactory<ITrackableModel, ITrackableViewModel>> _rowViewModelFactoryMock;
		private IEnumerable<ITrackableViewModel> _rows;

		[SetUp]
		public void BeforeEach()
		{
			_rowViewModelFactoryMock = new Mock<IRowViewModelFactory<ITrackableModel, ITrackableViewModel>>();
			_rowViewModelFactoryMock.Setup(f => f.Create(It.IsAny<ITrackableModel>())).Returns<ITrackableModel>(m => new TrackableViewModel(m));

			_tracker = new Tracker<ITrackableModel, ITrackableViewModel>(_rowViewModelFactoryMock.Object);

			var models = new Collection<ITrackableModel>
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
}