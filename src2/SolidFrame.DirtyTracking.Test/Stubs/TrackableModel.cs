using SolidFrame.Core.Interfaces.General;
using System;

namespace SolidFrame.DirtyTracking.Test.Stubs
{
	internal class TrackableModel : IHaveId
	{
		public TrackableModel(string name, int number)
		{
			Id = Guid.NewGuid();
			Name = name;
			Number = number;
		}

		public Guid Id { get; set; }
		public int Number { get; set; }
		public string Name { get; set; }
	}
}
