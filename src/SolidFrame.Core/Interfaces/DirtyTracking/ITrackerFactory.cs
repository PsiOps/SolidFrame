﻿using SolidFrame.Core.Interfaces.General;
using System;

namespace SolidFrame.Core.Interfaces.DirtyTracking
{
	public interface ITrackerFactory<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class, IHaveId
	{
		ITracker<TModel, TRowViewModel> Create();
	}
}