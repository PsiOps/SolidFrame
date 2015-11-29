using SolidFrame.Core.Interfaces;

namespace Example.WPF.Person.Types
{
	public interface IPersonListViewModelDepedencies
	{
		ITranslationService TranslationService { get; }
		IPersonDocumentConfiguration Configuration { get; }
	}

	public class PersonListViewModelDepedencies : IPersonListViewModelDepedencies
	{
		public PersonListViewModelDepedencies(ITranslationService translationService, IPersonDocumentConfiguration configuration)
		{
			TranslationService = translationService;
			Configuration = configuration;
		}

		public ITranslationService TranslationService { get; private set; }
		public IPersonDocumentConfiguration Configuration { get; private set; }
	}
}
