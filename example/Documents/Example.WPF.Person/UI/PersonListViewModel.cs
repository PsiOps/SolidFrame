using Example.Models;
using Example.WPF.Person.Types;
using Example.WPF.Resources.Web;
using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.Crud;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Core.Interfaces.Translation;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Example.WPF.Person.UI
{
	// TODO: Ribbon Save button
	// TODO: DirtyTracking.Clean()
	// TODO: Resource Error Handling
	// TODO: IsBusy indicator

	public interface IPersonListViewModel : IListViewModel, IAdd, ISave, ITranslate, IValidate<IPersonRowViewModel>
	{
		ITrackedCollection<PersonModel, IPersonRowViewModel> DataSource { get; set; }
	}

	public class PersonListViewModel : ViewModel, IPersonListViewModel
	{
		public Guid Id { get; private set; }
		public string Title { get; private set; }

		private readonly IPersonResource _personResource;
		private readonly IValidationService<IPersonRowViewModel> _validationService;
		private readonly ITrackedCollectionFactory<PersonModel, IPersonRowViewModel> _trackedCollectionFactory;

		public PersonListViewModel(IPersonListViewModelDepedencies dependencies)
		{
			_personResource = dependencies.PersonResource;
			_validationService = dependencies.ValidationService;
			_trackedCollectionFactory = dependencies.TrackedCollectionFactory;

			var document = dependencies.Document;

			Id = document.Id;
			Title = document.Name;

			RegisterToRibbon(dependencies.CrudGroupController);

			Translations = dependencies.TranslationService.GetTranslations(Id);

			RegisterValidations(dependencies.ValidationService);

			LoadData();
		}

		private void RegisterValidations(IValidationService<IPersonRowViewModel> validationService)
		{
			validationService.Register(this);
			validationService.AddAbsoluteRule(this, r => r.Number, Condition.MustBeGreaterThan, 0, Severity.Error, "{0} must be larger than zero");

			validationService.HasErrorsChanged += hasErrors => OnCanSaveChanged();
		}

		private void RegisterToRibbon(IRibbonControlGroupsController crudGroupController)
		{
			crudGroupController.Register(this);
		}

		private async void LoadData()
		{
			var models = await _personResource.Get(); // ToDo: Provide OnErrorCallback in Get method

			if (models == null) return;

			DataSource = _trackedCollectionFactory.Create(models);

			DataSource.IsDirtyChanged += isDirty => OnCanSaveChanged();

			foreach (var row in DataSource)
			{
				row.PropertyChanged += OnRowPropertyChanged;
			}
		}

		private ITrackedCollection<PersonModel, IPersonRowViewModel> _dataSource;

		public ITrackedCollection<PersonModel, IPersonRowViewModel> DataSource
		{
			get { return _dataSource; }
			set
			{
				_dataSource = value;
				OnPropertyChanged();
			}
		}

		public bool CanAdd()
		{
			return true;
		}

		public void Add()
		{
			var row = DataSource.AddTracked(new PersonModel());

			row.PropertyChanged += OnRowPropertyChanged;

			if(RowValidationTrigger != null)
				RowValidationTrigger(row, null);
		}

		private void OnCanAddChanged()
		{
			if (CanAddChanged != null)
				CanAddChanged();
		}

		public event CanCrudChangedHandler CanAddChanged;

		public bool CanSave()
		{
			if (DataSource == null) return false;

			return DataSource.IsDirty && !_validationService.HasErrors;
		}

		public void Save()
		{
			_personResource.Put(DataSource.GetDirtyModels());

			DataSource.Clean();
		}

		private void OnCanSaveChanged()
		{
			if (CanSaveChanged != null)
				CanSaveChanged();
		}

		public event CanCrudChangedHandler CanSaveChanged;

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
