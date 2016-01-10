using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Explorer
{
	public interface IExplorerItem
	{
		string Name { get; }
		IEnumerable<IExplorerItem> ChildItems { get; }
		Action Action { get; }
	}
}