using SolidFrame.Core.Interfaces.Explorer;
using SolidFrame.Explorer.Types;
using SolidFrame.Explorer.UI;
using System.Collections.ObjectModel;

namespace SolidFrame.Explorer.Logics
{
	public class ExplorerService : IExplorerService
	{
		private readonly IExplorerViewModel _explorerViewModel;
		private readonly IExplorerItemViewModelFactory _itemFactory;

		public ExplorerService(IExplorerServiceDependencies dependencies)
		{
			_explorerViewModel = dependencies.ExplorerViewModel;
			_itemFactory = dependencies.ExplorerItemViewModelFactory;
		}

		public void AddExplorerItem(IExplorerItem item)
		{
			_explorerViewModel.ExplorerItemViewModels.Add(CreateHierarchicalViewModel(item));
		}

		private IExplorerItemViewModel CreateHierarchicalViewModel(IExplorerItem item)
		{
			var childItemViewModels = new Collection<IExplorerItemViewModel>();

			foreach (var childItem in item.ChildItems)
			{
				childItemViewModels.Add(CreateHierarchicalViewModel(childItem));
			}

			return _itemFactory.Create(item, childItemViewModels);
		}
	}
}
