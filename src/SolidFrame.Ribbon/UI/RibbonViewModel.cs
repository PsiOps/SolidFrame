using SolidFrame.Core.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Ribbon.UI
{
	public interface IRibbonViewModel
	{
		ICollection<IRibbonTab> RibbonTabs { get; set; }
	}

	public class RibbonViewModel : IRibbonViewModel
	{
		public RibbonViewModel()
		{
			RibbonTabs = new ObservableCollection<IRibbonTab>();
		}

		public ICollection<IRibbonTab> RibbonTabs { get; set; }


	}
}
