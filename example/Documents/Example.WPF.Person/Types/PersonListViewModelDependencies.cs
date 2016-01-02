using Example.Models;
using Example.WPF.Person.UI;
using Example.WPF.Resources.Web;
using SolidFrame.Core.Interfaces.DirtyTracking;
using SolidFrame.Core.Interfaces.Ribbon;
using SolidFrame.Core.Interfaces.Translation;
using SolidFrame.Core.Interfaces.Validation;

namespace Example.WPF.Person.Types
{
	public interface IPersonListViewModelDepedencies
	{
		ITranslationService TranslationService { get; }
		IPersonDocumentConfiguration Document { get; }
		ICrudGroupController CrudGroupController { get; }
		IValidationService<IPersonRowViewModel> ValidationService { get; }
		IPersonResource PersonResource { get; }
		ITrackedCollectionFactory<IPersonModel, IPersonRowViewModel> TrackedCollectionFactory { get; }
	}

	public class PersonListViewModelDepedencies : IPersonListViewModelDepedencies 
	{
		public PersonListViewModelDepedencies(
			ITranslationService translationService, 
			IPersonDocumentConfiguration document, 
			ICrudGroupController crudGroupController, 
			IValidationService<IPersonRowViewModel> validationService, 
			IPersonResource personResource, 
			ITrackedCollectionFactory<IPersonModel, IPersonRowViewModel> trackedCollectionFactory)
		{
			TranslationService = translationService;
			Document = document;
			CrudGroupController = crudGroupController;
			ValidationService = validationService;
			PersonResource = personResource;
			TrackedCollectionFactory = trackedCollectionFactory;
		}

		public ITranslationService TranslationService { get; private set; }
		public IPersonDocumentConfiguration Document { get; private set; }
		public ICrudGroupController CrudGroupController { get; private set; }
		public IValidationService<IPersonRowViewModel> ValidationService { get; private set; }
		public IPersonResource PersonResource { get; private set; }
		public ITrackedCollectionFactory<IPersonModel, IPersonRowViewModel> TrackedCollectionFactory { get; private set; }
	}
}
