using System;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidationCondition
	{
		Guid Id { get; }
		bool Evaluate();
	}
}