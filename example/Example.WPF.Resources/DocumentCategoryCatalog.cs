using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Example.WPF.Resources
{
	public class ExampleDocumentCategoryCatalog : IDocumentCategoryCatalog
	{
		public static readonly IDocumentCategory CategoryOne = new DocumentCategory { Id = new Guid("182CBF6F-4E88-4E3C-B25B-87244D1A932D"), Name = "TK_CategoryOne", Rank = 0 };
		public static readonly IDocumentCategory CategoryTwo = new DocumentCategory { Id = new Guid("40962B5C-927A-4ACE-B91E-25358A39BB90"), Name = "TK_CategoryTwo", Rank = 1 };

		public IEnumerable<IDocumentCategory> List
		{
			get { return new Collection<IDocumentCategory> { CategoryOne, CategoryTwo}; }
		}
	}
}
