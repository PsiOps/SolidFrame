using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Types;
using System.Collections.ObjectModel;

namespace SolidFrame.Ribbon.UI
{
	public interface IRibbonViewModel
	{
		ObservableCollection<IRibbonTab> RibbonTabs { get; set; }
	}

	public class RibbonViewModel : IRibbonViewModel
	{
		public RibbonViewModel(IRibbonViewModelDependencies dependencies)
		{
			RibbonTabs = new ObservableCollection<IRibbonTab>();
			
			var crudTab = dependencies.RibbonTabFactory.Create("Crud");

			foreach (var ribbonControlGroup in dependencies.CrudGroupController.RibbonControlGroups)
			{
				crudTab.RibbonControlGroups.Add(ribbonControlGroup);
			}

			RibbonTabs.Add(crudTab);
		}

		public ObservableCollection<IRibbonTab> RibbonTabs { get; set; }
	}
}
