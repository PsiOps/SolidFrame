using SolidFrame.Core.Interfaces.General;
using SolidFrame.Explorer.Types;
using System;
using System.Collections.Generic;

namespace SolidFrame.Explorer.UI
{
	public interface IExplorerViewModel : IListViewModel
	{
		ICollection<IExplorerItemViewModel> ExplorerItemViewModels { get; set; }
	}

	public class ExplorerViewModel : IExplorerViewModel
	{
		public ExplorerViewModel(IExplorerServiceDependencies dependencies)
		{
			var explorerItemFactory = dependencies.ExplorerItemViewModelFactory;
		}

		public Guid Id { get {return new Guid("AFDFF6A8-5549-44DB-83FF-ED699C6005B8");}}
		public string Title { get { return "TK_Navigation"; }}

		public ICollection<IExplorerItemViewModel> ExplorerItemViewModels { get; set; }
	}
}
