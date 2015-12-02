using System;
using System.Linq.Expressions;

namespace SolidFrame.Resources.Helpers
{
	public interface IPropertyNameHelper
	{
		string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda);
	}
}