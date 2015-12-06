
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;

namespace SolidFrame.Validation.Logics
{
	public class ValidationRule<TCanBeValidated> : IValidationRule<TCanBeValidated> 
		where TCanBeValidated : ICanBeValidated
	{
		public ValidationRule(IConditionEvaluator<TCanBeValidated> evaluator, Severity severity, string message, params string[] propertyNames)
		{
			_evaluator = evaluator;

			Id = Guid.NewGuid();
			Message = message;
			Severity = severity;
			Properties = new HashSet<string>(propertyNames);
		}

		public Guid Id { get; private set; }
		public string Message { get; private set; }
		public Severity Severity { get; private set; }
		public HashSet<string> Properties { get; private set; }

		private readonly IConditionEvaluator<TCanBeValidated> _evaluator;

		public bool Evaluate(TCanBeValidated canBeValidated)
		{
			return _evaluator.Evaluate(canBeValidated);
		}
	}
}
