using SolidFrame.Core.Interfaces.Client;
using System;

namespace Example.WPF.Resources.Web.Configurations
{
	public interface IPersonResourceConfiguration : IApiResourceConfiguration
	{
	}

	public class PersonResourceConfiguration : IPersonResourceConfiguration
	{
		// TODO: Maybe get the baseurl as a dependency in a ctor

		public Uri Location { get { return new Uri("http://localhost:50090/api/person"); } }
		public string MediaType { get { return "application/json"; } }
	}
}
