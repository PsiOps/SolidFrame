﻿using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.DirtyTracking.Types;
using System;
using System.Collections.Generic;

namespace SolidFrame.DirtyTracking.Logics
{
	public class TrackedCollectionFactory<TModel, TRowViewModel> : ITrackedCollectionFactory<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, IEquatable<TModel>, IRowViewModel<TModel> 
		where TModel : class, IHaveId
	{
		private readonly ITrackerFactory<TModel, TRowViewModel> _trackerFactory;

		public TrackedCollectionFactory(ITrackerFactory<TModel, TRowViewModel> trackerFactory)
		{
			_trackerFactory = trackerFactory;
		}

		public ITrackedCollection<TModel, TRowViewModel> Create(IEnumerable<TModel> models)
		{
			return new TrackedCollection<TModel, TRowViewModel>(models, _trackerFactory.Create());
		}
	}
}
