using Example.Models;
using Example.WPF.Resources.Web.Configurations;
using SolidFrame.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Example.WPF.Resources.Web
{
	public interface IPersonResource
	{
		Task<IEnumerable<PersonModel>> Get();
	}

	public class PersonResource : ApiResourceBase, IPersonResource
	{
		public PersonResource(IPersonResourceConfiguration resource) : base(resource)
		{
		}

		public async Task<IEnumerable<PersonModel>> Get()
		{
			HttpResponseMessage response = await GetResponse();

			IEnumerable<PersonModel> persons = null;

			if (response.IsSuccessStatusCode)
			{
				persons = await response.Content.ReadAsAsync<IEnumerable<PersonModel>>();
			}

			return persons;
		}
	}
}
