using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Ribbon
{
	public interface IRibbonControlGroup : ICollection<IRibbonControl>
	{
		string Name { get; }
	}
}