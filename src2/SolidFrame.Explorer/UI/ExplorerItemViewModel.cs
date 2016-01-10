using GalaSoft.MvvmLight.Command;
using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.Explorer;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SolidFrame.Explorer.UI
{
	public interface IExplorerItemViewModel : IExplorerItem
	{
		ICommand ClickCommand { get; }
	}

	public class ExplorerItemViewModel : ViewModel, IExplorerItemViewModel
	{
		private readonly Func<bool> _canExecute;

		public string Name { get; private set; }
		public IEnumerable<IExplorerItem> ChildItems { get; private set; }
		public Action Action { get; private set; }

		public ExplorerItemViewModel(IExplorerItem explorerItem)
		{
			Name = explorerItem.Name;
			Action = explorerItem.Action;
			_canExecute = () => true;

		}

		public ExplorerItemViewModel(IExplorerItem explorerItem, IEnumerable<IExplorerItemViewModel> childItems) : this(explorerItem)
		{
			ChildItems = childItems;
		}

		private ICommand _clickCommand;

		// ReSharper disable once UnusedMember.Global
		public ICommand ClickCommand
		{
			get { return _clickCommand ?? (_clickCommand = new RelayCommand(Action, _canExecute)); }
		}
	}
}
