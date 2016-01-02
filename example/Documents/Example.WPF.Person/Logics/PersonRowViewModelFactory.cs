using Example.Models;
using Example.WPF.Person.UI;
using SolidFrame.Core.Interfaces.General;

namespace Example.WPF.Person.Logics
{
	public class PersonRowViewModelFactory : IRowViewModelFactory<IPersonModel, IPersonRowViewModel>
	{
		public IPersonRowViewModel Create(IPersonModel personModel)
		{
			return new PersonRowViewModel(personModel);
		}
	}
}
