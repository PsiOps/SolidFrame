using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITrackedCollection<TModel, TRowViewModel> : ICollection<TRowViewModel>
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class, IEquatable<TModel>
	{
		void AddTracked(TModel model);
		void RemoveTrackedById(Guid id);

		IEnumerable<TModel> GetDirtyModels();
	}
}