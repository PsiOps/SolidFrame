using SolidFrame.Core.Interfaces;
using SolidFrame.Explorer.UI;

namespace SolidFrame.Explorer.Types
{
	public interface IExplorerViewModelDependencies
	{
		ITranslationService TranslationService { get; }
		IExplorerItemFactory ExplorerItemFactory { get; }
		IDocumentCategoryCatalog DocumentCategoryCatalog { get; }
		IDocumentCatalog DocumentCatalog { get; }
	}

	public class ExplorerViewModelDependencies : IExplorerViewModelDependencies
	{
		public ExplorerViewModelDependencies(ITranslationService translationService, 
			IExplorerItemFactory explorerItemFactory, 
			IDocumentCategoryCatalog documentCategoryCatalog, 
			IDocumentCatalog documentCatalog)
		{
			TranslationService = translationService;
			ExplorerItemFactory = explorerItemFactory;
			DocumentCategoryCatalog = documentCategoryCatalog;
			DocumentCatalog = documentCatalog;
		}

		public ITranslationService TranslationService { get; private set; }
		public IExplorerItemFactory ExplorerItemFactory { get; private set; }
		public IDocumentCategoryCatalog DocumentCategoryCatalog { get; private set; }
		public IDocumentCatalog DocumentCatalog { get; private set; }
	}
}
