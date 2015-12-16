
using SolidFrame.Core.Interfaces.Validation;
using System;

namespace SolidFrame.Core.Types
{
	public delegate void CanCrudChangedHandler();
	public delegate void ValidationTriggerHandler<in TValidatable>(TValidatable validatable, string propertyName) where TValidatable : IValidatable;
	public delegate void HasErrorsChangedHandler(Guid notificationId, Guid subjectId);
}
