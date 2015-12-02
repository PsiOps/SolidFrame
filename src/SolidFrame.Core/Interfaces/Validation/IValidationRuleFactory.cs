
namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidationRuleFactory
	{
		IValidationRule<T> Create<T>(IConditionEvaluator<T> evaluator, string message, params string[] propertyNames);
	}
}