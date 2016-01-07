using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Types;
using System;
using System.Linq.Expressions;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidationService<T> where T : IValidatable
	{
		void Register(IValidate<T> validate);

		void AddAbsoluteRule(IHaveId haveId, Expression<Func<T, int>> propertyExpression, Condition condition, int value, Severity severity, string message);
		//void AddRelativeRule<TProp>(Expression<Func<TProp>> property1, ValidationCondition condition, Expression<Func<TProp>> property2, Severity severity, string message);
		//void AddUniqueRule<TProp>(Expression<Func<TProp>> property, Func<ICollection<TProp>> collection, Severity severity, string message);
		//void AddCustomRule(IValidationRule<T> customRule, Severity severity, string message);

		bool HasErrors { get; }
		event BooleanStateChangedHandler HasErrorsChanged;
	}
}