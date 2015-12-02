using SolidFrame.Core.Interfaces.Validation;

namespace SolidFrame.Validation.Logics
{
	public class ValidationRuleFactory : IValidationRuleFactory
	{
		public IValidationRule<T> Create<T>(IConditionEvaluator<T> evaluator, string message, params string[] propertyNames)
		{
			return new ValidationRule<T>(evaluator, message, propertyNames);
		}
	}
}
