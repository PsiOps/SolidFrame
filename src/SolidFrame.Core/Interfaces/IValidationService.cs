
using System;
using System.Linq.Expressions;

namespace SolidFrame.Core.Interfaces
{
	public interface IValidationService
	{
		//void Register(IValidate validate);

		//void AddRelativeRule<TProp>(func<TProp> property1, Condition condition, func<TProp> property2, ValidationType type, string message);
		void AddAbsoluteRule<TProp>(Expression<Func<TProp>> propertyExpression, Condition condition, TProp value, ValidationType type, string message);
		//void AddUniqueRule<TProp>(func<TProp> property, Condition condition, func<TProp> property2, ValidationType type, string message);
	}

	public class ValidationService : IValidationService
	{
		public void AddAbsoluteRule<TProp>(Expression<Func<TProp>> propertyExpression, Condition condition, TProp value, ValidationType type, string message)
		{
			var propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;

			var propertyValue = propertyExpression.Compile()();

			throw new NotImplementedException();
		}
	}

	public enum ValidationType
	{
		Warning,
		Exception
	}

	public enum Condition
	{
		MustBeLargerThan
	}

	public class Test
	{
		public Test(IValidationService service)
		{
			service.AddAbsoluteRule(() => Name, Condition.MustBeLargerThan, "Bla", ValidationType.Warning, "This aint right");
		}

		public string Name { get; set; }
		public int Number { get; set; }
	}
}