using Example.WPF.Person.Types;
using SolidFrame.Core.Interfaces.Crud;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Core.Interfaces.Translation;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Example.WPF.Person.UI
{
	public interface IPersonListViewModel : IListViewModel, IAdd, ITranslate
	{
		ObservableCollection<IPersonRowViewModel> DataSource { get; set; }
	}

	public class PersonListViewModel : IPersonListViewModel
	{
		public PersonListViewModel(IPersonListViewModelDepedencies dependencies)
		{
			var configuration = dependencies.Configuration;

			Id = configuration.Id;
			Title = configuration.Name;

			RegisterToRibbon(dependencies.CrudGroupController);

			Translations = dependencies.TranslationService.GetTranslations(configuration);

			DataSource = new ObservableCollection<IPersonRowViewModel>
			{
				new PersonRowViewModel{FirstName = "Bob", LastName = "Smith", Id = 1},
				new PersonRowViewModel{FirstName = "Andy", LastName = "Turner", Id = 2},
				new PersonRowViewModel{FirstName = "Jack", LastName = "Miller", Id = 3},
				new PersonRowViewModel{FirstName = "Randy", LastName = "Marsh", Id = 4}
			};
		}

		private void RegisterToRibbon(IRibbonControlGroupsController crudGroupController)
		{
			crudGroupController.Register(this);
		}

		public ObservableCollection<IPersonRowViewModel> DataSource { get; set; }

		public Guid Id { get; private set; }
		public string Title { get; private set; }

		public bool CanAdd()
		{
			return true;
		}

		public void Add()
		{
			DataSource.Add(new PersonRowViewModel { FirstName = "Alan", LastName = "Wake", Id = 5 });
		}

		private void OnCanAddChanged()
		{
			if (CanAddChanged != null)
				CanAddChanged();
		}

		public event CanCrudChangedHandler CanAddChanged;

		public IDictionary<string, string> Translations { get; set; }
	}
}
