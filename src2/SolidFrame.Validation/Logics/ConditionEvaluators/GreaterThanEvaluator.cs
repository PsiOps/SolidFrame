using SolidFrame.Core.Interfaces.Validation;
using System;

namespace SolidFrame.Validation.Logics.ConditionEvaluators
{
	public class GreaterThanIntEvaluator<T> : IConditionEvaluator<T>
	{
		private readonly Func<T, int> _value1Getter;
		private readonly Func<T, int> _value2Getter;

		public GreaterThanIntEvaluator(Func<T, int> value1Getter, Func<T, int> value2Getter)
		{
			_value1Getter = value1Getter;
			_value2Getter = value2Getter;
		}

		public bool Evaluate(T type)
		{
			return _value1Getter(type) > _value2Getter(type);
		}
	}
}
