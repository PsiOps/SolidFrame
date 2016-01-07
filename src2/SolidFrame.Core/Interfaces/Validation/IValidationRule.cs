using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Validation
{
	public interface IValidationRule<in TCanBeValidated> where TCanBeValidated : IValidatable
	{
		Guid Id { get; }
		string Message { get; }
		Severity Severity { get; }
		HashSet<string> Properties { get; }

		bool Evaluate(TCanBeValidated canBeValidated);
	}
}