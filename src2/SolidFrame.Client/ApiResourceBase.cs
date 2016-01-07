using SolidFrame.Core.Interfaces.Client;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SolidFrame.Client
{
	public class ApiResourceBase
	{
		private readonly IApiResourceConfiguration _resource;

		public ApiResourceBase(IApiResourceConfiguration resource)
		{
			_resource = resource;
		}

		public async Task<HttpResponseMessage> GetResponse()
		{
			HttpResponseMessage response;

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_resource.MediaType));

				response = await client.GetAsync(_resource.Location);
			}

			return response;
		}

		public async Task<HttpResponseMessage> PutResponse(string content)
		{
			HttpResponseMessage response;

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_resource.MediaType));

				response = await client.PutAsync(_resource.Location, content, new JsonMediaTypeFormatter());
			}

			return response;
		}
	}
}
