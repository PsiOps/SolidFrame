using GalaSoft.MvvmLight.Command;
using Prism.Regions;
using SolidFrame.Core.Base;
using SolidFrame.Core.Types;
using SolidFrame.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SolidFrame.Core.Interfaces;

namespace SolidFrame.Explorer.UI
{
	public class ExplorerItem : ViewModel
	{
		private readonly IRegionManager _regionManager;
		private readonly Action _clickAction;
		private readonly Func<bool> _canExecute;
		public string Name { get; private set; }

		public bool IsExpanded
		{
			get { return _isExpanded; }
			set
			{
				_isExpanded = value; 
				OnPropertyChanged();
			}
		}

		public ObservableCollection<ExplorerItem> Items { get; set; } 

		public ExplorerItem(IDocumentCategory category, IEnumerable<ExplorerItem> items)
		{
			Name = category.Name;
			_clickAction = () => IsExpanded = !IsExpanded;
			_canExecute = () => true;
			IsExpanded = true;

			Items = new ObservableCollection<ExplorerItem>(items);
		}

		public ExplorerItem(IDocumentConfiguration documentConfiguration, IRegionManager regionManager)
		{
			_regionManager = regionManager;
			Name = documentConfiguration.Name;
			_clickAction = () => _regionManager.RegisterViewWithRegion(Regions.Document, documentConfiguration.ViewType);
			_canExecute = () => true;
		}

		private ICommand _clickCommand;
		private bool _isExpanded;

		// ReSharper disable once UnusedMember.Global
		public ICommand ClickCommand
		{
			get { return _clickCommand ?? (_clickCommand = new RelayCommand(_clickAction, _canExecute)); }
		}
	}
}
