using System;

namespace SolidFrame.Core.Types
{
	public interface IDocument
	{
		Guid Id { get; }
		string Name { get; }
		Type ViewType { get; }
		IDocumentCategory Category { get; }
	}

	public class Document : IDocument
	{
		public Document(Guid id, string name, Type viewType, IDocumentCategory category)
		{
			Id = id;
			Name = name;
			ViewType = viewType;
			Category = category;
		}

		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public Type ViewType { get;  private set; }
		public IDocumentCategory Category { get; private set; }
	}
}
