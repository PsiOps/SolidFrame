using SolidFrame.Core.Interfaces;
using SolidFrame.Core.Types;
using System.Collections.Generic;
using SolidFrame.Translation.Types;

namespace SolidFrame.Translation.Logics
{
	public class TranslationService : ITranslationService
	{
		private readonly IDocumentCatalog _documentCatalog;

		public TranslationService(ITranslationServiceDependencies dependencies)
		{
			_documentCatalog = dependencies.DocumentCatalog;
		}

		public IDictionary<string, string> GetTranslations(IDocument document)
		{
			if (document.Id == _documentCatalog.TaskDocument.Id)
			{
				return new Dictionary<string, string>
				{
					{"Name", "Naam"},
					{"Remark", "Opmerking"},
					{"Rank", "Rang"}
				};
			}

			return new Dictionary<string, string>();
		}
	}
}
