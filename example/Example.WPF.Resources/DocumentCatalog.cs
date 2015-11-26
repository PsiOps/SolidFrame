using Example.WPF.Person.UI;
using SolidFrame.Core.Interfaces;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Example.WPF.Resources
{
	public class DocumentCatalog : IDocumentCatalog
	{
		// TODO: Create IDocument using IDocumentFactory

		public IDocument TaskDocument { get { return new Document(id: new Guid("51FC2D99-6C30-4924-A18F-B7C3B28DCEC7"), name: "TK_Task", viewType: typeof(PersonView), category: DocumentCategoryCatalog.FormulaDocuments); } }

		public IEnumerable<IDocument> List { get { return new Collection<IDocument> { TaskDocument }; } }
	}
}
