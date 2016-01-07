using System;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IConditionEvaluatorFactory
	{
		IConditionEvaluator<T> CreateGreaterThanEvaluator<T>(Func<T, int> value1Getter, Func<T, int> value2Getter);
		IConditionEvaluator<T> CreateGreaterThanEvaluator<T>(Func<T, double> value1Getter, Func<T, double> value2Getter);
	}
}