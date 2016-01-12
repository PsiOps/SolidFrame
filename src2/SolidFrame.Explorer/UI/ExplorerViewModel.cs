using SolidFrame.Core.Interfaces.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Explorer.UI
{
	public interface IExplorerViewModel : IListViewModel
	{
		ICollection<IExplorerItemViewModel> ExplorerItemViewModels { get; set; }
	}

	public class ExplorerViewModel : IExplorerViewModel
	{
		public Guid Id { get {return new Guid("AFDFF6A8-5549-44DB-83FF-ED699C6005B8");}}
		public string Title { get { return "TK_Navigation"; }}

		public ExplorerViewModel()
		{
			ExplorerItemViewModels = new ObservableCollection<IExplorerItemViewModel>();
		}

		public ICollection<IExplorerItemViewModel> ExplorerItemViewModels { get; set; }
	}
}
