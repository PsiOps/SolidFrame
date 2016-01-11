using SolidFrame.Core.Interfaces.Validation;
using SolidFrame.Core.Types;
using System;

namespace SolidFrame.Validation.Test.Stubs
{
	internal class ValidatableStub : IValidatable
	{
		public ValidatableStub(string validationName = "Test")
		{
			Id = Guid.NewGuid();
			ValidationName = validationName;
		}

		public int NumberInt { get; set; }
		public double NumberDouble { get; set; }
		public string Name { get; set; }

		public Guid Id { get; private set; }
		public string ValidationName { get; private set; }

		public event HasErrorsChangedHandler HasErrorsChanged;

		public bool HasErrors()
		{
			throw new NotImplementedException();
		}
	}
}