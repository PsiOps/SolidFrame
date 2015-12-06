using System;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface ICanBeValidated
	{
		Guid Id { get; }
		string ValidationName { get; }
	}
}