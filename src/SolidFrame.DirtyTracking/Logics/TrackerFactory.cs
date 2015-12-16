using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.General;
using System;

namespace SolidFrame.DirtyTracking.Logics
{
	public class TrackerFactory<TModel, TRowViewModel> : ITrackerFactory<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class, IEquatable<TModel>
	{
		private readonly IRowViewModelFactory<TModel, TRowViewModel> _rowViewModelFactory;

		public TrackerFactory(IRowViewModelFactory<TModel, TRowViewModel> rowViewModelFactory)
		{
			_rowViewModelFactory = rowViewModelFactory;
		}

		public ITracker<TModel, TRowViewModel> Create()
		{
			return new Tracker<TModel, TRowViewModel>(_rowViewModelFactory);
		}
	}
}
