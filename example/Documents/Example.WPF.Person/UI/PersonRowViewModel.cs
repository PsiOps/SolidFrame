
using Example.Models;
using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Validation;
using System;

namespace Example.WPF.Person.UI
{
	public interface IPersonRowViewModel : IValidatable, ITrackable, IEquatable<PersonModel>, IRowViewModel<PersonModel>
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		int Number { get; set; }
	}
	public class PersonRowViewModel : ViewModel, IPersonRowViewModel
	{

		public PersonRowViewModel(PersonModel personModel)
		{
			Id = personModel.Id;
			FirstName = personModel.FirstName;
			LastName = personModel.LastName;
			Number = personModel.Number;
		}

		private string _firstName;

		public string FirstName
		{
			get { return _firstName; }
			set
			{
				_firstName = value; 
				OnPropertyChanged();
			}
		}

		private string _lastName;

		public string LastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;
				OnPropertyChanged();
			}
		}

		private int _number;

		public int Number
		{
			get { return _number; }
			set
			{
				_number = value; 
				OnPropertyChanged();
			}
		}

		public Guid Id { get; private set; }

		public string ValidationName
		{
			get { return string.Format("{0} {1}", FirstName, LastName); }
		}

		public bool Equals(PersonModel other)
		{
			if (FirstName != other.FirstName) return false;
			if (LastName != other.LastName) return false;
			if (Number != other.Number) return false;
			return true;
		}

		public PersonModel ToModel()
		{
			return new PersonModel()
			{
				FirstName = FirstName,
				LastName = LastName,
				Number = Number
			};
		}
	}
}
