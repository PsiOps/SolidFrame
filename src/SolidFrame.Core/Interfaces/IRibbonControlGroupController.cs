using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces
{
	public interface IRibbonControlGroupsController
	{
		ICollection<IRibbonControlGroup> RibbonControlGroups { get; }
		void Register(IListViewModel document);
		void UnRegister(IListViewModel document);
	}
}