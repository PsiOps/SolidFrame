using SolidFrame.Core.Interfaces.Ribbon;
using System.Collections.ObjectModel;

namespace SolidFrame.Ribbon.Types
{
	public class RibbonControlGroup : Collection<IRibbonControl>, IRibbonControlGroup
	{
		public RibbonControlGroup(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }
	}
}
