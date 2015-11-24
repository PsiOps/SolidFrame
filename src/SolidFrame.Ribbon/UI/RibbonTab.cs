
using SolidFrame.Core.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Ribbon.UI
{

	public class RibbonTab : IRibbonTab
	{
		public RibbonTab(string name)
		{
			Name = name;
			RibbonControlGroups = new ObservableCollection<IRibbonControlGroup>();
		}

		public string Name { get; private set; }
		public ICollection<IRibbonControlGroup> RibbonControlGroups { get; private set; }
	}
}
