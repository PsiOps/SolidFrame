
using SolidFrame.Core.Types;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidationRuleFactory
	{
		IValidationRule<TCanBeValidated> Create<TCanBeValidated>(IConditionEvaluator<TCanBeValidated> evaluator, Severity severity, string message, params string[] propertyNames) 
			where TCanBeValidated : ICanBeValidated;
	}
}