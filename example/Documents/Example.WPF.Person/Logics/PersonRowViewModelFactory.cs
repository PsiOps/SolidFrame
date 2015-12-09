using Example.Models;
using Example.WPF.Person.UI;

namespace Example.WPF.Person.Logics
{
	public interface IPersonRowViewModelFactory
	{
		IPersonRowViewModel Create();
		IPersonRowViewModel Create(IPersonModel personModel);
	}

	public class PersonRowViewModelFactory : IPersonRowViewModelFactory
	{
		public IPersonRowViewModel Create()
		{
			return new PersonRowViewModel(new PersonModel{FirstName = "FirstName", LastName = "LastName", Number = 0});
		}

		public IPersonRowViewModel Create(IPersonModel personModel)
		{
			return new PersonRowViewModel(personModel);
		}
	}
}
