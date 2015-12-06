using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Core.Types;

namespace SolidFrame.Validation.Logics
{
	public class ValidationRuleFactory : IValidationRuleFactory
	{
		public IValidationRule<TCanBeValidated> Create<TCanBeValidated>(IConditionEvaluator<TCanBeValidated> evaluator, Severity severity, string message, params string[] propertyNames) 
			where TCanBeValidated : ICanBeValidated
		{
			return new ValidationRule<TCanBeValidated>(evaluator, severity, message, propertyNames);
		}
	}
}
