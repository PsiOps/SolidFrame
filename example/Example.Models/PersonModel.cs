namespace Example.Models
{
	public interface IPersonModel
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
	}
}
