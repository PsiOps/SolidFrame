using SolidFrame.Core.Types;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces
{
	public interface IDocumentCatalog
	{
		IDocument TaskDocument { get; }

		IEnumerable<IDocument> List { get; }
	}
}