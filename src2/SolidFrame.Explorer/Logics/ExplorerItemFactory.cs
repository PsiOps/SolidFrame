using SolidFrame.Core.Interfaces.Explorer;
using SolidFrame.Explorer.UI;
using System.Collections.Generic;

namespace SolidFrame.Explorer.Logics
{
	public interface IExplorerItemViewModelFactory
	{
		IExplorerItemViewModel Create(IExplorerItem explorerItem, IEnumerable<IExplorerItemViewModel> childItemViewModels);
	}

	public class ExplorerItemViewModelFactory : IExplorerItemViewModelFactory
	{
		public IExplorerItemViewModel Create(IExplorerItem explorerItem, IEnumerable<IExplorerItemViewModel> childItemViewModels)
		{
			throw new System.NotImplementedException();
		}
	}
}
