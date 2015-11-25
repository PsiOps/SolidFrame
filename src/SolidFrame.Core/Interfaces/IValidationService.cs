using System;

namespace SolidFrame.Core.Interfaces
{
	public interface IValidationService
	{
		void Register(IValidate validate);

		void AddRule<TValue>(
			Guid documentId, 
			Func<TValue> propertyValue, 
			RuleVerb verb, 
			RuleCondition condition,
			Func<TValue> targetValue);
	}
}