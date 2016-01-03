using Example.Models;
using Example.WPF.Person.Logics;
using Example.WPF.Person.Types;
using Example.WPF.Person.UI;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Core.Interfaces.General;

namespace Example.WPF.Person
{
	public class PersonModule : IModule
	{
		private readonly IUnityContainer _container;

		public PersonModule(IUnityContainer container)
		{
			_container = container;
		}

		public void Initialize()
		{
			_container.RegisterType<IDocumentConfiguration, PersonDocumentConfiguration>(typeof(PersonDocumentConfiguration).FullName);
			_container.RegisterType<IPersonDocumentConfiguration, PersonDocumentConfiguration>();
			_container.RegisterType<IPersonListViewModel, PersonListViewModel>();
			_container.RegisterType<IRowViewModelFactory<PersonModel, IPersonRowViewModel>, PersonRowViewModelFactory>();
			_container.RegisterType<IPersonListViewModelDepedencies, PersonListViewModelDepedencies>();
		}
	}
}
