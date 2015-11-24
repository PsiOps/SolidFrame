
using SolidFrame.Core.Interfaces;
using System.Collections.Generic;

namespace SolidFrame.Ribbon.UI
{

	public class RibbonTab : IRibbonTab
	{
		public RibbonTab(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }
		public ICollection<IRibbonControlGroup> RibbonControlGroups { get; private set; }
	}
}
