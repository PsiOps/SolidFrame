using SolidFrame.Core.Interfaces.General;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Ribbon
{
	public interface IRibbonControlGroupsController
	{
		ICollection<IRibbonControlGroup> RibbonControlGroups { get; }
		void Register(IListViewModel document);
		void UnRegister(IListViewModel document);
	}
}