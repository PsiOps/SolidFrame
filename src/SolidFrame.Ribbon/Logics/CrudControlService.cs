using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Types;
using System.Collections.Generic;

namespace SolidFrame.Ribbon.Logics
{
	public class CrudGroupController : ICrudGroupsController
	{
		public CrudGroupController(ICrudControlServiceDependencies dependencies)
		{
			GetButtons(dependencies.RibbonControlFactory);

			AddControlsToRibbon(dependencies);
		}

		private void GetButtons(IRibbonControlFactory ribbonControlFactory)
		{
			AddButton = ribbonControlFactory.CreateRibbonButton();
		}

		private void AddControlsToRibbon(ICrudControlServiceDependencies dependencies)
		{
			var crudTab = dependencies.RibbonTabFactory.Create("Crud");
			var crudGroup = dependencies.RibbonControlFactory.CreateRibbonControlGroup("Crud");

			crudGroup.Add(AddButton);
			crudTab.RibbonControlGroups.Add(crudGroup);

			dependencies.RibbonViewModel.RibbonTabs.Add(crudTab);
		}

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

		public IRibbonButtonControl AddButton { get; private set; }
	}
}