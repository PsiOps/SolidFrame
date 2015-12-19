using Example.Models;
using Example.WPF.Person.Logics;
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
	// TODO: DirtyTracking.Clean()
	// TODO: Resource.Put() and ISave
	// TODO: Resource Error Handling
	// TODO: IsBusy indicator
	// TODO: Pivot Document

	public interface IPersonListViewModel : IListViewModel, IAdd, ITranslate, IValidate<IPersonRowViewModel>
	{
		ITrackedCollection<IPersonModel, IPersonRowViewModel> DataSource { get; set; }
	}

	public class PersonListViewModel : ViewModel, IPersonListViewModel
	{
		public Guid Id { get; private set; }
		public string Title { get; private set; }

		private readonly IPersonRowViewModelFactory _rowViewModelFactory;
		private readonly IPersonResource _personResource;
		private readonly IValidationService<IPersonRowViewModel> _validationService;
		private readonly ITrackedCollectionFactory<IPersonModel, IPersonRowViewModel> _trackedCollectionFactory;

		public PersonListViewModel(IPersonListViewModelDepedencies dependencies)
		{
			_rowViewModelFactory = dependencies.RowViewModelFactory;
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

		private ITrackedCollection<IPersonModel, IPersonRowViewModel> _dataSource;

		public ITrackedCollection<IPersonModel, IPersonRowViewModel> DataSource
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

		public bool CanSave()
		{
			return DataSource.IsDirty && !_validationService.HasErrors;
		}

		public void Save()
		{
			//_personResource.Put(DataSource.GetDirtyModels());

			//DataSource.Clean();
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
