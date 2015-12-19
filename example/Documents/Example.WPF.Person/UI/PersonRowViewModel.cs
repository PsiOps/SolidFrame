﻿
using Example.Models;
using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.Validation;
using System;

namespace Example.WPF.Person.UI
{
	public interface IPersonRowViewModel : IValidatable, ITrackable, IPersonModel, IEquatable<IPersonModel>
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		int Number { get; set; }
	}
	public class PersonRowViewModel : ViewModel, IPersonRowViewModel
	{
		public PersonRowViewModel(IPersonModel personModel)
		{
			FirstName = personModel.FirstName;
			LastName = personModel.LastName;
			Number = personModel.Number;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Number { get; set; }
		
		public Guid Id { get; private set; }

		public string ValidationName
		{
			get { return string.Format("{0} {1}", FirstName, LastName); }
		}

		public bool Equals(IPersonModel other)
		{
			throw new NotImplementedException();
		}
	}
}
