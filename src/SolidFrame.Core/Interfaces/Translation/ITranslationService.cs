using SolidFrame.Core.Interfaces.Document;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Translation
{
	public interface ITranslationService
	{
		IDictionary<string, string> GetTranslations(IDocumentConfiguration documentConfiguration);
	}
}