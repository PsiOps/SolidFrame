using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Core.Interfaces.Translation;
using SolidFrame.Explorer.UI;
using System.Collections.Generic;

namespace SolidFrame.Explorer.Types
{
	public interface IExplorerViewModelDependencies
	{
		ITranslationService TranslationService { get; }
		IExplorerItemFactory ExplorerItemFactory { get; }
		IDocumentCategoryCatalog DocumentCategoryCatalog { get; }
		IEnumerable<IDocumentConfiguration> DocumentConfigurations { get; } 
	}

	public class ExplorerViewModelDependencies : IExplorerViewModelDependencies
	{
		public ExplorerViewModelDependencies(ITranslationService translationService, 
			IExplorerItemFactory explorerItemFactory, 
			IDocumentCategoryCatalog documentCategoryCatalog, 
			IEnumerable<IDocumentConfiguration> documentConfigurations)
		{
			TranslationService = translationService;
			ExplorerItemFactory = explorerItemFactory;
			DocumentCategoryCatalog = documentCategoryCatalog;
			DocumentConfigurations = documentConfigurations;
		}

		public ITranslationService TranslationService { get; private set; }
		public IExplorerItemFactory ExplorerItemFactory { get; private set; }
		public IDocumentCategoryCatalog DocumentCategoryCatalog { get; private set; }
		public IEnumerable<IDocumentConfiguration> DocumentConfigurations { get; private set; }
	}
}
