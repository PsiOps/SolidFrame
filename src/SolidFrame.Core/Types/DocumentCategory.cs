
using System;

namespace SolidFrame.Core.Types
{
	public interface IDocumentCategory
	{
		Guid Id { get; set; }
		string Name { get; set; }
		int Rank { get; set; }
	}

	public class DocumentCategory : IDocumentCategory
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Rank { get; set; }
	}
}
