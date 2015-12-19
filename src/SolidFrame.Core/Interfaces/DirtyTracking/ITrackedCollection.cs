using System;
using System.Collections.Generic;
using SolidFrame.Core.Types;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITrackedCollection<TModel, TRowViewModel> : ICollection<TRowViewModel>
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class
	{
		void AddTracked(TModel model);
		void RemoveTrackedById(Guid id);

		IEnumerable<TModel> GetDirtyModels();

		bool IsDirty { get; }
		event BooleanStateChangedHandler IsDirtyChanged;
	}
}