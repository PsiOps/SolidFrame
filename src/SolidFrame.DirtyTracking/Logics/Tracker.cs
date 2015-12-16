using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SolidFrame.DirtyTracking.Logics
{
	public class Tracker<TModel, TRowViewModel> : ITracker<TModel, TRowViewModel> 
		where TRowViewModel : class, ITrackable, TModel, IEquatable<TModel>
		where TModel : class, IEquatable<TModel>
	{
		private readonly IRowViewModelFactory<TModel, TRowViewModel> _rowViewModelFactory;
		private readonly IDictionary<Guid, TModel> _originalDictionary; 
		private readonly IDictionary<Guid, TModel> _dirtyModelsDictionary; 

		public Tracker(IRowViewModelFactory<TModel, TRowViewModel> rowViewModelFactory)
		{
			_rowViewModelFactory = rowViewModelFactory;
			_originalDictionary = new Dictionary<Guid, TModel>();
			_dirtyModelsDictionary = new Dictionary<Guid, TModel>();
		}

		public IEnumerable<TRowViewModel> ConvertAndTrack(IEnumerable<TModel> models)
		{
			var rows = new Collection<TRowViewModel>();

			foreach (var row in models.Select(ConvertAndTrack))
			{
				rows.Add(row);
			}

			return rows;
		}

		public TRowViewModel ConvertAndTrack(TModel model)
		{
			var row = _rowViewModelFactory.Create(model);

			Track(row, model);

			return row;
		}

		public void UnTrack(TRowViewModel row)
		{
			row.PropertyChanged -= TrackPropertyChange;

			var key = row.Id;

			if (_dirtyModelsDictionary.ContainsKey(key))
				_dirtyModelsDictionary.Remove(key);

			_originalDictionary.Remove(key);
		}

		public IEnumerable<TModel> GetDirtyModels()
		{
			return _dirtyModelsDictionary.Values;
		}

		private void Track(TRowViewModel row, TModel model)
		{
			row.PropertyChanged += TrackPropertyChange;

			_originalDictionary.Add(row.Id, model); // TODO: Do we need to do a clone of this model to prevent changes from outside to influence dirty tracking?
		}

		private void TrackPropertyChange(object sender, PropertyChangedEventArgs e)
		{
			var row = sender as TRowViewModel;

			if(row == null) 
				return;

			var key = row.Id;

			TModel model;

			if(!_originalDictionary.TryGetValue(key, out model))
				return;

			if (model.Equals(row))
			{
				RemoveDirtyModel(key);
				return;
			}

			if(!_dirtyModelsDictionary.ContainsKey(key))
				_dirtyModelsDictionary.Add(key, row);
		}

		private void RemoveDirtyModel(Guid key)
		{
			if (_dirtyModelsDictionary.ContainsKey(key))
				_dirtyModelsDictionary.Remove(key);
		}
	}
}
