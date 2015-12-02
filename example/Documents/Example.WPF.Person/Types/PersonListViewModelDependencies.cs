using Example.WPF.Person.UI;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Core.Interfaces.Translation;
using SolidFrame.Core.Interfaces.Validation;

namespace Example.WPF.Person.Types
{
	public interface IPersonListViewModelDepedencies
	{
		ITranslationService TranslationService { get; }
		IPersonDocumentConfiguration Configuration { get; }
		ICrudGroupController CrudGroupController { get; }
		IValidationService<IPersonRowViewModel> ValidationService { get; }
	}

	public class PersonListViewModelDepedencies : IPersonListViewModelDepedencies 
	{
		public PersonListViewModelDepedencies(
			ITranslationService translationService, 
			IPersonDocumentConfiguration configuration, 
			ICrudGroupController crudGroupController, 
			IValidationService<IPersonRowViewModel> validationService)
		{
			TranslationService = translationService;
			Configuration = configuration;
			CrudGroupController = crudGroupController;
			ValidationService = validationService;
		}

		public ITranslationService TranslationService { get; private set; }
		public IPersonDocumentConfiguration Configuration { get; private set; }
		public ICrudGroupController CrudGroupController { get; private set; }
		public IValidationService<IPersonRowViewModel> ValidationService { get; private set; }
	}
}
