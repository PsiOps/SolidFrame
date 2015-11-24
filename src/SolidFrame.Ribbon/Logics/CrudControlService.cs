using SolidFrame.Core.Interfaces;
using System.Collections.Generic;

namespace SolidFrame.Ribbon.Logics
{
	public class CrudControlService : ICrudControlService, ICrudGroupController
	{
		public void Register(IListViewModel listViewModel)
		{
			var addListViewModel = listViewModel as IAdd;

			if (addListViewModel != null)
			{
				AddButton.ExecuteAction = addListViewModel.Add;
				AddButton.CanExecute = addListViewModel.CanAdd;
				addListViewModel.CanAddChanged += AddButton.RaiseCanExecuteChanged;
			}
			else
			{
				AddButton.CanExecute = () => false;
			}

		}

		public void UnRegister(IListViewModel listViewModel)
		{
			var addListViewModel = listViewModel as IAdd;

			if (addListViewModel != null)
			{
				AddButton.ExecuteAction = null;
				AddButton.CanExecute = null;
				addListViewModel.CanAddChanged -= AddButton.RaiseCanExecuteChanged;
			}
		}

		public ICollection<IRibbonControlGroup> RibbonControlGroups { get; private set; }

		public IRibbonControl AddButton { get; private set; }
	}
}