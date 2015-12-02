using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Resources.Helpers;

namespace SolidFrame.Validation.Types
{
	public interface IValidationServiceDependencies
	{
		IConditionEvaluatorFactory ConditionEvaluatorFactory { get; }
		IValidationRuleFactory ValidationRuleFactory { get; }
		IPropertyNameHelper PropertyNameHelper{ get; }
	}

	public class ValidationServiceDependencies : IValidationServiceDependencies
	{
		public ValidationServiceDependencies(IValidationRuleFactory validationRuleFactory, IConditionEvaluatorFactory conditionEvaluatorFactory, IPropertyNameHelper propertyNameHelper)
		{
			ValidationRuleFactory = validationRuleFactory;
			ConditionEvaluatorFactory = conditionEvaluatorFactory;
			PropertyNameHelper = propertyNameHelper;
		}

		public IValidationRuleFactory ValidationRuleFactory { get; private set; }
		public IPropertyNameHelper PropertyNameHelper { get; private set; }
		public IConditionEvaluatorFactory ConditionEvaluatorFactory { get; private set; }
	}
}
