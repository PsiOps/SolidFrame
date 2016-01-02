using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITracker<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class, IHaveId
	{
		IEnumerable<TRowViewModel> ConvertAndTrack(IEnumerable<TModel> models);
		TRowViewModel ConvertAndTrack(TModel model);
		void UnTrack(TRowViewModel row);
		IEnumerable<TModel> GetDirtyModels();

		bool IsDirty { get; }
		event BooleanStateChangedHandler IsDirtyChanged;
	}
}