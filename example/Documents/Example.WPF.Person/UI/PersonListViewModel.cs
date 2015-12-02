using Example.WPF.Person.Types;
using SolidFrame.Core.Interfaces.Crud;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Core.Interfaces.Translation;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Example.WPF.Person.UI
{
	public interface IPersonListViewModel : IListViewModel, IAdd, ITranslate, IValidateRows<IPersonRowViewModel>
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

			Translations = dependencies.TranslationService.GetTranslations(Id);

			DataSource = new ObservableCollection<IPersonRowViewModel>
			{
				new PersonRowViewModel{FirstName = "Bob", LastName = "Smith", Id = 1},
				new PersonRowViewModel{FirstName = "Andy", LastName = "Turner", Id = 2},
				new PersonRowViewModel{FirstName = "Jack", LastName = "Miller", Id = 3},
				new PersonRowViewModel{FirstName = "Randy", LastName = "Marsh", Id = 4}
			};

			foreach (var personRowViewModel in DataSource)
			{
				personRowViewModel.PropertyChanged += OnRowPropertyChanged;
			}

			RegisterValidations(dependencies.ValidationService);
		}

		private void RegisterValidations(IValidationService<IPersonRowViewModel> validationService)
		{
			validationService.Register(this);
			validationService.AddAbsoluteRule(this, r => r.Id, Condition.MustBeGreaterThan, 0, Severity.Error, "Test {0}");
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
			var row = new PersonRowViewModel {FirstName = "Alan", LastName = "Wake", Id = 0};

			row.PropertyChanged += OnRowPropertyChanged;

			DataSource.Add(row);

			if(RowValidationTrigger != null)
				RowValidationTrigger(row, null);
		}

		private void OnCanAddChanged()
		{
			if (CanAddChanged != null)
				CanAddChanged();
		}

		public event CanCrudChangedHandler CanAddChanged;

		private void OnRowPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (RowValidationTrigger == null) return;

			var row = sender as IPersonRowViewModel;

			if (row == null) return;

			RowValidationTrigger(row, args.PropertyName);
		}

		public event ValidationTriggerHandler<IPersonRowViewModel> RowValidationTrigger;

		public IDictionary<string, string> Translations { get; set; }
	}
}
