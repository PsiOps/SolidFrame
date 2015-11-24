using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces
{
	public interface IRibbonTab
	{
		string Name { get; }

		ICollection<IRibbonControlGroup> RibbonControlGroups { get; }
	}
}