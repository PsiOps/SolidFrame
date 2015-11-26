using Example.WPF.Person.Types;
using Example.WPF.Person.UI;
using Microsoft.Practices.Unity;
using Prism.Modularity;

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
			_container.RegisterType<IPersonListViewModel, PersonListViewModel>();
			_container.RegisterType<IPersonListViewModelDepedencies, PersonListViewModelDepedencies>();
		}
	}
}
