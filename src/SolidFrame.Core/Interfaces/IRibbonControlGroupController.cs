using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces
{
	public interface IRibbonControlGroupController
	{
		void AddToTab(IRibbonTab ribbonTab);
	}

	public interface IRibbonTab
	{
		string Name { get; }

		ICollection<IRibbonControlGroup> RibbonControlGroups { get; }
	}

	public interface IRibbonControlGroup : ICollection<IRibbonControl>
	{
		string Name { get; }
	}

}