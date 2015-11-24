using Prism.Regions;
using SolidFrame.Core.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SolidFrame.Explorer.UI
{
	public interface IExplorerItemFactory
	{
		ExplorerItem CreateCategoryItem(IDocumentCategory documentCategory, IEnumerable<IDocument> documents);
	}

	public class ExplorerItemFactory : IExplorerItemFactory
	{
		private readonly IRegionManager _regionManager;

		public ExplorerItemFactory(IRegionManager regionManager)
		{
			_regionManager = regionManager;
		}

		private ExplorerItem CreateDocumentItem(IDocument document)
		{
			return new ExplorerItem(document, _regionManager);
		}

		public ExplorerItem CreateCategoryItem(IDocumentCategory documentCategory, IEnumerable<IDocument> documents)
		{
			var documentItems = new Collection<ExplorerItem>();

			foreach (var document in documents)
			{
				documentItems.Add(CreateDocumentItem(document));
			}

			return new ExplorerItem(documentCategory, documentItems);
		}
	}
}
