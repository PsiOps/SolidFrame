using System;
using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Translation
{
	public interface ITranslationService
	{
		IDictionary<string, string> GetTranslations(Guid documentId);
	}
}