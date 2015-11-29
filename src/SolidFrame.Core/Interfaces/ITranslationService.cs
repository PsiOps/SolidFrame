using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces
{
	public interface ITranslationService
	{
		IDictionary<string, string> GetTranslations(IDocumentConfiguration documentConfiguration);
	}
}