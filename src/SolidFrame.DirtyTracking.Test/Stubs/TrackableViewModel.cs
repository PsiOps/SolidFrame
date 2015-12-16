﻿
using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.DirtyTracking;
using System;

namespace SolidFrame.DirtyTracking.Test.Stubs
{
	internal interface ITrackableViewModel : ITrackableModel, ITrackable
	{
	}

	internal class TrackableViewModel : ViewModel, ITrackableViewModel
	{
		private string _name;
		private int _number;

		public TrackableViewModel(ITrackableModel model)
		{
			Id = model.Id;
			_name = model.Name;
			_number = model.Number;
		}

		public bool Equals(ITrackableModel other)
		{
			throw new NotImplementedException();
		}

		public Guid Id { get; private set; }

		public int Number
		{
			get { return _number; }
			set
			{
				_number = value; 
				OnPropertyChanged();
			}
		}

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value; 
				OnPropertyChanged();
			}
		}
	}
}