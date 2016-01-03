using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITrackedCollection<TModel, TRowViewModel> : ICollection<TRowViewModel>
		where TRowViewModel : class, ITrackable, IEquatable<TModel>
		where TModel : class
	{
		TRowViewModel AddTracked(TModel model);
		void RemoveTrackedById(Guid id);

		IEnumerable<TModel> GetDirtyModels();
		void Clean();

		bool IsDirty { get; }
		event BooleanStateChangedHandler IsDirtyChanged;
	}
}