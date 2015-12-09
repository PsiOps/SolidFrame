using SolidFrame.Core.Interfaces.Client;
using System;

namespace Example.WPF.Resources.Web.Configurations
{
	public interface IPersonResourceConfiguration : IApiResourceConfiguration
	{
	}

	public class PersonResourceConfiguration : IPersonResourceConfiguration
	{
		public Uri Location { get { return new Uri("http://localhost:50090/api/person"); } }
		public string MediaType { get { return "application/json"; } }
	}
}
