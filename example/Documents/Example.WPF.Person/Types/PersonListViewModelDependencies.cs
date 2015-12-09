using Example.WPF.Person.Logics;
using Example.WPF.Person.UI;
using Example.WPF.Resources.Web;
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
		IPersonResource PersonResource { get; }
		IPersonRowViewModelFactory RowViewModelFactory { get; }
	}

	public class PersonListViewModelDepedencies : IPersonListViewModelDepedencies 
	{
		public PersonListViewModelDepedencies(
			ITranslationService translationService, 
			IPersonDocumentConfiguration configuration, 
			ICrudGroupController crudGroupController, 
			IValidationService<IPersonRowViewModel> validationService, 
			IPersonResource personResource, 
			IPersonRowViewModelFactory rowViewModelFactory)
		{
			TranslationService = translationService;
			Configuration = configuration;
			CrudGroupController = crudGroupController;
			ValidationService = validationService;
			PersonResource = personResource;
			RowViewModelFactory = rowViewModelFactory;
		}

		public ITranslationService TranslationService { get; private set; }
		public IPersonDocumentConfiguration Configuration { get; private set; }
		public ICrudGroupController CrudGroupController { get; private set; }
		public IValidationService<IPersonRowViewModel> ValidationService { get; private set; }
		public IPersonResource PersonResource { get; private set; }
		public IPersonRowViewModelFactory RowViewModelFactory { get; private set; }
	}
}
