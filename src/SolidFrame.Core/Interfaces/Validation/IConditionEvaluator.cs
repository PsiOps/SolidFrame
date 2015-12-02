namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IConditionEvaluator<T>
	{
		bool Evaluate(T type);
	}
}