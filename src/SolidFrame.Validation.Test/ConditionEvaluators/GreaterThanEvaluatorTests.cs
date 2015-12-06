using NUnit.Framework;
using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Validation.Logics.ConditionEvaluators;
using SolidFrame.Validation.Test.Stubs;

namespace SolidFrame.Validation.Test.ConditionEvaluators
{
	[TestFixture]
	public class DescribeEvaluation
	{
		private IConditionEvaluator<CanBeValidatedStub> _greaterThanEvaluator;

		[SetUp]
		public void BeforeEach()
		{
			_greaterThanEvaluator = new GreaterThanIntEvaluator<CanBeValidatedStub>(stub => stub.NumberInt, stub => 0);
		}

		[Test]
		public void It_returns_true_when_value1_is_greater_than_value2()
		{
			var row = new CanBeValidatedStub
			{
				NumberInt = 1
			};

			Assert.IsTrue(_greaterThanEvaluator.Evaluate(row));
		}

		[Test]
		public void It_returns_false_when_value1_is_equal_or_lesser_than_value2()
		{
			var row = new CanBeValidatedStub
			{
				NumberInt = 0
			};

			Assert.IsFalse(_greaterThanEvaluator.Evaluate(row));

			row.NumberInt = -1;

			Assert.IsFalse(_greaterThanEvaluator.Evaluate(row));
		}
	}
}
