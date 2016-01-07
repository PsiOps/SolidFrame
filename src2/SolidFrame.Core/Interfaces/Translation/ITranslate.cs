using System.Collections.Generic;

namespace SolidFrame.Core.Interfaces.Translation
{
	public interface ITranslate
	{
		IDictionary<string, string> Translations { get; set; } 
	}
}