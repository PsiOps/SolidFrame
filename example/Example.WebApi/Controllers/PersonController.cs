using Example.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;

namespace Example.WebApi.Controllers
{
	[Route("api/person")]
	public class PersonController : ApiController
	{
		[System.Web.Mvc.HttpGet]
		public IEnumerable<PersonModel> Get()
		{
			return new Collection<PersonModel>
			{
				new PersonModel{FirstName = "Bob", LastName = "Smith", Number = 1},
				new PersonModel{FirstName = "Andy", LastName = "Turner", Number = 2},
				new PersonModel{FirstName = "Jack", LastName = "Miller", Number = 3},
				new PersonModel{FirstName = "Randy", LastName = "Marsh", Number = 4}
			};
		}

		[System.Web.Mvc.HttpPut]
		public void Put(IEnumerable<IPersonModel> persons)
		{
			
		}
	}
}