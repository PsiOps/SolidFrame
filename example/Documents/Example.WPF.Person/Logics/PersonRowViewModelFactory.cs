using Example.Models;
using Example.WPF.Person.UI;
using SolidFrame.Core.Interfaces.General;

namespace Example.WPF.Person.Logics
{
	public class PersonRowViewModelFactory : IRowViewModelFactory<PersonModel, IPersonRowViewModel>
	{
		public IPersonRowViewModel Create(PersonModel personModel)
		{
			return new PersonRowViewModel(personModel);
		}
	}
}
