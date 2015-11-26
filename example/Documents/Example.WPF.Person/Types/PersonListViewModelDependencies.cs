using SolidFrame.Core.Interfaces;

namespace Example.WPF.Person.Types
{
	public interface IPersonListViewModelDepedencies
	{
		ITranslationService TranslationService { get; }
		IDocumentCatalog DocumentCatalog { get; }
	}

	public class PersonListViewModelDepedencies : IPersonListViewModelDepedencies
	{
		public PersonListViewModelDepedencies(ITranslationService translationService, IDocumentCatalog documentCatalog)
		{
			TranslationService = translationService;
			DocumentCatalog = documentCatalog;
		}

		public ITranslationService TranslationService { get; private set; }
		public IDocumentCatalog DocumentCatalog { get; private set; }
	}
}
