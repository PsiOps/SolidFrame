using SolidFrame.Core.Interfaces.General;
using SolidFrame.Core.Types;
using System;
using System.Linq.Expressions;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidationService<T>
	{
		void Register(IValidateRows<T> validate);
		//void Register(IValidateCells validate);

		void AddAbsoluteRule(IHaveId haveId, Expression<Func<T, int>> propertyExpression, Condition condition, int value, Severity severity, string message);
		//void AddRelativeRule<TProp>(Expression<Func<TProp>> property1, ValidationCondition condition, Expression<Func<TProp>> property2, ValidationSeverity type, string message);
		//void AddUniqueRule<TProp>(Expression<Func<TProp>> property, Func<ICollection<TProp>> collection, ValidationSeverity type, string message);

		void AddCustomRule(IValidationRule<T> customRule, Severity type, string message);
	}
}