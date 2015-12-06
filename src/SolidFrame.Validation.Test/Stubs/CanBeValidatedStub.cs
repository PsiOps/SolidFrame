using SolidFrame.Core.Interfaces.Validation;
using System;

namespace SolidFrame.Validation.Test.Stubs
{
	public class CanBeValidatedStub : ICanBeValidated
	{
		public CanBeValidatedStub(string validationName = "Test")
		{
			Id = Guid.NewGuid();
			ValidationName = validationName;
		}

		public int NumberInt { get; set; }
		public double NumberDouble { get; set; }
		public string Name { get; set; }

		public Guid Id { get; private set; }
		public string ValidationName { get; private set; }
	}
}