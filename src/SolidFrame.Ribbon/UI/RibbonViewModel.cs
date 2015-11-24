using SolidFrame.Core.Interfaces;
using SolidFrame.Ribbon.Interfaces;
using SolidFrame.Ribbon.Types;
using System.Collections.ObjectModel;
using System.Linq;

namespace SolidFrame.Ribbon.UI
{
	public interface IRibbonViewModel
	{
		ObservableCollection<IRibbonTab> RibbonTabs { get; set; }
	}

	public class RibbonViewModel : IRibbonViewModel, ICrudGroupController
	{
		public RibbonViewModel(IRibbonViewModelDependencies dependencies)
		{
			RibbonTabs = new ObservableCollection<IRibbonTab>();
			
			var crudTab = dependencies.RibbonTabFactory.Create("Crud");

			AddToTab(crudTab);

			RibbonTabs.Add(crudTab);
		}

		public ObservableCollection<IRibbonTab> RibbonTabs { get; set; }

		public IRibbonControl AddButton { get; private set; }

		public void AddToTab(IRibbonTab ribbonTab)
		{
			ribbonTab.RibbonControlGroups.Single().Add(AddButton);
		}
	}
}
