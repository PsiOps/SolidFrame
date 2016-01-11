using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SolidFrame.Resources.Helpers
{
	public static class PropertyNameHelper
	{
		public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
		{
			var member = propertyLambda.Body as MemberExpression;
			if (member == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a method, not a property.",
					propertyLambda));

			var propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
				throw new ArgumentException(string.Format(
					"Expression '{0}' refers to a field, not a property.",
					propertyLambda));

			return propInfo.Name;
		}
	}
}
