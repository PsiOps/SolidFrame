using GalaSoft.MvvmLight.Command;
using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.Ribbon;
using System;
using System.Windows.Input;

namespace SolidFrame.Ribbon.Controls
{
	public class RibbonButton : ViewModel, IRibbonButtonControl
	{
		public RibbonButton(string name)
		{
			Name = name;
			IsEnabled = true;
		}

		public Func<bool> CanExecute { get; set; }

		public Action ExecuteAction { get; set; }

		public void RaiseCanExecuteChanged()
		{
			((RelayCommand)ClickCommand).RaiseCanExecuteChanged();
		}

		private ICommand _clickCommand;

		public ICommand ClickCommand
		{
			get { return _clickCommand ?? (_clickCommand = new RelayCommand(ExecuteAction, CanExecute)); }
		}

		public string Name { get; private set; }
		public bool IsEnabled { get; set; }
	}
}
