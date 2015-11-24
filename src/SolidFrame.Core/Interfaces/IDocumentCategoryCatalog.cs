using SolidFrame.Core.Types;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces
{
	public interface IDocumentCategoryCatalog
	{
		IEnumerable<IDocumentCategory> List { get; } 
	}
}