using Microsoft.Practices.ObjectBuilder2;
using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Translation;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Core.Types;
using SolidFrame.Resources.Helpers;
using SolidFrame.Validation.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SolidFrame.Validation.Logics
{
	public class ValidationService<T> : IValidationService<T>
	{
		public ValidationService(IValidationServiceDependencies dependencies)
		{
			_validationRules = new List<IValidationRule<T>>();
			_validationRuleFactory = dependencies.ValidationRuleFactory;
			_conditionEvaluatorFactory = dependencies.ConditionEvaluatorFactory;
			_propertyNameHelper = dependencies.PropertyNameHelper;
		}

		public void Register(IValidateRows<T> validate)
		{
			validate.RowValidationTrigger += EvaluateRules;
		}

		private void EvaluateRules(T type, string propertyName)
		{
			_validationRules.Where(vr => propertyName == null || vr.Properties.Contains(propertyName)).ForEach(vr => vr.Evaluate(type));
		}

		public void AddAbsoluteRule(IHaveId haveId, Expression<Func<T, int>> propertyExpression, Condition condition, int value, Severity severity, string message)
		{
			var propertyName = _propertyNameHelper.GetPropertyName(propertyExpression);

			var formattedMessage = GetFormattedMessage(haveId, propertyName, message);

			var propertyValueGetter = propertyExpression.Compile();

			IConditionEvaluator<T> evaluator = null;

			switch (condition)
			{
				case Condition.MustBeGreaterThan:
					evaluator = _conditionEvaluatorFactory.CreateGreaterThanEvaluator(propertyValueGetter, type => value);
					break;
				case Condition.MustEqual:
					break;
				default:
					throw new ArgumentOutOfRangeException("condition", condition, null);
			}

			var rule = _validationRuleFactory.Create(evaluator, formattedMessage, propertyName);

			_validationRules.Add(rule);
		}

		private static string GetFormattedMessage(IHaveId haveId, string propertyName, string message)
		{
			// ReSharper disable once SuspiciousTypeConversion.Global
			var translate = haveId as ITranslate;

			if (translate == null)
				return string.Format(message, propertyName);

			var translations = translate.Translations;

			return string.Format(translations[message], translations[propertyName]);
		}

		public void AddCustomRule(IValidationRule<T> customRule, Severity type, string message)
		{
			throw new NotImplementedException();
		}

		private readonly ICollection<IValidationRule<T>> _validationRules;
		private readonly IValidationRuleFactory _validationRuleFactory;
		private readonly IConditionEvaluatorFactory _conditionEvaluatorFactory;
		private readonly IPropertyNameHelper _propertyNameHelper;
	}
}
