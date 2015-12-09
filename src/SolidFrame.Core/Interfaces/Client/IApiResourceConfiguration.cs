using System;

namespace SolidFrame.Core.Interfaces.Client
{
	public interface IApiResourceConfiguration
	{
		Uri Location { get; }
		string MediaType { get; }
	}
}