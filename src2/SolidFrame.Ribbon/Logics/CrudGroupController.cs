using SolidFrame.Core.Interfaces.Crud;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Ribbon.Types;
using System.Collections.Generic;

namespace SolidFrame.Ribbon.Logics
{
	public class CrudGroupController : ICrudGroupController
	{
		public CrudGroupController(ICrudGroupControllerDependencies dependencies)
		{
			GetButtons(dependencies.RibbonControlFactory);

			AddControlsToRibbon(dependencies);
		}

		private void GetButtons(IRibbonControlFactory ribbonControlFactory)
		{
			AddButton = ribbonControlFactory.CreateRibbonButton("TK_Add");
			SaveButton = ribbonControlFactory.CreateRibbonButton("TK_Save");
		}

		private void AddControlsToRibbon(ICrudGroupControllerDependencies dependencies)
		{
			var crudTab = dependencies.RibbonTabFactory.Create("TK_Crud");
			var crudGroup = dependencies.RibbonControlFactory.CreateRibbonControlGroup("TK_Crud");

			crudGroup.Add(AddButton);
			crudGroup.Add(SaveButton);

			crudTab.RibbonControlGroups.Add(crudGroup);

			dependencies.RibbonViewModel.RibbonTabs.Add(crudTab);

			dependencies.RibbonViewModel.SelectedTab = crudTab;
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

			var saveListViewModel = listViewModel as ISave;

			if (saveListViewModel != null)
			{
				SaveButton.ExecuteAction = saveListViewModel.Save;
				SaveButton.CanExecute = saveListViewModel.CanSave;
				saveListViewModel.CanSaveChanged += SaveButton.RaiseCanExecuteChanged;
			}
			else
			{
				SaveButton.CanExecute = () => false;
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

			var saveListViewModel = listViewModel as ISave;

			if (saveListViewModel != null)
			{
				SaveButton.ExecuteAction = null;
				SaveButton.CanExecute = null;
				saveListViewModel.CanSaveChanged -= SaveButton.RaiseCanExecuteChanged;
			}
		}

		public ICollection<IRibbonControlGroup> RibbonControlGroups { get; private set; }

		public IRibbonButtonControl AddButton { get; private set; }
		public IRibbonButtonControl SaveButton { get; private set; }
	}
}