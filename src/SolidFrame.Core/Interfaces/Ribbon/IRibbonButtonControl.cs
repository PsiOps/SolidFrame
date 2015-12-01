using System;
using System.Windows.Input;

namespace SolidFrame.Core.Interfaces.Ribbon
{
	public interface IRibbonButtonControl : IRibbonControl
	{
		Func<bool> CanExecute { get; set; }
		Action ExecuteAction { get; set; }
		void RaiseCanExecuteChanged();
		ICommand ClickCommand { get; }
	}
}