using Example.Models;
using Example.WPF.Resources.Web.Configurations;
using Newtonsoft.Json;
using SolidFrame.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Example.WPF.Resources.Web
{
	public interface IPersonResource
	{
		Task<IEnumerable<PersonModel>> Get();
		Task<bool> Put(IEnumerable<IPersonModel> models);
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

		public async Task<bool> Put(IEnumerable<IPersonModel> models)
		{
			var content = JsonConvert.SerializeObject(models);

			HttpResponseMessage response = await PutResponse(content);

			return response.IsSuccessStatusCode;
		}
	}
}
