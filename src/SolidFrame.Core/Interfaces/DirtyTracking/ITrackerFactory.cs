using System;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITrackerFactory<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class
	{
		ITracker<TModel, TRowViewModel> Create();
	}
}