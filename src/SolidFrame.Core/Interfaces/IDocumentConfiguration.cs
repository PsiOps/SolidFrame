using System;

namespace SolidFrame.Core.Interfaces
{
	public interface IDocumentConfiguration
	{
		Guid Id { get; }
		string Name { get; }
		Type ViewType { get; }
		Guid CategoryId { get; }
	}
}