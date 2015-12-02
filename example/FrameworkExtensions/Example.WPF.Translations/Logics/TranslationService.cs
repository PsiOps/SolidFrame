using Example.WPF.Resources;
using SolidFrame.Core.Interfaces.Translation;
using System;
using System.Collections.Generic;

namespace Example.WPF.Translations.Logics
{
	public class TranslationService : ITranslationService
	{
		public IDictionary<string, string> GetTranslations(Guid documentId)
		{
			if (documentId == DocumentIdCatalog.PersonDocumentId)
			{
				return new Dictionary<string, string>
				{
					{"FirstName", "Voornaam"},
					{"LastName", "Achternaam"},
					{"Id", "Nummer"},
					{"Test {0}", "Translated Test {0}"}
				};
			}

			return new Dictionary<string, string>();
		}
	}
}
