using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Validation.Logics.ConditionEvaluators;
using System;

namespace SolidFrame.Validation.Logics
{
	public class ConditionEvaluatorFactory : IConditionEvaluatorFactory
	{
		public IConditionEvaluator<T> CreateGreaterThanEvaluator<T>(Func<T, int> value1Getter, Func<T, int> value2Getter)
		{
			return new GreaterThanIntEvaluator<T>(value1Getter, value2Getter);
		}

		public IConditionEvaluator<T> CreateGreaterThanEvaluator<T>(Func<T, double> value1Getter, Func<T, double> value2Getter)
		{
			throw new NotImplementedException();
		}
	}
}
