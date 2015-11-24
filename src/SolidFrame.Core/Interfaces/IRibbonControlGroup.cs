using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces
{
	public interface IRibbonControlGroup : ICollection<IRibbonControl>
	{
		string Name { get; }
	}
}