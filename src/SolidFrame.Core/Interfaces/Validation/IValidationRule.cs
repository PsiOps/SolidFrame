using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidationRule<in T>
	{
		bool Evaluate(T type);
		HashSet<string> Properties { get; }
	}
}