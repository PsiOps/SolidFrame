using SolidFrame.Core.Interfaces.General;
using System;

namespace Example.Models
{
	public interface IPersonModel : IHaveId
	{
		string FirstName { get; set; }
		string LastName { get; set; }
		int Number { get; set; }
	}

	public class PersonModel : IPersonModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Number { get; set; }

		private Guid? _id;

		public Guid Id
		{
			get
			{
				if (_id == null)
				{
					_id = Guid.NewGuid();
				}

				return _id.Value;
			}
		}
	}
}
