using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Interfaces;
using SolidFrame.Ribbon.Types;
using SolidFrame.Ribbon.UI;

namespace SolidFrame.Ribbon.Logics
{
	public class RibbonService : IRibbonService
	{
		private readonly IRibbonViewModel _ribbonViewModel;

		public RibbonService(IRibbonServiceDependencies ribbonServiceDependencies)
		{
			_ribbonViewModel = ribbonServiceDependencies.RibbonViewModel;
		}

		public void Register(IListViewModel listViewModel)
		{
			var crudGroupController = (ICrudGroupController) _ribbonViewModel;

			var addListViewModel = listViewModel as IAdd;

			if (addListViewModel != null)
			{
				crudGroupController.AddButton.ExecuteAction = addListViewModel.Add;
				crudGroupController.AddButton.CanExecute = addListViewModel.CanAdd;
				addListViewModel.CanAddChanged += crudGroupController.AddButton.RaiseCanExecuteChanged;
			}
			else
			{
				crudGroupController.AddButton.CanExecute = () => false;
			}

		}

		public void UnRegister(IListViewModel listViewModel)
		{
			var crudGroupController = (ICrudGroupController)_ribbonViewModel;

			var addListViewModel = listViewModel as IAdd;

			if (addListViewModel != null)
			{
				crudGroupController.AddButton.ExecuteAction = null;
				crudGroupController.AddButton.CanExecute = null;
				addListViewModel.CanAddChanged -= crudGroupController.AddButton.RaiseCanExecuteChanged;
			}
		}
	}
}