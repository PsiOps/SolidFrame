
using SolidFrame.Core.Interfaces.Validation;
using System.Collections.Generic;

namespace SolidFrame.Validation.Logics
{
	public class ValidationRule<T> : IValidationRule<T>
	{
		private readonly IConditionEvaluator<T> _evaluator;
		private readonly string _message;

		public ValidationRule(IConditionEvaluator<T> evaluator, string message, params string[] propertyNames)
		{
			_evaluator = evaluator;
			_message = message;
			Properties = new HashSet<string>(propertyNames);
		}

		public bool Evaluate(T type)
		{
			return _evaluator.Evaluate(type);
		}

		public HashSet<string> Properties { get; private set; }
	}
}
