using SolidFrame.Core.Base;
using SolidFrame.Core.Interfaces.Ribbon;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Ribbon.UI
{
	public interface IRibbonViewModel
	{
		ICollection<IRibbonTab> RibbonTabs { get; }
		IRibbonTab SelectedTab { get; set; }
	}

	public class RibbonViewModel : ViewModel, IRibbonViewModel
	{

		public RibbonViewModel()
		{
			RibbonTabs = new ObservableCollection<IRibbonTab>();
		}

		public ICollection<IRibbonTab> RibbonTabs { get; private set; }

		private IRibbonTab _selectedTab;

		public IRibbonTab SelectedTab
		{
			get { return _selectedTab; }
			set
			{
				_selectedTab = value;
				OnPropertyChanged();
			}
		}
	}
}
