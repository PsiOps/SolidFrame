using SolidFrame.Core.Interfaces.Validation;
using System;
using System.Linq.Expressions;

namespace SolidFrame.Validation.Logics
{
	public class ValidationService : IValidationService
	{
		public void AddAbsoluteRule<TProp>(Expression<Func<TProp>> propertyExpression, Condition condition, TProp value, ValidationType type,
			string message)
		{
			throw new NotImplementedException();
		}
	}
}
