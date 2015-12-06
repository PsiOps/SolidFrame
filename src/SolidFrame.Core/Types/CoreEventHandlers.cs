
using SolidFrame.Core.Interfaces.Validation;

namespace SolidFrame.Core.Types
{
	public delegate void CanCrudChangedHandler();
	public delegate void ValidationTriggerHandler<in TCanBeValidated>(TCanBeValidated canBeValidated, string propertyName) where TCanBeValidated : ICanBeValidated;
}
