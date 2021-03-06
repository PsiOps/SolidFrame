﻿using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SolidFrame.DirtyTracking.Types
{
	public class TrackedCollection<TModel, TRowViewModel> : ObservableCollection<TRowViewModel>, ITrackedCollection<TModel, TRowViewModel>
		where TRowViewModel : class, ITrackable, IEquatable<TModel>, IRowViewModel<TModel> 
		where TModel : class, IHaveId
	{
		private readonly ITracker<TModel, TRowViewModel> _tracker;

		public TrackedCollection(IEnumerable<TModel> models, ITracker<TModel, TRowViewModel> tracker)
			: base(tracker.ConvertAndTrack(models))
		{
			_tracker = tracker;

			_tracker.IsDirtyChanged += state => OnIsDirtyChanged();
		}

		public TRowViewModel AddTracked(TModel model)
		{
			var row = _tracker.ConvertAndTrack(model);

			Add(row);

			return row;
		}

		public void RemoveTrackedById(Guid id)
		{
			var row = this.Single(r => r.Id == id);

			_tracker.UnTrack(row);

			Remove(row);
		}

		public IEnumerable<TModel> GetDirtyModels()
		{
			return _tracker.GetDirtyModels();
		}

		public void Clean()
		{
			_tracker.Clean();
		}

		public bool IsDirty { get { return _tracker.IsDirty; } }

		private void OnIsDirtyChanged()
		{
			if (IsDirtyChanged != null)
				IsDirtyChanged(IsDirty);
		}

		public event BooleanStateChangedHandler IsDirtyChanged;
	}
}
