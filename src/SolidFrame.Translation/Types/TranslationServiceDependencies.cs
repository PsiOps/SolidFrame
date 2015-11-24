using SolidFrame.Core.Interfaces;

namespace SolidFrame.Translation.Types
{
	public interface ITranslationServiceDependencies
	{
		IDocumentCatalog DocumentCatalog { get; }
	}

	public class TranslationServiceDependencies : ITranslationServiceDependencies
	{
		public TranslationServiceDependencies(IDocumentCatalog documentCatalog)
		{
			DocumentCatalog = documentCatalog;
		}

		public IDocumentCatalog DocumentCatalog { get; private set; }
	}
}