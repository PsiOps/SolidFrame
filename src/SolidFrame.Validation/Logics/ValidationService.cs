using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Interfaces.Notifications;
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
	public class ValidationService<TValidatable> : IValidationService<TValidatable> where TValidatable : IValidatable
	{
		public ValidationService(IValidationServiceDependencies dependencies)
		{
			_validationRules = new List<IValidationRule<TValidatable>>();
			_validationRuleFactory = dependencies.ValidationRuleFactory;
			_conditionEvaluatorFactory = dependencies.ConditionEvaluatorFactory;
			_propertyNameHelper = dependencies.PropertyNameHelper;
			_notificationService = dependencies.NotificationService;
		}

		public void Register(IValidate<TValidatable> validate)
		{
			validate.ValidationTrigger += EvaluateRules;
		}

		private void EvaluateRules(TValidatable validatable, string propertyName)
		{
			foreach (var validationRule in _validationRules)
			{
				if(propertyName != null && !validationRule.Properties.Contains(propertyName)) continue;

				if (validationRule.Evaluate(validatable))
				{
					_notificationService.TryRemoveNotification(validationRule.Id, validatable.Id);

					if (_ruleViolations.Remove(new Tuple<Guid, Guid>(validationRule.Id, validatable.Id)))
						RaiseHasErrorsChanged();

					continue;
				}

				_notificationService.AddNotification(validationRule.Id, validatable.Id, validatable.ValidationName, validationRule.Message);

				RaiseHasErrorsChanged();

				_ruleViolations.Add(new Tuple<Guid, Guid>(validationRule.Id, validatable.Id));
			}
		}

		private void RaiseHasErrorsChanged()
		{
			if(!_ruleViolations.Any())
				OnHasErrorsChanged();
		}

		public void AddAbsoluteRule(IHaveId haveId, Expression<Func<TValidatable, int>> propertyExpression, Condition condition, int value, Severity severity, string message)
		{
			var propertyName = _propertyNameHelper.GetPropertyName(propertyExpression);

			var formattedMessage = GetFormattedMessage(haveId, propertyName, message);

			var propertyValueGetter = propertyExpression.Compile();

			// Everything below here could be genericer
			IConditionEvaluator<TValidatable> evaluator = null;

			switch (condition)
			{
				case Condition.MustBeGreaterThan:
					evaluator = _conditionEvaluatorFactory.CreateGreaterThanEvaluator(propertyValueGetter, canBeValidated => value);
					break;
				case Condition.MustEqual:
					break;
				default:
					throw new ArgumentOutOfRangeException("condition", condition, null);
			}

			var rule = _validationRuleFactory.Create(evaluator, severity, formattedMessage, propertyName);

			_validationRules.Add(rule);
		}

		public bool HasErrors { get { return _ruleViolations.Any(); } }

		private void OnHasErrorsChanged()
		{
			if (HasErrorsChanged != null)
				HasErrorsChanged(HasErrors);
		}

		public event BooleanStateChangedHandler HasErrorsChanged;

		private static string GetFormattedMessage(IHaveId haveId, string propertyName, string message)
		{
			// ReSharper disable once SuspiciousTypeConversion.Global
			var translate = haveId as ITranslate;

			if (translate == null)
				return string.Format(message, propertyName);

			var translations = translate.Translations;

			return string.Format(translations[message], translations[propertyName]);
		}

		private readonly ICollection<IValidationRule<TValidatable>> _validationRules;
		private readonly IValidationRuleFactory _validationRuleFactory;
		private readonly IConditionEvaluatorFactory _conditionEvaluatorFactory;
		private readonly IPropertyNameHelper _propertyNameHelper;
		private readonly INotificationService _notificationService;

		private readonly HashSet<Tuple<Guid, Guid>> _ruleViolations = new HashSet<Tuple<Guid, Guid>>();
	}
}
