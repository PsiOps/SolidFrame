using SolidFrame.Core.Interfaces.General;
using System;

namespace Example.Models
{
	public class PersonModel : IHaveId
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
