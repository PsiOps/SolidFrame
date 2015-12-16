using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITracker<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class, IEquatable<TModel>
	{
		IEnumerable<TRowViewModel> ConvertAndTrack(IEnumerable<TModel> models);
		TRowViewModel ConvertAndTrack(TModel model);
		void UnTrack(TRowViewModel row);
		IEnumerable<TModel> GetDirtyModels();
	}
}