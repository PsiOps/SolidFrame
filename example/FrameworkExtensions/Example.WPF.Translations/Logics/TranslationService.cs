using Example.WPF.Resources;
using SolidFrame.Core.Interfaces.Document;
using SolidFrame.Core.Interfaces.Translation;
using System.Collections.Generic;

namespace Example.WPF.Translations.Logics
{
	public class TranslationService : ITranslationService
	{
		public IDictionary<string, string> GetTranslations(IDocumentConfiguration documentConfiguration)
		{
			if (documentConfiguration.Id == DocumentIdCatalog.PersonDocumentId)
			{
				return new Dictionary<string, string>
				{
					{"FirstName", "Voornaam"},
					{"LastName", "Achternaam"},
					{"Id", "Nummer"}
				};
			}

			return new Dictionary<string, string>();
		}
	}
}
