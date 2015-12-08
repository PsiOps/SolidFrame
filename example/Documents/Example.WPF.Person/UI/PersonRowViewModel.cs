
using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.Validation;
using System;
using System.ComponentModel;

namespace Example.WPF.Person.UI
{
	public interface IPersonRowViewModel : ICanBeValidated, INotifyPropertyChanged
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		int Number { get; set; }
	}
	public class PersonRowViewModel : ViewModel, IPersonRowViewModel
	{
		public PersonRowViewModel()
		{

		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Number { get; set; }
		
		public Guid Id { get; private set; }
		public string ValidationName { get; private set; }
	}
}
