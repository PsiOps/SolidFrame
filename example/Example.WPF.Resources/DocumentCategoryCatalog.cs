using SolidFrame.Core.Interfaces;
using SolidFrame.Core.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Example.WPF.Resources
{
	public class DocumentCategoryCatalog : IDocumentCategoryCatalog
	{
		public static readonly IDocumentCategory CommonPoolDocuments = new DocumentCategory { Id = new Guid("182CBF6F-4E88-4E3C-B25B-87244D1A932D"), Name = "TK_CommonPool", Rank = 0 };
		public static readonly IDocumentCategory GeneralDocuments = new DocumentCategory { Id = new Guid("40962B5C-927A-4ACE-B91E-25358A39BB90"), Name = "TK_General", Rank = 1 };
		public static readonly IDocumentCategory FormulaDocuments = new DocumentCategory { Id = new Guid("D21C6981-66DF-45B3-8498-A4623101996A"), Name = "TK_Formula", Rank = 2 };

		public IEnumerable<IDocumentCategory> List
		{
			get { return new Collection<IDocumentCategory> { CommonPoolDocuments, GeneralDocuments, FormulaDocuments }; }
		}
	}
}
