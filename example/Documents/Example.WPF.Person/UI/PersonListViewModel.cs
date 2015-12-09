using Example.WPF.Person.Logics;
using Example.WPF.Person.Types;
using Example.WPF.Resources.Web;
using SolidFrame.Core.Base;
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
	public interface IPersonListViewModel : IListViewModel, IAdd, ITranslate, IValidate<IPersonRowViewModel>
	{
		ICollection<IPersonRowViewModel> DataSource { get; set; }
	}

	public class PersonListViewModel : ViewModel, IPersonListViewModel
	{
		private readonly IPersonRowViewModelFactory _rowViewModelFactory;
		private readonly IPersonResource _personResource;
		private ICollection<IPersonRowViewModel> _dataSource;

		public PersonListViewModel(IPersonListViewModelDepedencies dependencies)
		{
			_rowViewModelFactory = dependencies.RowViewModelFactory;
			_personResource = dependencies.PersonResource;

			var configuration = dependencies.Configuration;

			Id = configuration.Id;
			Title = configuration.Name;

			RegisterToRibbon(dependencies.CrudGroupController);

			Translations = dependencies.TranslationService.GetTranslations(Id);

			RegisterValidations(dependencies.ValidationService);

			LoadData();
		}

		private async void LoadData()
		{
			var rows = new Collection<IPersonRowViewModel>();

			var models = await _personResource.Get();

			foreach (var personModel in models)
			{
				var row = _rowViewModelFactory.Create(personModel);

				row.PropertyChanged += OnRowPropertyChanged;

				rows.Add(row);
			}

			DataSource = new ObservableCollection<IPersonRowViewModel>(rows);
		}

		private void RegisterValidations(IValidationService<IPersonRowViewModel> validationService)
		{
			validationService.Register(this);
			validationService.AddAbsoluteRule(this, r => r.Number, Condition.MustBeGreaterThan, 0, Severity.Error, "{0} must be larger than zero");
		}

		private void RegisterToRibbon(IRibbonControlGroupsController crudGroupController)
		{
			crudGroupController.Register(this);
		}

		public ICollection<IPersonRowViewModel> DataSource
		{
			get { return _dataSource; }
			set
			{
				_dataSource = value;
				OnPropertyChanged();
			}
		}

		public Guid Id { get; private set; }
		public string Title { get; private set; }

		public bool CanAdd()
		{
			return true;
		}

		public void Add()
		{
			var row = _rowViewModelFactory.Create();

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
