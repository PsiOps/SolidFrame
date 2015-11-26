
namespace Example.WPF.Person.UI
{
	public interface IPersonRowViewModel
	{
		string FirstName { get; set; }
		int Id { get; set; }
		string LastName { get; set; }
	}
	public class PersonRowViewModel : IPersonRowViewModel
	{
		public PersonRowViewModel()
		{

		}

		public string FirstName { get; set; }
		public int Id { get; set; }
		public string LastName { get; set; }
	}
}
