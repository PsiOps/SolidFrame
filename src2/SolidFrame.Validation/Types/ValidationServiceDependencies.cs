using SolidFrame.Core.Interfaces.Notifications;
using SolidFrame.Core.Interfaces.Validation;

namespace SolidFrame.Validation.Types
{
	public interface IValidationServiceDependencies
	{
		IConditionEvaluatorFactory ConditionEvaluatorFactory { get; }
		IValidationRuleFactory ValidationRuleFactory { get; }
		INotificationService NotificationService{ get; }
	}

	public class ValidationServiceDependencies : IValidationServiceDependencies
	{
		public ValidationServiceDependencies(IValidationRuleFactory validationRuleFactory, IConditionEvaluatorFactory conditionEvaluatorFactory, INotificationService notificationService)
		{
			ValidationRuleFactory = validationRuleFactory;
			ConditionEvaluatorFactory = conditionEvaluatorFactory;
			NotificationService = notificationService;
		}

		public IConditionEvaluatorFactory ConditionEvaluatorFactory { get; private set; }
		public IValidationRuleFactory ValidationRuleFactory { get; private set; }
		public INotificationService NotificationService { get; private set; }
	}
}
