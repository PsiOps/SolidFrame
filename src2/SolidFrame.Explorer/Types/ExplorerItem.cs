using SolidFrame.Core.Interfaces.Explorer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Explorer.Types
{
	public class ExplorerItem : IExplorerItem
	{
		public ExplorerItem(string name, Action action, IEnumerable<IExplorerItem> childItems) : this(name, action)
		{
			ChildItems = childItems;
		}

		public ExplorerItem(string name, Action action)
		{
			Name = name;
			Action = action;
			ChildItems = new Collection<IExplorerItem>();
		}

		public string Name { get; private set; }
		public IEnumerable<IExplorerItem> ChildItems { get; private set; }
		public Action Action { get; private set; }
	}
}
