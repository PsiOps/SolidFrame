using GalaSoft.MvvmLight.Command;
using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SolidFrame.Explorer.UI
{
	public interface IExplorerItem
	{
		string Name { get; }
		bool IsExpanded { get; set; }
		ICollection<IExplorerItem> Items { get; }
		ICommand ClickCommand { get; }
	}

	public class ExplorerItem : ViewModel, IExplorerItem
	{
		private readonly Action _loadDocumentAction;
		private readonly IDocumentConfiguration _documentConfiguration;
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

		public ICollection<IExplorerItem> Items { get; private set; } 

		public ExplorerItem(IDocumentCategory category, IEnumerable<IExplorerItem> items)
		{
			Name = category.Name;
			_loadDocumentAction = () => IsExpanded = !IsExpanded;
			_canExecute = () => true;
			IsExpanded = true;

			Items = new ObservableCollection<IExplorerItem>(items);
		}

		public ExplorerItem(IDocumentConfiguration documentConfiguration, Action loadDocumentAction)
		{
			Name = documentConfiguration.Name;
			_loadDocumentAction = loadDocumentAction;
			_canExecute = () => true;
		}

		private ICommand _clickCommand;
		private bool _isExpanded;

		// ReSharper disable once UnusedMember.Global
		public ICommand ClickCommand
		{
			get { return _clickCommand ?? (_clickCommand = new RelayCommand(_loadDocumentAction, _canExecute)); }
		}
	}
}
