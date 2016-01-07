using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITrackedCollectionFactory<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, IEquatable<TModel>
		where TModel : class
	{
		ITrackedCollection<TModel, TRowViewModel> Create(IEnumerable<TModel> models);
	}
}