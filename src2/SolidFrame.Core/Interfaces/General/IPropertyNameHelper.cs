using System;
using System.Linq.Expressions;

namespace SolidFrame.Core.Interfaces.General
{
	public interface IPropertyNameHelper
	{
		string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda);
	}
}