using SolidFrame.Core.Types;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Document
{
	public interface IDocumentCategoryCatalog
	{
		IEnumerable<IDocumentCategory> List { get; } 
	}
}