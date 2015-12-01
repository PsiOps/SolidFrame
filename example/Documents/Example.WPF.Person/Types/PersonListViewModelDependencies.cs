using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Core.Interfaces.Translation;

namespace Example.WPF.Person.Types
{
	public interface IPersonListViewModelDepedencies
	{
		ITranslationService TranslationService { get; }
		IPersonDocumentConfiguration Configuration { get; }
		ICrudGroupController CrudGroupController { get; }
	}

	public class PersonListViewModelDepedencies : IPersonListViewModelDepedencies 
	{
		public PersonListViewModelDepedencies(
			ITranslationService translationService, 
			IPersonDocumentConfiguration configuration, 
			ICrudGroupController crudGroupController)
		{
			TranslationService = translationService;
			Configuration = configuration;
			CrudGroupController = crudGroupController;
		}

		public ITranslationService TranslationService { get; private set; }
		public IPersonDocumentConfiguration Configuration { get; private set; }
		public ICrudGroupController CrudGroupController { get; private set; }
	}
}
