
using SolidFrame.Core.Base;
using System.ComponentModel;

namespace Example.WPF.Person.UI
{
	public interface IPersonRowViewModel : INotifyPropertyChanged
	{
		string FirstName { get; set; }
		int Id { get; set; }
		string LastName { get; set; }
	}
	public class PersonRowViewModel : ViewModel, IPersonRowViewModel
	{
		public PersonRowViewModel()
		{

		}

		public string FirstName { get; set; }
		public int Id { get; set; }
		public string LastName { get; set; }
	}
}
