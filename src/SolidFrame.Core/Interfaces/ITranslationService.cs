using SolidFrame.Core.Types;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces
{
	public interface ITranslationService
	{
		IDictionary<string, string> GetTranslations(IDocument document);
	}
}