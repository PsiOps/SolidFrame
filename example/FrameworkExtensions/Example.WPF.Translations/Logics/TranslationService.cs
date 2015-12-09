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
					{"Number", "Nummer"},
					{"{0} must be larger than zero", "{0} moet groter zijn dan nul."}
				};
			}

			return new Dictionary<string, string>();
		}
	}
}
