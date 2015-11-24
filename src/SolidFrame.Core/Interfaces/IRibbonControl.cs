using System;

namespace SolidFrame.Core.Interfaces
{
	public interface IRibbonControl
	{
		Func<bool> CanExecute { get; set; }
		Action ExecuteAction { get; set; }
		void RaiseCanExecuteChanged();
	}
}