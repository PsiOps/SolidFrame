using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITrackedCollectionFactory<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class, IEquatable<TModel>
	{
		ITrackedCollection<TModel, TRowViewModel> Create(IEnumerable<TModel> models);
	}
}