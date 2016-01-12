using System;

namespace SolidFrame.Core.Interfaces.Document
{
	public interface IDocumentConfiguration
	{
		Guid Id { get; }
		string Name { get; }
		Type ViewType { get; }
	}
}