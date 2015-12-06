using SolidFrame.Core.Interfaces.Notifications;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Resources.Helpers;

namespace SolidFrame.Validation.Types
{
	public interface IValidationServiceDependencies
	{
		IConditionEvaluatorFactory ConditionEvaluatorFactory { get; }
		IValidationRuleFactory ValidationRuleFactory { get; }
		IPropertyNameHelper PropertyNameHelper{ get; }
		INotificationService NotificationService{ get; }
	}

	public class ValidationServiceDependencies : IValidationServiceDependencies
	{
		public ValidationServiceDependencies(IValidationRuleFactory validationRuleFactory, IConditionEvaluatorFactory conditionEvaluatorFactory, IPropertyNameHelper propertyNameHelper, INotificationService notificationService)
		{
			ValidationRuleFactory = validationRuleFactory;
			ConditionEvaluatorFactory = conditionEvaluatorFactory;
			PropertyNameHelper = propertyNameHelper;
			NotificationService = notificationService;
		}

		public IConditionEvaluatorFactory ConditionEvaluatorFactory { get; private set; }
		public IValidationRuleFactory ValidationRuleFactory { get; private set; }
		public IPropertyNameHelper PropertyNameHelper { get; private set; }
		public INotificationService NotificationService { get; private set; }
	}
}
