using Example.WPF.Person.Types;
using SolidFrame.Core.Interfaces;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Example.WPF.Person.UI
{
	public interface IPersonListViewModel : IListViewModel, ICanBeBusy, IAdd
	{
		ObservableCollection<IPersonRowViewModel> DataSource { get; set; }
		IDictionary<string, string> Translations { get; }
	}

	public class PersonListViewModel : IPersonListViewModel
	{
		public PersonListViewModel(IPersonListViewModelDepedencies dependencies)
		{
			var configuration = dependencies.Configuration;

			Id = configuration.Id;
			Title = configuration.Name;

			Translations = dependencies.TranslationService.GetTranslations(configuration);

			DataSource = new ObservableCollection<IPersonRowViewModel>
			{
				new PersonRowViewModel{FirstName = "Bob", LastName = "Smith", Id = 1},
				new PersonRowViewModel{FirstName = "Andy", LastName = "Turner", Id = 2},
				new PersonRowViewModel{FirstName = "Jack", LastName = "Miller", Id = 3},
				new PersonRowViewModel{FirstName = "Randy", LastName = "Marsh", Id = 4}
			};
		}

		public ObservableCollection<IPersonRowViewModel> DataSource { get; set; }

		public IDictionary<string, string> Translations { get; private set; }

		public bool IsBusy { get; private set; }
		public string IsBusyText { get; private set; }

		public Guid Id { get; private set; }
		public string Title { get; private set; }

		public bool CanAdd()
		{
			return true;
		}

		public void Add()
		{
			throw new NotImplementedException();
		}

		private void OnCanAddChanged()
		{
			if (CanAddChanged != null)
				CanAddChanged();
		}

		public event CanCrudChangedHandler CanAddChanged;
	}
}
