using SolidFrame.Core.Interfaces.General;
using System;

namespace SolidFrame.DirtyTracking.Test.Stubs
{
	internal interface ITrackableModel : IEquatable<ITrackableModel>, IHaveId
	{
		int Number { get; set; }
		string Name { get; set; }
	}

	internal class TrackableModel : ITrackableModel
	{
		public TrackableModel(string name, int number)
		{
			Id = Guid.NewGuid();
			Name = name;
			Number = number;
		}

		public bool Equals(ITrackableModel other)
		{
			if (other.Name != Name)
				return false;

			if (other.Number != Number)
				return false;

			return true;
		}

		public Guid Id { get; private set; }
		public int Number { get; set; }
		public string Name { get; set; }
	}
}
